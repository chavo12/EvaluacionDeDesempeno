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
    /// Lógica de interacción para ucTextBoxNumeroEntero.xaml
    /// </summary>
    public partial class ucTextBoxNumeroLong : UserControl
    {
 
        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;

        public string WaterText { get { return _WaterText.Text; } set { _WaterText.Text = value; } }

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


        public ucTextBoxNumeroLong()
        {
            InitializeComponent();
            DataContext = new DataBoundObject();            
        }


        
        public long? Text
        {
            get {
                
                if (LBSFramework.Helppers.Validaciones.validaNumero(txtText.Text) && !string.IsNullOrWhiteSpace(txtText.Text))
                    return (long.Parse(txtText.Text));
                else
                    return null;
                }
            
            set {
                if (LBSFramework.Helppers.Validaciones.validaNumero(txtText.Text))
                    {
                        txtText.Text = value.ToString();
                        lblTexto.Content = txtText.Text;
                    }
                }
        }

        //Retorna un valor Booleano indicando la coherencia del dato ingresado y el tipo Entero
        public bool esValido()
        {
            if (LBSFramework.Helppers.Validaciones.validaNumero(txtText.Text))
                return true;
            else
                return false;            
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

                if (e.Key.ToString().Length == 1 && char.IsLetter(e.Key.ToString()[0]))
                    e.Handled = true;
                                

                if (e.Key.ToString().Length == 1 && !char.IsDigit(e.Key.ToString()[0]))
                    e.Handled = true;                

            }//Fin tecla Back
        }//Fin KeyPress

        private void txtText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ucTextChange != null)
                ucTextChange(sender,e);
        }

        public void foco()
        {
            txtText.Focusable = true;
            txtText.Focus();
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
