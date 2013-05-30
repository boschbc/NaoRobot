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
        private static int DefaultFps = 5;
        private static string SubscriberID = "CameraMonitor";

        private int fps;
        private Camera camera;
        private UpdaterThread worker;
        
        public CameraMonitor()
        {
            fps = DefaultFps;
            Init();
        }

        public CameraMonitor(int fps_)
        {
            fps = fps_;
            Init();
        }

        private void Init()
        {
            if (NaoState.Instance.Connected)
            {
                camera = new Camera(SubscriberID);
                camera.Enabled = true;
            }
            else
                camera = null;
            worker = new UpdaterThread(Interval, UpdateContent);
            InitializeComponent();
            HookEventHandlers();
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

        /*
         * Stops updating this component.
         */
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
                camera = new Camera(SubscriberID);
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
            Image<Rgb,Byte> image = camera.GetImage();
            Image img = image.ToBitmap(image.Width,image.Height);
            Console.WriteLine("TEST");
            imageContainer.Image = img;
        }
    }
}
