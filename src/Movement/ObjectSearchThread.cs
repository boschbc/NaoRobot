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
            LookForObject();
        }

        public void LookForObject()
        {
            ObjectRecogniser rec = ObjectRecogniser.GetInstance();
            ArrayList pictureInfos;

            while (!found)
            {
                if (!Walk.Instance.IsMoving()) running = false;
                ArrayList data = rec.GetObjectData();
                pictureInfos = data.Count == 0 ? data : (ArrayList)data[1];
                
                for (int i = 0;!found && i < pictureInfos.Count; i++)
                {
                    ObjectCalculations((ArrayList)pictureInfos[i]);
                }
                Thread.Sleep(250);
            }
            Console.WriteLine("Exit LookForObjects");
        }

        public void ObjectCalculations(ArrayList pictureInfo)
        {
            ArrayList labels = (ArrayList)pictureInfo[0];
            if (StringToInt((String)labels[0]) == objectId)
            {
                CalculateAngle((ArrayList)pictureInfo[3]);
            }
        }

        public void CalculateAngle(ArrayList boundryPoint)
        {
            Console.WriteLine("----------");
            for (int j = 0; j < boundryPoint.Count; j++)
            {
                ArrayList p = (ArrayList)boundryPoint[j];
                Console.WriteLine("x: " + p[0] + ", y: " + p[1]);
            }
        }

        public int StringToInt(String s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch (FormatException e)
            {
                Console.WriteLine("Input string is not a sequence of digits. Exception:" + e);
                return -1;
            }
            catch (OverflowException e)
            {
                Console.WriteLine("The number cannot fit in an Int32. Exception:" + e);
                return -1;
            }
        }
    }
}
