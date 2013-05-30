﻿using System;
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

        //Looks for a object and then dissects the data
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
                double angle = CalculateAngle((ArrayList)pictureInfo[3]);
                double distance = CalculateDistance((ArrayList)pictureInfo[3]);
            }
        }

        //Takes the smallest and biggest x and then calculates the angle
        //between the nao and the middle point of the object.
        public float CalculateAngle(ArrayList boundryPoints)
        {
            float smallestX = SmallestBoundryPointX(boundryPoints);
            float biggestX = BiggestBoundryPointX(boundryPoints);
            float middle = (smallestX + biggestX) / 2;
            float angle = middle/4F;
            return angle;
        }
        //Takes the smallest and biggest x and then calculats the
        //scalling of the object. Through that it finds the distance to the object
        public float CalculateDistance(ArrayList boundryPoints)
        {
            float smallestX = SmallestBoundryPointX(boundryPoints);
            float biggestX = BiggestBoundryPointX(boundryPoints);
            float size = biggestX - smallestX;
            float distance = ObjectRecogniser.estimateDistance(size);
            return distance;
        }
        //returns smallest x from a Arraylist of boundryPoints
        public float SmallestBoundryPointX(ArrayList boundryPoints)
        {
            for (int j = 0; j < boundryPoints.Count; j++)
            {
                ArrayList p = (ArrayList)boundryPoints[j];
            }
            return 0.0f;
        }
        //returns smallest x from a Arraylist of boundryPoints
        public float BiggestBoundryPointX(ArrayList boundryPoints)
        {
            for (int j = 0; j < boundryPoints.Count; j++)
            {
                ArrayList p = (ArrayList)boundryPoints[j];
            }
            return 0.0f;
        }

        public double getSizeObject(int objectId)
        {
            return 14;
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
