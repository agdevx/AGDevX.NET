using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AGDevX.Exceptions;

namespace AGDevX.IEnumerables;

public static class IEnumerableExtensions
{
    /// <summary>
    /// Determines if an IEnumerable is null or empty
    /// </summary>
    /// <typeparam name="T">Type of the IEnumerable</typeparam>
    /// <param name="enumerable">IEnumerable to check (required)</param>
    /// <returns>True if the IEnumerable is null or empty</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable)
    {
        var isNullOrEmpty = enumerable == null;
        isNullOrEmpty = isNullOrEmpty || !enumerable!.Any();
        return isNullOrEmpty;
    }


    /// <summary>
    /// Determines if two string IEnumerables have a common element
    /// </summary>
    /// <param name="enumerable1">IEnumerable 1 to check against IEnumerable 2 (required)</param>
    /// <param name="enumerable2">IEnumerable 2 to check against IEnumerable 1 (required)</param>
    /// <param name="stringComparer">StringComparer to use when comparing the strings (required)</param>
    /// <returns>True if two elements have a common element. Otherwise, false.</returns>
    public static bool HasCommonStringElement(this IEnumerable<string> enumerable1, IEnumerable<string> enumerable2, StringComparer? stringComparer = default)
    {
        if (enumerable2 == null)
        {
            throw new ExtensionMethodParameterNullException(nameof(enumerable2));
        }

        stringComparer ??= StringComparer.OrdinalIgnoreCase;

        return enumerable1.Intersect(enumerable2, stringComparer).Any();
    }

    /// <summary>
    /// Converts an IEnumerable of type T to a DataTable with the provided column name
    /// </summary>
    /// <typeparam name="T">Type of the IEnumerable</typeparam>
    /// <param name="enumerable">IEnumerable to turn into a DataTable (required)</param>
    /// <param name="columnName">Name to give the new DataTable (required)</param>
    /// <returns>Database with rows the correspond to the IEnumerable. If the IEnumerable is null or empty, the returned DataTable will have no rows.</returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> enumerable, string columnName = "Id")
    {
        var dataTable = new DataTable("DT");
        dataTable.Columns.Add(columnName);

        if (enumerable.IsNullOrEmpty())
        {
            return dataTable;
        }

        foreach(var val in enumerable)
        {
            dataTable.Rows.Add(val);
        }

        return dataTable;
    }

    /// <summary>
    /// Determines if a string IEnumerable contains the provided string
    /// </summary>
    /// <param name="strings">IEnumerable of strings to check against (required)</param>
    /// <param name="str">String to check against the IEnumerable (required)</param>
    /// <param name="stringComparer">StringComparer to use when comparing the strings (required)</param>
    /// <returns>True if the IEnumerable conatins the provided string. Otherwise, false.</returns>
    public static bool ContainsIgnoreCase(this IEnumerable<string?> strings, string str, StringComparer? stringComparer = default)
    {
        if (str == null)
        {
            throw new ExtensionMethodParameterNullException(nameof(str));
        }

        stringComparer ??= StringComparer.OrdinalIgnoreCase;

        return strings.Contains(str, stringComparer);
    }

    /// <summary>
    /// Determines if there are any records in an IEnumerable
    /// </summary>
    /// <typeparam name="T">Type of the IEnumerable</typeparam>
    /// <param name="enumerable">IEnumerable to check (required)</param>
    /// <returns>True if there are any rows in the IEnumerable. Otherwise, false (even if the IEnumerable is null).</returns>
    public static bool AnySafe<T>(this IEnumerable<T>? enumerable)
    {
        return enumerable != null && enumerable.Any();
    }
}