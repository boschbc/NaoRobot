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
        private int objectId;
        private double dist;

        public ObjectSearchThread(int objectID, double dist)
        {
            this.objectId = objectID;
            this.dist = dist;
        }

        public override void Run()
        {
            Call(LookForObject);
        }

        public void LookForObject()
        {
            ObjectRecogniser rec = ObjectRecogniser.GetInstance();
            ArrayList pictureInfos;
            Logger.Log(this, "Start LookForObjects");
            while (running && !found)
            {
                ArrayList data = rec.GetObjectData();
                pictureInfos = data.Count == 0 ? data : (ArrayList)data[1];

                for (int i = 0;!found && i < pictureInfos.Count; i++)
                {
                    ArrayList pictureInfo = (ArrayList)pictureInfos[i];
                    ArrayList labels = (ArrayList)pictureInfo[0];

                    if (StringToInt((String)labels[0]) == objectId)
                    {
                        ArrayList boundryPoint = (ArrayList)pictureInfo[3];
                        Console.WriteLine("----------");
                        for (int j = 0; j < boundryPoint.Count; j++)
                        {
                            ArrayList p = (ArrayList)boundryPoint[j];
                            Logger.Log(this, "x: " + p[0] + ", y: " + p[1]);
                        }
                    }
                }
                Thread.Sleep(250);
            }
            Logger.Log(this, "Exit LookForObjects");
        }

        public int StringToInt(String s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch (FormatException)
            {
                Console.WriteLine("Input string is not a sequence of digits.");
                return -1;
            }
            catch (OverflowException)
            {
                Console.WriteLine("The number cannot fit in an Int32.");
                return -1;
            }
        }
    }
}
