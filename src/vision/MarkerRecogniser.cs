using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aldebaran.Proxies;
using System.Collections;
using Naovigate.Util;

namespace Naovigate.vision
{
    public class MarkerRecogniser
    {
        private LandMarkDetectionProxy detector;
        private MemoryProxy memory;

        public static MarkerRecogniser instance = null;

        public static MarkerRecogniser GetInstance()
        {
            return instance == null ? new MarkerRecogniser(NaoState.GetIP(),NaoState.GetPort()) : instance;
        }

        public MarkerRecogniser(String ip, int port)
        {
            detector = new LandMarkDetectionProxy(ip, port);
            detector.subscribe("MarkerRecogniser", 1000, 0F);

            memory = new MemoryProxy(ip, port);
        }

        //returns [[TimeStampField][Mark_info_0, Mark_info_1, . . . , Mark_info_N-1]] when N landmarks are detected 
        public ArrayList getMarkerData()
        {
            return (ArrayList)memory.getData("LandmarkDetected");
        }
    }
}
