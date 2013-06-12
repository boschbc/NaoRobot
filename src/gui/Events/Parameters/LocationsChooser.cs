using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Naovigate.GUI.Util;

namespace Naovigate.GUI.Events.Parameters
{
    /// <summary>
    /// A simple class that allows the user to choose an array of points (x, y). A maximum of 5 points may be chosen.
    /// </summary>
    public sealed partial class LocationsChooser : UserControl, IParamChooser
    {
        private static readonly int MAX_POINTS = 5;

        private List<PointControl> points;

        public LocationsChooser()
        {
            InitializeComponent();
            points = new List<PointControl>();
        }

        /// <summary>
        /// Adds a new Point-field to the control, if there are less than 5 present.
        /// </summary>
        public void AddPoint()
        {
            if (points.Count >= MAX_POINTS)
                return;
            PointControl p = new PointControl();
            flowLayoutPanel.Controls.Add(p);
            points.Add(p);
        }

        /// <summary>
        /// A list of points representing the user's choice.
        /// </summary>
        public List<Point> Locations
        {
            get 
            {
                List<Point> locations = new List<Point>();
                foreach (PointControl p in points)
                    locations.Add(new Point(p.X, p.Y));
                return locations;
            }
        }

        /// <summary>
        /// This chooser's value.
        /// </summary>
        public Object Value
        {
            get { return Locations; }
        }

        private void addPointButton_Click(object sender, EventArgs e)
        {
            AddPoint();
        }
    }
}
