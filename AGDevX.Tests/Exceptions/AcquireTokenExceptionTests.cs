using System;
using AGDevX.Exceptions;
using Xunit;

namespace AGDevX.Tests.Exceptions;

public sealed class AcquireTokenExceptionTests
{
    public class When_throwing_an_AcquireTokenException
    {
        [Fact]
        public void And_has_correct_default_code_then_assert_true()
        {
            //-- Arrange
            var defaultCode = "ACQUIRE_TOKEN_EXCEPTION";

            //-- Assert
            Assert.Equal(defaultCode, new AcquireTokenException().Code);
        }

        [Fact]
        public void And_has_correct_code_then_assert_true()
        {
            //-- Arrange
            var code = "ex";

            //-- Assert
            Assert.Equal(code, new AcquireTokenException("msg", code).Code);
        }

        [Fact]
        public void And_has_correct_message_then_assert_true()
        {
            //-- Arrange
            var message = "Test message";

            //-- Assert
            Assert.Equal(message, new AcquireTokenException(message).Message);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_sure_it_has_inner_exception()
        {
            //-- Arrange
            var message = "Test message";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(message, new AcquireTokenException(message, innerException).Message);
            Assert.Same(innerException, new AcquireTokenException(message, innerException).InnerException);
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
            Assert.Equal(message, new AcquireTokenException(message, code, innerException).Message);
            Assert.Equal(code, new AcquireTokenException(message, code, innerException).Code);
            Assert.Same(innerException, new AcquireTokenException(message, code, innerException).InnerException);
        }
    }
}