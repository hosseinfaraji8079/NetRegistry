﻿using System.Reflection;

namespace Registry.API.Extensions;

public static class AttributeExtension
{
    public static TAttribute? GetAttribute<TAttribute>(this Enum enumValue)
        where TAttribute : Attribute
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<TAttribute>();
    }
}