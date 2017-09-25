using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LBSFramework.ControlesWPF
{
    public class eMailValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            return new ValidationResult(LBSFramework.Helppers.Validaciones.validaEmail(value.ToString()), string.Format("El eMail ingresado no posee un formato Valido."));
        }
    }//Fin Valido Mail


    public class FechaValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            return new ValidationResult(LBSFramework.Helppers.Validaciones.validaFecha(value.ToString().Trim()), string.Format("La Fecha Ingresada no es Valida."));
        }
    }//Fin Valido Fecha

    public class EnteroValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            return new ValidationResult(LBSFramework.Helppers.Validaciones.validaNumero(value.ToString().Trim()), string.Format("El valor ingresado debe ser un Numero."));
        }
    }//Fin Valido Entero

    public class DecimalValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
       {

            return new ValidationResult(LBSFramework.Helppers.Validaciones.validaNumeroDecimal(value.ToString().Trim()), string.Format("El valor ingresado debe ser un Numero."));
        }
    }//Fin Valido Decimal
    
    public class TextoLibreValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {

            return new ValidationResult(true, string.Format("El valor ingresado debe ser un Numero."));
        }
    }//Fin Valido TextoLibreValidationRule

}
