using System;
using System.Linq;
using System.Collections;
using Aldebaran.Proxies;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Grabbing
{
    /// <description>Class that handles the 'grab' command. Grab an element right in front of the robot</description>
    class Grabber
    {
        private static Grabber instance;
        MotionProxy motion;
        RobotPostureProxy posture;
        
        /*
         * Constructor
         */
        public Grabber()
        {
            motion = NaoState.MotionProxy;
            posture = NaoState.PostureProxy;
            instance = this;
        }
        
        public static Grabber Instance
        {
            get
            {
                return instance == null ? instance = new Grabber() : instance;
            }
        }

        /*
         * The movemont for the grabbing
         */
        public void Grab()
        {
            // Apply crouch posture first.
            posture.goToPosture("Crouch", 1.0f);
            NaoState.Update();

            // Move those arms, boy.
            motion.positionInterpolations(
                new[] { "LArm", "RArm" }.ToList(),  // Move left and right arms.
                2,                                  // MotionProxy.ROBOT_FRAME. Not defined by NaoQI.NET.
                new[] {                             // Positions for each arm.
                    // Left arm positions: (x, y, z, θx, θy, θz)
                    new[] { 0.5, 0.0, 0.1, 0.1, 0.1, 0.0 },
                    // Right arm positions: (x, y, z, θx, θy, θz)
                    new[] { -0.5, 0.0, 0.1, -0.1, 0.1, 0.0 }
                },
                63f,                                // Move rotation AND position.
                new[] { new[] { 0.5f },             // Movement times of left positions.
                        new[] { 0.5f } },           // Movement times of right positions.
                false                               // No absolute movement.
            );
            NaoState.Update();
        }

        public void PutDown()
        {
            // testing: move to holding position
            CoolGrabber.Instance.doSomething();

            Walk.Instance.InitMove();
            NaoState.SpeechProxy.say("Put Down Object");
            ArrayList names = new ArrayList(2);
            names.Add("LArm");
            names.Add("RArm");
            motion.setStiffnesses(names, 1F);

            float armsDown = 0.8f;
            float kneelDepth = 0.5f;
            
            LowerArms(armsDown);
            Pose.Instance.Kneel(kneelDepth);
            Release();
            posture.goToPosture("Stand", 1F);
        }

        private void LowerArms(float armsDown)
        {
            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");

            angles.Add(armsDown);
            angles.Add(armsDown);

            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }

        private void Release()
        {
            // spread arms
            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            names.Add("RShoulderRoll");
            names.Add("LShoulderRoll");
            angles.Add(-0.25f);
            angles.Add(0.25f);

            motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }

        public static void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
