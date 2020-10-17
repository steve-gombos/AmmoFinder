using AmmoFinder.Common.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace AmmoFinder.Functions
{
    public class RefreshRetailerProducts
    {
        private readonly IRefreshProducts _refreshProducts;

        public RefreshRetailerProducts(IRefreshProducts refreshProducts)
        {
            _refreshProducts = refreshProducts;
        }

        [FunctionName("RefreshRetailerProducts")]
        public void Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer, ILogger logger)
        {
            logger.LogInformation("C# Timer trigger function executed.");

            try
            {
                _refreshProducts.Refresh().Wait();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
