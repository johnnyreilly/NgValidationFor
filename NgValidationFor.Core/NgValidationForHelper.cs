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
        private static NgValidationApi _ngValidationApi;

        static NgValidationForHelper()
        {
            Initialize();
        }

        public static void Initialize()
        {
            _ngValidationApi = new NgValidationApi();
        }

        public static string NgValidationFor<TModel, TProperty>(
            this TModel model,
            Expression<Func<TModel, TProperty>> propertyExpression) where TModel : class
        {
            return _ngValidationApi.NgValidationFor(model, propertyExpression);
        }
    }
}
