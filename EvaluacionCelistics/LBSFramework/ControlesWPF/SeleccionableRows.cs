using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Data;
using System.Windows.Media.Imaging;

namespace LBSFramework.ControlesWPF
{
    class SeleccionableRows : IValueConverter
    {
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LBSFramework.Entitys.Entity)
            {
                LBSFramework.Entitys.Entity row = value as LBSFramework.Entitys.Entity;

                if (row != null)
                {
                    System.Windows.Visibility source;

                    if (row.Seleccionable)
                    {
                        source = System.Windows.Visibility.Visible;
                    }

                        
                    else source = System.Windows.Visibility.Hidden;

                    return source;
                }
            }
            return null;
        }

        //public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        //{
        //    if (value is LBSFramework.Entitys.Entity)
        //    {
        //        LBSFramework.Entitys.Entity row = value as LBSFramework.Entitys.Entity;

        //        if (row != null)
        //        {
        //            if (row.Seleccionable)
        //            {
        //                BitmapImage source;

        //                if (row.Seleccionar)
        //                {
        //                    Uri uri = new Uri("pack://siteoforigin:,,,/Resources/SeleccionEnable.png");
        //                    source = new BitmapImage(uri);
        //                }
        //                else
        //                {
        //                    Uri uri = new Uri("pack://siteoforigin:,,,/Resources/SeleccionDisable.png");
        //                    source = new BitmapImage(uri);
        //                }

        //                return source;
        //            }
        //            if (!row.Seleccionable)
        //            {
        //                //Uri uri = new Uri("pack://application:,,,/Images/red.jpg");
        //                Uri uri = new Uri("pack://siteoforigin:,,,/Resources/NoSeleccionable.png");
        //                BitmapImage source = new BitmapImage(uri);
        //                return source;
        //            }
        //        }
        //    }
        //    return null;
        //}

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

    }

    

}
