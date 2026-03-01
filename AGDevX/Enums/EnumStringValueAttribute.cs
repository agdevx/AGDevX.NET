using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace AGDevX.Enums;

/// <summary>
/// Decorator to provide a string value to an Enum since C# does not support Enums with a string value
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class EnumStringValueAttribute : Attribute
{
    /// <summary>
    /// The string value associated with the decorated enum field.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EnumStringValueAttribute"/> class with the specified string value.
    /// </summary>
    /// <param name="value">The string value to associate with the decorated enum field.</param>
    public EnumStringValueAttribute(string value)
    {
        Value = value;
    }
}

public static class EnumStringValueAttributeExtensions
{
    private static readonly ConcurrentDictionary<string, string> _displayNameCache = new();

    /// <summary>
    /// Retrieves the string value for an Enum decorated with the EnumStringValueAttribute
    /// </summary>
    /// <param name="value">Value of the Enum for which to retrieve the string value (required)</param>
    /// <returns>Value of the EnumStringValueAttribute. Otherwise, the ToString() value of the Enum if the Enum is not decorated with an EnumStringValueAttribute</returns>
    public static string StringValue(this Enum value)
    {
        var key = $"{value.GetType().FullName}.{value}";

        var stringValue = _displayNameCache.GetOrAdd(key, x =>
        {
            var stringValues = (EnumStringValueAttribute[])value.GetType()
                                                               !.GetTypeInfo()
                                                               !.GetField(value.ToString())
                                                               !.GetCustomAttributes(typeof(EnumStringValueAttribute), false);

            return stringValues.Length > 0 ? stringValues[0].Value : value.ToString();
        });

        return stringValue;
    }
}