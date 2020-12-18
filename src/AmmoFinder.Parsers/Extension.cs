namespace AmmoFinder.Parsers
{
    public static class Extension
    {
        private static CasingParser Casing = new CasingParser();
        public static string GetCasing(this string description)
        {
            return Casing.Parse(description);
        }

        private static RoundCountParser RoundCount = new RoundCountParser();
        public static string GetRoundCount(this string description)
        {
            return RoundCount.Parse(description);
        }

        private static GrainParser Grain = new GrainParser();
        public static string GetGrain(this string description)
        {
            return Grain.Parse(description);
        }

        private static CaliberParser Caliber = new CaliberParser();
        public static string GetCaliber(this string description)
        {
            return Caliber.Parse(description);
        }

        private static BulletTypeParser BulletType = new BulletTypeParser();
        public static string GetBulletType(this string description)
        {
            return BulletType.Parse(description);
        }

        private static BrandParser Brand = new BrandParser();
        public static string GetBrand(this string description)
        {
            return Brand.Parse(description);
        }
    }
}
