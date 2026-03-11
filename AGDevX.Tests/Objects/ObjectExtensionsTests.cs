using AGDevX.Objects;
using Xunit;

namespace AGDevX.Tests.Objects;

public class ObjectExtensionsTests
{
    public class When_calling_IsNull
    {
        [Theory]
        [InlineData(null)]
        public void And_the_object_is_null_then_return_true(object? obj)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = obj.IsNull();

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData("a")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(1)]
        [InlineData(1.0)]
        [InlineData(char.MinValue)]
        [InlineData(' ')]
        [InlineData('`')]
        public void And_the_object_is_not_null_then_return_true(object obj)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = obj.IsNull();

            //-- Assert
            Assert.False(result);
        }
    }

    public class When_calling_IsNotNull
    {
        [Theory]
        [InlineData("a")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(1)]
        [InlineData(1.0)]
        [InlineData(char.MinValue)]
        [InlineData(' ')]
        [InlineData('`')]
        public void And_the_object_is_not_null_then_return_true(object obj)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = obj.IsNotNull();

            //-- Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null)]
        public void And_the_object_is_null_then_return_false(object? obj)
        {
            //-- Arrange
            //-- <see InlineData>

            //-- Act
            var result = obj.IsNotNull();

            //-- Assert
            Assert.False(result);
        }
    }
}