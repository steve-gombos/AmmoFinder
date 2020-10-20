﻿using AmmoFinder.Common.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace AmmoFinder.Common.UnitTests
{
    public class ExtensionTests
    {
        [Fact]
        public void GetXmlComments_IsValid()
        {
            // Arrange
            var assembly = typeof(ExtensionTests).Assembly;
            var assemblyName = assembly.GetName().Name;
            var expected = Path.Combine(AppContext.BaseDirectory, $"{assemblyName}.xml");

            // Act
            var actual = assembly.GetXmlComments();

            // Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData("thisisateststring", 0, 6, "thisis")]
        [InlineData("thisisateststring", 14, 6, "ing")]
        [InlineData("", 0, 6, "")]
        public void RightFromIndex_IsValid(string input, int startIndex, int length, string expected)
        {
            // Act
            var actual = input.RightFromIndex(startIndex, length);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("thisisateststring", 0, 6, "")]
        [InlineData("thisisateststring", 14, 6, "eststr")]
        [InlineData("", 0, 6, "")]
        public void LeftFromIndex_IsValid(string input, int startIndex, int length, string expected)
        {
            // Act
            var actual = input.LeftFromIndex(startIndex, length);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("11 12 ", true, "12")]
        [InlineData("11 12 ", false, "11")]
        public void GetDigitsUntilWhiteSpace_IsValid(string input, bool isLeft, string expected)
        {
            // Act
            var actual = input.GetDigitsUntilWhiteSpace(isLeft);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(ExtensionTestData))]
        public void ContainsAny_IsValid(string input, IEnumerable<string> items, bool expected)
        {
            // Act
            var actual = input.ContainsAny(items);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [ClassData(typeof(ExtensionTestData))]
        public void ContainsAny_StringComparison_IsValid(string input, IEnumerable<string> items, bool expected)
        {
            // Act
            var actual = input.ContainsAny(items, StringComparison.CurrentCultureIgnoreCase);

            // Assert
            Assert.Equal(expected, actual);
        }

    }
}
