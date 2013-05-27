using System;
using System.Collections.Generic;

using Aldebaran.Proxies;

using System.Collections;
using Naovigate.Util;

namespace Naovigate.Vision
{
    public class MarkerRecogniser
    {
        private LandMarkDetectionProxy detector;
        private MemoryProxy memory;

        public static MarkerRecogniser instance = null;

        public static double FRANKENAO2C = 8.4;

        public static MarkerRecogniser GetInstance()
        {
            return instance == null ? instance = new MarkerRecogniser() : instance;
        }

        public MarkerRecogniser()
        {
            detector = NaoState.Instance.LandMarkDetectionProxy;
            detector.subscribe("MarkerRecogniser", 1000, 0F);

            memory = NaoState.Instance.MemoryProxy;

            Camera camera = new Camera("MarkerRecogniser");
            camera.StartVideo();
            camera.CalibrateCamera(3);
            camera.StopVideo();
        }

        public static double estimateDistance(float sizeY)
        {
            return (1 / sizeY)/FRANKENAO2C;
        }

        //returns [[TimeStampField][Mark_info_0, Mark_info_1, . . . , Mark_info_N-1]] when N landmarks are detected 
        public ArrayList GetMarkerData()
        {
            return (ArrayList)memory.getData("LandmarkDetected");
        }
    }
}

