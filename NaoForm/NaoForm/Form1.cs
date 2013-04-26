using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aldebaran.Proxies;
using System.Threading;
using Aldebaran.LanguageCompatibility;
using System.Collections;

namespace NaoForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button1.Click += new System.EventHandler(this.button1_Click);
        }
    }
}
