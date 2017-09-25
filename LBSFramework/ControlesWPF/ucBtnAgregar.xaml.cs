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
    /// Lógica de interacción para ucBtnAceptar.xaml
    /// </summary>
    public partial class ucBtnAgregar : UserControl
    {
        public ucBtnAgregar()
        {
            InitializeComponent();
        }

        public Button oButton
        {

            get { return Boton; }

            set { Boton = value; }

        }

        public event RoutedEventHandler ButtonClick
        {
            add { Boton.Click += value; }
            remove { Boton.Click -= value; }
        }
        public System.Windows.Media.ImageSource ImageSource
        {
            get { return imageB.Source; }
            set { imageB.Source = value; }
        }
        public string TextButton
        {
            get { return textB.Text; }
            set { textB.Text = value; }
        }
        public bool IsDefault
        {
            get { return Boton.IsDefault; }
            set { Boton.IsDefault = value; }
        }
        public bool IsCancel
        {
            get { return Boton.IsCancel; }
            set { Boton.IsCancel = value; }
        }

        public void foco()
        {
            Boton.Focusable = true;
            Boton.Focus();
        }
    }
}
