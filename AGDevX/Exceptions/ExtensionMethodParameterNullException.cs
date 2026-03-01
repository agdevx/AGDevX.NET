using System;

namespace AGDevX.Exceptions;

/// <summary>
/// Exception to throw if a required parameter was not provided to an extension method
/// </summary>
public sealed class ExtensionMethodParameterNullException : CodedArgumentNullException
{
    /// <inheritdoc />
    public override string Code { get; set; } = "EXTENSION_METHOD_PARAMETER_NULL_EXCEPTION";

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtensionMethodParameterNullException"/> class with the default code.
    /// </summary>
    public ExtensionMethodParameterNullException() : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtensionMethodParameterNullException"/> class with a specified argument name.
    /// </summary>
    /// <param name="argumentName">The name of the argument that caused the exception.</param>
    public ExtensionMethodParameterNullException(string argumentName) : base(argumentName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtensionMethodParameterNullException"/> class with a specified argument name and code.
    /// </summary>
    /// <param name="argumentName">The name of the argument that caused the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    public ExtensionMethodParameterNullException(string argumentName, string code) : base(argumentName, code)
    {
        Code = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtensionMethodParameterNullException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ExtensionMethodParameterNullException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExtensionMethodParameterNullException"/> class with a specified argument name, code, and inner exception.
    /// </summary>
    /// <param name="argumentName">The name of the argument that caused the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ExtensionMethodParameterNullException(string argumentName, string code, Exception innerException) : base(argumentName, code, innerException)
    {
        Code = code;
    }
}