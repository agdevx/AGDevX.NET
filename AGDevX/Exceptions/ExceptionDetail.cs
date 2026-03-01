using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using AGDevX.Assemblies;

namespace AGDevX.Exceptions;

/// <summary>
/// Information relevant to a developer that has been extracted from an execption and put into an easy to consume model
/// </summary>
public sealed class ExceptionDetail
{
    /// <summary>
    /// A code identifying the type or origin of the exception.
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// The exception error message.
    /// </summary>
    public required string Message { get; set; }

    /// <summary>
    /// The parsed stack frames from the exception, or <see langword="null"/> if stack frames were not included.
    /// </summary>
    public IEnumerable<StackFrameModel>? StackFrames { get; set; }

    /// <summary>
    /// The inner exception detail, or <see langword="null"/> if there is no inner exception.
    /// </summary>
    public ExceptionDetail? InnerException { get; set; }

    /// <summary>
    /// Structured representation of a single stack frame.
    /// </summary>
    public sealed class StackFrameModel
    {
        /// <summary>
        /// The line number in the source code file where the frame originated.
        /// </summary>
        public int LineNumber { get; set; }

        /// <summary>
        /// The method signature where the frame originated.
        /// </summary>
        public string? Method { get; set; }

        /// <summary>
        /// The fully qualified class name where the frame originated.
        /// </summary>
        public string? Class { get; set; }

        /// <summary>
        /// The full name of the assembly containing the frame.
        /// </summary>
        public string? AssemblyName { get; set; }

        /// <summary>
        /// The file path of the assembly containing the frame.
        /// </summary>
        public string? AssemblyFile { get; set; }

        /// <summary>
        /// The source code file path where the frame originated.
        /// </summary>
        public string? CodeFile { get; set; }
    }
}

public static class ExceptionDetailExtensions
{
    //-- Inspired by https://stackoverflow.com/a/65972998/5372598

    /// <summary>
    /// Recursively retrieves structured information about an exception
    /// </summary>
    /// <param name="codedEx">The CodedException from which to retrieve structured information (required)</param>
    /// <param name="includeStackFrames">Determines whether or not to return Stack Frames (required)</param>
    /// <param name="filterStackFrames">Determines wither or not to filter the Stack Frames (required)</param>
    /// <param name="assemblyPrefixes">Assembly FullName prefixes that is used to filter the StackFrames (required)</param>
    /// <returns><see cref="ExceptionDetail"/></returns>
    public static ExceptionDetail GetExceptionDetail(this CodedApplicationException codedEx, bool includeStackFrames = true, bool filterStackFrames = false, IEnumerable<string>? assemblyPrefixes = default)
    {
        assemblyPrefixes ??= new List<string>();
        var code = codedEx.Code;

        var stackTrace = new StackTrace(codedEx, true);

        var exceptionDetail = new ExceptionDetail
        {
            Code = code,
            Message = codedEx.Message,
            StackFrames = includeStackFrames
                ? stackTrace.GetFrames().Select(sfm => new ExceptionDetail.StackFrameModel
                {
                    LineNumber = sfm.GetFileLineNumber(),
                    Method = GetMethodSignature(sfm.GetMethod()),
                    Class = sfm.GetMethod()?.DeclaringType?.FullName,
                    AssemblyName = sfm.GetMethod()?.DeclaringType?.Assembly?.FullName,
                    AssemblyFile = sfm.GetMethod()?.DeclaringType?.Assembly?.Location,
                    CodeFile = sfm.GetFileName(),
                }).Where(sf => !filterStackFrames || (filterStackFrames && !assemblyPrefixes.Any()) || AssemblyUtility.AssemblyNameStartsWithAnyPrefix(sf.AssemblyName, assemblyPrefixes))
                : null
        };

        if (codedEx.InnerException is CodedApplicationException innerCodedEx)
        {
            exceptionDetail.InnerException = innerCodedEx.GetExceptionDetail() ?? null;
        }
        else
        {
            exceptionDetail.InnerException = codedEx?.InnerException?.GetExceptionDetail("EXCEPTION") ?? null;
        }

        return exceptionDetail;
    }

