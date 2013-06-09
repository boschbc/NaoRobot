using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.GUI.LiveCamera
{
    public sealed partial class LiveCamera : UserControl, IRealtimeField
    {
        private static readonly int DEFAULT_FPS = 5;
        private static readonly string SUBSCRIBER_ID = "LiveCamera";

        private int fps;
        private bool active;
        private Camera camera;
        private UpdaterThread updater;
        
        public LiveCamera()
        {
            InitializeComponent();
            fps = DEFAULT_FPS;
            updater = new UpdaterThread(Interval, UpdateContent);
            Active = false;
        }

        public LiveCamera(int fps)
            : this()
        {
            this.fps = fps;
        }

        private int Interval
        {
            get { return 1000 / fps; }
        }

        public Camera Camera
        {
            get { return camera; }
        }

        public bool Active
        {
            get { return active; }
            set
            {
                if (value)
                    Activate();
                else
                    Deactivate();
                UpdateEnabledCheckBox();
            }
        }

        public Func<Image> ImageSource
        {
            get;
            set;
        }

        private void CreateCamera()
        {
            try
            {
                Logger.Log(this, "Creating camera...");
                camera = new Camera(SUBSCRIBER_ID);
                Logger.Log(this, "Camera created.");
            }
            catch (UnavailableConnectionException) 
            {
                Logger.Log(this, "Could not instantiate Camera, connection unavailable.");
            }
        }

        private void Activate()
        {
            active = false;
            if (camera == null)
                CreateCamera();
            if (camera != null)
            {
                camera.Subscribe();
                cameraEnhancer.Target = camera;
                updater.Enabled = true;
                active = true;
            }
        }

        private void Deactivate()
        {
            active = false;
            ResetContent();
            if (camera != null)
            {
                camera.Unsubscribe();
                cameraEnhancer.Target = null;
            }
            updater.Enabled = false;
        }

        private void UpdateEnabledCheckBox()
        {
            //Avoid cross-thread exception:
            if (cameraEnabled.InvokeRequired)
                cameraEnabled.Invoke(new MethodInvoker(UpdateEnabledCheckBox));
            else
                cameraEnabled.Checked = active;
        }

        public void ResetContent()
        {
            if (imageContainer.InvokeRequired)
                imageContainer.Invoke(new MethodInvoker(ResetContent));
            else
                imageContainer.Image = new Bitmap(1, 1);
        }

        public void UpdateContent()
        {
            if (camera == null || !NaoState.Instance.Connected)
            {
                Active = false;
                return;
            }
            if (NaoState.Instance.OutOfDate(Interval))
            {
                try
                {
                    NaoState.Instance.Update();
                }
                catch (UnavailableConnectionException)
                {
                    Logger.Log(this, "Could not update, connection unavailable.");
                }
            }

            if (ImageSource == null)
                imageContainer.Image = camera.GetBitMap();
            else
                imageContainer.Image = ImageSource();
        }

        private void cameraEnhanced_CheckedChanged(object sender, EventArgs e)
        {
            if (cameraEnhanced.Checked)
                ImageSource = cameraEnhancer.Enhance;
            else
                ImageSource = null;
        }

        private void cameraEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (cameraEnabled.Checked)
                Active = true;
            else
                Active = false;
        }
    }
}
