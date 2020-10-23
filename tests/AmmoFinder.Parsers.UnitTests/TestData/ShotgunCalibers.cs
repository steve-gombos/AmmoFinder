using Xunit;

namespace AmmoFinder.Parsers.UnitTests.TestData
{
    public class ShotgunCaliber : TheoryData<string, string>
    {
        public ShotgunCaliber()
        {
            Add("WOLF Power Buckshot 12 Gauge 00 Buck 5rd Box", "12 Gauge");
            Add("5 Rounds of 12ga Ammo by Hornady - 300 grain SST Sabot Slug", "12 Gauge");
            Add("250 Rounds of 20ga Ammo by NobelSport - 7/8 ounce #7 steel shot", "20 Gauge");
            Add("32 Gauge - 2-1/2\" 1 / 2oz. #6 Shot - Rio - 250 Rounds", "32 Gauge");
        }
    }
}
