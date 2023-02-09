using System;
using System.Globalization;

namespace PharmacyAdmin.Model
{
    public static class DecimalExtensions
    {
        public static string ToUIString(this decimal value)
        {
            //return value.ToString(CultureInfo.CurrentUICulture);
            //return value.ToString("0.##"); // returns "0"  when decimalVar == 0

            return value.ToString("#.##"); 
        }
    }
}
