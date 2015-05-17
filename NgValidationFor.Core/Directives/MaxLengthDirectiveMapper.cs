using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NgValidationFor.Core.Directives
{
    public class MaxLengthDirectiveMapper : DirectiveMapperBase<MaxLengthAttribute>
    {
        public override Func<CustomAttributeData, string> MapperFunction
        {
            get
            {
                return customAttributeData =>
                {
                    var validation = "";

                    if (customAttributeData.ConstructorArguments.Count > 0)
                        validation += "ng-maxlength=\"" + customAttributeData.ConstructorArguments[0].Value + "\"";

                    return validation;
                };
            }
        }
    }
}
