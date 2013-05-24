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
        public PutDownWorker()
        {

        }

        public override void Run()
        {
            Call(PutDown);
            running = false;
        }

        /*
         * put down the object the nao is holding
         */
        public void PutDown()
        {
            float kneelDepth = 1f;
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
         * lower the arms
         */
        private void LowerArms(float armsDown)
        {
            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            names.Add("LShoulderPitch");
            names.Add("RShoulderPitch");

            angles.Add(armsDown);
            angles.Add(armsDown);

            Grabber.Instance.Motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }

        /*
         * release the object
         */
        private void Release()
        {

            ArrayList names = new ArrayList();
            ArrayList angles = new ArrayList();

            names.Add("RShoulderRoll");
            names.Add("LShoulderRoll");
            names.Add("RHand");
            names.Add("LHand");
            // spread arms
            angles.Add(-0.25f);
            angles.Add(0.25f);
            // release hands
            angles.Add(1);
            angles.Add(1);

            Grabber.Instance.Motion.angleInterpolationWithSpeed(names, angles, 0.3F);
        }
    }
}
