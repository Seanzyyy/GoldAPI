using DataWarehouseApi.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        private readonly Goldcontext _goldContext;

        public GoldController(Goldcontext goldContext)
        {
            _goldContext = goldContext;
        }

        [HttpGet]
        [Route("customers")]
        public async Task<ActionResult> GetCustomers()
        {
            return Ok(
                await _goldContext.DimCustomers
                      .AsNoTracking()
                      .Take(40)
                      .ToListAsync()
                );
        }

        [HttpGet("products")]
        public async Task<ActionResult> GetProducts()
        {
            return Ok(await _goldContext.DimProducts.ToListAsync());
        }

        [HttpGet]
        [Route("Orders")]
        public async Task<ActionResult<List<FactOrder>>> GetOrders()
        {
            var orders = await _goldContext.FactOrders.ToListAsync();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }
    }
}
