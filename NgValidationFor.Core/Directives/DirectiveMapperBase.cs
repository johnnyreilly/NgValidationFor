using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NgValidationFor.Core.Directives
{
    public abstract class DirectiveMapperBase<TValidationAttribute> : IDirectiveMapper where TValidationAttribute : ValidationAttribute
    {
        public Type Annotation {
            get { return typeof (TValidationAttribute); }
        }

        public abstract Func<CustomAttributeData, string> MapperFunction { get; }
    }
}