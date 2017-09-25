using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LBSFramework.ControlesForm
{
    public partial class ucLabel : UserControl
    {

        public bool VisibleLabel
        {
            get { return lblLabel.Visible; }
            set { lblLabel.Visible = value; }
        }

        public string Texto
        {
            get { return lblLabel.Text; }
            set { lblLabel.Text = value; }
        }

        public ucLabel()
        {
            InitializeComponent();
        }
        

    }
}
