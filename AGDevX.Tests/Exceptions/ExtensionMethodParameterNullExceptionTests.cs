using System;
using AGDevX.Exceptions;
using Xunit;

namespace AGDevX.Tests.Exceptions;

public sealed class ExtensionMethodParameterNullExceptionTests
{
    public class When_throwing_a_ExtensionMethodParameterNullException
    {
        [Fact]
        public void And_has_correct_default_code_then_assert_true()
        {
            //-- Arrange
            var defaultCode = "EXTENSION_METHOD_PARAMETER_NULL_EXCEPTION";

            //-- Assert
            Assert.Equal(defaultCode, new ExtensionMethodParameterNullException().Code);
        }

        [Fact]
        public void And_has_correct_code_then_assert_true()
        {
            //-- Arrange
            var code = "ex";

            //-- Assert
            Assert.Equal(code, new ExtensionMethodParameterNullException("msg", code).Code);
        }

        [Fact]
        public void And_has_correct_message_then_assert_true()
        {
            //-- Arrange
            var argumentName = "argumentName";
            var message = "Value cannot be null. (Parameter 'argumentName')";

            //-- Assert
            Assert.Equal(message, new ExtensionMethodParameterNullException(argumentName).Message);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_sure_it_has_inner_exception()
        {
            //-- Arrange
            var message = "Test message";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(message, new ExtensionMethodParameterNullException(message, innerException).Message);
            Assert.Same(innerException, new ExtensionMethodParameterNullException(message, innerException).InnerException);
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
            Assert.Equal(message, new ExtensionMethodParameterNullException(message, code, innerException).Message);
            Assert.Equal(code, new ExtensionMethodParameterNullException(message, code, innerException).Code);
            Assert.Same(innerException, new ExtensionMethodParameterNullException(message, code, innerException).InnerException);
        }
    }
}