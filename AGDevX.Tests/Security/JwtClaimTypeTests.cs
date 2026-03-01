using AGDevX.Enums;
using AGDevX.Security;
using Xunit;

namespace AGDevX.Tests.Security;

public sealed class JwtClaimTypeTests
{
    public class When_calling_StringValue
    {
        [Theory]
        [InlineData(JwtClaimType.Issuer, "iss")]
        [InlineData(JwtClaimType.Subject, "sub")]
        [InlineData(JwtClaimType.Audience, "aud")]
        [InlineData(JwtClaimType.Expiration, "exp")]
        [InlineData(JwtClaimType.NotBefore, "nbf")]
        [InlineData(JwtClaimType.IssuedAt, "iat")]
        [InlineData(JwtClaimType.JwtId, "jti")]
        [InlineData(JwtClaimType.Name, "name")]
        [InlineData(JwtClaimType.GivenName, "given_name")]
        [InlineData(JwtClaimType.FamilyName, "family_name")]
        [InlineData(JwtClaimType.MiddleName, "middle_name")]
        [InlineData(JwtClaimType.Nickname, "nickname")]
        [InlineData(JwtClaimType.PreferredUsername, "preferred_username")]
        [InlineData(JwtClaimType.Profile, "profile")]
        [InlineData(JwtClaimType.Picture, "picture")]
        [InlineData(JwtClaimType.Website, "website")]
        [InlineData(JwtClaimType.Email, "email")]
        [InlineData(JwtClaimType.EmailVerified, "email_verified")]
        [InlineData(JwtClaimType.Gender, "gender")]
        [InlineData(JwtClaimType.Birthdate, "birthdate")]
        [InlineData(JwtClaimType.ZoneInfo, "zoneinfo")]
        [InlineData(JwtClaimType.Locale, "locale")]
        [InlineData(JwtClaimType.PhoneNumber, "phone_number")]
        [InlineData(JwtClaimType.PhoneNumberVerified, "phone_number_verified")]
        [InlineData(JwtClaimType.Address, "address")]
        [InlineData(JwtClaimType.UpdatedAt, "updated_at")]
        [InlineData(JwtClaimType.AuthorizedParty, "azp")]
        [InlineData(JwtClaimType.Nonce, "nonce")]
        [InlineData(JwtClaimType.AuthTime, "auth_time")]
        [InlineData(JwtClaimType.AccessTokenHash, "at_hash")]
        [InlineData(JwtClaimType.CodeHash, "c_hash")]
        [InlineData(JwtClaimType.AuthenticationContextClassReference, "acr")]
        [InlineData(JwtClaimType.AuthenticationMethodsReference, "amr")]
        [InlineData(JwtClaimType.SessionId, "sid")]
        [InlineData(JwtClaimType.Scope, "scope")]
        [InlineData(JwtClaimType.ClientId, "client_id")]
        [InlineData(JwtClaimType.Roles, "roles")]
        public void Then_return_expected_string_value(JwtClaimType claimType, string expected)
        {
            //-- Act
            var result = claimType.StringValue();

            //-- Assert
            Assert.Equal(expected, result);
        }
    }
}
