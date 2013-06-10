using System;
using System.Collections;
using System.Drawing;

using Aldebaran.Proxies;
using Emgu.CV;
using Emgu.CV.Structure;

using Naovigate.Util;

namespace Naovigate.Vision
{
    /// <summary>
    /// A class that controls and manages one video proxy to a Nao.
    /// </summary>
    internal sealed class Camera : IDisposable
    {
        private string subscriberID;
        private VideoDeviceProxy videoProxy;
        
        /// <summary>
        /// Creates a new instance using given subscriber-ID.
        /// </summary>
        /// <param name="subID">The ID under which to subscribe to the Nao's video proxy.</param>
        /// <exception cref="UnavailableConnectionException">If not connected to any Nao.</exception>
        public Camera(string subID)
        {
            subscriberID = subID;
            videoProxy = Proxies.GetProxy<VideoDeviceProxy>();
            Unsubscribe();  //Make sure that there is no other subscriber with this ID
        }

        /// <summary>
        /// Subscribers to the Nao's video stream.
        /// </summary>
        public void Subscribe()
        {
            if (!NaoState.Instance.Connected)
                return;
            try
            {
                subscriberID = videoProxy.subscribeCamera(subscriberID, 0,
                    1 /*kQVGA*/, 13 /*kRGB*/, 30);
            }
            catch
            {
                Logger.Log(this, "Could not subscribe to video proxy.");  
            }
        }

        /// <summary>
        /// Unsubscribes from the Nao's video stream.
        /// </summary>
        public void Unsubscribe()
        {
            if (!NaoState.Instance.Connected)
                return;
            try
            {
                videoProxy.unsubscribe(subscriberID);
            }
            catch
            {
                Logger.Log(this, "Could not unsubscribe from video proxy.");
            }
        }

        /// <summary>
        /// Fetches an image in raw format from the Nao's camera.
        /// Pre: The camera is subscribed (using Subscribe()).
        /// </summary>
        /// <returns>
        /// An array containing all sorts of data about the image see NaoQI docs for details.
        /// Returns null if no image could be read.
        /// </returns>
        private ArrayList GetRawImage()
        {
            try
            {
                videoProxy.setColorSpace(subscriberID, 13);
                ArrayList imageObject = (ArrayList)videoProxy.getImageRemote(subscriberID);
                return imageObject;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves an image from the Nao's camera.
        /// </summary>
        /// <returns>
        /// A bitmap, or
        /// null if no image could be retrieved.
        /// </returns>
        public Bitmap GetBitMap()
        {
            ArrayList imageObject = GetRawImage();
            if (imageObject == null)
                return null;

            int width = (int) imageObject[0];
            int height = (int) imageObject[1];
            byte[] imageBytes = (byte[]) imageObject[6];
            var stride = 4 * ((width * 3 + 3) / 4);
            Bitmap imageBitMap = new Bitmap(width, height, stride,
                                System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                                System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(imageBytes, 0));
            return imageBitMap;     
        }

        /// <summary>
        /// Retrieves an image from the Nao's camera.
        /// </summary>
        /// <returns>
        /// An Image object, or
        /// null if no image could be retrieved.
        /// </returns>
        public Image<Rgb, Byte> GetImage()
        {
            Bitmap bitmap = GetBitMap();
            if (bitmap == null)
                return null;
            Image<Rgb, Byte> image;
            try
            {
                image = new Image<Rgb, Byte>(bitmap);
            }
            catch (NotSupportedException) 
            {
                Image<Bgr,Byte> imageBgr = new Image<Bgr, Byte>(GetBitMap());
                image = imageBgr.Convert<Rgb, Byte>();
            }
            return image;
        }

        /// <summary>
        /// Who knows what this does?
        /// </summary>
        /// <param name="p"></param>
        public void CalibrateCamera(int p)
        {
            videoProxy.setCameraParameter(subscriberID, 22, p);
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public void Dispose()
        {
            if (videoProxy != null)
                videoProxy.Dispose();
        }
    }
}
