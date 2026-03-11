using System;
using System.Collections.Generic;
using AGDevX.Exceptions;
using AGDevX.IEnumerables;
using Xunit;

namespace AGDevX.Tests.IEnumerables;

public class IEnumerableExtensionsTests
{
    public class When_calling_IsNullOrEmpty
    {
        [Fact]
        public void And_the_list_is_null_then_return_true()
        {
            //-- Arrange
            List<string>? strings = null;

            //-- Act
            var isNullOrEmpty = strings.IsNullOrEmpty();

            //-- Assert
            Assert.True(isNullOrEmpty);
        }

        [Fact]
        public void And_the_list_is_empty_then_return_true()
        {
            //-- Arrange
            List<string> strings = new();

            //-- Act
            var isNullOrEmpty = strings.IsNullOrEmpty();

            //-- Assert
            Assert.True(isNullOrEmpty);
        }

        [Fact]
        public void And_the_list_is_not_null_nor_empty_then_return_false()
        {
            //-- Arrange
            var strings = new List<string>
            {
                "Hello"
            };

            //-- Act
            var isNullOrEmpty = strings.IsNullOrEmpty();

            //-- Assert
            Assert.False(isNullOrEmpty);
        }
    }

    public class When_calling_HasCommonStringElement
    {
        [Fact]
        public void And_list1_has_values_in_common_with_list2_then_return_true()
        {
            //-- Arrange
            var strings1 = new List<string>
            {
                "hi", "there", "hello", "it's", "me", "I'm", "the", "adstronaut"
            };

            var strings2 = new List<string>
            {
                "hello"
            };

            //-- Act
            var hasCommonElement = strings1.HasCommonStringElement(strings2);

            //-- Assert
            Assert.True(hasCommonElement);
        }

        [Fact]
        public void And_list1_does_not_have_values_in_common_with_list2_then_return_false()
        {
            //-- Arrange
            var strings1 = new List<string>
            {
                "hi", "there", "hello", "it's", "me", "I'm", "the", "adstronaut"
            };

            var strings2 = new List<string>
            {
                "oh", "my", "golly"
            };

            //-- Act
            var hasCommonElement = strings1.HasCommonStringElement(strings2);

            //-- Assert
            Assert.False(hasCommonElement);
        }

        [Fact]
        public void And_list1_is_not_null_with_list2_null_then_throw_exception()
        {
            //-- Arrange
            var strings1 = new List<string>
            {
                "hi", "there", "hello", "it's", "me", "I'm", "the", "adstronaut"
            };

            List<string>? strings2 = null;

            //-- Act & Assert
            Assert.Throws<ExtensionMethodParameterNullException>(() => strings1.HasCommonStringElement(strings2));
        }
    }

    public class When_calling_ToDataTable
    {
        [Fact]
        public void And_list_is_null_then_return_empty_data_table()
        {
            //-- Arrange
            List<Guid>? ids = null;

            //-- Act
            var dataTable = ids.ToDataTable();

            //-- Assert
            Assert.True(dataTable.Rows.Count == 0);
        }

        [Fact]
        public void And_list_is_empty_then_return_empty_data_table()
        {
            //-- Arrange
            var ids = new List<Guid>();

            //-- Act
            var dataTable = ids.ToDataTable();

            //-- Assert
            Assert.True(dataTable.Rows.Count == 0);
        }

        [Fact]
        public void And_list_has_1_record_with_the_default_column_name_then_return_data_table_with_1_record_and_the_default_column_name()
        {
            //-- Arrange
            var guid = Guid.NewGuid();
            var ids = new List<Guid>
            {
                guid
            };

            //-- Act
            var dataTable = ids.ToDataTable();

            //-- Assert
            Assert.True(dataTable.Rows.Count == 1);
            Assert.True(dataTable.Columns.Count == 1);
            Assert.True(dataTable.Columns.Contains("Id"));
            Assert.True(new Guid(dataTable.Rows[0]["Id"].ToString()!) == guid);
        }

        [Fact]
        public void And_list_has_2_records_then_return_data_table_with_2_records()
        {
            //-- Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var ids = new List<Guid>
            {
                guid1,
                guid2
            };

            //-- Act
            var dataTable = ids.ToDataTable();

            //-- Assert
            Assert.True(dataTable.Rows.Count == 2);
            Assert.True(dataTable.Columns.Count == 1);
            Assert.True(dataTable.Columns.Contains("Id"));
            Assert.True(new Guid(dataTable.Rows[0]["Id"].ToString()!) == guid1);
            Assert.True(new Guid(dataTable.Rows[1]["Id"].ToString()!) == guid2);
        }

        [Fact]
        public void And_list_has_a_custom_column_name_then_return_data_table_the_custom_column_name()
        {
            //-- Arrange
            var guid1 = Guid.NewGuid();
            var guid2 = Guid.NewGuid();
            var ids = new List<Guid>
            {
                guid1,
                guid2
            };

            //-- Act
            var dataTable = ids.ToDataTable("TheId");

            //-- Assert
            Assert.True(dataTable.Rows.Count == 2);
            Assert.True(dataTable.Columns.Count == 1);
            Assert.True(dataTable.Columns.Contains("TheId"));
            Assert.True(new Guid(dataTable.Rows[0]["TheId"].ToString()!) == guid1);
            Assert.True(new Guid(dataTable.Rows[1]["TheId"].ToString()!) == guid2);
        }
    }

    public class When_calling_ContainsIgnoreCase
    {
        [Fact]
        public void And_list_has_matching_value_then_return_true()
        {
            //-- Arrange
            var strings = new List<string>
            {
                "hi", "there", "hello", "it's", "me", "I'm", "the", "adstronaut"
            };
                
            var str = "hello";

            //-- Act
            var contains = strings.ContainsIgnoreCase(str);

            //-- Assert
            Assert.True(contains);
        }

        [Fact]
        public void And_list_has_null_element_with_null_element_match_then_throw_exception()
        {
            //-- Arrange
            var strings = new List<string?>
            {
                "hi", "there", "hello", "it's", "me", "I'm", "the", "adstronaut", null
            };

            string? str = null;

            //-- Act & Assert
            Assert.Throws<ExtensionMethodParameterNullException>(() => strings.ContainsIgnoreCase(str));
        }

        [Fact]
        public void And_list_does_not_have_matching_value_then_return_false()
        {
            //-- Arrange
            var strings = new List<string>
            {
                "hi", "there", "hello", "it's", "me", "I'm", "the", "adstronaut"
            };

            var str = "hey";

            //-- Act
            var contains = strings.ContainsIgnoreCase(str);

            //-- Assert
            Assert.False(contains);
        }
    }

    public class When_calling_AnySafe
    {
        [Fact]
        public void And_list_is_non_empty_then_return_true()
        {
            //-- Arrange
            var strings = new List<string>
            {
                "hi", "there", "hello", "it's", "me", "I'm", "the", "adstronaut"
            };

            //-- Act
            var contains = strings.AnySafe();

            //-- Assert
            Assert.True(contains);
        }

        [Fact]
        public void And_list_is_null_then_return_false()
        {
            //-- Arrange
            List<string>? strings = null;

            //-- Act
            var contains = strings.AnySafe();

            //-- Assert
            Assert.False(contains);
        }

        [Fact]
        public void And_list_is_empty_then_return_false()
        {
            //-- Arrange
            List<string> strings = new();

            //-- Act
            var contains = strings.AnySafe();

            //-- Assert
            Assert.False(contains);
        }
    }
}