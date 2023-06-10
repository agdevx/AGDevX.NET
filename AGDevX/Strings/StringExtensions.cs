using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AGDevX.Exceptions;

namespace AGDevX.Strings;

public static class StringExtensions
{
    /// <summary>
    /// Determines if two string are equal ignoring case
    /// </summary>
    /// <param name="string1">String 1 to compare to string 2 (required)</param>
    /// <param name="string2">String 2 to compare to string 1 (required)</param>
    /// <param name="stringComparison">StringComparison to use when comparing the strings (required)</param>
    /// <returns>True if the two strings are equal ignoring the case. Otherwise, false.</returns>
    public static bool EqualsIgnoreCase(this string string1, [AllowNull] string string2, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
    {
        if (string2 == null)
        {
            throw new ExtensionMethodParameterNullException(nameof(string2));
        }

        return string1.Equals(string2, stringComparison);
    }

    /// <summary>
    /// Determines if a string starts with another string ignoring case
    /// </summary>
    /// <param name="string1">String 1 to compare to string 1 (required)</param>
    /// <param name="string2">String 2 used to compare to string 1 (required)</param>
    /// <param name="stringComparison">StringComparison to use when comparing the strings (required)</param>
    /// <returns>True if string 1 starts with string 2 ignoring case. Otherwise, false.</returns>
    public static bool StartsWithIgnoreCase(this string string1, [AllowNull] string string2, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
    {
        if (string2 == null)
        {
            throw new ExtensionMethodParameterNullException(nameof(string2));
        }

        return string1.StartsWith(string2, stringComparison);
    }

    /// <summary>
    /// Determines if a string is contained within another string ignoring case
    /// </summary>
    /// <param name="string1">String 1 to determine if it contains string 2 (required)</param>
    /// <param name="string2">String 2 to determine if it is contained in string 1 (required)</param>
    /// <param name="stringComparison">StringComparison to use when comparing the strings (required)</param>
    /// <returns>True if string 1 contains string 2 ignoring case. Otherwise, false.</returns>
    public static bool ContainsIgnoreCase(this string string1, [AllowNull] string string2, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase)
    {
        if (string2 == null)
        {
            throw new ExtensionMethodParameterNullException(nameof(string2));
        }

        return string1.Contains(string2, stringComparison);
    }

    /// <summary>
    /// Determines if a string is null or whitespace
    /// </summary>
    /// <param name="str">String to check (required)</param>
    /// <returns>True if the string is null or whitespace. Otherwise, false.</returns>
    public static bool IsNullOrWhiteSpace([AllowNull] [NotNullWhen(false)] this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// Determines if a string is not null and not whitespace
    /// </summary>
    /// <param name="str">String to check (required)</param>
    /// <returns>True if the string is not null and not whitespace. Otherwise, false.</returns>
    public static bool IsNotNullNorWhiteSpace([AllowNull] [NotNullWhen(true)] this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// Determines if a string only consists of whitespace
    /// </summary>
    /// <param name="str">String to check (required)</param>
    /// <returns>True if the string only consists of whitespace. Otherwise, false.</returns>
    public static bool IsWhiteSpace([NotNullWhen(true)] this string str)
    {
        return str != string.Empty && str.All(s => s == ' ');
    }

    /// <summary>
    /// Determines if a string contains at least one character that is not whitespace
    /// </summary>
    /// <param name="str">String to check (required)</param>
    /// <returns>True if a string contains at least one character that is not whitespace. Otherwise, false.</returns>
    public static bool IsNotWhiteSpace([MaybeNullWhen(true)] this string str)
    {
        return str == string.Empty || !str.All(s => s == ' ');
    }

    /// <summary>
    /// Determines if a string is empty
    /// </summary>
    /// <param name="str">String to check (required)</param>
    /// <returns>True if the string is empty. Otherwise, false.</returns>
    public static bool IsEmpty([NotNullWhen(true)] this string str)
    {
        return str == string.Empty;
    }

    /// <summary>
    /// Determines if a string is not empty
    /// </summary>
    /// <param name="str">String to check (required)</param>
    /// <returns>True if the string is not empty. Otherwise, false.</returns>
    public static bool IsNotEmpty([NotNullWhen(true)] this string str)
    {
        return str != string.Empty;
    }

    /// <summary>
    /// Returns null if the string is null or whitespace. Otherwise, returns the string.
    /// </summary>
    /// <param name="str">String to check (required)</param>
    /// <returns>Null if the string is null or whitespace. Otherwise, the string.</returns>
    public static string? NullIfNullOrWhiteSpace([NotNullIfNotNull(nameof(str))] this string? str)
    {
        return str.IsNullOrWhiteSpace() ? null : str;
    }
}