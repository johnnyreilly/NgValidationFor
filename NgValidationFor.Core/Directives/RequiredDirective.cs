using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NgValidationFor.Core.Directives
{
    public class RequiredDirective : IDirective
    {
        public Type Annotation { get; private set; }
        public Func<CustomAttributeData, string> MapperFunction { get; private set; }

        public RequiredDirective()
        {
            Annotation = typeof(RequiredAttribute);

            MapperFunction = customAttributeData => "required=\"required\"";
        }
    }
}
