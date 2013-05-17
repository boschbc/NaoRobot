using System;
using System.Linq;
using Aldebaran.Proxies;
using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.Haptics
{
    /// <description>Class that handles the 'grab' command. Grab an element right in front of the robot</description>
    class Grabber
    {
        private GoalCommunicator goal;
        private static Grabber instance;

        public static Grabber Instance
        {
            get
            {
                return instance == null ? instance = new Grabber() : instance;
            }
        }

        /*
         * Constructor
         */
        public Grabber()
        {
            this.goal = GoalCommunicator.Instance;
        }

        /*
         * The movemont for the grabbing
         */
        public void Grab()
        {
            MotionProxy motionProxy = NaoState.MotionProxy;
            RobotPostureProxy postureProxy = NaoState.PostureProxy;

            // Apply crouch posture first.
            postureProxy.goToPosture("Crouch", 1.0f);
            NaoState.Update();

            // Move those arms, boy.
            motionProxy.positionInterpolations(
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

        }

        public static void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
