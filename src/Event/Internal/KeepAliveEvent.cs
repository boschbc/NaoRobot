using Naovigate.Event;
using Naovigate.Event.NaoToGoal;
namespace Naovigate.Event.Internal
{
    public class KeepAliveEvent : DataSendingNaoEvent
    {
        public KeepAliveEvent() : base(EventCode.KeepAlive) { }

        public override EventCode EventCode
        {
            get { return EventCode.KeepAlive; }
        }
    }
}