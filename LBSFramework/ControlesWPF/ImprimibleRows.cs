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
    class ImprimibleRows : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {           
            if (value is LBSFramework.Entitys.Entity)
            {
                LBSFramework.Entitys.Entity row = value as LBSFramework.Entitys.Entity;

                if (row != null)
                {
                    BitmapImage source;

                    if (row.Imprimible)
                    {
                        //if (row.Imprimir)
                        //{
                        Uri uri = new Uri("pack://siteoforigin:,,,/Resources/ImprimibleEnable.png");
                        source = new BitmapImage(uri);
                        return source;

                        //}
                        //else
                        //{
                        //    Uri uri = new Uri("pack://siteoforigin:,,,/Resources/ImprimibleDisable.png");
                        //    source = new BitmapImage(uri);
                        //    return source;

                        //}
                    }
                    else
                    {
                        Uri uri = new Uri("pack://siteoforigin:,,,/Resources/NoImprimible.png");
                        source = new BitmapImage(uri);

                        return source;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new System.NotImplementedException();
        }

    }
}
