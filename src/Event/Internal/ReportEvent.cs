using System;
using Naovigate.Util;
using Aldebaran.Proxies;

namespace Naovigate.Event.Internal
{
    /// <summary>
    /// give some information about the connected nao.
    /// </summary>
    public sealed class ReportEvent : NaoEvent
    {
        private String report = "";
        public ReportEvent() { }

        /// <summary>
        /// Fires the event.
        /// </summary>
        public override void Fire()
        {
            Logger.Log(this, "Building report.");
            if (!NaoState.Instance.Connected)
            {
                report += "Not connected to Nao";
            } else try
            {
                HasHands();
                HasMarkerDetection();
            }
            catch
            {
                Logger.Log(this, "Unexpected error building report.");
            }
            Report();
        }

        private void HasHands()
        {
            string msg = "Nao has Controllable hands: ";
            try
            {
                Logger.Log(this, "Accessing hands");
                Proxies.GetProxy<MotionProxy>().getAngles("LHand", false);
                msg += true;
            }
            catch
            {
                msg += false;
            }
            report += msg + "\n";
        }

        private void HasMarkerDetection()
        {
            string msg = "Nao has Marker Detection: ";
            try
            {
                Logger.Log(this, "Accessing Marker Detection");
                Proxies.GetProxy<LandMarkDetectionProxy>();
                msg += true;
            }
            catch
            {
                msg += false;
            }
            report += msg + "\n";
        }

        private void Report()
        {
            if (report == "") report = "Nothing to report";
            Logger.Log(this, report);
        }

        /// <summary>
        /// return this event's EventCode.
        /// </summary>
        public override EventCode EventCode
        {
            get { return EventCode.Report; }
        }
    }
}
