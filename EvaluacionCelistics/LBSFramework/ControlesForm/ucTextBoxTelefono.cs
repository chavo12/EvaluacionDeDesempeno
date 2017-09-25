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
    public partial class ucTextBoxTelefono : UserControl
    {

        #region Propiedades
        /********************************************************/

        private bool _valorRequerido;

        public bool ValorRequerido
        {
            get { return _valorRequerido; }
            set { _valorRequerido = value; }
        }

        public string Valor
        {
            get { return txtTelefono.Text; }
            set {
                if (!Helppers.Validaciones.validaTelefono(value))
                    {
                        errorProvider1.SetError(txtTelefono, "El valor ingresado debe ser un Telefono valido");
                    }
                    else
                        errorProvider1.SetError(txtTelefono, "");

                txtTelefono.Text = value; }
        }

        /********************************************************/
        #endregion Fin Propiedades

        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;


        public ucTextBoxTelefono()
        {
            InitializeComponent();
            errorProvider1.ContainerControl = this;
            errorProvider1.SetIconPadding(this.txtTelefono, -20);
        }

        private void txtTelefono_Validating(object sender, CancelEventArgs e)
        {
            if(ValorRequerido && txtTelefono.Text=="")
                errorProvider1.SetError(txtTelefono, "El Telefono es Obligatorio");
            else
                if (!Helppers.Validaciones.validaTelefono(txtTelefono.Text))
                {
                    errorProvider1.SetError(txtTelefono, "El valor ingresado debe ser un Telefono valido");
                }
                else
                    errorProvider1.SetError(txtTelefono, "");
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            if (ucTextChange != null)
            {
                ucTextChange(sender, e);
            }
        }
    }
}
