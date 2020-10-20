using AmmoFinder.Retailers.AimSurplus;
using AmmoFinder.Retailers.BulkAmmo;
using AmmoFinder.Retailers.Cabelas;
using AmmoFinder.Retailers.LuckyGunner;
using Microsoft.Extensions.DependencyInjection;

namespace AmmoFinder.Retailers
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRetailers(this IServiceCollection services)
        {
            services.AddAimSurplusClient();
            services.AddBulkAmmoClient();
            services.AddCabelasClient();
            services.AddLuckyGunnerClient();

            return services;
        }
    }
}
