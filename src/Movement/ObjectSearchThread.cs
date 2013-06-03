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

        public ObjectSearchThread(int objectID)
        {
            //TODO
        }

        public ObjectSearchThread(int objectID, double dist)
        {
            this.objectId = objectID;
            this.dist = dist;
        }

        /// <summary>
        /// True if the object was found.
        /// </summary>
        public bool ObjectFound
        {
            get { return true; }
        }

        public override void Run()
        {
            //LookForObject();
        }

        //Looks for a object and then dissects the data
        //public void LookForObject()
        //{
        //    ObjectRecogniser rec = ObjectRecogniser.GetInstance();
        //    ArrayList pictureInfos;

        //    while (!found)
        //    {
        //        if (!Walk.Instance.IsMoving()) running = false;
        //        ArrayList data = rec.GetObjectData();
        //        pictureInfos = data.Count == 0 ? data : (ArrayList)data[1];
        //        for (int i = 0;!found && i < pictureInfos.Count; i++)
        //        {
        //            Console.WriteLine("Object found");
        //            ObjectCalculations((ArrayList)pictureInfos[i]);
        //        }
        //        Thread.Sleep(250);
        //    }
        //    Console.WriteLine("Exit LookForObjects");
        //}

        //public void ObjectCalculations(ArrayList pictureInfo)
        //{
        //    ArrayList labels = (ArrayList)pictureInfo[0];
        //    if (StringToInt((String)labels[0]) == objectId)
        //    {
        //        float angle = CalculateAngle((ArrayList)pictureInfo[3]);
        //        float distance = CalculateDistance((ArrayList)pictureInfo[3]);
        //        Console.WriteLine("distance: " + distance);
        //        Call(() => Walk.Instance.StartWalking(0.5F, 0, Math.Max(-1, Math.Min(1, angle))));
        //        Console.WriteLine("walking");
        //        if (MarkerRecogniser.estimateDistance(distance) <= dist)
        //        {
        //            running = false;
        //        }
        //        Walk.Instance.StopMove();
        //        Console.WriteLine("Exit LookForObject");
        //    }
        //}

        ////Takes the smallest and biggest x and then calculates the angle
        ////between the nao and the middle point of the object.
        //public float CalculateAngle(ArrayList boundryPoints)
        //{
        //    float smallestX = SmallestBoundryPointX(boundryPoints);
        //    float biggestX = BiggestBoundryPointX(boundryPoints);
        //    float middle = (smallestX + biggestX) / 2;
        //    float angle = middle/4F;
        //    return angle;
        //}
        ////Takes the smallest and biggest x and then calculats the
        ////scalling of the object. Through that it finds the distance to the object
        //public float CalculateDistance(ArrayList boundryPoints)
        //{
        //    float smallestX = SmallestBoundryPointX(boundryPoints);
        //    float biggestX = BiggestBoundryPointX(boundryPoints);
        //    float size = biggestX - smallestX;
        //    float distance = ObjectRecogniser.estimateDistance(size);
        //    return distance;
        //}
        ////returns smallest x from a Arraylist of boundryPoints
        //public float SmallestBoundryPointX(ArrayList boundryPoints)
        //{
        //    float smallest = 1000;
        //    for (int j = 0; j < boundryPoints.Count; j++)
        //    {
        //        ArrayList p = (ArrayList)boundryPoints[j];
        //        if (smallest > (float)p[0])
        //        {
        //            smallest = (float)p[0];
        //        }
        //    }
        //    return smallest;
        //}
        ////returns smallest x from a Arraylist of boundryPoints
        //public float BiggestBoundryPointX(ArrayList boundryPoints)
        //{
        //    float biggest= -10000;
        //    for (int j = 0; j < boundryPoints.Count; j++)
        //    {
        //        ArrayList p = (ArrayList)boundryPoints[j];
        //        if (biggest < (float)p[0])
        //        {
        //            biggest = (float)p[0];
        //        }
        //    }
        //    return biggest;
        //}

        //public double getSizeObject(int objectId)
        //{
        //    return 14;
        //}

        //public int StringToInt(String s)
        //{
        //    try
        //    {
        //        return Convert.ToInt32(s);
        //    }
        //    catch (FormatException e)
        //    {
        //        Console.WriteLine("Input string is not a sequence of digits. Exception:" + e);
        //        return -1;
        //    }
        //    catch (OverflowException e)
        //    {
        //        Console.WriteLine("The number cannot fit in an Int32. Exception:" + e);
        //        return -1;
        //    }
        //}
    }
}
