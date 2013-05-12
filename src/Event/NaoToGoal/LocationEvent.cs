using System;
using System.Collections.Generic;
using System.Drawing;

using Naovigate.Util;

namespace Naovigate.Event.NaoToGoal
{
    /*
     * Sends the position the Nao is at to Goal.
     */
    public class LocationEvent : NaoToGoalEvent
    {
        private PointF location;

        /*
         * Explicit constructor.
         */
        public LocationEvent(PointF location)
        {
            this.location = location;
        }

        /*
         * Explicit constructor.
         */
        public LocationEvent(float x, float y)
        {
            location = new PointF(x, y);
        }

        /*
         * Implicit constructor.
         */
        public LocationEvent()
        {
            location = NaoState.Location;
        }

        /*
         * See the INaoEvent class docs for documentation of this method.
         */
        public override void Fire()
        {
            //Send over the network to goal
        }
    }
}
