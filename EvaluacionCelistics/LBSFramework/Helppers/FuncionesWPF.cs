using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

namespace LBSFramework.Helppers
{
   public class FuncionesWPF
    {
       public static void AbrirVentana( DockingManager dcmMain, LayoutDocumentPane panel, string titulo, string contenId, object content,LayoutDocument vent, double alto = 0, double ancho = 0)
       {

           LayoutDocument ventExist = null;
           try
           {
                ventExist = (from a in dcmMain.Layout.Descendents().OfType<LayoutDocument>() where a.ContentId == contenId select a).Single();
           }
           catch
           { }

           dcmMain.Visibility = System.Windows.Visibility.Visible;
           if (ventExist != null)
           {
               ((LayoutDocument)ventExist).IsActive = true;
           }
           else
           {
               LayoutDocumentPane panelVent = null;
               var panelVentList = dcmMain.Layout.Descendents().OfType<LayoutDocumentPane>().ToList();
               if (panelVentList != null && panelVentList.Count > 0)
               {
                   panelVent = panelVentList[0];
               }
               vent.Title = titulo;
               vent.ContentId = contenId;
               vent.Content = content;
               if (alto > 0) vent.FloatingHeight = alto;
               if (ancho > 0) vent.FloatingWidth = ancho;
               if (panelVent != null)
               {
                   panelVent.Children.Add(vent);
               }
               else panel.Children.Add(vent);
               vent.IsActive = true;
           }
       }

       

       public static void CerrarVentana(object sender, EventArgs e)
       {
           LBSFramework.Entitys.Items i = (LBSFramework.Entitys.Items)sender;

           List<LayoutContent> List;

           List = ((LayoutDocumentPane)i.Id).Children.Where(d => d.ContentId == i.Descripcion).ToList();

           if (List != null && List.Count > 0)
           {
               List[0].Close();
           }
           
       }
    }
}
