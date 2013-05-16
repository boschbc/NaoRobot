using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using Aldebaran.Proxies;

using Naovigate.Util;

namespace Naovigate.Vision
{
    public class Camera
    {
        private string subscriberID;
        private VideoDeviceProxy videoProxy;

        public Camera(string subID)
        {
            subscriberID = subID;
            videoProxy = NaoState.VideoProxy;
        }

        /*
        * Inits the camera of the Nao with a specified subscriber ID.
        */
        public void StartVideo()
        {
            StopVideo();
            videoProxy.subscribeCamera(subscriberID, 0, 1 /*kQVGA*/, 13 /*kRGB*/, 30);
        }

        /*
        * unsubscribe the camera of the nao with a specified name
        */
        public void StopVideo()
        {
            try
            {
                videoProxy.unsubscribe(subscriberID);
            }
            catch
            {
                return; //Console.WriteLine("DisposeVideo: No Camera subscribed.");
            }
        }

        /*
         * Fetches the current image from Nao's camera, in raw form.
         * @throws an exception if not connected to any Nao.
         */
        private ArrayList GetRawImage()
        {
            StartVideo();
            ArrayList imageObject = (ArrayList)videoProxy.getImageRemote(subscriberID);
            StopVideo();
            return imageObject;

        }

        /*
         * Fetches the current image from Nao's camera.
         * @returns null if not connected to any Nao.
         */
        public Image GetImage()
        {
            if (!NaoState.Connected)
                return null;

            ArrayList imageObject = GetRawImage();
            int width = (int)imageObject[0];
            int height = (int)imageObject[1];
            byte[] imageBytes = (byte[])imageObject[6];
            var stride = 4 * ((width * 3 + 3) / 4);
            return new Bitmap(width, height, stride,
                                System.Drawing.Imaging.PixelFormat.Format24bppRgb,
                                System.Runtime.InteropServices.Marshal.UnsafeAddrOfPinnedArrayElement(imageBytes, 0));
            
        }
        public void CallibrateCamera(int p)
        {
            //kCameraBrightnessID  0
            //int brightness = videoProxy.getCameraParameter(subscriberID, 0);
            //videoProxy.setCameraParameter(subscriberID, 0, brightness);
            //kCameraContrastID     1
            //int contrast = videoProxy.getCameraParameter(subscriberID, 1);
            //videoProxy.setCameraParameter(subscriberID, 1, contrast);
            //kCameraSaturationID   2
            //int saturation = videoProxy.getCameraParameter(subscriberID, 1);
            //videoProxy.setCameraParameter(subscriberID, 2, saturation);
            //int gain = videoProxy.getCameraParameter(subscriberID, 6);
            //videoProxy.setCameraParameter(subscriberID, 6, gain);
            //kCameraExposureAlgorithmID
            videoProxy.setCameraParameter(subscriberID, 22,p);
        }
    }
}
