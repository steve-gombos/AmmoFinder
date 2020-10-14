namespace AmmoFinder.Parsers
{
    public static class Extension
    {
        public static CasingParser Casing { get; set; } = new CasingParser();
        public static string GetCasing(this string description)
        {
            return Casing.Parse(description);
        }

        public static RoundCountParser RoundCount { get; set; } = new RoundCountParser();
        public static string GetRoundCount(this string description)
        {
            return RoundCount.Parse(description);
        }

        public static GrainParser Grain { get; set; } = new GrainParser();
        public static string GetGrain(this string description)
        {
            return Grain.Parse(description);
        }

        public static CaliberParser Caliber { get; set; } = new CaliberParser();
        public static string GetCaliber(this string description)
        {
            return Caliber.Parse(description);
        }

        public static ProjectileTypeParser ProjectileType { get; set; } = new ProjectileTypeParser();
        public static string GetProjectileType(this string description)
        {
            return ProjectileType.Parse(description);
        }

        public static BrandParser Brand { get; set; } = new BrandParser();
        public static string GetBrand(this string description)
        {
            return Brand.Parse(description);
        }
    }
}
