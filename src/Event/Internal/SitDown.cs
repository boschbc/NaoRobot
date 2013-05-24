using System;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    class SitDownEvent : NaoEvent
    {
        public SitDownEvent() { }

        public override void Fire()
        {
            Pose.Instance.SitDown();
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
