using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;

namespace LungoApp.Views.Learn
{
    public class STRValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string text = (string)value;
            if(text.Length < 1)
            {
                return new ValidationResult(false, "Add More");
            }
            return ValidationResult.ValidResult;
        }
    }
}
