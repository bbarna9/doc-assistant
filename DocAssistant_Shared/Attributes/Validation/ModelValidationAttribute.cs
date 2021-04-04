using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DocAssistant_Common.Attributes
{
    public abstract class ModelValidationAttribute : ValidationAttribute
    {
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        
        public char [] InvalidCharacters { get; set; }

        /// <summary>
        /// <para>Tuple.Item1 = PropertyName</para>
        /// <para>Tuple.Item2 = List of ErrorMessages (string)</para>
        /// </summary>
        public List<(string, List<string>)> ValidationErrors
        {
            get;
            private set;
        }
        
        protected ModelValidationAttribute()
        {
            this.MinLength = 0;
            this.MaxLength = int.MaxValue;
        }
        protected ModelValidationAttribute(int minLength, int maxLength)
        {
            this.MinLength = minLength;
            this.MaxLength = maxLength;
        }

        protected ModelValidationAttribute(int minLength, int maxLength, char[] invalidCharacters) : this(minLength,maxLength)
        {
            this.InvalidCharacters = invalidCharacters;
        }

        protected void CreateValidationErrorsListIfNotExists()
        {
            if (this.ValidationErrors != null) return;
            
            this.ValidationErrors = new List<(string, List<string>)>();
        }
    }
}