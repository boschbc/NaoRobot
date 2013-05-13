using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static double FRANKENAOC = 5.414;

        public static MarkerRecogniser GetInstance()
        {
            return instance == null ? instance = new MarkerRecogniser(NaoState.IP.ToString(), NaoState.Port) : instance;
        }

        public MarkerRecogniser(String ip, int port)
        {
            detector = new LandMarkDetectionProxy(ip, port);
            detector.subscribe("MarkerRecogniser", 1000, 0F);

            memory = new MemoryProxy(ip, port);
        }

        public static double estimateDistance(float sizeY)
        {
            return (1 / sizeY)/FRANKENAOC;
        }

        //returns [[TimeStampField][Mark_info_0, Mark_info_1, . . . , Mark_info_N-1]] when N landmarks are detected 
        public ArrayList GetMarkerData()
        {
            return (ArrayList)memory.getData("LandmarkDetected");
        }
    }
}

