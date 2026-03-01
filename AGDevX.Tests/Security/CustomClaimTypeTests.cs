using AGDevX.Enums;
using AGDevX.Security;
using Xunit;

namespace AGDevX.Tests.Security;

public sealed class CustomClaimTypeTests
{
    public class When_calling_StringValue
    {
        [Theory]
        [InlineData(CustomClaimType.RequestIp, "request_ip")]
        [InlineData(CustomClaimType.AppMetadata, "app_metadata")]
        [InlineData(CustomClaimType.CreatedAt, "created_at")]
        [InlineData(CustomClaimType.UserId, "user_id")]
        [InlineData(CustomClaimType.IsActive, "isActive")]
        public void Then_return_expected_string_value(CustomClaimType claimType, string expected)
        {
            //-- Act
            var result = claimType.StringValue();

            //-- Assert
            Assert.Equal(expected, result);
        }
    }
}
