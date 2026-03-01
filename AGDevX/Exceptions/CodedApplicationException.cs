using System;

namespace AGDevX.Exceptions;

/// <summary>
/// Base exception from which to inherit for coded application-specific exceptions.
/// This inherits from ApplicationException and adds the concept of a "Code" that can provide additional context into the origin of the exception.
/// </summary>
public class CodedApplicationException : ApplicationException
{
    /// <summary>
    /// A code providing additional context into the origin of the exception.
    /// </summary>
    public virtual string Code { get; set; } = "CODED_APPLICATION_EXCEPTION";

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedApplicationException"/> class with the default code.
    /// </summary>
    public CodedApplicationException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedApplicationException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public CodedApplicationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedApplicationException"/> class with a specified error message and code.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    public CodedApplicationException(string message, string code) : base(message)
    {
        Code = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedApplicationException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public CodedApplicationException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CodedApplicationException"/> class with a specified error message, code, and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public CodedApplicationException(string message, string code, Exception innerException) : base(message, innerException)
    {
        Code = code;
    }
}