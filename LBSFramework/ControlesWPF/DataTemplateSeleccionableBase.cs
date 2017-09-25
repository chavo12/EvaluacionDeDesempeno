using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;


namespace LBSFramework.ControlesWPF
{
    class DataTemplateSeleccionableBase : DataTemplateSelector
    {
        protected UserControl GetMainWindow(DependencyObject inContainer)
        {
            DependencyObject c = inContainer;
            while (true)
            {
                DependencyObject p = VisualTreeHelper.GetParent(c);

                if (c is UserControl)
                {
                    return c as UserControl;
                }
                else
                {
                    c = p;
                }
            }
        }

        public override DataTemplate SelectTemplate(object inItem, DependencyObject inContainer)
        {
            LBSFramework.Entitys.Entity row = inItem as LBSFramework.Entitys.Entity;

            if (row != null)
            {
                UserControl w = GetMainWindow(inContainer);
                return (DataTemplate)w.FindResource("btnCheck");

            }
            return null;
        }
    }
}


