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
    /// Lógica de interacción para ucTextBoxeMail.xaml
    /// </summary>
    public partial class ucTextBoxeMail : UserControl
    {

        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;



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

        public ucTextBoxeMail()
        {
            InitializeComponent();
            DataContext = new DataBoundObject();            
        }

        public string Text
        {
            get
            {
                if (LBSFramework.Helppers.Validaciones.validaEmail(txtText.Text))
                    return (txtText.Text);
                else
                    return null;
            }

            set
            {
                txtText.Text = value.ToString();
            }
        }

        //Retorna un valor Booleano indicando la coherencia del dato ingresado y el tipo Entero
        public bool esValido()
        {
            if (LBSFramework.Helppers.Validaciones.validaEmail(txtText.Text))
                return true;
            else
                return false;
        }



        private void txtText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
              {
                   if (ucAprietaEnter != null)
                       ucAprietaEnter(sender, e);
               }            
        }//Fin KeyPress

        private void txtText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ucTextChange != null)
                ucTextChange(sender, e);
        }
    }
}
