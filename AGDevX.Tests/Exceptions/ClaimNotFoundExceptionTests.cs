using System;
using AGDevX.Exceptions;
using Xunit;

namespace AGDevX.Tests.Exceptions;

public sealed class ClaimNotFoundExceptionTests
{
    public class When_throwing_a_ClaimNotFoundException
    {
        [Fact]
        public void And_has_correct_default_code_then_assert_true()
        {
            //-- Arrange
            var defaultCode = "CLAIM_NOT_FOUND_EXCEPTION";

            //-- Assert
            Assert.Equal(defaultCode, new ClaimNotFoundException().Code);
        }

        [Fact]
        public void And_has_correct_code_then_assert_true()
        {
            //-- Arrange
            var code = "ex";

            //-- Assert
            Assert.Equal(code, new ClaimNotFoundException("msg", code).Code);
        }

        [Fact]
        public void And_has_correct_message_then_assert_true()
        {
            //-- Arrange
            var message = "Test message";

            //-- Assert
            Assert.Equal(message, new ClaimNotFoundException(message).Message);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_sure_it_has_inner_exception()
        {
            //-- Arrange
            var message = "Test message";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(message, new ClaimNotFoundException(message, innerException).Message);
            Assert.Same(innerException, new ClaimNotFoundException(message, innerException).InnerException);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_all_properties_are_correct()
        {
            //-- Arrange
            var message = "Test message";
            var code = "ex";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(message, new ClaimNotFoundException(message, code, innerException).Message);
            Assert.Equal(code, new ClaimNotFoundException(message, code, innerException).Code);
            Assert.Same(innerException, new ClaimNotFoundException(message, code, innerException).InnerException);
        }
    }
}