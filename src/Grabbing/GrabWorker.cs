using System.Collections;
using Aldebaran.Proxies;
using Naovigate.Movement;
using Naovigate.Util;


namespace Naovigate.Grabbing
{
    public class GrabWorker : ActionExecutor
    {
        public static readonly float grabSpeed = 0.3f;

        private static readonly ArrayList spreadArmsNames = new ArrayList(new string[] { "LShoulderRoll", "RShoulderRoll", "LElbowYaw", "RElbowYaw", "LShoulderPitch", "RShoulderPitch", "LHand", "RHand" });
        private static readonly ArrayList spreadArmsAngles = new ArrayList(new float[] { 1.3265F, -1.3265F, -1.2F, 1.2F, 0.5F, 0.5F, 1F, 1F });
        
        private static readonly ArrayList closeArmsAroundObjectNames = new ArrayList(new string[] { "LShoulderRoll", "RShoulderRoll" });
        private static readonly ArrayList closeArmsAroundObjectAngles = new ArrayList(new float[] { -0.3142F, 0.3142F });
        
        private static readonly ArrayList grabHandsNames = new ArrayList(new string[] { "LHand", "RHand" });
        private static readonly ArrayList grabHandsAngles = new ArrayList(new float[] { 0F, 0F });

        private static readonly ArrayList shoulderPitch = new ArrayList(new string[] { "LShoulderPitch", "RShoulderPitch" });
        private static readonly ArrayList elbowRoll = new ArrayList(new string[] { "LElbowRoll", "RElbowRoll" });

        MotionProxy motion;
        RobotPostureProxy posture;
        public GrabWorker()
        {
            motion = Grabber.Instance.Motion;
            posture = Grabber.Instance.Posture;
        }

        public override void Run()
        {
            Call(Grab);
        }

        /// <summary>
        /// Make a grabbing move in an attempt to grab an object in front of the Nao.
        /// </summary>
        public void Grab()
        {
            Call(Pose.Instance.StandUp);
            Call(StiffenArms);
            Call(SpreadArms);
            Call(CloseArmsAroundObject);
            //Call(GrabHands);
            Call(HoldForWalking);
        }

        public void StiffenArms()
        {
            motion.setStiffnesses(new ArrayList(new string[]{ "LArm", "RArm"}), 1F);
        }

        public void SpreadArms()
        {
            motion.angleInterpolationWithSpeed(spreadArmsNames, spreadArmsAngles, grabSpeed);
        }

        public void CloseArmsAroundObject()
        {
            motion.angleInterpolationWithSpeed(closeArmsAroundObjectNames, closeArmsAroundObjectAngles, grabSpeed);
        }

        /// <summary>
        /// close the Nao's hands
        /// </summary>
        public void GrabHands()
        {
            motion.setStiffnesses(grabHandsNames, 0.4F);
            motion.angleInterpolationWithSpeed(grabHandsNames, grabHandsAngles, grabSpeed);
        }

        /// <summary>
        /// Put the object as close to the Nao's chest as possible,
        /// to keep the Nao as ballanced as possible.
        /// </summary>
        public void HoldForWalking()
        {
            motion.angleInterpolationWithSpeed(shoulderPitch, new ArrayList(new float[] { 0F, 0F }), grabSpeed);
            motion.angleInterpolationWithSpeed(elbowRoll, new ArrayList(new float[] { -1.4F, 1.4F }), grabSpeed);
            motion.angleInterpolationWithSpeed(shoulderPitch, new ArrayList(new float[] { 1.4F, 1.4F }), grabSpeed);
        }
    }
}
