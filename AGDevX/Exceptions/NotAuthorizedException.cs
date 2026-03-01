using System;

namespace AGDevX.Exceptions;

/// <summary>
/// Exception to throw if the ClaimsPrincipal is attempting to access a resource or perform a function that it is not allowed to
/// </summary>
public sealed class NotAuthorizedException : CodedApplicationException
{
    /// <inheritdoc />
    public override string Code { get; set; } = "NOT_AUTHORIZED_EXCEPTION";

    /// <summary>
    /// Initializes a new instance of the <see cref="NotAuthorizedException"/> class with the default code.
    /// </summary>
    public NotAuthorizedException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotAuthorizedException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public NotAuthorizedException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotAuthorizedException"/> class with a specified error message and code.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    public NotAuthorizedException(string message, string code) : base(message, code)
    {
        Code = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotAuthorizedException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NotAuthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NotAuthorizedException"/> class with a specified error message, code, and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public NotAuthorizedException(string message, string code, Exception innerException) : base(message, code, innerException)
    {
        Code = code;
    }
}