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
    public partial class ucTextBoxTextoLibre : UserControl
    {

         #region Propiedades
        /********************************************************/

        private bool _valorRequerido;
        private bool _multilinea;

        public bool Multilinea
        {
            get { return _multilinea; }
            set { _multilinea = value; }
        }

        public bool ValorRequerido
        {
            get { return _valorRequerido; }
            set { _valorRequerido = value; }
        }

        public string Valor
        {
            get { return txtTexto.Text; }
            set { txtTexto.Text = value; }
        }

        /********************************************************/
        #endregion Fin Propiedades

        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;


        public ucTextBoxTextoLibre()
        {
            InitializeComponent();
            errorProvider1.ContainerControl = this;
            errorProvider1.SetIconPadding(this.txtTexto, -20);
            txtTexto.Multiline = _multilinea;
        }

        private void txtTexto_Validating(object sender, CancelEventArgs e)
        {
            if(ValorRequerido && txtTexto.Text=="")
                errorProvider1.SetError(txtTexto, "El Campo es Obligatorio");
        }

        private void txtTexto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                //Si aprieta Enter se dispara el Metodo ucAprietaEnter
                if (e.KeyChar == '\r')
                {
                    e.Handled = true;
                    if (ucAprietaEnter != null)
                        ucAprietaEnter(sender, e);
                }
            }
        }

        private void txtTexto_TextChanged(object sender, EventArgs e)
        {
            if (ucTextChange != null)
            {
                ucTextChange(sender, e);
            }
        }
    }
}
