using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LBSFramework.ConexionBD
{
    class Ambiente
    {
        private static String  Escritorio = "escritorio";
        private static String WebService = "webservice";
        private static String Comparador = "comparador";
        private static String Service = "service";
        private static String SitioWeb = "sitioweb";


        public static string AmbienteActual()
        {
           // System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            //Reviso en que ambiente se esta ejecutando el modulo
            
            return System.Configuration.ConfigurationManager.AppSettings.GetValues("ambiente")[0];
        }

        public static Boolean EsClienteEscritorio() 
        {
            return (AmbienteActual().Equals(Ambiente.Escritorio));                
        }

        public static Boolean EsComparador()
        {
            return (AmbienteActual().Equals(Ambiente.Comparador));
        }

        public static Boolean EsWebService()
        {
            return (AmbienteActual().Equals(Ambiente.WebService));
        }

        public static Boolean EsServicio()
        {
            return (AmbienteActual().Equals(Ambiente.Service));
        }

        public static Boolean EsSitioWeb()
        {
            return (AmbienteActual().Equals(Ambiente.SitioWeb));
        }


        public static string ConexionWebSQL() 
        {
            System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            //Reviso en que ambiente se esta ejecutando el modulo
            return System.Configuration.ConfigurationManager.AppSettings.GetValues("cadena")[0];
        }

        public static string ConexionSitioWeb()
        {
            return System.Configuration.ConfigurationManager.AppSettings.GetValues("cadena")[0];
        }

        public static string ConexionEscritorio() 
        {
            //return @"Data Source=(localdb)\Projects;Initial Catalog=AMEPBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";//"Data Source=Iguazu;Initial Catalog=AmepDesarrollo;Integrated Security=True"; 
           // return @"Data Source=Iguazu;Initial Catalog=AmepDesarrollo;Integrated Security=True";
            return System.Configuration.ConfigurationManager.AppSettings.GetValues("cadena")[0]; 
        }

        public static string ConexionComparador()
        {
            
            return "Retornar la Conexion de Comparador, determinar como manejarla";
        }

        public static string ConexionServicio()
        {
            return "Retornar la Conexion de Servicio, determinar como manejarla";
        }

    }
}
