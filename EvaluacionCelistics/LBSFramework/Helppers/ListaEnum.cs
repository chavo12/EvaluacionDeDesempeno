using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LBSFramework.Entitys;

namespace LBSFramework.Helppers
{
   public class ListaEnum
    {

       public static List<Items> ListaSexo()
       {
           Items m = new Items("M", "Masculino");
           Items f = new Items("F", "Femenino");
           List<Items> list = new List<Items>();
           list.Add(m);
           list.Add(f);
           return list;
       }
    }
}
