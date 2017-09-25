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
    public partial class ucTextBoxFecha : UserControl
    {
        #region Propiedades
        /********************************************************/

        private bool _valorRequerido;

        public DateTime? Valor
        {
            get {
                if (Helppers.Validaciones.validaFecha(txtFecha.Text) & txtFecha.Text!="")
                    return (DateTime.Parse(txtFecha.Text));
                else
                    return null;
                }
            set {
                if (Helppers.Validaciones.validaFecha(value.ToString()))                
                    txtFecha.Text = value.ToString(); 
                }
        }

        /// <summary>
        /// Indica si se trata de un campo Obligatorio
        /// </summary>
        public bool ValorRequerido
        {
            get { return _valorRequerido; }
            set { _valorRequerido = value; }
        }

        /// <summary>
        /// Retorna Verdadero o Falso si la Fecha ingresada es Valida.
        /// </summary>
        public bool FechaValida
        {
            get { return Helppers.Validaciones.validaFecha(txtFecha.Text); }
        }


        /********************************************************/
        #endregion Fin Propiedades

        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;

        public ucTextBoxFecha()
        {
            InitializeComponent();
            errorProvider1.ContainerControl = this;
            errorProvider1.SetIconPadding(this.txtFecha, -20);
        }

        private void txtFecha_Validating(object sender, CancelEventArgs e)
        {
            if (ValorRequerido && txtFecha.Text == "")
                errorProvider1.SetError(txtFecha, "El valor ingresado debe tener Formato de Fecha y es Obligatorio.");
            else
            {
                if (!FechaValida && txtFecha.Text != "")
                {
                    errorProvider1.SetError(txtFecha, "El valor ingresado debe tener Formato de Fecha.");
                }
                else
                    errorProvider1.SetError(txtFecha, "");
            }
        }

        private void txtFecha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back)
            {
                //Segun la Tecla que presione cambio por otros caracteres
                switch (e.KeyChar)
                {
                    //Si aprieta Enter se dispara el Metodo ucAprietaEnter
                    case '\r':
                        {
                            e.Handled = true;
                            
                            if (ucAprietaEnter != null)
                                ucAprietaEnter(sender, e);

                            break;
                        }
                    case '.':
                    case ',':
                    case '-':
                        // si se pulsa en estos caracteres, se convertirá en /
                        e.Handled = true;
                        SendKeys.Send("/");
                        break;
                }//Fin Switch
            }//Fin tecla Back
        }//Fin KeyPress

        private void dtpFecha_ValueChanged(object sender, EventArgs e)
        {
            txtFecha.Text = dtpFecha.Value.ToShortDateString();
        }

        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
            if (ucTextChange != null)
            {
                ucTextChange(sender, e);
            }
        }
    }
}
