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

        public Grabber()
        {
            this.goal = GoalCommunicator.Instance;
            this.goal.RegisterHandler("grab", this.OnGrabCommand);
        }

        public void OnGrabCommand()
        {
            MotionProxy motionProxy = NaoState.MotionProxy;
            RobotPostureProxy postureProxy = NaoState.PostureProxy;

            // Apply crouch posture first.
            postureProxy.goToPosture("Crouch", 1.0f);
            NaoState.Update();

            // Move those arms, boy.
            motionProxy.positionInterpolations(
                new[] { "LArm", "RArm" }.ToList(),
                MotionProxy.FRAME_ROBOT,
                new[] {
                    new[] { 1.0 },
                    new[] { 1.0 }
                },
                63f, 0.5f, false
            );
            NaoState.Update();
        }
    }
}
