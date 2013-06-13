using System;
using System.Collections;
using Aldebaran.Proxies;
using Naovigate.Util;

namespace Naovigate.Vision
{
    internal sealed class MarkerRecogniser : IDisposable
    {
        private LandMarkDetectionProxy detector;
        private MemoryProxy memory;

        public static MarkerRecogniser instance = null;

        public static double FRANKENAO2C = 5.577;

        public static MarkerRecogniser Instance
        {
            get { return instance == null ? instance = new MarkerRecogniser() : instance; }
        }

        public MarkerRecogniser()
        {
            detector = Proxies.GetProxy<LandMarkDetectionProxy>();
            detector.subscribe("MarkerRecogniser", 1000, 0F);
            memory = Proxies.GetProxy<MemoryProxy>();

            Camera camera = new Camera("MarkerRecogniser");
            camera.Subscribe();
            camera.CalibrateCamera(3);
            camera.Unsubscribe();
        }

        public static double estimateDistance(float sizeY)
        {
            return (1 / sizeY)/FRANKENAO2C;
        }

        /// <summary>
        /// Retrieves the marker data from the Nao's memory.
        /// </summary>
        /// <returns>
        /// A list of the form: 
        /// [TimeStamp, MarkerInformation[N], CameraPoseInNaoSpace, CameraPoseInWorldSpace, CurrentCameraName].
        /// </returns>
        public ArrayList GetMarkerData()
        {
            return (ArrayList)memory.getData("LandmarkDetected");
        }

        /// <summary>
        /// Returns a list with all detected marker IDs.
        /// Returns an empty list if no markers were detected.
        /// </summary>
        /// <returns>A list with marker IDs.</returns>
        public ArrayList GetDetectedMarkers()
        {
            //markerData = [TimeStamp, MarkerInformation[N], CameraPoseInNaoSpace, CameraPoseInWorldSpace, CurrentCameraName].
            ArrayList markerData = GetMarkerData();   
            ArrayList markerInfos = markerData.Count == 0 ? new ArrayList() : (ArrayList)markerData[1];
            ArrayList markerIDs = new ArrayList();

            foreach (ArrayList markerInfo in markerInfos)
            {
                markerIDs.Add(((ArrayList)markerInfo[1])[0]);  //markerInfo = [ShapeInfo, MarkerID]
            }

            return markerIDs;
        }

        /// <summary>
        /// Disposes of this instance.
        /// </summary>
        public void Dispose()
        {
            if (detector != null)
                detector.Dispose();
            if (memory != null)
                memory.Dispose();
        }
    }
}

