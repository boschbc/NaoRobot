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
            updateTimer.Interval = 1000 / Fps;
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
