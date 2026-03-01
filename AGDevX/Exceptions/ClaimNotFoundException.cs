using System;

namespace AGDevX.Exceptions;

/// <summary>
/// Exception to throw if a claim is unable to be retrieved from a ClaimsPrincipal
/// </summary>
public sealed class ClaimNotFoundException : CodedApplicationException
{
    /// <inheritdoc />
    public override string Code { get; set; } = "CLAIM_NOT_FOUND_EXCEPTION";

    /// <summary>
    /// Initializes a new instance of the <see cref="ClaimNotFoundException"/> class with the default code.
    /// </summary>
    public ClaimNotFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClaimNotFoundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public ClaimNotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClaimNotFoundException"/> class with a specified error message and code.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    public ClaimNotFoundException(string message, string code) : base(message, code)
    {
        Code = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClaimNotFoundException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ClaimNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClaimNotFoundException"/> class with a specified error message, code, and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public ClaimNotFoundException(string message, string code, Exception innerException) : base(message, code, innerException)
    {
        Code = code;
    }
}