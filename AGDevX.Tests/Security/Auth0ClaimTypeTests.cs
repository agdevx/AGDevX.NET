using AGDevX.Enums;
using AGDevX.Security;
using Xunit;

namespace AGDevX.Tests.Security;

public sealed class Auth0ClaimTypeTests
{
    public class When_calling_StringValue
    {
        [Theory]
        [InlineData(Auth0ClaimType.GrantType, "gty")]
        public void Then_return_expected_string_value(Auth0ClaimType claimType, string expected)
        {
            //-- Act
            var result = claimType.StringValue();

            //-- Assert
            Assert.Equal(expected, result);
        }
    }
}
