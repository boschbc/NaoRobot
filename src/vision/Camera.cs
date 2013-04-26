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

namespace Aldebaran.NaoCamCSharpExample
{
    public struct NaoCamImageFormat
    {
        public string name;
        public int id;
        public int width;
        public int height;
    }

    public class Camera
    {
        //private VideoDeviceProxy _videoDeviceProxy = null;
        //private string _ip = "145.94.188.219";

        public List<NaoCamImageFormat> NaoCamImageFormats = new List<NaoCamImageFormat>();

        public static byte[] GetImage(VideoDeviceProxy proxy)
        {
            byte[] imageBytes = new byte[0];

            try
            {
                if (proxy != null)
                {
                    Object imageObject = proxy.getImageRemote("dotNetExample");
                    imageBytes = (byte[])((ArrayList)imageObject)[6];
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Camera.GetImage exception: " + e);
            }
            return imageBytes;
        }

        public static void AMain()
        {
            String ip = "145.94.191.20";
            TextToSpeechProxy tts = new TextToSpeechProxy(ip, 9559);
            tts.say("Hello");
            VideoDeviceProxy video = new VideoDeviceProxy(ip, 9559);
            video.unsubscribe("dotNetExample");
            video.subscribeCamera("dotNetExample", 0, 1 /*320*240*/, 13 /*kBGR*/, 30);
            byte[] b = GetImage(video);
            ByteArrayToFile("D:\\Testmap\\image.jpg",b);
            MemoryStream stream = new MemoryStream(b);
            Bitmap data = new Bitmap(stream);
            Console.Write(data.GetPixel(3,3));
            //Image img = Image.FromStream(stream);
            
        }

        public static bool ByteArrayToFile(string FileName, byte[] ByteArray)
        {
            try
            {
                // Open file for reading
                System.IO.FileStream FileStream =
                   new System.IO.FileStream(FileName, System.IO.FileMode.Create,
                                            System.IO.FileAccess.Write);
                // Writes a block of bytes to this stream using data from
                // a byte array.
                FileStream.Write(ByteArray, 0, ByteArray.Length);

                // close file stream
                FileStream.Close();

                return true;
            }
            catch (Exception e)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}",
                                  e.ToString());
            }

            // error occured, return false
            return false;
        }
    }
}