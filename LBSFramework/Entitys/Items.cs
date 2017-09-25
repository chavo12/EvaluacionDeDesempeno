using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBSFramework.Entitys
{
    public class Items
    {
        private object _id;

        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        public object Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public Items(object id, string descrip)
        {
            _id = id;
            _descripcion = descrip;
        }

        public Items()
        { }
    }
}
