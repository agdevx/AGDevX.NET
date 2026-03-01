using System;

namespace AGDevX.Exceptions;

/// <summary>
/// Base exception from which to inherit for coded null argument exceptions.
/// This inherits from ArgumentNullException and adds the concept of a "Code" that can provide additional context into the origin of the exception.
/// </summary>
public class CodedArgumentNullException : ArgumentNullException
{
    /// <summary>
    /// A code providing additional context into the origin of the exception.
    /// </summary>
    public virtual string Code { get; set; } = "CODED_ARGUMENT_NULL_EXCEPTION";

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with the default code.
    /// </summary>
    public CodedArgumentNullException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with a specified argument name.
    /// </summary>
    /// <param name="argumentName">The name of the argument that caused the exception.</param>
    public CodedArgumentNullException(string argumentName) : base(argumentName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with a specified argument name and code.
    /// </summary>
    /// <param name="argumentName">The name of the argument that caused the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    public CodedArgumentNullException(string argumentName, string code) : base(argumentName)
    {
        Code = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public CodedArgumentNullException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedArgumentNullException"/> class with a specified argument name, code, and inner exception.
    /// </summary>
    /// <param name="argumentName">The name of the argument that caused the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public CodedArgumentNullException(string argumentName, string code, Exception innerException) : base(argumentName, innerException)
    {
        Code = code;
    }
}