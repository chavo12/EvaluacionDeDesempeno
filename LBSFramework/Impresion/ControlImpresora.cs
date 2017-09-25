using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace LBSFramework.Impresion
{
    public class ControlImpresora
    {

        public static bool Existe(string nombreImpresora)
        {
            if (String.IsNullOrEmpty(nombreImpresora)) { throw new ArgumentNullException("printerName"); }
            return PrinterSettings.InstalledPrinters.Cast<string>().Any(name => nombreImpresora.ToUpper().Trim() == name.ToUpper().Trim());
        }

        public static string NombreImpresoraPredeterminada()
        {
            var settings = new PrinterSettings();
            return settings.PrinterName;
        }

        public static string ObtenerNombreImpresora(string claveImpresora)
        {
            string nombreImpresora = "";

            if (!String.IsNullOrEmpty(claveImpresora))
            {
                /* Obtiene del archivo de configuración el nombre de la impresora a usar para la clave recibida */
                nombreImpresora = System.Configuration.ConfigurationManager.AppSettings[claveImpresora];
            }

            if ((!string.IsNullOrEmpty(nombreImpresora)) && Existe(nombreImpresora))
                return nombreImpresora;
            else
                return "";
        }

        public static bool EstablecerPredeterminada(string claveImpresora, bool seleccionarSiNoExiste = true)
        {
            var type = Type.GetTypeFromProgID("WScript.Network");
            var instance = Activator.CreateInstance(type);
            bool resultado = false;
            string nombreImpresora = "";

            if (!String.IsNullOrEmpty(claveImpresora))
            {
                /* Obtiene del archivo de configuración el nombre de la impresora a usar para la clave recibida */
                nombreImpresora = System.Configuration.ConfigurationManager.AppSettings[claveImpresora];
            }

            if (! String.IsNullOrEmpty(nombreImpresora))
            {
                /* Vino un nombre de impresora a establecer */
                try
                {
                    /* Intenta establecer la impresora predeterminada */
                    type.InvokeMember("SetDefaultPrinter", System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { nombreImpresora });                
                    resultado = true;
                }
                catch
                {
                    /* No se pudo establecer esa impresora */
                    resultado = false;
                }
            }

            if ((! resultado) && seleccionarSiNoExiste)
            {
                /* Ofrece al usuario la selección de la impresora deseada */
                try
                {
                    PrintDialog prtd = new PrintDialog();
                    if (prtd.ShowDialog() == DialogResult.OK)
                    {
                        /* Intenta establecer la impresora elegida */
                        type.InvokeMember("SetDefaultPrinter", System.Reflection.BindingFlags.InvokeMethod, null, instance, new object[] { prtd.PrinterSettings.PrinterName });
                        resultado = true;
                    }
                    else
                    {
                        /* No se confirmó selección de impresora */
                        resultado = false;
                    }
                }
                catch
                {
                    /* Ocurió un error inesperado */
                    resultado = false;
                }
            }

            return resultado;
        }

        public static PrintDialog ObtenerImpresora (string nombreImpresora)
        {
            bool resultado = false;
            PrintDialog prtd = new PrintDialog();
            
            if (!String.IsNullOrEmpty(nombreImpresora) && Existe(nombreImpresora))
            {
                /* Vino un nombre de impresora a obtener */
                /* Intenta obtener la impresora */
                prtd.PrinterSettings.PrinterName = nombreImpresora;
                if (prtd.PrinterSettings.IsValid)
                {
                    resultado = true;
                }
            }

            if (! resultado)
            {
                if (prtd.ShowDialog() == DialogResult.OK)
                {
                    resultado = true;
                }
                else
                {
                    /* No se confirmó selección de impresora */
                    resultado = false;
                }
            }

            if (resultado)
                return prtd;
            else
                return null;
        }
    }
}
