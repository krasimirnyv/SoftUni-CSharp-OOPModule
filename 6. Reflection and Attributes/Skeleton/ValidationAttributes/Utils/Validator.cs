using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ValidationAttributes.Attributes;

namespace ValidationAttributes.Utils;

public static class Validator
{
    public static bool IsValid(object obj)
    {
        Type type = obj.GetType();

        PropertyInfo[] properties = type
            .GetProperties()
            .Where(prop => prop.CustomAttributes.Any(ca => typeof(MyValidationAttribute).IsAssignableFrom(ca.AttributeType)))
            .ToArray();

        foreach (PropertyInfo property in properties)
        {
            IEnumerable<MyValidationAttribute> attributes = property
                .GetCustomAttributes()
                .Where(ca => typeof(MyValidationAttribute).IsAssignableFrom(ca.GetType()))
                .Cast<MyValidationAttribute>();

            foreach (MyValidationAttribute attribute in attributes)
            {
                if (!attribute.IsValid(property.GetValue(obj)))
                {
                    return false;
                }
            }
        }

        return true;
    }
}