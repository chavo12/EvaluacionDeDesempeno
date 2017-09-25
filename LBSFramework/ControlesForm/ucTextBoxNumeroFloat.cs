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
    public partial class ucTextBoxNumeroFloat : UserControl
    {
        
        #region Propiedades
        /********************************************************/

        private bool _valorRequerido;

        public bool ValorRequerido
        {
            get { return _valorRequerido; }
            set { _valorRequerido = value; }
        }

        public float Valor
        {
            get {
                if (txtNumero.Text != string.Empty && Helppers.Validaciones.validaNumeroDecimal(txtNumero.Text))
                    return float.Parse(txtNumero.Text);
                else
                    return 0;                    
                }
            set {
                if (value != null && Helppers.Validaciones.validaNumeroDecimal(value.ToString())) 
                    txtNumero.Text = value.ToString();                 
                }
        }

        /********************************************************/
        #endregion Fin Propiedades


        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;


        public ucTextBoxNumeroFloat()
        {
            InitializeComponent();
            errorProvider1.ContainerControl = this;
            errorProvider1.SetIconPadding(this.txtNumero, -20);
        }

        private void txtNumero_Validating(object sender, CancelEventArgs e)
        {
            if (ValorRequerido && txtNumero.Text == "")
            {
                errorProvider1.SetError(txtNumero, "Valor Obligatorio");
            }
            else
            {
                if (!Helppers.Validaciones.validaNumeroDecimal(txtNumero.Text))
                {
                    errorProvider1.SetError(txtNumero, "El valor ingresado debe ser un Numero");
                }
                else
                    errorProvider1.SetError(txtNumero, "");
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
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
                if (txtNumero.Text.Length != 0 && e.KeyChar == '-')
                {
                    e.Handled = true;
                }
                //Si no verifico que el carater sea el signo '-' no se podrian poner numeros negativos, porque el primer caracter sin mas numeros no es un entero y no valida
                if (e.KeyChar != '-' && !Helppers.Validaciones.validaNumeroDecimal(txtNumero.Text + e.KeyChar.ToString()))
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
