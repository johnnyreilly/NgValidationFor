using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NgValidationFor.Core.Directives
{
    public class RequiredDirectiveMapper : DirectiveMapperBase<RequiredAttribute>
    {
        public override Func<CustomAttributeData, string> MapperFunction 
        {
            get
            {
                return customAttributeData => "required=\"required\"";
            }
        }
    }
}
