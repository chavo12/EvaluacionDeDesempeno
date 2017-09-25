using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LBSFramework.ControlesWPF
{
    /// <summary>
    /// Lógica de interacción para ucTextBoxFecha.xaml
    /// </summary>
    public partial class ucTextBoxFecha : UserControl
    {

        public TextBox oTextbox
        {
            get
            {
                return (txtFecha);
            }

            set
            {
                txtFecha = value;
            }
        }

        public DatePicker oDatePicker
        {
            get
            {
                return (dtpFecha);
            }

            set
            {
                dtpFecha = value;
            }
        }
        


        public ucTextBoxFecha()
        {
            InitializeComponent();
            DataContext = new DataBoundObject();
        }

        private void dtpFecha_ValueChanged(object sender, System.EventArgs e)
        {
            if (dtpFecha.SelectedDate == null)
                txtFecha.Text = "";
            else
                txtFecha.Text = dtpFecha.SelectedDate.Value.ToShortDateString();

            if (ucTextChange != null)
            {
                ucTextChange(sender, e);
            }
        }


        #region Propiedades
        /********************************************************/

        private bool _valorRequerido;

        public DateTime? Valor
        {
            get
            {
                //if (Helppers.Validaciones.validaFecha(txtFecha.Text) && txtFecha.Text != "")
                if (dtpFecha.SelectedDate.HasValue && Helppers.Validaciones.validaFecha(dtpFecha.SelectedDate.Value.ToShortDateString()))
                    return (DateTime.Parse(dtpFecha.SelectedDate.Value.ToShortDateString()));
                else
                    return null;
            }
            set
            {
                if (value != null)
                {
                    DateTime fecha = value.Value;
                    if (Helppers.Validaciones.validaFecha(fecha.ToShortDateString()))
                        dtpFecha.SelectedDate = fecha;
                    txtFecha.Text = dtpFecha.SelectedDate.Value.ToShortDateString();
                    lblTexto.Content = txtFecha.Text;
                }
                else
                {
                    dtpFecha.SelectedDate=null;                    
                }
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


        private void txtFecha_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Back)
            {
                if (e.Key.ToString().Length == 1 && char.IsLetter(e.Key.ToString()[0]))
                    e.Handled = true;

                if (e.Key == Key.Space)
                    e.Handled = true;

                //Segun la Tecla que presione cambio por otros caracteres
                switch (e.Key)
                {
                    //Si aprieta Enter se dispara el Metodo ucAprietaEnter
                    case Key.Enter:
                        {
                            e.Handled = true;

                            if (ucAprietaEnter != null)
                                ucAprietaEnter(sender, e);

                            break;
                        }
                    case Key.Decimal:
                    case Key.OemComma:
                    case Key.Subtract:
                        // si se pulsa en estos caracteres, se convertirá en /
                        e.Handled = true;
                        txtFecha.Text = txtFecha.Text.Insert(txtFecha.Text.Length, "/");
                        txtFecha.Select(txtFecha.Text.Length, 0);


                        break;
                }//Fin Switch
            }//Fin tecla Back
        }//Fin KeyPress


        private void txtFecha_TextChanged(object sender, EventArgs e)
        {
            if (ucTextChange != null)
            {
                ucTextChange(sender, e);
            }
        }

        public void foco()
        {
            txtFecha.Focusable = true;
            txtFecha.Focus();
        }

        private void txtFecha_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Back)
            {
                if (e.Key.ToString().Length == 1 && char.IsLetter(e.Key.ToString()[0]))
                    e.Handled = true;

                if (e.Key == Key.Space)
                    e.Handled = true;

                //Segun la Tecla que presione cambio por otros caracteres
                switch (e.Key)
                {
                    //Si aprieta Enter se dispara el Metodo ucAprietaEnter
                    case Key.Enter:
                        {
                            e.Handled = true;

                            if (ucAprietaEnter != null)
                                ucAprietaEnter(sender, e);

                            break;
                        }
                    case Key.Decimal:
                    case Key.OemComma:
                    case Key.Subtract:
                        // si se pulsa en estos caracteres, se convertirá en /
                        e.Handled = true;
                        txtFecha.Text = txtFecha.Text.Insert(txtFecha.Text.Length, "/");
                        txtFecha.Select(txtFecha.Text.Length, 0);


                        break;
                }//Fin Switch
            }//Fin tecla Back
        }

        private void dtpFecha_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void UserControl_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
              try
            {
                if (!(bool)e.NewValue)
                {
                    pnLabel.Visibility = System.Windows.Visibility.Visible;
                    txtFecha.Visibility = System.Windows.Visibility.Collapsed;
                    dtpFecha.Visibility = System.Windows.Visibility.Collapsed;
                    lblTexto.Content = txtFecha.Text;
                }
                else
                {
                    pnLabel.Visibility = System.Windows.Visibility.Collapsed;
                    txtFecha.Visibility = System.Windows.Visibility.Visible;
                    dtpFecha.Visibility = System.Windows.Visibility.Visible;
                }

            }
            catch (Exception)
            {
                pnLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

      

    }
}
