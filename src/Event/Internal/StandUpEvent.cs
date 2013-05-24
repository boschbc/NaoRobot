using System;

using Naovigate.Movement;

namespace Naovigate.Event.Internal
{
    class StandUpEvent : NaoEvent
    {
        public StandUpEvent() { }

        public override void Fire()
        {
            Pose.Instance.StandUp();
        }

        public override void Abort()
        {
            throw new NotImplementedException();
        }
    }
}
