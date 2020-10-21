using AmmoFinder.Parsers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AmmoFinder.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        [HttpGet("cartridge-types")]
        public ActionResult<Dictionary<string, IEnumerable<string>>> GetCalibers()
        {
            var calibers = new Calibers().ToDictionary(k => k.Key, v => v.Value.Select(x => x.Name));

            return Ok(calibers);
        }

        [HttpGet("brands")]
        public ActionResult<IEnumerable<string>> GetBrands()
        {
            var brands = typeof(Brands).GetFields().Where(x => x.GetValue(x) is string).Select(x => (string)x.GetValue(x));

            return Ok(brands);
        }
    }
}