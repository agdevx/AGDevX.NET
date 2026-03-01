using System;
using AGDevX.Exceptions;
using Xunit;

namespace AGDevX.Tests.Exceptions;

public sealed class CodedApplicationExceptionTests
{
    public class When_throwing_a_CodedApplicationException
    {
        [Fact]
        public void And_has_correct_default_code_then_assert_true()
        {
            //-- Arrange
            var defaultCode = "CODED_APPLICATION_EXCEPTION";

            //-- Assert
            Assert.Equal(defaultCode, new CodedApplicationException().Code);
        }

        [Fact]
        public void And_has_correct_code_then_assert_true()
        {
            //-- Arrange
            var code = "ex";

            //-- Assert
            Assert.Equal(code, new CodedApplicationException("msg", code).Code);
        }

        [Fact]
        public void And_has_correct_message_then_assert_true()
        {
            //-- Arrange
            var message = "Test message";

            //-- Assert
            Assert.Equal(message, new CodedApplicationException(message).Message);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_sure_it_has_inner_exception()
        {
            //-- Arrange
            var message = "Test message";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(message, new CodedApplicationException(message, innerException).Message);
            Assert.Same(innerException, new CodedApplicationException(message, innerException).InnerException);
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
            Assert.Equal(message, new CodedApplicationException(message, code, innerException).Message);
            Assert.Equal(code, new CodedApplicationException(message, code, innerException).Code);
            Assert.Same(innerException, new CodedApplicationException(message, code, innerException).InnerException);
        }
    }
}