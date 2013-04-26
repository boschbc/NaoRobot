using System;
using Aldebaran.Proxies;
using Naovigate.Communication;
using Naovigate.Util;

namespace Naovigate.Haptics
{
    /// <description>Class that handles the 'grab' command. Grab an element right in front of the robot</description>
    class Grabber
    {
        /**
         * private NaoProxyManager proxyManager;
        private GoalCommunicator goal;

        public Grabber()
        {
            this.proxyManager = NaoProxyManager.Instance;
            this.goal = GoalCommunicator.Instance;
            this.goal.RegisterHandler("grab", this.OnGrabCommand);
        }

        public void OnGrabCommand()
        {
            MotionProxy motionProxy = this.proxyManager.Motion;
            RobotPostureProxy postureProxy = this.proxyManager.RobotPosture;

            // Apply crouch posture first.
            postureProxy.goToPosture("Crouch", 1.0);
            NaoState.Update();

            // Move those arms, boy.
            motionProxy.positionInterpolations(
                new[] { "LArm", "RArm" },
                MotionProxy.FRAME_ROBOT,
                new[] {
                    new[] { },
                    new[] { }
                },
                63, 0.5, false
            );
            NaoState.Update();
        }*/
    }
}
