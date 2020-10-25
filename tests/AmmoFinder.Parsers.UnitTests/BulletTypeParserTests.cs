using Xunit;

namespace AmmoFinder.Parsers.UnitTests
{
    public class BulletTypeParserTests
    {
        [Theory]
        [InlineData("", null)]
        [InlineData(null, null)]
        [InlineData("<p>Red Army Standard 7.62x39 122grn FMJ ammunition. Features a Polymer coated steel case. Packaged 20rds to a box, 50boxes per case (1,000rds). 122 grain FMJ bullet, and Berdan primed (Non-corrosive) Steel case. Made in Russia.&nbsp;&nbsp;</p>", "FMJ")]
        [InlineData("<p>Red Army Standard 7.62x39 122grn ammunition. Features a Polymer coated steel case. Packaged 20rds to a box, 50boxes per case (1,000rds). 122 grain bullet, and Berdan primed (Non-corrosive) Steel case. Made in Russia.</p>", null)]
        public void BulletTypeParser_IsValid(string input, string expected)
        {
            // Arrange
            var parser = new BulletTypeParser();

            // Act
            var actual = parser.Parse(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
