using System.Collections.Generic;
using System.Security.Claims;
using AGDevX.Security;
using Xunit;

namespace AGDevX.Tests.Security;

public sealed class ClaimsPrincipalExtensionsTryGetTests
{
    private static ClaimsPrincipal CreatePrincipal(params Claim[] claims)
        => new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"));

    private static ClaimsPrincipal EmptyPrincipal()
        => new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>(), "mock"));

    #region TryGetIssuer

    public class When_calling_TryGetIssuer
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("iss", "https://auth.example.com"));

            //-- Act
            var result = cp.TryGetIssuer();

            //-- Assert
            Assert.Equal("https://auth.example.com", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetIssuer();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetSubject

    public class When_calling_TryGetSubject
    {
        [Fact]
        public void And_the_sub_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("sub", "user123"));

            //-- Act
            var result = cp.TryGetSubject();

            //-- Assert
            Assert.Equal("user123", result);
        }

        [Fact]
        public void And_the_nameidentifier_claim_exists_then_return_fallback_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim(ClaimTypes.NameIdentifier, "user456"));

            //-- Act
            var result = cp.TryGetSubject();

            //-- Assert
            Assert.Equal("user456", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetSubject();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetAudiences

    public class When_calling_TryGetAudiences
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_values()
        {
            //-- Arrange
            var cp = CreatePrincipal(
                new Claim("aud", "api1"),
                new Claim("aud", "api2"));

            //-- Act
            var result = cp.TryGetAudiences();

            //-- Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains("api1", result);
            Assert.Contains("api2", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetAudiences();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetExpiration

    public class When_calling_TryGetExpiration
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("exp", "1700000000"));

            //-- Act
            var result = cp.TryGetExpiration();

            //-- Assert
            Assert.Equal(1700000000, result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetExpiration();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetNotBefore

    public class When_calling_TryGetNotBefore
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("nbf", "1699999000"));

            //-- Act
            var result = cp.TryGetNotBefore();

            //-- Assert
            Assert.Equal(1699999000, result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetNotBefore();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetIssuedAt

    public class When_calling_TryGetIssuedAt
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("iat", "1699998000"));

            //-- Act
            var result = cp.TryGetIssuedAt();

            //-- Assert
            Assert.Equal(1699998000, result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetIssuedAt();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetJwtId

    public class When_calling_TryGetJwtId
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("jti", "abc-123"));

            //-- Act
            var result = cp.TryGetJwtId();

            //-- Assert
            Assert.Equal("abc-123", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetJwtId();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetName

    public class When_calling_TryGetName
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("name", "August Geier"));

            //-- Act
            var result = cp.TryGetName();

            //-- Assert
            Assert.Equal("August Geier", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetName();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetGivenName

    public class When_calling_TryGetGivenName
    {
        [Fact]
        public void And_the_given_name_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("given_name", "August"));

            //-- Act
            var result = cp.TryGetGivenName();

            //-- Assert
            Assert.Equal("August", result);
        }

        [Fact]
        public void And_the_claimtypes_givenname_claim_exists_then_return_fallback_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim(ClaimTypes.GivenName, "August"));

            //-- Act
            var result = cp.TryGetGivenName();

            //-- Assert
            Assert.Equal("August", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetGivenName();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetFamilyName

    public class When_calling_TryGetFamilyName
    {
        [Fact]
        public void And_the_family_name_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("family_name", "Geier"));

            //-- Act
            var result = cp.TryGetFamilyName();

            //-- Assert
            Assert.Equal("Geier", result);
        }

        [Fact]
        public void And_the_claimtypes_surname_claim_exists_then_return_fallback_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim(ClaimTypes.Surname, "Geier"));

            //-- Act
            var result = cp.TryGetFamilyName();

            //-- Assert
            Assert.Equal("Geier", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetFamilyName();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetMiddleName

    public class When_calling_TryGetMiddleName
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("middle_name", "James"));

            //-- Act
            var result = cp.TryGetMiddleName();

            //-- Assert
            Assert.Equal("James", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetMiddleName();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region Simple String Claims (Nickname through ClientId)

    public class When_calling_TryGetNickname
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("nickname", "auggie"));
            Assert.Equal("auggie", cp.TryGetNickname());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetNickname());
        }
    }

    public class When_calling_TryGetPreferredUsername
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("preferred_username", "august"));
            Assert.Equal("august", cp.TryGetPreferredUsername());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetPreferredUsername());
        }
    }

    public class When_calling_TryGetProfile
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("profile", "https://example.com/august"));
            Assert.Equal("https://example.com/august", cp.TryGetProfile());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetProfile());
        }
    }

    public class When_calling_TryGetPicture
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("picture", "https://example.com/photo.jpg"));
            Assert.Equal("https://example.com/photo.jpg", cp.TryGetPicture());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetPicture());
        }
    }

    public class When_calling_TryGetWebsite
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("website", "https://example.com"));
            Assert.Equal("https://example.com", cp.TryGetWebsite());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetWebsite());
        }
    }

    public class When_calling_TryGetEmail
    {
        [Fact]
        public void And_the_email_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("email", "agdevx@example.com"));

            //-- Act
            var result = cp.TryGetEmail();

            //-- Assert
            Assert.Equal("agdevx@example.com", result);
        }

        [Fact]
        public void And_the_claimtypes_email_claim_exists_then_return_fallback_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim(ClaimTypes.Email, "agdevx@example.com"));

            //-- Act
            var result = cp.TryGetEmail();

            //-- Assert
            Assert.Equal("agdevx@example.com", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetEmail();

            //-- Assert
            Assert.Null(result);
        }
    }

    public class When_calling_TryGetEmailVerified
    {
        [Fact]
        public void And_the_claim_exists_with_true_then_return_true()
        {
            var cp = CreatePrincipal(new Claim("email_verified", "true"));
            Assert.True(cp.TryGetEmailVerified());
        }

        [Fact]
        public void And_the_claim_exists_with_false_then_return_false()
        {
            var cp = CreatePrincipal(new Claim("email_verified", "false"));
            Assert.False(cp.TryGetEmailVerified());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetEmailVerified());
        }
    }

    public class When_calling_TryGetGender
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("gender", "male"));
            Assert.Equal("male", cp.TryGetGender());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetGender());
        }
    }

    public class When_calling_TryGetBirthdate
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("birthdate", "1990-01-15"));
            Assert.Equal("1990-01-15", cp.TryGetBirthdate());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetBirthdate());
        }
    }

    public class When_calling_TryGetZoneInfo
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("zoneinfo", "America/Chicago"));
            Assert.Equal("America/Chicago", cp.TryGetZoneInfo());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetZoneInfo());
        }
    }

    public class When_calling_TryGetLocale
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("locale", "en-US"));
            Assert.Equal("en-US", cp.TryGetLocale());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetLocale());
        }
    }

    public class When_calling_TryGetPhoneNumber
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("phone_number", "+15551234567"));
            Assert.Equal("+15551234567", cp.TryGetPhoneNumber());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetPhoneNumber());
        }
    }

    public class When_calling_TryGetPhoneNumberVerified
    {
        [Fact]
        public void And_the_claim_exists_with_true_then_return_true()
        {
            var cp = CreatePrincipal(new Claim("phone_number_verified", "true"));
            Assert.True(cp.TryGetPhoneNumberVerified());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetPhoneNumberVerified());
        }
    }

    public class When_calling_TryGetAddress
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("address", "{\"street_address\":\"123 Main St\"}"));
            Assert.Equal("{\"street_address\":\"123 Main St\"}", cp.TryGetAddress());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetAddress());
        }
    }

    public class When_calling_TryGetUpdatedAt
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("updated_at", "1700001000"));
            Assert.Equal(1700001000, cp.TryGetUpdatedAt());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetUpdatedAt());
        }
    }

    public class When_calling_TryGetAuthorizedParty
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("azp", "my-client-app"));
            Assert.Equal("my-client-app", cp.TryGetAuthorizedParty());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetAuthorizedParty());
        }
    }

    public class When_calling_TryGetNonce
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("nonce", "nonce123"));
            Assert.Equal("nonce123", cp.TryGetNonce());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetNonce());
        }
    }

    public class When_calling_TryGetAuthTime
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("auth_time", "1699997000"));
            Assert.Equal(1699997000, cp.TryGetAuthTime());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetAuthTime());
        }
    }

    public class When_calling_TryGetAccessTokenHash
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("at_hash", "hash123"));
            Assert.Equal("hash123", cp.TryGetAccessTokenHash());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetAccessTokenHash());
        }
    }

    public class When_calling_TryGetCodeHash
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("c_hash", "codehash456"));
            Assert.Equal("codehash456", cp.TryGetCodeHash());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetCodeHash());
        }
    }

    public class When_calling_TryGetAuthenticationContextClassReference
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("acr", "urn:mace:incommon:iap:silver"));
            Assert.Equal("urn:mace:incommon:iap:silver", cp.TryGetAuthenticationContextClassReference());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetAuthenticationContextClassReference());
        }
    }

    public class When_calling_TryGetAuthenticationMethodsReference
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("amr", "mfa"));
            Assert.Equal("mfa", cp.TryGetAuthenticationMethodsReference());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetAuthenticationMethodsReference());
        }
    }

    public class When_calling_TryGetSessionId
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("sid", "session-abc"));
            Assert.Equal("session-abc", cp.TryGetSessionId());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetSessionId());
        }
    }

    public class When_calling_TryGetClientId
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("client_id", "my-client"));
            Assert.Equal("my-client", cp.TryGetClientId());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetClientId());
        }
    }

    #endregion

    #region TryGetScopes

    public class When_calling_TryGetScopes
    {
        [Fact]
        public void And_the_claim_exists_then_return_split_values()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("scope", "openid profile email"));

            //-- Act
            var result = cp.TryGetScopes();

            //-- Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Contains("openid", result);
            Assert.Contains("profile", result);
            Assert.Contains("email", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetScopes();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetRoles

    public class When_calling_TryGetRoles
    {
        [Fact]
        public void And_the_claim_exists_then_return_split_values()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("roles", "admin user"));

            //-- Act
            var result = cp.TryGetRoles();

            //-- Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains("admin", result);
            Assert.Contains("user", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetRoles();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetExternalId

    public class When_calling_TryGetExternalId
    {
        [Fact]
        public void And_the_subject_claim_exists_then_return_subject_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("sub", "ext-user-123"));

            //-- Act
            var result = cp.TryGetExternalId();

            //-- Assert
            Assert.Equal("ext-user-123", result);
        }

        [Fact]
        public void And_the_user_id_claim_exists_then_return_user_id_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("user_id", "custom-user-456"));

            //-- Act
            var result = cp.TryGetExternalId();

            //-- Assert
            Assert.Equal("custom-user-456", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act
            var result = cp.TryGetExternalId();

            //-- Assert
            Assert.Null(result);
        }
    }

    #endregion

    #region TryGetIsActive

    public class When_calling_TryGetIsActive
    {
        [Fact]
        public void And_the_claim_exists_with_true_then_return_true()
        {
            var cp = CreatePrincipal(new Claim("isActive", "true"));
            Assert.True(cp.TryGetIsActive());
        }

        [Fact]
        public void And_the_claim_exists_with_false_then_return_false()
        {
            var cp = CreatePrincipal(new Claim("isActive", "false"));
            Assert.False(cp.TryGetIsActive());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetIsActive());
        }
    }

    #endregion

    #region TryGetGrantType

    public class When_calling_TryGetGrantType
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("gty", "client-credentials"));
            Assert.Equal("client-credentials", cp.TryGetGrantType());
        }

        [Fact]
        public void And_the_claim_is_missing_then_return_null()
        {
            Assert.Null(EmptyPrincipal().TryGetGrantType());
        }
    }

    #endregion

    #region Case-Insensitive Claim Lookup

    public class When_looking_up_claims_with_different_casing
    {
        [Fact]
        public void And_the_claim_type_has_different_casing_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("EMAIL", "test@example.com"));

            //-- Act
            var result = cp.TryGetEmail();

            //-- Assert
            Assert.Equal("test@example.com", result);
        }
    }

    #endregion
}
