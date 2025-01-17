﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace DocAssistant_Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true, Inherited = true)]
    public class CommonValidationAttribute : ModelValidationAttribute
    {
        public CommonValidationAttribute(string pattern) : base(pattern) {}
        
        public override bool IsValid([NotNull] object value)
        {
            var val = (string) value;

            return value != null && this.pattern.IsMatch(val);
        }
    }
}