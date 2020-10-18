using System;
using System.Collections.Generic;
using System.Linq;

namespace AmmoFinder.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        ///     A string extension method that query if '@this' contains any values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains any values, otherwise false.</returns>
        public static bool ContainsAny(this string @this, IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        ///     A string extension method that query if '@this' contains any values.
        /// </summary>
        /// <param name="this">The @this to act on.</param>
        /// <param name="comparisonType">Type of the comparison.</param>
        /// <param name="values">A variable-length parameters list containing values.</param>
        /// <returns>true if it contains any values, otherwise false.</returns>
        public static bool ContainsAny(this string @this, StringComparison comparisonType, IEnumerable<string> values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) != -1)
                {
                    return true;
                }
            }
            return false;
        }

        public static string RightFromIndex(this string value, int startIndex, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var diff = value.Length - startIndex;
            var maxLengthCorrected = diff < maxLength ? diff : maxLength;

            return value.Substring(startIndex, maxLengthCorrected);
        }

        public static string LeftFromIndex(this string value, int startIndex, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var maxLengthCorrected = startIndex < maxLength ? startIndex : maxLength;
            var indexDiff = startIndex - maxLengthCorrected;
            var startIndexCorrected = indexDiff > 0 ? indexDiff : 0;

            return value.Substring(startIndexCorrected, maxLengthCorrected);
        }

        public static string GetDigitsUntilWhiteSpace(this string value, bool isLeft = true)
        {
            var split = value.Split(" ");
            if (isLeft)
            {
                split = split.Reverse().ToArray();
            }

            foreach (var set in split)
            {
                if (string.IsNullOrWhiteSpace(set))
                    continue;

                if (set.All(char.IsDigit))
                    return set;
            }

            return null;
        }
    }
}
