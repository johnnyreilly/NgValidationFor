using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;

namespace NgValidationFor.Core
{
    public static class NgValidationFor<TModel>
    {
        public static string GetAttributes<TProperty>(
            Expression<Func<TModel, TProperty>> propertyExpression)
        {
            // Return to native if true not passed
            //if (!useNativeUnobtrusiveAttributes)
            //    return htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);

            //var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            //var attributes = Mapper.GetUnobtrusiveValidationAttributes(htmlHelper, expression, htmlAttributes, metadata);
            //
            //var dropDown = Mapper.GenerateHtmlWithoutMvcUnobtrusiveAttributes(() =>
            //    htmlHelper.DropDownListFor(expression, selectList, optionLabel, attributes));
            //
            //return dropDown;

            //Type type = typeof(TModel);

            var member = propertyExpression.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a method, not a property.",
                    propertyExpression));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format("Expression '{0}' refers to a field, not a property.",
                    propertyExpression));
            //PropertyDescriptorCollection props2 = TypeDescriptor.GetProperties(propertyExpression.Body);


            //if (type != propInfo.ReflectedType &&
            //    !type.IsSubclassOf(propInfo.ReflectedType))
            //    throw new ArgumentException(
            //        string.Format("Expresion '{0}' refers to a property that is not from type {1}.", propertyExpression,
            //            type));

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
            var ngAttributes = validationAttributes.Select(x =>
            {
                if (x.AttributeType == typeof (RequiredAttribute))
                {
                    return "ng-required";
                }
                return null;
            })
            .Where(x => !string.IsNullOrEmpty(x));

            // Return a string of the attributes to render
            var ngAttributesString = string.Join(" ", ngAttributes);
            return ngAttributesString;
        }
    }
}
