using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NgValidationFor.Core.Directives
{
    public class MinLengthDirectiveMapper : DirectiveMapperBase<MinLengthAttribute>
    {
        public override Func<CustomAttributeData, string> MapperFunction
        {
            get
            {
                return customAttributeData =>
                {
                    var validation = "";

                    if (customAttributeData.ConstructorArguments.Count > 0)
                        validation += "ng-minlength=\"" + customAttributeData.ConstructorArguments[0].Value + "\"";

                    return validation;
                };
            }
        }
    }
}
