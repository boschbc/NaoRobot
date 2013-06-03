﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Naovigate.GUI.Util;

namespace Naovigate.GUI.Popups.ParamChooser
{
    public partial class LocationsChooser : UserControl, IParamChooser
    {
        private List<PointControl> points;

        public LocationsChooser()
        {
            InitializeComponent();
        }

        public void AddPoint()
        {
            PointControl p = new PointControl();
            flowLayoutPanel.Controls.Add(p);
            if (points == null)
                points = new List<PointControl>();
            points.Add(p);
        }

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