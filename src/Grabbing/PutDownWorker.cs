using System;
using System.Collections;

using Aldebaran.Proxies;

using Naovigate.Communication;
using Naovigate.Movement;
using Naovigate.Util;

namespace Naovigate.Grabbing
{
    public class PutDownWorker : ActionExecutor
    {
        public static readonly float putDownSpeed = 0.3f;
        public static readonly float kneelDepth = 1f;

        private static readonly ArrayList releaseNames = new ArrayList(new string[] { "RShoulderRoll", "LShoulderRoll", "RHand", "LHand" });
        private static readonly ArrayList releaseAngles = new ArrayList(new float[] { -0.25f, 0.25f ,1f, 1f});

        /// <summary>
        /// Default constructor.
        /// </summary>
        public PutDownWorker() { }

        /// <summary>
        /// The put-down procedure will begin.
        /// </summary>
        public override void Run()
        {
            Call(PutDown);
        }

        /// <summary>
        /// The Nao will attempt to put down the object it is holding.
        /// If it is stable, it will first kneel before releasing the object,
        /// otherwise it will just drop it.
        /// The Pose module may or may not try to stabilize the Nao.
        /// </summary>
        /// <exception cref="InvalidOperationException">The Nao is not holding any object.</exception>
        public void PutDown()
        {
            if (!Grabber.Instance.HoldingObject())
                throw new InvalidOperationException("PutDown() while not holding any object.");
            Call(() => NaoState.Instance.SpeechProxy.say("Put Down"));
            if (Pose.Instance.Balanced)
            {
                Call(() => NaoState.Instance.SpeechProxy.say("Stable"));
                Call(() => Pose.Instance.Kneel(kneelDepth));
            }
            else
            {
                Call(() => NaoState.Instance.SpeechProxy.say("Unstable"));
            }
            Call(Release);
            Call(Pose.Instance.StandUp);
        }

        /// <summary>
        /// The Nao will spread its arms and soften its fingertips.
        /// </summary>
        private void Release()
        {
            Grabber.Instance.Motion.angleInterpolationWithSpeed(releaseNames, releaseAngles, putDownSpeed);
        }
    }
}
