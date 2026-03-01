using System.Collections.Generic;
using System.Security.Claims;
using AGDevX.Exceptions;
using AGDevX.Security;
using Xunit;

namespace AGDevX.Tests.Security;

public sealed class ClaimsPrincipalExtensionsGetTests
{
    private static ClaimsPrincipal CreatePrincipal(params Claim[] claims)
        => new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"));

    private static ClaimsPrincipal EmptyPrincipal()
        => new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>(), "mock"));

    #region GetIssuer

    public class When_calling_GetIssuer
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("iss", "https://auth.example.com"));

            //-- Act
            var result = cp.GetIssuer();

            //-- Assert
            Assert.Equal("https://auth.example.com", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act & Assert
            Assert.Throws<ClaimNotFoundException>(() => cp.GetIssuer());
        }
    }

    #endregion

    #region GetSubject

    public class When_calling_GetSubject
    {
        [Fact]
        public void And_the_sub_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim("sub", "user123"));

            //-- Act
            var result = cp.GetSubject();

            //-- Assert
            Assert.Equal("user123", result);
        }

        [Fact]
        public void And_the_nameidentifier_claim_exists_then_return_fallback_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim(ClaimTypes.NameIdentifier, "user456"));

            //-- Act
            var result = cp.GetSubject();

            //-- Assert
            Assert.Equal("user456", result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            //-- Arrange
            var cp = EmptyPrincipal();

            //-- Act & Assert
            Assert.Throws<ClaimNotFoundException>(() => cp.GetSubject());
        }
    }

    #endregion

    #region GetAudiences

    public class When_calling_GetAudiences
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_values()
        {
            //-- Arrange
            var cp = CreatePrincipal(
                new Claim("aud", "api1"),
                new Claim("aud", "api2"));

            //-- Act
            var result = cp.GetAudiences();

            //-- Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetAudiences());
        }
    }

    #endregion

    #region Value Type Claims (int)

    public class When_calling_GetExpiration
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("exp", "1700000000"));
            Assert.Equal(1700000000, cp.GetExpiration());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetExpiration());
        }
    }

    public class When_calling_GetNotBefore
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("nbf", "1699999000"));
            Assert.Equal(1699999000, cp.GetNotBefore());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetNotBefore());
        }
    }

    public class When_calling_GetIssuedAt
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("iat", "1699998000"));
            Assert.Equal(1699998000, cp.GetIssuedAt());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetIssuedAt());
        }
    }

    public class When_calling_GetUpdatedAt
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("updated_at", "1700001000"));
            Assert.Equal(1700001000, cp.GetUpdatedAt());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetUpdatedAt());
        }
    }

    public class When_calling_GetAuthTime
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("auth_time", "1699997000"));
            Assert.Equal(1699997000, cp.GetAuthTime());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetAuthTime());
        }
    }

    #endregion

    #region Simple String Claims

    public class When_calling_GetJwtId
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("jti", "abc-123"));
            Assert.Equal("abc-123", cp.GetJwtId());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetJwtId());
        }
    }

    public class When_calling_GetName
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("name", "August Geier"));
            Assert.Equal("August Geier", cp.GetName());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetName());
        }
    }

    public class When_calling_GetGivenName
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("given_name", "August"));
            Assert.Equal("August", cp.GetGivenName());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetGivenName());
        }
    }

    public class When_calling_GetFamilyName
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("family_name", "Geier"));
            Assert.Equal("Geier", cp.GetFamilyName());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetFamilyName());
        }
    }

    public class When_calling_GetMiddleName
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("middle_name", "James"));
            Assert.Equal("James", cp.GetMiddleName());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetMiddleName());
        }
    }

    public class When_calling_GetNickname
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("nickname", "auggie"));
            Assert.Equal("auggie", cp.GetNickname());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetNickname());
        }
    }

    public class When_calling_GetPreferredUsername
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("preferred_username", "august"));
            Assert.Equal("august", cp.GetPreferredUsername());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetPreferredUsername());
        }
    }

    public class When_calling_GetProfile
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("profile", "https://example.com/august"));
            Assert.Equal("https://example.com/august", cp.GetProfile());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetProfile());
        }
    }

    public class When_calling_GetPicture
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("picture", "https://example.com/photo.jpg"));
            Assert.Equal("https://example.com/photo.jpg", cp.GetPicture());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetPicture());
        }
    }

    public class When_calling_GetWebsite
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("website", "https://example.com"));
            Assert.Equal("https://example.com", cp.GetWebsite());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetWebsite());
        }
    }

    public class When_calling_GetEmail
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim(ClaimTypes.Email, "agdevx@reddwarfjmcagdx.com"));

            //-- Act
            var result = cp.GetEmail();

            //-- Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            //-- Arrange
            var cp = CreatePrincipal(new Claim(ClaimTypes.Name, "August Geier"));

            //-- Act & Assert
            Assert.Throws<ClaimNotFoundException>(() => cp.GetEmail());
        }
    }

    public class When_calling_GetEmailVerified
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("email_verified", "true"));
            Assert.True(cp.GetEmailVerified());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetEmailVerified());
        }
    }

    public class When_calling_GetGender
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("gender", "male"));
            Assert.Equal("male", cp.GetGender());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetGender());
        }
    }

    public class When_calling_GetBirthdate
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("birthdate", "1990-01-15"));
            Assert.Equal("1990-01-15", cp.GetBirthdate());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetBirthdate());
        }
    }

    public class When_calling_GetZoneInfo
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("zoneinfo", "America/Chicago"));
            Assert.Equal("America/Chicago", cp.GetZoneInfo());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetZoneInfo());
        }
    }

    public class When_calling_GetLocale
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("locale", "en-US"));
            Assert.Equal("en-US", cp.GetLocale());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetLocale());
        }
    }

    public class When_calling_GetPhoneNumber
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("phone_number", "+15551234567"));
            Assert.Equal("+15551234567", cp.GetPhoneNumber());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetPhoneNumber());
        }
    }

    public class When_calling_GetPhoneNumberVerified
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("phone_number_verified", "true"));
            Assert.True(cp.GetPhoneNumberVerified());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetPhoneNumberVerified());
        }
    }

    public class When_calling_GetAddress
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("address", "{\"street_address\":\"123 Main St\"}"));
            Assert.Equal("{\"street_address\":\"123 Main St\"}", cp.GetAddress());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetAddress());
        }
    }

    public class When_calling_GetAuthorizedParty
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("azp", "my-client-app"));
            Assert.Equal("my-client-app", cp.GetAuthorizedParty());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetAuthorizedParty());
        }
    }

    public class When_calling_GetNonce
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("nonce", "nonce123"));
            Assert.Equal("nonce123", cp.GetNonce());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetNonce());
        }
    }

    public class When_calling_GetAccessTokenHash
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("at_hash", "hash123"));
            Assert.Equal("hash123", cp.GetAccessTokenHash());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetAccessTokenHash());
        }
    }

    public class When_calling_GetCodeHash
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("c_hash", "codehash456"));
            Assert.Equal("codehash456", cp.GetCodeHash());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetCodeHash());
        }
    }

    public class When_calling_GetAuthenticationContextClassReference
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("acr", "urn:mace:incommon:iap:silver"));
            Assert.Equal("urn:mace:incommon:iap:silver", cp.GetAuthenticationContextClassReference());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetAuthenticationContextClassReference());
        }
    }

    public class When_calling_GetAuthenticationMethodsReference
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("amr", "mfa"));
            Assert.Equal("mfa", cp.GetAuthenticationMethodsReference());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetAuthenticationMethodsReference());
        }
    }

    public class When_calling_GetSessionId
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("sid", "session-abc"));
            Assert.Equal("session-abc", cp.GetSessionId());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetSessionId());
        }
    }

    public class When_calling_GetClientId
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("client_id", "my-client"));
            Assert.Equal("my-client", cp.GetClientId());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetClientId());
        }
    }

    #endregion

    #region List Claims

    public class When_calling_GetScopes
    {
        [Fact]
        public void And_the_claim_exists_then_return_split_values()
        {
            var cp = CreatePrincipal(new Claim("scope", "openid profile"));
            var result = cp.GetScopes();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetScopes());
        }
    }

    public class When_calling_GetRoles
    {
        [Fact]
        public void And_the_claim_exists_then_return_split_values()
        {
            var cp = CreatePrincipal(new Claim("roles", "admin user"));
            var result = cp.GetRoles();
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetRoles());
        }
    }

    #endregion

    #region ExternalId

    public class When_calling_GetExternalId
    {
        [Fact]
        public void And_the_subject_claim_exists_then_return_value()
        {
            var cp = CreatePrincipal(new Claim("sub", "ext-user-123"));
            Assert.Equal("ext-user-123", cp.GetExternalId());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetExternalId());
        }
    }

    #endregion

    #region IsActive

    public class When_calling_GetIsActive
    {
        [Fact]
        public void And_the_claim_exists_then_return_value()
        {
            var cp = CreatePrincipal(new Claim("isActive", "true"));
            Assert.True(cp.GetIsActive());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetIsActive());
        }
    }

    #endregion

    #region GrantType

    public class When_calling_GetGrantType
    {
        [Fact]
        public void And_the_claim_exists_then_return_claim_value()
        {
            var cp = CreatePrincipal(new Claim("gty", "client-credentials"));
            Assert.Equal("client-credentials", cp.GetGrantType());
        }

        [Fact]
        public void And_the_claim_is_missing_then_throw_ClaimNotFoundException()
        {
            Assert.Throws<ClaimNotFoundException>(() => EmptyPrincipal().GetGrantType());
        }
    }

    #endregion
}
