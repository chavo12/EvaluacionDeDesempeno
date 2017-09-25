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
    /// Lógica de interacción para ucTextBoxMarcaAugua.xaml
    /// </summary>
    public partial class ucTextBoxLibre : UserControl
    {
        
        public ucTextBoxLibre()
        {
            InitializeComponent();
            DataContext = new DataBoundObject();               
        }

        public event EventHandler ucTextChange;
        public event EventHandler ucAprietaEnter;

        public  WatermarkTextBox oTextbox
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

        private bool _mulitline;

        public bool Mulitline
        {
            get { 
                    return _mulitline; 
                }
            set { 
                    _mulitline = value;
                    if (value)
                    {
                        txtText.TextWrapping = TextWrapping.Wrap;
                        txtText.AcceptsReturn = true;
                    }
                    else {
                        txtText.TextWrapping = TextWrapping.NoWrap;
                        txtText.AcceptsReturn = false;
                    }

                }
        }

        
        public string WaterText { get { return _WaterText.Text; } set { _WaterText.Text = value; } }


        public string Text
        {
            get
            {
                    return (txtText.Text);                
            }

            set
            {
                if (string.IsNullOrEmpty(value)) txtText.Text = "";
                else
                {

                    txtText.Text = value.ToString();
                    lblTexto.Content = txtText.Text;
                }
            }
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

        public void foco()
        {
            if (txtText != null)
            {
                    txtText.Focusable = true;
                    txtText.Focus();
                    txtText.SelectAll();
            }
        }

        private void ucTextBox_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
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
