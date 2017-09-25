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
    class BorrableRows : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is LBSFramework.Entitys.Entity)
            {
                LBSFramework.Entitys.Entity row = value as LBSFramework.Entitys.Entity;

                if (row != null)
                {
                    if (row.Borrable)
                    {
                        Uri uri = new Uri("pack://siteoforigin:,,,/Resources/Borrar16.png");
                        //Uri uri = new Uri("pack://application:,,,/Images/green.jpg");
                        BitmapImage source = new BitmapImage(uri);
                        return source;
                    }
                    if (!row.Borrable)
                    {
                        //Uri uri = new Uri("pack://application:,,,/Images/red.jpg");
                        Uri uri = new Uri("pack://siteoforigin:,,,/Resources/Borrar16Disable.png");
                        BitmapImage source = new BitmapImage(uri);
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