    /// <summary>
    /// Recursively retrieves structured information about an exception
    /// </summary>
    /// <param name="ex">The Exception from which to retrieve structured information (required)</param>
    /// <param name="includeStackFrames">Determines whether or not to return Stack Frames (required)</param>
    /// <param name="filterStackFrames">Determines wither or not to filter the Stack Frames (required)</param>
    /// <param name="assemblyPrefixes">Assembly FullName prefixes that is used to filter the StackFrames (required)</param>
    /// <returns><see cref="ExceptionDetail"/></returns>
    public static ExceptionDetail GetExceptionDetail(this Exception ex, string code, bool includeStackFrames = true, bool filterStackFrames = false, IEnumerable<string>? assemblyPrefixes = default)
    {
        assemblyPrefixes ??= new List<string>();

        var stackTrace = new StackTrace(ex, true);

        var exceptionDetail = new ExceptionDetail
        {
            Code = code,
            Message = ex.Message,
            StackFrames = includeStackFrames
                ? stackTrace.GetFrames().Select(sfm => new ExceptionDetail.StackFrameModel
                {
                    LineNumber = sfm.GetFileLineNumber(),
                    Method = GetMethodSignature(sfm.GetMethod()),
                    Class = sfm.GetMethod()?.DeclaringType?.FullName,
                    AssemblyName = sfm.GetMethod()?.DeclaringType?.Assembly?.FullName,
                    AssemblyFile = sfm.GetMethod()?.DeclaringType?.Assembly?.Location,
                    CodeFile = sfm.GetFileName(),
                }).Where(sf => !filterStackFrames || (filterStackFrames && !assemblyPrefixes.Any()) || AssemblyUtility.AssemblyNameStartsWithAnyPrefix(sf.AssemblyName, assemblyPrefixes))
                : null,
            InnerException = ex?.InnerException?.GetExceptionDetail(code) ?? null
        };

        return exceptionDetail;
    }

    private static string? GetMethodSignature(MethodBase? methodBase)
    {
        if (methodBase == null)
        {
            return null;
        }

        var declaringTypeNameRegex = new Regex(@"(<)(.*?)(>)");
        var declaringTypeNameMatch = declaringTypeNameRegex.Match(methodBase.DeclaringType?.Name ?? string.Empty);
        //-- "<GetRoles>d__5"

        var methodName = methodBase.Name;
        //-- scoped method <GetExceptionDetail_IncludeStackFrames_FilterStackFrames>g__asdfasdfasdf|7_0

        var methodNameRegex = new Regex(@"(__)(.*?)(\|)");
        var methodNameMatch = methodNameRegex.Match(methodName);

        if (declaringTypeNameMatch.Success)
        {
            methodName = declaringTypeNameMatch.Groups[2].Value;
        }

        if (methodNameMatch.Success)
        {
            methodName = methodNameMatch.Groups[2].Value;
        }

        var methodSignatureStringBuilder = new StringBuilder(methodName);

        //-- Generic Method
        if (methodBase is MethodInfo info && info.IsGenericMethod)
        {
            var genericArgs = info.GetGenericArguments();

            methodSignatureStringBuilder.Append('<');
            methodSignatureStringBuilder.Append(string.Join(", ", genericArgs.Select(ga => ga.Name)).Trim());
            methodSignatureStringBuilder.Append('>');
        }

        //-- Arguments
        var parameterInfos = methodBase.GetParameters();

        methodSignatureStringBuilder.Append('(');
        methodSignatureStringBuilder.Append(string.Join(", ", parameterInfos.Select(pi => $"{ pi.ParameterType?.Name ?? "<unknown type>" } { pi.Name }")).Trim());
        methodSignatureStringBuilder.Append(')');

        return methodSignatureStringBuilder.ToString();
    }
}