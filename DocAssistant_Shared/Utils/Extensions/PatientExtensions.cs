using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using DocAssistant_Common.Models;

namespace DocAssistant_Common.Utils.Extensions
{
    public static class PatientExtensions
    {

        public static bool ValidateData(this Patient patient, string propertyName, out List<string> errorMessages) =>
            Validation.ValidateProperty(propertyName, patient, out errorMessages);

    }
}