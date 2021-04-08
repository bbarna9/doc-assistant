using System;

namespace DocAssistant_Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true, Inherited = true)]
    public class FixedAttribute : Attribute
    {
        // Marks a property as not modifiable
    }
}