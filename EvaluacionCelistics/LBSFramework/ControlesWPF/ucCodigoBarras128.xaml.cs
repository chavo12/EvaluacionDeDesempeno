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
using GenCode128;
using System.IO;
using System.Drawing.Imaging;

namespace LBSFramework.ControlesWPF
{
    /// <summary>
    /// Lógica de interacción para ucCodigoBarras128.xaml
    /// </summary>
    public partial class ucCodigoBarras128 : UserControl
    {
        public ucCodigoBarras128()
        {
            InitializeComponent();
        }


        public TextBlock oTextBlock
        {

            get { return txtCodigo; }

            set { txtCodigo = value; }

        }
        

        public void GenerarCodigoBarras(string sTexto, int iTamanio)
        {
            try
            {
                var image = (System.Drawing.Image)Code128Rendering.MakeBarcodeImage(sTexto, iTamanio, true);
                // Winforms Image we want to get the WPF Image from...
                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();
                MemoryStream memoryStream = new MemoryStream();
                // Save to a memory stream...
                image.Save(memoryStream, ImageFormat.Bmp);
                // Rewind the stream...
                memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                bitmap.StreamSource = memoryStream;
                bitmap.EndInit();

                ImgCodigo.Source = bitmap;
                txtCodigo.Text = sTexto;
            }
            catch 
            {                
            }
        }
    }
}
