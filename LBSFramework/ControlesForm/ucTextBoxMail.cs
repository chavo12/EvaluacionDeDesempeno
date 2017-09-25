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
    public partial class ucTextBoxMail : UserControl
    {
        #region Propiedades
        /********************************************************/

        private bool _valorRequerido;

        public bool ValorRequerido
        {
            get { return _valorRequerido; }
            set { _valorRequerido = value; }
        }

        public string Texto
        {
            get { return txtMail.Text; }
            set { txtMail.Text = value; }
        }

        /********************************************************/
        #endregion Fin Propiedades

        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;


        public ucTextBoxMail()
        {
            InitializeComponent();
            errorProvider1.ContainerControl = this;
            errorProvider1.SetIconPadding(this.txtMail, -20);
        }

        private void txtMail_Validating(object sender, CancelEventArgs e)
        {
            if(ValorRequerido && txtMail.Text=="")
                errorProvider1.SetError(txtMail, "El E-Mail es Obligatorio");
            else
                if (!Helppers.Validaciones.validaEmail(txtMail.Text))
                {
                    errorProvider1.SetError(txtMail, "El valor ingresado debe ser una direccion de E-Mail valida");
                }
                else
                    errorProvider1.SetError(txtMail, "");
        }

        private void txtMail_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtMail_TextChanged(object sender, EventArgs e)
        {
            if (ucTextChange != null)
            {
                ucTextChange(sender, e);
            }
        }
    }
}
