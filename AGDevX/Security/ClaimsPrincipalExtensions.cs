using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using AGDevX.Enums;
using AGDevX.Exceptions;
using AGDevX.IEnumerables;
using AGDevX.Strings;

namespace AGDevX.Security;

/// <summary>
/// Extensions for pulling Claims out of a ClaimsPrincipal
/// </summary>
public static class ClaimsPrincipalExtensions
{
    #region Issuer

    /// <summary>
    /// Returns the Issuer claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetIssuer(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetIssuer()
                    ?? throw new ClaimNotFoundException("An Issuer claim was not found");
    }

    /// <summary>
    /// Returns the Issuer claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetIssuer(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Issuer.StringValue());
    }

    #endregion

    #region Subject

    /// <summary>
    /// Returns the Subject claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetSubject(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetSubject()
                    ?? throw new ClaimNotFoundException("A Subject claim was not found");
    }

    /// <summary>
    /// Returns the Subject claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetSubject(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Subject.StringValue())
                    ?? claimsPrincipal.GetClaimValue<string>(ClaimTypes.NameIdentifier);
    }

    #endregion

    #region Audience

    /// <summary>
    /// Returns the Audience claim values as a read-only list. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static IReadOnlyList<string> GetAudiences(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetAudiences()
                    ?? throw new ClaimNotFoundException("An Audience claim was not found");
    }

    /// <summary>
    /// Returns the Audience claim values as a read-only list, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static IReadOnlyList<string>? TryGetAudiences(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValues<string>(JwtClaimType.Audience.StringValue());
    }

    #endregion

    #region Expiration

    /// <summary>
    /// Returns the Expiration claim value as an integer. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static int GetExpiration(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetExpiration()
                    ?? throw new ClaimNotFoundException("An Expiration claim was not found");
    }

    /// <summary>
    /// Returns the Expiration claim value as an integer, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static int? TryGetExpiration(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaimType.Expiration.StringValue());
        if (claim == null) return null;
        return (int)Convert.ChangeType(claim.Value, typeof(int), CultureInfo.InvariantCulture);
    }

    #endregion

    #region NotBefore

    /// <summary>
    /// Returns the NotBefore claim value as an integer. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static int GetNotBefore(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetNotBefore()
                    ?? throw new ClaimNotFoundException("A NotBefore claim was not found");
    }

    /// <summary>
    /// Returns the NotBefore claim value as an integer, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static int? TryGetNotBefore(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaimType.NotBefore.StringValue());
        if (claim == null) return null;
        return (int)Convert.ChangeType(claim.Value, typeof(int), CultureInfo.InvariantCulture);
    }

    #endregion

    #region IssuedAt

    /// <summary>
    /// Returns the IssuedAt claim value as an integer. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static int GetIssuedAt(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetIssuedAt()
                    ?? throw new ClaimNotFoundException("An IssuedAt claim was not found");
    }

    /// <summary>
    /// Returns the IssuedAt claim value as an integer, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static int? TryGetIssuedAt(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaimType.IssuedAt.StringValue());
        if (claim == null) return null;
        return (int)Convert.ChangeType(claim.Value, typeof(int), CultureInfo.InvariantCulture);
    }

    #endregion

    #region JwtId

    /// <summary>
    /// Returns the JwtId claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetJwtId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetJwtId()
                    ?? throw new ClaimNotFoundException("A JwtId claim was not found");
    }

    /// <summary>
    /// Returns the JwtId claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetJwtId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.JwtId.StringValue());
    }

    #endregion

    #region Name

    /// <summary>
    /// Returns the Name claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetName()
                    ?? throw new ClaimNotFoundException("A Name claim was not found");
    }

    /// <summary>
    /// Returns the Name claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Name.StringValue());
    }

    #endregion

    #region GivenName

    /// <summary>
    /// Returns the GivenName claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetGivenName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetGivenName()
                    ?? throw new ClaimNotFoundException("A Given Name claim was not found");
    }

    /// <summary>
    /// Returns the GivenName claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetGivenName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.GivenName.StringValue())
                    ?? claimsPrincipal.GetClaimValue<string>(ClaimTypes.GivenName);
    }

    #endregion

    #region FamilyName

    /// <summary>
    /// Returns the FamilyName claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetFamilyName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetFamilyName()
                    ?? throw new ClaimNotFoundException("A Family Name claim was not found");
    }

    /// <summary>
    /// Returns the FamilyName claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetFamilyName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.FamilyName.StringValue())
                    ?? claimsPrincipal.GetClaimValue<string>(ClaimTypes.Surname);
    }

    #endregion

    #region MiddleName

    /// <summary>
    /// Returns the MiddleName claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetMiddleName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetMiddleName()
                    ?? throw new ClaimNotFoundException("A Middle Name claim was not found");
    }

    /// <summary>
    /// Returns the MiddleName claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetMiddleName(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.MiddleName.StringValue());
    }

    #endregion

    #region Nickname

    /// <summary>
    /// Returns the Nickname claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetNickname(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetNickname()
                    ?? throw new ClaimNotFoundException("A Nickname claim was not found");
    }

    /// <summary>
    /// Returns the Nickname claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetNickname(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Nickname.StringValue());
    }

    #endregion

    #region PreferredUsername

    /// <summary>
    /// Returns the PreferredUsername claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetPreferredUsername(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetPreferredUsername()
                    ?? throw new ClaimNotFoundException("A Preferred Username claim was not found");
    }

    /// <summary>
    /// Returns the PreferredUsername claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetPreferredUsername(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.PreferredUsername.StringValue());
    }

    #endregion

    #region Profile

    /// <summary>
    /// Returns the Profile claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetProfile(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetProfile()
                    ?? throw new ClaimNotFoundException("A Profile claim was not found");
    }

    /// <summary>
    /// Returns the Profile claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetProfile(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Profile.StringValue());
    }

    #endregion

    #region Picture

    /// <summary>
    /// Returns the Picture claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetPicture(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetPicture()
                    ?? throw new ClaimNotFoundException("A Picture claim was not found");
    }

    /// <summary>
    /// Returns the Picture claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetPicture(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Picture.StringValue());
    }

    #endregion

    #region Website

    /// <summary>
    /// Returns the Website claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetWebsite(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetWebsite()
                    ?? throw new ClaimNotFoundException("A Website claim was not found");
    }

    /// <summary>
    /// Returns the Website claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetWebsite(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Website.StringValue());
    }

    #endregion

    #region Email

    /// <summary>
    /// Returns the Email claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetEmail()
                    ?? throw new ClaimNotFoundException("An Email claim was not found");
    }

    /// <summary>
    /// Returns the Email claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetEmail(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Email.StringValue())
                    ?? claimsPrincipal.GetClaimValue<string>(ClaimTypes.Email);
    }

    #endregion

    #region EmailVerified

    /// <summary>
    /// Returns the EmailVerified claim value as a boolean. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static bool GetEmailVerified(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetEmailVerified()
                    ?? throw new ClaimNotFoundException("An EmailVerified claim was not found");
    }

    /// <summary>
    /// Returns the EmailVerified claim value as a boolean, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static bool? TryGetEmailVerified(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaimType.EmailVerified.StringValue());
        if (claim == null) return null;
        return (bool)Convert.ChangeType(claim.Value, typeof(bool), CultureInfo.InvariantCulture);
    }

    #endregion

    #region Gender

    /// <summary>
    /// Returns the Gender claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetGender(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetGender()
                    ?? throw new ClaimNotFoundException("A Gender claim was not found");
    }

    /// <summary>
    /// Returns the Gender claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetGender(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Gender.StringValue());
    }

    #endregion

    #region Birthdate

    /// <summary>
    /// Returns the Birthdate claim value parsed as a <see cref="DateTime"/>.
    /// Throws <see cref="ClaimNotFoundException"/> if the claim is missing or cannot be parsed.
    /// </summary>
    public static DateTime GetBirthdate(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetBirthdate()
                    ?? throw new ClaimNotFoundException("A Birthdate claim was not found");
    }

    /// <summary>
    /// Returns the Birthdate claim value parsed as a <see cref="DateTime"/>,
    /// or <see langword="null"/> if the claim is missing or the value cannot be parsed.
    /// </summary>
    public static DateTime? TryGetBirthdate(this ClaimsPrincipal claimsPrincipal)
    {
        var value = claimsPrincipal.GetClaimValue<string>(JwtClaimType.Birthdate.StringValue());
        if (value == null) return null;
        return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) ? date : null;
    }

    #endregion

    #region ZoneInfo

    /// <summary>
    /// Returns the ZoneInfo claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetZoneInfo(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetZoneInfo()
                    ?? throw new ClaimNotFoundException("A ZoneInfo claim was not found");
    }

    /// <summary>
    /// Returns the ZoneInfo claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetZoneInfo(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.ZoneInfo.StringValue());
    }

    #endregion

    #region Locale

    /// <summary>
    /// Returns the Locale claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetLocale(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetLocale()
                    ?? throw new ClaimNotFoundException("A Locale claim was not found");
    }

    /// <summary>
    /// Returns the Locale claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetLocale(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Locale.StringValue());
    }

    #endregion

    #region PhoneNumber

    /// <summary>
    /// Returns the PhoneNumber claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetPhoneNumber()
                    ?? throw new ClaimNotFoundException("A PhoneNumber claim was not found");
    }

    /// <summary>
    /// Returns the PhoneNumber claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetPhoneNumber(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.PhoneNumber.StringValue());
    }

    #endregion

    #region PhoneNumberVerified

    /// <summary>
    /// Returns the PhoneNumberVerified claim value as a boolean. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static bool GetPhoneNumberVerified(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetPhoneNumberVerified()
                    ?? throw new ClaimNotFoundException("A PhoneNumberVerified claim was not found");
    }

    /// <summary>
    /// Returns the PhoneNumberVerified claim value as a boolean, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static bool? TryGetPhoneNumberVerified(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaimType.PhoneNumberVerified.StringValue());
        if (claim == null) return null;
        return (bool)Convert.ChangeType(claim.Value, typeof(bool), CultureInfo.InvariantCulture);
    }

    #endregion

    #region Address

    /// <summary>
    /// Returns the Address claim value parsed as a <see cref="JsonElement"/>.
    /// Throws <see cref="ClaimNotFoundException"/> if the claim is missing or the value is not valid JSON.
    /// </summary>
    public static JsonElement GetAddress(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetAddress()
                    ?? throw new ClaimNotFoundException("An Address claim was not found");
    }

    /// <summary>
    /// Returns the Address claim value parsed as a <see cref="JsonElement"/>,
    /// or <see langword="null"/> if the claim is missing or the value is not valid JSON.
    /// </summary>
    /// <remarks>
    /// The underlying <see cref="JsonDocument"/> is not disposed because <see cref="JsonElement"/>
    /// becomes invalid after its parent document is disposed. Callers should not hold long-lived
    /// references to the returned value in memory-sensitive scenarios.
    /// </remarks>
    public static JsonElement? TryGetAddress(this ClaimsPrincipal claimsPrincipal)
    {
        var value = claimsPrincipal.GetClaimValue<string>(JwtClaimType.Address.StringValue());
        if (value == null) return null;
        try
        {
            return JsonDocument.Parse(value).RootElement;
        }
        catch (JsonException)
        {
            return null;
        }
    }

    #endregion

    #region UpdatedAt

    /// <summary>
    /// Returns the UpdatedAt claim value as an integer. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static int GetUpdatedAt(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetUpdatedAt()
                    ?? throw new ClaimNotFoundException("An UpdatedAt claim was not found");
    }

    /// <summary>
    /// Returns the UpdatedAt claim value as an integer, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static int? TryGetUpdatedAt(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaimType.UpdatedAt.StringValue());
        if (claim == null) return null;
        return (int)Convert.ChangeType(claim.Value, typeof(int), CultureInfo.InvariantCulture);
    }

    #endregion

    #region AuthorizedParty

    /// <summary>
    /// Returns the AuthorizedParty claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetAuthorizedParty(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetAuthorizedParty()
                    ?? throw new ClaimNotFoundException("An AuthorizedParty claim was not found");
    }

    /// <summary>
    /// Returns the AuthorizedParty claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetAuthorizedParty(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.AuthorizedParty.StringValue());
    }

    #endregion

    #region Nonce

    /// <summary>
    /// Returns the Nonce claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetNonce(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetNonce()
                    ?? throw new ClaimNotFoundException("A Nonce claim was not found");
    }

    /// <summary>
    /// Returns the Nonce claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetNonce(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.Nonce.StringValue());
    }

    #endregion

    #region AuthTime

    /// <summary>
    /// Returns the AuthTime claim value as an integer. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static int GetAuthTime(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetAuthTime()
                    ?? throw new ClaimNotFoundException("An AuthTime claim was not found");
    }

    /// <summary>
    /// Returns the AuthTime claim value as an integer, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static int? TryGetAuthTime(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(JwtClaimType.AuthTime.StringValue());
        if (claim == null) return null;
        return (int)Convert.ChangeType(claim.Value, typeof(int), CultureInfo.InvariantCulture);
    }

    #endregion

    #region AccessTokenHash

    /// <summary>
    /// Returns the AccessTokenHash claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetAccessTokenHash(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetAccessTokenHash()
                    ?? throw new ClaimNotFoundException("An AccessTokenHash claim was not found");
    }

    /// <summary>
    /// Returns the AccessTokenHash claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetAccessTokenHash(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.AccessTokenHash.StringValue());
    }

    #endregion

    #region CodeHash

    /// <summary>
    /// Returns the CodeHash claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetCodeHash(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetCodeHash()
                    ?? throw new ClaimNotFoundException("A CodeHash claim was not found");
    }

    /// <summary>
    /// Returns the CodeHash claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetCodeHash(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.CodeHash.StringValue());
    }

    #endregion

    #region AuthenticationContextClassReference

    /// <summary>
    /// Returns the AuthenticationContextClassReference claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetAuthenticationContextClassReference(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetAuthenticationContextClassReference()
                    ?? throw new ClaimNotFoundException("An AuthenticationContextClassReference claim was not found");
    }

    /// <summary>
    /// Returns the AuthenticationContextClassReference claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetAuthenticationContextClassReference(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.AuthenticationContextClassReference.StringValue());
    }

    #endregion

    #region AuthenticationMethodsReference

    /// <summary>
    /// Returns the AuthenticationMethodsReference claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetAuthenticationMethodsReference(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetAuthenticationMethodsReference()
                    ?? throw new ClaimNotFoundException("An AuthenticationMethodsReference claim was not found");
    }

    /// <summary>
    /// Returns the AuthenticationMethodsReference claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetAuthenticationMethodsReference(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.AuthenticationMethodsReference.StringValue());
    }

    #endregion

    #region SessionId

    /// <summary>
    /// Returns the SessionId claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetSessionId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetSessionId()
                    ?? throw new ClaimNotFoundException("A SessionId claim was not found");
    }

    /// <summary>
    /// Returns the SessionId claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetSessionId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.SessionId.StringValue());
    }

    #endregion

    #region Scope

    /// <summary>
    /// Returns the Scope claim values as a read-only list. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static IReadOnlyList<string> GetScopes(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetScopes()
                    ?? throw new ClaimNotFoundException("A Scope claim was not found");
    }

    /// <summary>
    /// Returns the Scope claim values as a read-only list, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static IReadOnlyList<string>? TryGetScopes(this ClaimsPrincipal claimsPrincipal)
    {
        var scopeStr = claimsPrincipal.GetClaimValue<string>(JwtClaimType.Scope.StringValue());
        return scopeStr?.Split(' ').ToList();
    }

    #endregion

    #region ClientId

    /// <summary>
    /// Returns the ClientId claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetClientId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetClientId()
                    ?? throw new ClaimNotFoundException("A ClientId claim was not found");
    }

    /// <summary>
    /// Returns the ClientId claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetClientId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(JwtClaimType.ClientId.StringValue());
    }

    #endregion

    #region Roles

    /// <summary>
    /// Returns the Roles claim values as a read-only list. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static IReadOnlyList<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetRoles()
                    ?? throw new ClaimNotFoundException("A Roles claim was not found");
    }

    /// <summary>
    /// Returns the Roles claim values as a read-only list, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static IReadOnlyList<string>? TryGetRoles(this ClaimsPrincipal claimsPrincipal)
    {
        var rolesStr = claimsPrincipal.GetClaimValue<string>(JwtClaimType.Roles.StringValue());
        return rolesStr?.Split(' ').ToList();
    }

    #endregion

    #region ExternalId

    /// <summary>
    /// Returns the ExternalId claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetExternalId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetExternalId()
                    ?? throw new ClaimNotFoundException("An External Id claim was not found");
    }

    /// <summary>
    /// Returns the ExternalId claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetExternalId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetSubject()
                    ?? claimsPrincipal.GetClaimValue<string>(CustomClaimType.UserId.StringValue());
    }

    #endregion

    #region IsActive

    /// <summary>
    /// Returns the IsActive claim value as a boolean. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static bool GetIsActive(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetIsActive()
                    ?? throw new ClaimNotFoundException("An IsActive claim was not found");
    }

    /// <summary>
    /// Returns the IsActive claim value as a boolean, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static bool? TryGetIsActive(this ClaimsPrincipal claimsPrincipal)
    {
        var claim = claimsPrincipal.GetClaim(CustomClaimType.IsActive.StringValue());
        if (claim == null) return null;
        return (bool)Convert.ChangeType(claim.Value, typeof(bool), CultureInfo.InvariantCulture);
    }

    #endregion

    #region GrantType

    /// <summary>
    /// Returns the GrantType claim value. Throws <see cref="ClaimNotFoundException"/> if the claim is missing.
    /// </summary>
    public static string GetGrantType(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.TryGetGrantType()
                    ?? throw new ClaimNotFoundException("A Grant Type claim was not found");
    }

    /// <summary>
    /// Returns the GrantType claim value, or <see langword="null"/> if the claim is missing.
    /// </summary>
    public static string? TryGetGrantType(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal.GetClaimValue<string>(Auth0ClaimType.GrantType.StringValue());
    }

    #endregion

    #region Private Helpers

    private static T? GetClaimValue<T>(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var claim = claimsPrincipal.GetClaim(claimType);

        if (claim == null)
        {
            return default;
        }

        return (T)Convert.ChangeType(claim.Value, typeof(T), CultureInfo.InvariantCulture);
    }

    private static Claim? GetClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        if (claimType.IsNullOrWhiteSpace())
        {
            return default;
        }

        return claimsPrincipal.FindFirst(c => c.Type.EqualsIgnoreCase(claimType));
    }

    private static List<T>? GetClaimValues<T>(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var claims = claimsPrincipal.GetClaims(claimType);

        if (claims.IsNullOrEmpty())
        {
            return default;
        }

        return claims!.Select(c => (T)Convert.ChangeType(c.Value, typeof(T), CultureInfo.InvariantCulture)).ToList();
    }

    private static List<Claim>? GetClaims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        if (claimType.IsNullOrWhiteSpace())
        {
            return default;
        }

        var claims = claimsPrincipal.FindAll(c => c.Type.EqualsIgnoreCase(claimType)).ToList();
        return claims.Count == 0 ? default : claims;
    }

    #endregion
}
