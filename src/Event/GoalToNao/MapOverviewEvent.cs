using System;
using System.Collections.Generic;
using Naovigate.Movement;
using Naovigate.Navigation;
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

            List<Map.Tile> mapOverview = new List<Map.Tile>(width * height);
            for (int i = 0; i < width * height; i++) {
                byte value = this.stream.ReadByte();
                if (!Enum.IsDefined(typeof(Map.Tile), value)) {
                    return;
                }

                mapOverview[i] = (Map.Tile)Enum.ToObject(typeof(Map.Tile), value);
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

