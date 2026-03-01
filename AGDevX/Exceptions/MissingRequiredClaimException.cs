using System;

namespace AGDevX.Exceptions;

/// <summary>
/// Exception to throw if a required Claim is missing from a ClaimsPrincipal
/// </summary>
public sealed class MissingRequiredClaimException : CodedApplicationException
{
    /// <inheritdoc />
    public override string Code { get; set; } = "MISSING_REQUIRED_CLAIM_EXCEPTION";

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingRequiredClaimException"/> class with the default code.
    /// </summary>
    public MissingRequiredClaimException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingRequiredClaimException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public MissingRequiredClaimException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingRequiredClaimException"/> class with a specified error message and code.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    public MissingRequiredClaimException(string message, string code) : base(message, code)
    {
        Code = code;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingRequiredClaimException"/> class with a specified error message and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public MissingRequiredClaimException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingRequiredClaimException"/> class with a specified error message, code, and inner exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="code">A code providing additional context into the origin of the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public MissingRequiredClaimException(string message, string code, Exception innerException) : base(message, code, innerException)
    {
        Code = code;
    }
}