using System;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Vision;

namespace Naovigate.GUI.LiveCamera
{
    /// <summary>
    /// A control that displays live images from a camera.
    /// </summary>
    internal sealed partial class LiveCamera : UserControl, IRealtimeField
    {
        private static readonly int DEFAULT_FPS = 5;
        private static readonly string SUBSCRIBER_ID = "LiveCamera";

        private int fps;
        private bool active;
        private Camera camera;
        private UpdaterThread updater;
        
        /// <summary>
        /// Creates a new instance of this control with default FPS of 5.
        /// </summary>
        public LiveCamera()
        {
            InitializeComponent();
            fps = DEFAULT_FPS;
            updater = new UpdaterThread(Interval, UpdateContent);
            Active = false;
        }

        /// <summary>
        /// Creates a new instance of this control with the given FPS.
        /// </summary>
        /// <param name="fps">The desired refresh rate of the video feed.</param>
        public LiveCamera(int fps)
            : this()
        {
            this.fps = fps;
        }

        /// <summary>
        /// This control's refresh rate in ms.
        /// </summary>
        private int Interval
        {
            get { return 1000 / fps; }
        }

        /// <summary>
        /// The camera from which this control retrieves images.
        /// </summary>
        public Camera Camera
        {
            get { return camera; }
        }

        /// <summary>
        /// True if the control is active (displaying the video-feed).
        /// </summary>
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

        /// <summary>
        /// A customizable source for the images.
        /// </summary>
        public Func<Image> ImageSource
        {
            get;
            set;
        }

        /// <summary>
        /// Attempts to create an instance of a Camera class.
        /// </summary>
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

        /// <summary>
        /// If not linked to any camera, attempts to create one.
        /// Proceeds to subscribe to the camera's video-feed
        /// </summary>
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

        /// <summary>
        /// Clears the video display, unsubscribes the camera and stops refreshing the control.
        /// </summary>
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

        /// <summary>
        /// Sets the enabled checkbox checked property in accordance to the active state of this control.
        /// </summary>
        private void UpdateEnabledCheckBox()
        {
            //Avoid cross-thread exception:
            if (cameraEnabled.InvokeRequired)
                cameraEnabled.Invoke(new MethodInvoker(UpdateEnabledCheckBox));
            else
                cameraEnabled.Checked = active;
        }

        /// <summary>
        /// Clears the video display.
        /// </summary>
        public void ResetContent()
        {
            if (imageContainer.InvokeRequired)
                imageContainer.Invoke(new MethodInvoker(ResetContent));
            else
                imageContainer.Image = new Bitmap(1, 1);
        }

        /// <summary>
        /// Updates the video display.
        /// </summary>
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
