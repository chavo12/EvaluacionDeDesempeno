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

using Xceed.Wpf.Toolkit;

namespace LBSFramework.ControlesWPF
{
    /// <summary>
    /// Lógica de interacción para ucTextBoxNumeroDecimal.xaml
    /// </summary>
    public partial class ucTextBoxNumeroDecimal : UserControl
    {
        public ucTextBoxNumeroDecimal()
        {
            InitializeComponent();
            DataContext = new DataBoundObject();
        }

        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;

        public decimal? minValue { get; set; }

        public decimal? maxValue { get; set; }

        public WatermarkTextBox oTextbox
        {
            get
            {
                return (txtText);
            }

            set
            {
                txtText = value;
            }
        }

        public string WaterText { get { return _WaterText.Text; } set { _WaterText.Text = value; } }

        public decimal? Text
        {
            get
            {
                decimal num;
                if (decimal.TryParse(txtText.Text, out num))
                    return num;
                else
                    return null;
            }

            set
            {
                if (esValido(value.ToString()))
                {
                    txtText.Text = string.Format("{0:F}", value);
                    lblTexto.Content = txtText.Text;
                }
            }
        }

        //Retorna un valor Booleano indicando la coherencia del dato ingresado y el tipo Entero
        public bool esValido(string valor)
        {
            bool valid;
            decimal res;
            string val = "OK";
            if (LBSFramework.Helppers.Validaciones.validaNumeroDecimal(valor))
            {
                res = decimal.Parse(valor);

                if (minValue.HasValue && res < minValue.Value)
                    val = "El valor mínimo permitido es: " + minValue.Value.ToString();

                if (maxValue.HasValue && res > maxValue.Value)
                    val = "El valor máximo permitido es: " + maxValue.Value.ToString();

                if (val == "OK")
                    valid = true;
                else
                {
                    valid = false;
                    System.Windows.MessageBox.Show(val, "Valor fuera de rango", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    foco();
                }

            }
            else
            {
                valid = false;
                //System.Windows.MessageBox.Show("El valor no es decimal", "tipo no válido", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return valid;

        }



        private void txtText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Back)
            {
                if (e.Key == Key.Enter)
                {
                    if (ucAprietaEnter != null)
                        ucAprietaEnter(sender, e);
                }

                if (e.Key == Key.Decimal)
                {
                    // si se pulsa en estos caracteres, se convertirá en /
                    e.Handled = true;
                    txtText.Text = txtText.Text.Insert(txtText.Text.Length, ",");
                    txtText.Select(txtText.Text.Length, 0);
                }


                if (e.Key.ToString().Length == 1 && char.IsLetter(e.Key.ToString()[0]))
                    e.Handled = true;


                if (e.Key.ToString().Length == 1 && !char.IsDigit(e.Key.ToString()[0]))
                    e.Handled = true;

            }//Fin tecla Back
        }//Fin KeyPress



        private void txtText_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (ucTextChange != null)
                ucTextChange(sender, e);
            
        }

        public bool SoloLectura
        {
            get { return txtText.IsReadOnly; }
            set { txtText.IsReadOnly = value; }
        }

        public void foco()
        {
            txtText.Focusable = true;
            txtText.Focus();
            txtText.SelectAll();
        }

        private void txtText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key!= Key.Enter)
                if (!esValido(txtText.Text))
                    txtText.Text = "";
        }

        private void ucText_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (!(bool)e.NewValue)
                {
                    pnLabel.Visibility = System.Windows.Visibility.Visible;
                    txtText.Visibility = System.Windows.Visibility.Collapsed;
                    pnimg.Visibility = System.Windows.Visibility.Collapsed;
                    lblTexto.Content = txtText.Text;
                }
                else
                {
                    pnLabel.Visibility = System.Windows.Visibility.Collapsed;
                    txtText.Visibility = System.Windows.Visibility.Visible;
                    pnimg.Visibility = System.Windows.Visibility.Visible;
                }

            }
            catch (Exception)
            {
                pnLabel.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

   

    }
}
