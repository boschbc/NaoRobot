/**
 * @author Aldebaran Robotics
 * Aldebaran Robotics (c) 2009 All Rights Reserved.\n
 *
 * Version : $Id$
 */
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
        private void StartVideo()
        {
            StopVideo();
            videoProxy.subscribeCamera(subscriberID, 0, 1 /*kQVGA*/, 13 /*kRGB*/, 30);
        }

        /*
        * unsubscribe the camera of the nao with a specified name
        */
        private void StopVideo()
        {
            try
            {
                videoProxy.unsubscribe(subscriberID);
            }
            catch
            {
                Console.WriteLine("DisposeVideo: No Camera subscribed.");
            }
        }

        /**
         * Fetches the current image from Nao's camera, in raw form.
         * @throws an exception if not connected to any Nao.
         **/
        private ArrayList GetRawImage()
        {
            StartVideo();
            ArrayList imageObject = (ArrayList)videoProxy.getImageRemote(subscriberID);
            StopVideo();
            return imageObject;

        }

        /**
         * Fetches the current image from Nao's camera.
         * @returns null if not connected to any Nao.
         **/
        public Image GetImage()
        {
            if (!NaoState.IsConnected())
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
    }
}