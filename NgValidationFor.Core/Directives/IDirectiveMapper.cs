using System;
using System.Reflection;

namespace NgValidationFor.Core.Directives
{
    public interface IDirectiveMapper
    {
        Type Annotation { get; }
        Func<CustomAttributeData, string> MapperFunction { get; }
    }
}