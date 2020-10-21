using AmmoFinder.Parsers.UnitTests.TestData;
using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class CaliberParserTests
    {
        [Theory]
        [InlineData("", null)]
        [ClassData(typeof(RimfireCaliber))]
        [ClassData(typeof(HandgunCalibers))]
        [ClassData(typeof(ShotgunCaliber))]
        [ClassData(typeof(RifleCaliber))]
        public void Caliber_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new CaliberParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
