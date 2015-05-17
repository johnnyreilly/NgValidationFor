using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace NgValidationFor.Core.Directives
{
    public class StringLengthDirectiveMapper : DirectiveMapperBase<StringLengthAttribute>
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

                    var minLengthAttribute = customAttributeData.NamedArguments
                            .Where(x => x.MemberName == "MinimumLength")
                            .Cast<CustomAttributeNamedArgument?>()
                            .FirstOrDefault();
                    if (minLengthAttribute != null)
                        validation += "ng-minlength=\"" + minLengthAttribute.Value.TypedValue.Value + "\"";

                    return validation;
                };
            }
        }
    }
}
