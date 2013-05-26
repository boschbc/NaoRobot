using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Aldebaran.Proxies;
using Naovigate.Communication;
using Naovigate.Util;
using Naovigate.Movement;

namespace Naovigate.Grabbing
{
    public class PutDownWorker : ActionExecutor
    {
        public static readonly float putDownSpeed = 0.3f;
        public static readonly float kneelDepth = 1f;

        private static readonly ArrayList releaseNames = new ArrayList(new string[] { "RShoulderRoll", "LShoulderRoll", "RHand", "LHand" });
        private static readonly ArrayList releaseAngles = new ArrayList(new float[] { -0.25f, 0.25f ,1f, 1f});

        public PutDownWorker(){ }

        public override void Run()
        {
            Call(PutDown);
        }

        /*
         * Put down the object the nao is holding.
         * if the Nao is stable, it will kneel to release the object,
         * else if will just drop it. Pose.Balanced may or may not attempt
         * to stabalise the Nao.
         */
        public void PutDown()
        {
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

        /*
         * release the object
         * more specific, it will spread its arms and
         * lower the grip in the hands.
         */
        private void Release()
        {
            Grabber.Instance.Motion.angleInterpolationWithSpeed(releaseNames, releaseAngles, putDownSpeed);
        }
    }
}
