using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using NgValidationFor.Core.Directives;

namespace NgValidationFor.Core
{
    public class NgValidationApi
    {
        readonly List<IDirectiveMapper> _directiveMappers;

        public NgValidationApi()
        {
            // Scan assembly for directive mappers
            var directiveMappers = Assembly.GetExecutingAssembly()
                .GetTypes().Where(t => !t.IsAbstract && !t.IsInterface && typeof(IDirectiveMapper).IsAssignableFrom(t))
                .Select(x => (IDirectiveMapper)Activator.CreateInstance(x));

            _directiveMappers = new List<IDirectiveMapper>(directiveMappers);
            //{
            //new RequiredDirective()
            //};
        }

        public IHtmlString NgValidationFor<TModel, TProperty>(
            TModel model,
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
                .CustomAttributes
                .Where(x => x.AttributeType.BaseType != null && x.AttributeType.BaseType.IsAssignableFrom(typeof(ValidationAttribute)));

            // Convert .NET validation attributes to the strings that represent angular validation directive attributes
            var ngAttributes = validationAttributes
                .Select(customAttributeData =>
                {
                    var directiveMapper =
                        _directiveMappers.FirstOrDefault(x => x.Annotation == customAttributeData.AttributeType);

                    return (directiveMapper != null)
                        ? directiveMapper.MapperFunction(customAttributeData)
                        : null;
                })
                .Where(x => !string.IsNullOrEmpty(x));

            // Return a string of the attributes to render
            var ngAttributesString = new HtmlString(string.Join(" ", ngAttributes));
            return ngAttributesString;
        }
    }


}
