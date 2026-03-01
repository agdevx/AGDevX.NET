using AGDevX.Exceptions;
using AGDevX.Strings;
using Xunit;

namespace AGDevX.Tests.Strings;

public class StringExtensionsTests
{
    public class When_calling_EqualsIgnoreCase
    {
        [Theory]
        [InlineData("equal", "EQUAL")]
        [InlineData("equal", "equal")]
        public void And_the_string_are_equal_then_return_true(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str1.EqualsIgnoreCase(str2);

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("equal", "not equal")]
        public void And_the_string_are_not_equal_then_return_false(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str1.EqualsIgnoreCase(str2);

            //-- Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("equal", null)]
        public void And_the_str2_param_is_null_then_throw_exception(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act & Assert
            Assert.Throws<ExtensionMethodParameterNullException>(() => str1.EqualsIgnoreCase(str2));
        }
    }

    public class When_calling_StartsWithIgnoreCase
    {
        [Theory]
        [InlineData("equal", "EQUAL")]
        [InlineData("equal", "equal")]
        public void And_the_string_starts_with_string_then_return_true(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str1.StartsWithIgnoreCase(str2);

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("equal", "not equal")]
        public void And_the_string_does_not_start_with_string_then_return_false(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str1.StartsWithIgnoreCase(str2);

            //-- Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("equal", null)]
        public void And_the_str2_param_is_null_then_throw_exception(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act & Assert
            Assert.Throws<ExtensionMethodParameterNullException>(() => str1.StartsWithIgnoreCase(str2));
        }
    }

    public class When_calling_ContainsIgnoreCase
    {
        [Theory]
        [InlineData("equal", "EQUAL")]
        [InlineData("equal", "equal")]
        public void And_the_string_contains_the_string_then_return_true(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str1.ContainsIgnoreCase(str2);

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("equal", "not equal")]
        public void And_the_string_does_not_contain_the_string_then_return_false(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str1.ContainsIgnoreCase(str2);

            //-- Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("equal", null)]
        public void And_the_str2_param_is_null_then_throw_exception(string str1, string str2)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act & Assert
            Assert.Throws<ExtensionMethodParameterNullException>(() => str1.ContainsIgnoreCase(str2));
        }
    }

    public class When_calling_IsNullOrWhiteSpace
    {
        [Theory]
        [InlineData(null)]
        [InlineData("   ")]
        public void And_the_string_is_null_or_whitespace_then_return_true(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsNullOrWhiteSpace();

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("something")]
        public void And_the_string_is_not_null_or_whitespace_then_return_false(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsNullOrWhiteSpace();

            //-- Assert
            Assert.False(result);
        }
    }

    public class When_calling_IsNotNullNorWhiteSpace
    {
        [Theory]
        [InlineData("something")]
        public void And_the_string_is_not_null_or_whitespace_then_return_true(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsNotNullNorWhiteSpace();

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("   ")]
        public void And_the_string_null_or_whitespace_then_return_false(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsNotNullNorWhiteSpace();

            //-- Assert
            Assert.False(result);
        }
    }

    public class When_calling_IsWhiteSpace
    {
        [Theory]
        [InlineData(" ")]
        [InlineData("        ")]
        public void And_the_string_is_whitespace_then_return_true(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsWhiteSpace();

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(" b")]
        [InlineData("c ")]
        [InlineData(" d ")]
        [InlineData(" . ")]
        public void And_the_string_is_not_whitespace_then_return_false(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsWhiteSpace();

            //-- Assert
            Assert.False(result);
        }
    }

    public class When_calling_IsNotWhiteSpace
    {
        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData(" b")]
        [InlineData("c ")]
        [InlineData(" d ")]
        [InlineData(" . ")]
        public void And_the_string_is_null_or_not_whitespace_then_return_true(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsNotWhiteSpace();

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData("        ")]
        public void And_the_string_is_whitespace_only_then_return_false(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsNotWhiteSpace();

            //-- Assert
            Assert.False(result);
        }
    }

    public class When_calling_IsEmpty
    {
        [Theory]
        [InlineData("")]
        public void And_the_string_is_empty_only_then_return_true(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsEmpty();

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData(" b")]
        [InlineData("c ")]
        [InlineData(" d ")]
        [InlineData(" . ")]
        [InlineData(" ")]
        [InlineData("      ")]
        public void And_the_string_is_null_or_whitespace_or_has_non_whitespace_value_then_return_false(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsEmpty();

            //-- Assert
            Assert.False(result);
        }
    }

    public class When_calling_IsNotEmpty
    {
        [Theory]
        [InlineData(null)]
        [InlineData("a")]
        [InlineData(" b")]
        [InlineData("c ")]
        [InlineData(" d ")]
        [InlineData(" . ")]
        [InlineData(" ")]
        [InlineData("      ")]
        public void And_the_string_is_null_or_whitespace_or_has_non_whitespace_value_then_return_true(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsNotEmpty();

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("")]
        public void And_the_string_is_empty_only_then_return_false(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.IsNotEmpty();

            //-- Assert
            Assert.False(result);
        }
    }

    public class When_calling_NullIfNullOrWhiteSpace
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("      ")]
        public void And_the_string_is_null_or_whitespace_or_empty_then_return_null(string? str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.NullIfNullOrWhiteSpace();

            //-- Assert
            Assert.Null(result);
        }

        [Theory]
        [InlineData("a")]
        [InlineData(" b")]
        [InlineData("c ")]
        [InlineData(" d ")]
        [InlineData(" . ")]
        public void And_the_string_is_not_null_nor_whitespace_nor_empty_then_return_string(string str)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = str.NullIfNullOrWhiteSpace();

            //-- Assert
            Assert.Equal(str, result);
        }
    }
}