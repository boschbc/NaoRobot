using System;
using System.Collections.Generic;
using Naovigate.Movement;

namespace Naovigate.Event.GoalToNao
{
    public class MapOverviewEvent : NaoEvent
    {
        public new static readonly EventCode code = EventCode.MapOverview;

        /// <summary>
        /// Initializes a new instance of the <see cref="Naovigate.MapOverviewEvent"/> class. Unpacks received data.
        /// </summary>
        public MapOverviewEvent()
        {
            this.Unpack();
        }

        /// <summary>
        /// Unpack event parameters from stream.
        /// </summary>
        private void Unpack()
        {
            int width = this.stream.ReadByte();
            int height = this.stream.ReadByte();

            List<Map.FieldType> mapOverview = new List<Map.FieldType>(width * height);
            for (int i = 0; i < width * height; i++) {
                byte value = this.stream.ReadByte();
                if (!Enum.IsDefined(typeof(Map.FieldType), value)) {
                    return;
                }

                mapOverview[i] = (Map.FieldType)Enum.ToObject(typeof(Map.FieldType), value);
            }
        }

        /// <summary>
        /// Fire this event.
        /// </summary>
        public override void Fire()
        {

        }

        /// <summary>
        /// Abort this event.
        /// </summary>
        public override void Abort()
        {

        }
    }
}

