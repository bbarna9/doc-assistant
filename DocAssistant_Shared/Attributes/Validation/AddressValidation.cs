using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DocAssistant_Common.Models;
using DocAssistant_Common.Utils;

namespace DocAssistant_Common.Attributes
{
    public class AddressValidation : ModelValidationAttribute
    {
        public override bool IsValid([NotNull] object value)
        {
            var address = (Address) value;

            if (!Validation.ValidateProperty("Country", address, out var countryValidationErrors))
            {
                this.CreateValidationErrorsListIfNotExists();
                this.ValidationErrors.Add(("Country",countryValidationErrors));
            }

            if (!Validation.ValidateProperty("State", address, out var stateValidationErrors))
            {
                this.CreateValidationErrorsListIfNotExists();
                this.ValidationErrors.Add(("State",stateValidationErrors));
            }
            
            if (!Validation.ValidateProperty("City", address, out var cityValidationErrors))
            {
                this.CreateValidationErrorsListIfNotExists();
                this.ValidationErrors.Add(("City",cityValidationErrors));
            }
            
            if (!Validation.ValidateProperty("Street", address, out var streetValidationErrors))
            {
                this.CreateValidationErrorsListIfNotExists();
                this.ValidationErrors.Add(("Street",streetValidationErrors));
            }

            if (!Validation.ValidateProperty("Street", address, out streetValidationErrors))
            {
                this.CreateValidationErrorsListIfNotExists();
                this.ValidationErrors.Add(("Street",streetValidationErrors));
            }
            
            if (!Validation.ValidateProperty("ZIP", address, out var zipValidationErrors))
            {
                this.CreateValidationErrorsListIfNotExists();
                this.ValidationErrors.Add(("ZIP",zipValidationErrors));
            }

            return
                (countryValidationErrors is not null) ||
                (stateValidationErrors is not null) ||
                (cityValidationErrors is not null) ||
                (streetValidationErrors is not null) ||
                (zipValidationErrors is not null);
        }
    }
}