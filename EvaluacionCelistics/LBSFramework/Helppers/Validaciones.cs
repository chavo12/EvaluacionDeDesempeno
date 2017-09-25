using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LBSFramework.Helppers
{
    public class Validaciones
    {


        //----------------Metods Privados-----------------------
        /// <summary>
        /// retorna verdadero si se cumple la condicion para la cadena que ingresamos
        /// </summary>
        /// <param name="aValidar"></param>
        /// <returns></returns>
        private static Boolean validaBoolean(string aValidar, string condicion)
        {
            return (Regex.IsMatch(aValidar, condicion));
        }


        /// <summary>
        /// evalua una cadena segun la condicion que deseamos y que su longitud sea igual a la que especificamos
        /// </summary>
        /// <param name="laCadena"></param>
        /// <param name="cantidadCaracteres"></param>
        /// <returns></returns>
        private static Boolean validaBoolean(string laCadena, int cantidadCaracteres, string condicion)
        {
            //Valida por condicion y tamaño de palabra
            return (Regex.IsMatch(laCadena, condicion) && (laCadena.Length == cantidadCaracteres));

        }

        //------------------------Publicos-----------------
        /// <summary>
        /// Valida que un texto sea unicamente letras y caracteres "." y ","
        /// arroja False si posee caracteres especiales.
        /// </summary>
        /// <param name="elTestBox"></param>
        /// <returns></returns>
        public static Boolean validaTexto(string laCadena)
        {
            string condicion = @"\A[a-zA-Z0-9]*([ ]*[a-zA-Z0-9]*[/]*[,]*[.]*[a-zA-Z0-9]*)*\Z";
            return (validaBoolean(laCadena, condicion));
        }


        /// <summary>
        /// valida que el texto contenga unicamente numeros positivos
        /// </summary>
        /// <param name="elTextBox"></param>
        public static Boolean validaNumero(string elNumero)
        {
            int res;
            return int.TryParse(elNumero, out res);
            //string condicion = @"\A[0-9]*\Z";
            //return validaBoolean(elNumero, condicion);
        }
                
        public static Boolean validaFecha(string LaFecha)
        {
            if(LaFecha=="")
                return true;

            string condicion = @"^\d{1,2}\/\d{1,2}\/\d{2,4}$";
            return validaBoolean(LaFecha, condicion);
        }
        


        /// <summary>
        /// permite solo numeros positivos y un espacio
        /// </summary>
        /// <param name="elTextbox"></param>
        public static Boolean validaNumeroyVacio(string elNumeroYVacio)
        // Valida que solo pueda contener Numeros
        {
            string condicion = @"\A[0-9]+\Z";
            return validaBoolean(elNumeroYVacio, condicion);
        }

        /// <summary>
        /// acepta numeros positivos y negativos
        /// </summary>
        /// <param name="elNumZ"></param>
        /// <returns></returns>
        public static Boolean validaNumeroPositivoOnegativo(string elNumZ)
        // Valida un TexBox que solo pueda contener Numeros y retorna true si es correcto o false cc
        {
            //string condicion = @"\A[-]*[0-9]+\Z";
            string condicion = @"^[+-]?\d+(\.\d+)?$";
            return (validaBoolean(elNumZ, condicion));
        }


        /// <summary>
        /// Valida telefonos del tipo 0221-15-531-4484 con tantos guiones separando numeros como sea necesario 
        /// pero no permite dos guiones juntos ni al final del numero (45--45) (456-546-) "Error"
        /// </summary>
        /// <param name="elTel"></param>
        public static Boolean validaTelefono(string elTel)
        {
            string condicion = @"\A[0-9]*([-]{1}[0-9]+)*\Z";
            return validaBoolean(elTel, condicion);
        }


        /// <summary>
        /// Valida un Textbox que solo contenga letras
        /// </summary>
        /// <param name="laPalabra"></param>
        public static Boolean validaPalabra(string laPalabra)
        {
            string condicion = @"\A[a-zA-Z]*\Z";
            return validaBoolean(laPalabra, condicion);
        }


        /// <summary>
        /// Valida un Texto que solo contenga letras y este formado por una cantidad especifica de caracteres
        /// </summary>
        /// <param name="laPalabra"></param>
        /// <param name="cantidadCaracteres"></param>
        public static Boolean validaPalabrayLongitud(string laPalabra, int cantidadCaracteres)
        {
            string condicion = @"\A[a-zA-Z]*\Z";
            return validaBoolean(laPalabra, cantidadCaracteres, condicion);
        }


        /// <summary>
        ///Valida un numero decimal teniendo en cuenta el separador de decimales el punto
        ///Lo tomo asi ya que resulta mas comodo el pinto para usar el pad nuemrico
        /// </summary>
        /// <param name="elDecimal"></param>
        public static bool validaNumeroDecimal(string elDecimal)
        {
            decimal res;

            return decimal.TryParse(elDecimal, out res);

            
            //string condicion = @"\A[-]*[0-9]*[.]*[,]*[0-9]*\Z";
            //return validaBoolean(elDecimal, condicion);
        }

        /// <summary>
        /// Valido que la cadena ingresada sea un Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static Boolean validaEmail(String email)
        {
            String expresion;
            //un Email bien ingresado nunca podria tener las siguientes combinaciones de caracteres
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*[ ]*";
            //si la cadena ingresada contiene alguna de las combinaciones anteriores 
            //devuelvo FALSE que indica que la expresion no es un Email
            if (Regex.IsMatch(email, expresion))
            {

                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;//la cadena ingresada es un Email
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Valida que el numero recibido sea cuit (el número puedo o no contener los guiones.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static Boolean esCuit(string numero)
        {
            numero = numero.Replace("-", string.Empty);
            long res;
            if (numero.Length == 11 && long.TryParse(numero, out res))
            {
                return int.Parse(numero.Substring(10)) == CalcularDigitoCuit(numero);
            }
            else return false;
        }


        public static int CalcularDigitoCuit(string cuit)
        {
            int[] mult = new[] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 };
            char[] nums = cuit.ToCharArray();
            int total = 0;
            for (int i = 0; i < mult.Length; i++) 
            {
                total += int.Parse(nums[i].ToString()) * mult[i];
            }
            var resto = total % 11;
            return resto == 0 ? 0 : resto == 1 ? 9 : 11 - resto;
        }

    }
}
