using AmmoFinder.Common.Interfaces;
using AmmoFinder.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AmmoFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetailersController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public RetailersController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RetailerModel>> GetRetailers()
        {
            var retailers = _productsRepository.GetRetailers();

            if (retailers?.Any() != true)
                return NotFound();

            return Ok(retailers);
        }
    }
}