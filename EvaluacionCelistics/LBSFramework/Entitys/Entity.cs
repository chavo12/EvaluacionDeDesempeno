using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBSFramework.Entitys
{
    public abstract class Entity
    {

        #region Atributos y Propiedades
        /********************************************************/

        private PK _Identificador;


        public PK Identificador
        {
            get { return _Identificador; }
            set { _Identificador = value; }
        }

        private bool _borrable;
        private bool _editable;
        private bool _seleccionable;
        private bool _imprimible;
        private bool _Check;
        //private bool _Seleccionar;
        private bool _imprimir;

               
        public bool Imprimir
        {
            get { return _imprimir; }
            set { _imprimir = value; }
        }

        public bool Check
        {
            get { return _Check; }
            set { _Check = value; }
        }

        //public bool Seleccionar
        //{
        //    get { return _Seleccionar; }
        //    set { _Seleccionar = value; }
        //}

        public bool Imprimible
        {
            get { return _imprimible; }
            set { _imprimible = value; }
        }

        public bool Seleccionable
        {
            get { return _seleccionable; }
            set { _seleccionable = value; }
        }
        public bool Editable
        {
            get { return _editable; }
            set { _editable = value; }
        }

        public bool Borrable
        {
            get { return _borrable; }
            set { _borrable = value; }
        }

        /********************************************************/
        #endregion Fin Atributos y Propiedades


        public Entity()
        {
            //Creamos la lista de PKs
            _Identificador = new PK();

        }

    }
}
