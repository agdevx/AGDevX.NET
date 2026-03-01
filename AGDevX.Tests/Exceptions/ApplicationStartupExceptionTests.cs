using System;
using AGDevX.Exceptions;
using Xunit;

namespace AGDevX.Tests.Exceptions;

public sealed class ApplicationStartupExceptionTests
{
    public class When_throwing_an_ApplicationStartupException
    {
        [Fact]
        public void And_has_correct_default_code_then_assert_true()
        {
            //-- Arrange
            var defaultCode = "APPLICATION_STARTUP_EXCEPTION";

            //-- Assert
            Assert.Equal(defaultCode, new ApplicationStartupException().Code);
        }

        [Fact]
        public void And_has_correct_code_then_assert_true()
        {
            //-- Arrange
            var code = "ex";

            //-- Assert
            Assert.Equal(code, new ApplicationStartupException("msg", code).Code);
        }

        [Fact]
        public void And_has_correct_message_then_assert_true()
        {
            //-- Arrange
            var message = "Test message";

            //-- Assert
            Assert.Equal(message, new ApplicationStartupException(message).Message);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_sure_it_has_inner_exception()
        {
            //-- Arrange
            var message = "Test message";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(message, new ApplicationStartupException(message, innerException).Message);
            Assert.Same(innerException, new ApplicationStartupException(message, innerException).InnerException);
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
            Assert.Equal(message, new ApplicationStartupException(message, code, innerException).Message);
            Assert.Equal(code, new ApplicationStartupException(message, code, innerException).Code);
            Assert.Same(innerException, new ApplicationStartupException(message, code, innerException).InnerException);
        }
    }
}