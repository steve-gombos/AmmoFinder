using AmmoFinder.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AmmoFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRefreshProducts _refreshProducts;

        public TestController(IRefreshProducts refreshProducts)
        {
            _refreshProducts = refreshProducts;
        }

        [HttpGet]
        public IActionResult RefreshProducts()
        {
            _refreshProducts.Refresh();

            return Ok();
        }
    }
}