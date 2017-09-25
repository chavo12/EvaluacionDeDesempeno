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
    public partial class ucBtnAceptar : UserControl
    {
        public ucBtnAceptar()
        {
            InitializeComponent();
        }

        public event EventHandler ucClick;

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ucClick != null)
            {                
                ucClick(sender, e);
            }
        }



    }
}
