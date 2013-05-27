using System;
using System.Threading;
using System.Collections;
using Naovigate.Util;
using Naovigate.Movement;
using Naovigate.Vision;

namespace Naovigate.Movement
{
    public class ObjectSearchThread : ActionExecutor
    {
        private bool found;
        private String objectId;
        private String side;
        private double dist;

        public ObjectSearchThread(String objectID, String side, double dist)
        {
            this.objectId = objectID;
            this.side = side;
            this.dist = dist;
        }

        public override void Run()
        {
            LookForObject();
        }

        public void LookForObject()
        {
            ObjectRecogniser rec = ObjectRecogniser.GetInstance();
            ArrayList objects;

            while (running && !found)
            {
                if (!Walk.Instance.IsMoving()) running = false;
                ArrayList data = rec.GetObjectData();
                objects = data.Count == 0 ? data : (ArrayList)data[1];
                for (int i = 0; running && !found && i < objects.Count; i++)
                {
                    ArrayList marker = (ArrayList)objects[i];
                    if (running && (String)((ArrayList)objects[1])[0] == objectId)
                    {
                        float angle = ((float)((ArrayList)objects[0])[1]) / 4F;
                        Call(() => Walk.Instance.StartWalking(0.5F, 0, Math.Max(-1, Math.Min(1, angle))));

                        float sizeY = ((float)((ArrayList)objects[0])[4]);

                        if (running && MarkerRecogniser.estimateDistance(sizeY) <= dist)
                        {
                            Walk.Instance.StopMove();
                            found = true;
                        }
                    }
                }
                Thread.Sleep(250);
            }
            Console.WriteLine("Exit LookForObjects");
            }  
        }
}
