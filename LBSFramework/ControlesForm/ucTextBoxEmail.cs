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
    public partial class ucTextBoxEmail : UserControl
    {

        #region Propiedades
        /********************************************************/

        public string Texto
        {
            get { return txtNumerico.Text; }
            set { txtNumerico.Text = value; }
        }

        /********************************************************/
        #endregion Fin Propiedades

        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;


        public ucTextBoxEmail()
        {
            InitializeComponent();
            errorProvider1.ContainerControl = this;
            errorProvider1.SetIconPadding(this.txtNumerico, -20);
        }

        private void txtNumerico_Validating(object sender, CancelEventArgs e)
        {
            if (!Helppers.Validaciones.validaNumeroDecimal(txtNumerico.Text))
            {
                errorProvider1.SetError(txtNumerico, "El valor ingresado debe ser Numerico");
            }
            else
                errorProvider1.SetError(txtNumerico, "");
        }

        private void txtNumerico_KeyPress(object sender, KeyPressEventArgs e)
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

                if (e.KeyChar == '.')
                {
                    // si se pulsa en el punto se convertirá en coma
                    e.Handled = true;
                    SendKeys.Send(",");
                }

                //Valido que si es el primer caracter pueda ser el signo menos
                if (txtNumerico.Text.Length != 0 && e.KeyChar == '-' )
                {
                    //if( !Helppers.Validaciones.validaNumeroDecimal(e.KeyChar.ToString()))
                        e.Handled = true;
                }

                if (!Helppers.Validaciones.validaNumeroDecimal(e.KeyChar.ToString()))
                    e.Handled = true;
            }
        }

        private void txtNumerico_TextChanged(object sender, EventArgs e)
        {
            if (ucTextChange != null)
            {
                ucTextChange(sender, e);
            }
        }
    }
}
