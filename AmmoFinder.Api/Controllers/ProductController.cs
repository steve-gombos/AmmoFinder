using AmmoFinder.Common.Interfaces;
using AmmoFinder.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;

namespace AmmoFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductModel>> GetProducts()
        {
            var products = _productsRepository.GetProducts();

            if (products?.Any() != true)
                return NotFound();

            return Ok(products);
        }
    }
}