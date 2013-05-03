using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.GUI
{
    public partial class CameraMonitor : UserControl, IRealtimeField
    {
        private static int Fps = 5;

        public CameraMonitor()
        {
            InitializeComponent();
            HookEventHandlers();
            updateTimer.Interval = 1000 / Fps;
        }

        public void HookEventHandlers()
        {
            cameraEnabler.CheckedChanged += new EventHandler(ToggleCamera);
        }

        public void ToggleCamera(Object sender, EventArgs e)
        {
            Console.WriteLine("Cam control");
            if (cameraEnabler.Checked)
            {
                NaoState.InitVideo();
                updateTimer.Enabled = true;
            }
            else
            {
                updateTimer.Enabled = false;
                NaoState.UnsubscribeVideo();
            }
        }

        public void UpdateContent()
        {
            if (NaoState.OutOfDate(updateTimer.Interval))
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
            Image image = NaoState.GetImage();
            imageContainer.Image = image;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            UpdateContent();
        }
    }
}
