using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Threading;

using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.GUI
{
    public partial class CameraMonitor : UserControl, IRealtimeField
    {
        private static int DefaultFps = 5;

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
            camera = new Camera("CameraMonitor");
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

        public void UpdateContent()
        {
            if (NaoState.OutOfDate(Interval))
            {
                try
                {
                    NaoState.Update();
                }
                catch (UnavailableConnectionException e)
                {
                    Console.WriteLine("Caught exception: " + e.Message);
                    return;
                }
            }
            Image image = camera.GetImage();
            imageContainer.Image = image;
        }
    }
}
