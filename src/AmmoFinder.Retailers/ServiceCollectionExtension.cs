using AmmoFinder.Retailers.AimSurplus;
using AmmoFinder.Retailers.AmmoDotCom;
using AmmoFinder.Retailers.BulkAmmo;
using AmmoFinder.Retailers.Cabelas;
using AmmoFinder.Retailers.LuckyGunner;
using AmmoFinder.Retailers.SportsmansGuide;
using AngleSharp;
using Microsoft.Extensions.DependencyInjection;

namespace AmmoFinder.Retailers
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRetailers(this IServiceCollection services)
        {
            services.AddAngleSharp();
            services.AddAimSurplusClient();
            services.AddAmmoDotComClient();
            services.AddBulkAmmoClient();
            services.AddCabelasClient();
            services.AddLuckyGunnerClient();
            services.AddSportsmansGuideClient();

            return services;
        }

        public static IServiceCollection AddAngleSharp(this IServiceCollection services)
        {
            services.AddSingleton(BrowsingContext.New(Configuration.Default));

            return services;
        }
    }
}
