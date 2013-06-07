using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naovigate.Event.NaoToGoal
{
    /// <summary>
    /// Notify goal that the Nao is no longer holding an object.
    /// </summary>
    public class DroppedObjectEvent : DataSendingNaoEvent
    {
        public new static readonly EventCode code = EventCode.Dropped;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public DroppedObjectEvent() : base((byte)EventCode.Dropped) { }

    }
}
