﻿using AmmoFinder.Common.Interfaces;
using AmmoFinder.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AmmoFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRefreshProducts _refreshProducts;
        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository, IRefreshProducts refreshProducts)
        {
            _refreshProducts = refreshProducts;
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

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshProducts()
        {
            await _refreshProducts.Refresh();

            return Ok();
        }
    }
}