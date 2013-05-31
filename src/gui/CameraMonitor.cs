using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;

using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.GUI
{
    public partial class CameraMonitor : UserControl, IRealtimeField
    {
        private static readonly int DEFAULT_FPS = 5;
        private static readonly string SUBSCRIBER_ID = "CameraMonitor";

        private int fps;
        private Camera camera;
        private UpdaterThread worker;
        
        public CameraMonitor()
        {
            fps = DEFAULT_FPS;

            if (NaoState.Instance.Connected)
            {
                camera = new Camera(SUBSCRIBER_ID);
                camera.Enabled = true;
            }
            else
                camera = null;
            worker = new UpdaterThread(Interval, UpdateContent);
            InitializeComponent();
            HookEventHandlers();
        }

        public CameraMonitor(int fps_) : this()
        {
            fps = fps_;
        }

        private int Interval
        {
            get { return 1000 / fps; }
        }

        private void HookEventHandlers()
        {
            cameraEnabler.CheckedChanged += new EventHandler(ToggleCamera);
        }

        public void ToggleCamera(Object sender, EventArgs e)
        {
            if (cameraEnabler.Checked)
            {
                worker.Enabled = true;
            }
            else
            {
                worker.Enabled = false;
            }
        }

        public void StopUpdate()
        {
            worker.Enabled = false;
        }

        public void UpdateContent()
        {
            if (!NaoState.Instance.Connected)
                return;
            else if (camera == null)
            {
                camera = new Camera(SUBSCRIBER_ID);
                camera.Enabled = true;
            }
            if (NaoState.Instance.OutOfDate(Interval))
            {
                try
                {
                    NaoState.Instance.Update();
                }
                catch (UnavailableConnectionException e)
                {
                    Console.WriteLine("Caught exception: " + e.Message);
                    return;
                }
            }
            Image<Rgb, Byte> image = camera.GetImage();
            Processing ps = new Processing(camera);
            Image<Gray, Byte> enchancedImg = ps.EnchancedImage();
            Image img = image.ToBitmap(image.Width, image.Height);
            Image enchImg = enchancedImg.ToBitmap(image.Width,image.Height);
            if (cameraEnhancer.Checked)
            {
                imageContainer.Image = enchImg;
            }
            else
            {
                imageContainer.Image = img;
            }
        }
    }
}
