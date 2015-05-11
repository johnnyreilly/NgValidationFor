using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace NgValidationFor.Core
{
    public static class NgValidationForHelper
    {
        private static Dictionary<Type, Func<CustomAttributeData, string>> _validationMappers;

        static NgValidationForHelper()
        {
            Initialize();
        }

        public static void Initialize()
        {
            _validationMappers = new Dictionary<Type, Func<CustomAttributeData, string>>();

            _validationMappers.Add(typeof(RequiredAttribute), customAttributeData => "required=\"required\"");
        }

        public static string NgValidationFor<TModel, TProperty>(
            this TModel model,
            Expression<Func<TModel, TProperty>> propertyExpression) where TModel : class
        {
            var member = propertyExpression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.",
                    propertyExpression));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.",
                    propertyExpression));

            var validationAttributes = propInfo
                .CustomAttributes.Where(x =>
                {
                    var currentAttr = x.AttributeType;
                    while (currentAttr.BaseType != null)
                    {
                        currentAttr = currentAttr.BaseType;
                        if (currentAttr == typeof(ValidationAttribute))
                            return true;
                    }
                    return false;
                });

            // Convert .NET validation attributes to the strings that represent angular validation directive attributes
            var ngAttributes = validationAttributes
                .Select(customAttributeData => (_validationMappers.ContainsKey(customAttributeData.AttributeType))
                    ? _validationMappers[customAttributeData.AttributeType](customAttributeData)
                    : null)
                .Where(x => !string.IsNullOrEmpty(x));

            // Return a string of the attributes to render
            var ngAttributesString = string.Join(" ", ngAttributes);
            return ngAttributesString;
        }
    }
}
