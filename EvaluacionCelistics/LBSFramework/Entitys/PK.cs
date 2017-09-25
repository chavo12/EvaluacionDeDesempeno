using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace LBSFramework.Entitys
{
    #region Clase ObjetoPK
    /********************************************************/
		
    class objetoPK
    {
        private string _Nombre;
        private object _Valor;

        public object Valor
        {
            get { return _Valor; }
            set { _Valor = value; }
        }

        public string Nombre
        {
            get { return _Nombre; }
            set { _Nombre = value; }
        }

        public objetoPK(string oNombre, object oValor)
        {
            _Nombre = oNombre;
            _Valor = oValor;
        }
    }



    /********************************************************/
    #endregion Fin Clase ObjetoPK
	
    public class PK
    {

        #region Atributos y propiedades
        /********************************************************/

        private List<objetoPK> _Identificador;
        
               
        /********************************************************/
        #endregion Fin Atributos y propiedades

        public PK()
        {
            _Identificador = new List<objetoPK>();
        }

        #region Metodos
        /********************************************************/
                
        /// <summary>
        /// Agrega una PK a la Entidad
        /// </summary>
        /// <param name="oNombre"></param>
        /// <param name="oValor"></param>
        public void identidadAdd(string oNombre, object oValor)
        {
            //Valido que no se repita el nombre de la PK
            if ((from p in _Identificador where p.Nombre == oNombre select p.Nombre).ToList<string>().Count==0)
                _Identificador.Add(new objetoPK(oNombre, oValor));
            else
                valorSetPorNombre(oNombre, oValor);
        }

        /// <summary>
        /// Retorna la Cantidad de PKs que tiene la Entidad
        /// </summary>
        /// <returns></returns>
        public int cantidadDePKs()
        {
            return _Identificador.Count();
        }

        /// <summary>
        /// Retorna el Valor de la PK con el Nombre que recibo por parametro
        /// </summary>
        /// <param name="oNombre"></param>
        /// <returns></returns>
        public object valorGetPorNombre(string oNombre)
        {
            return (from p in _Identificador where p.Nombre == oNombre select p.Valor).SingleOrDefault<object>();
        }

        public void valorSetPorNombre(string oNombre, Object oValor)
        {
            (from p in _Identificador where p.Nombre == oNombre select p).ToList<objetoPK>()[0].Valor=oValor;
        }

        /// <summary>
        /// Recibe el nombre de la PK y la Elimina de la lista de PKs
        /// </summary>
        /// <param name="oNombre"></param>
        public void quitarPK(string oNombre)
        {
            try
            {   //Verifico que tenga PKs cargadas
                if (cantidadDePKs() > 0)
                {
                    //Busco la PK dentro de la Lista y la Elimino, como no aceptamos dos PKs con el Mismo nombre solamente deberia retornar un
                    //solo valor para eliminar
                    _Identificador.Remove((from p in _Identificador where p.Nombre == oNombre select p).ToList<objetoPK>()[0]);
                }
            }
            catch
            { }
        }//Fin quitarPK


        public void actualizarEntidadDesdePk(Entity oEntidad)
        {
            //Recorro la lista de propiedades del objeto
            foreach (objetoPK o in _Identificador)
            {
                //me ubico en la propiedad
                PropertyInfo _Prop = oEntidad.GetType().GetProperty(o.Nombre);
                //Actualizo el Valor

                switch (_Prop.PropertyType.FullName)
                {
                    case "System.Int32":
                        _Prop.SetValue(oEntidad, (int)o.Valor, null);
                        break;
                    case "System.Boolean":
                        _Prop.SetValue(oEntidad,(Boolean)o.Valor, null);
                        break;
                    case "System.Char":
                        _Prop.SetValue(oEntidad, (Char)o.Valor, null);
                        break;
                    case "System.DateTime":
                        _Prop.SetValue(oEntidad, (DateTime)o.Valor, null);
                        break;
                    case "System.Decimal":
                        _Prop.SetValue(oEntidad, (Decimal)o.Valor, null);
                        break;
                    case "System.Double":
                        _Prop.SetValue(oEntidad, (Double)o.Valor, null);
                        break;
                    case "System.Guid":
                        _Prop.SetValue(oEntidad, Guid.Parse(o.Valor.ToString()) , null);
                        break;
                    case "System.String":
                        _Prop.SetValue(oEntidad, o.Valor.ToString(), null);
                        break;
                    default:
                        _Prop.SetValue(oEntidad, o.Valor, null);
                        break;
                }//Fin Switch
                o.Valor = _Prop.GetValue(oEntidad, null);
               
            }
        }//Fin actualizarEntidadDesdePk

        private bool esEmpty(object valor)
        {
            bool result = false;
            switch (valor.GetType().FullName)
            {
                //case "System.Int32":
                //    result = (int)valor == 0;
                //    break;
                case "System.Boolean":
                    result = (bool)valor == false;
                    break;
                case "System.DateTime":
                    result = (DateTime)valor == DateTime.MinValue;
                    break;
                //case "System.Decimal":
                //    result = (decimal)valor == 0;
                //    break;
                //case "System.Double":
                //    result = (double)valor == 0;
                //    break;
                case "System.Guid":
                    result = (Guid)valor == Guid.Empty;
                    break;
                case "System.String":
                    result = (string)valor == string.Empty;
                    break;
                default:
                    break;
            }//Fin Switch
            return result;
        }

        /// <summary>
        /// Retorna Verdadero o Falseo si las PKs de la Entidad se Encuentran Cargadas en la Misma
        /// </summary>
        /// <param name="oEntidad"></param>
        /// <returns></returns>
        public Boolean ExistePK()
        {
            Boolean retObj = false;

            if (_Identificador.Count != 0 && (!_Identificador.Exists(p => p.Valor == null || esEmpty(p.Valor))))
                retObj = true;

            return retObj;
        }

        public object IndiceValor(int i)
        {
            return _Identificador[i].Valor;            
        }

        public object IndiceNombre(int i)
        {
            return _Identificador[i].Nombre;
        }


        public bool esPK(string nombrePK)
        {
            if ((from p in _Identificador where p.Nombre == nombrePK select p).ToList<objetoPK>().Count == 0)
                return false;
            else
                return true;
        }

        /********************************************************/
        #endregion Fin Metodos
    }
}
