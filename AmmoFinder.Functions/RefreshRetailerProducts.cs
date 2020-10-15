using AmmoFinder.Common.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace AmmoFinder.Functions
{
    public class RefreshRetailerProducts
    {
        private readonly IRefreshProducts _refreshProducts;
        private readonly ILogger<RefreshRetailerProducts> _logger;

        public RefreshRetailerProducts(IRefreshProducts refreshProducts, ILogger<RefreshRetailerProducts> logger)
        {
            _refreshProducts = refreshProducts;
            _logger = logger;
        }

        [FunctionName("RefreshRetailerProducts")]
        public void Run([TimerTrigger("0 */15 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            try
            {
                _refreshProducts.Refresh().Wait();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
