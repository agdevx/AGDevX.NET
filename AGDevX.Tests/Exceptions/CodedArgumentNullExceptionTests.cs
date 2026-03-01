using System;
using AGDevX.Exceptions;
using Xunit;

namespace AGDevX.Tests.Exceptions;

public sealed class CodedArgumentNullExceptionTests
{
    public class When_throwing_a_CodedArgumentNullException
    {
        [Fact]
        public void And_has_correct_default_code_then_assert_true()
        {
            //-- Arrange
            var defaultCode = "CODED_ARGUMENT_NULL_EXCEPTION";

            //-- Assert
            Assert.Equal(defaultCode, new CodedArgumentNullException().Code);
        }

        [Fact]
        public void And_has_correct_code_then_assert_true()
        {
            //-- Arrange
            var code = "ex";

            //-- Assert
            Assert.Equal(code, new CodedArgumentNullException("arg", code).Code);
        }

        [Fact]
        public void And_has_correct_argument_name_then_assert_true()
        {
            //-- Arrange
            var argumentName = "testArgument";

            //-- Assert
            Assert.Equal(argumentName, new CodedArgumentNullException(argumentName).ParamName);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_sure_it_has_inner_exception()
        {
            //-- Arrange
            var message = "Test message";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(message, new CodedArgumentNullException(message, innerException).Message);
            Assert.Same(innerException, new CodedArgumentNullException(message, innerException).InnerException);
        }

        [Fact]
        public void And_should_have_inner_exception_then_make_all_properties_are_correct()
        {
            //-- Arrange
            var argumentName = "testArgument";
            var code = "ex";
            var innerExceptionMessage = "Inner exception message";
            var innerException = new Exception(innerExceptionMessage);

            //-- Assert
            Assert.Equal(argumentName, new CodedArgumentNullException(argumentName, code, innerException).ParamName);
            Assert.Equal(code, new CodedArgumentNullException(argumentName, code, innerException).Code);
            Assert.Same(innerException, new CodedArgumentNullException(argumentName, code, innerException).InnerException);
        }
    }
}
