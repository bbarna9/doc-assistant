using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace DocAssistant_Common.Utils
{
    public static class Validation
    {
        public static bool ValidateProperty(string propertyName, object instance, out List<string> errorMessages)
        {
            var property = instance.GetType().GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Default | BindingFlags.Instance);

            return ValidateProperty(property,instance,out errorMessages);
        }
        private static bool ValidateProperty(PropertyInfo propertyInfo, object instance, out List<string> errorMessages)
        {

            var value = propertyInfo.GetValue(instance);

            var validationAttributes = propertyInfo.GetCustomAttributes(false)
                .Where(attribute => attribute.GetType().IsAssignableTo(typeof(ValidationAttribute)))
                .ToList();

            var failedValidations = validationAttributes.Where(attribute =>
                !((ValidationAttribute) attribute).IsValid(value)).ToList();
   
            if (failedValidations.Count > 0)
            {
                errorMessages = failedValidations.Select(validationResult => ((ValidationAttribute) validationResult).ErrorMessage).ToList();
                return false;
            }

            errorMessages = null;
            
            return true;
        }
    }
}