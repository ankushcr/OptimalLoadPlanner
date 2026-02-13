using Microsoft.AspNetCore.Mvc;
using SmartLoadApi.Models;
using SmartLoadApi.Services;

namespace SmartLoadApi.Controllers
{
    [ApiController]
    [Route("api/v1/load-optimizer")]
    public class LoadOptimizerController : ControllerBase
    {
        private readonly LoadOptimizerService _service;

        public LoadOptimizerController(LoadOptimizerService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("optimize")]
        public IActionResult Optimize([FromBody] LoadRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (request.Orders.Count > 22)
                return StatusCode(413, "Too many orders (max 22)");

            if (!IsRouteCompatible(request.Orders))
                return BadRequest("Orders must share same origin and destination");

            if (!IsDateValid(request.Orders))
                return BadRequest("Invalid pickup/delivery dates");

            if (HasDuplicateOrderIds(request.Orders))
                return BadRequest("Duplicate order Ids");

            var result = _service.Optimize(request.Truck, request.Orders);

            return Ok(result);

        }

        private bool IsRouteCompatible(List<Order> orders)
        {
            if (orders.Count == 0) return true;

            var origin = orders[0].Origin;
            var destination = orders[0].Destination;

            return orders.All(o =>
                string.Equals(o.Origin, origin, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(o.Destination, destination, StringComparison.OrdinalIgnoreCase));
        }

        private bool IsDateValid(List<Order> orders)
        {
            return orders.All(o =>
                o.PickupDate <= o.DeliveryDate);
        }

        private bool HasDuplicateOrderIds(List<Order> orders)
        {
            return orders
                .GroupBy(o => o.Id)
                .Any(g => g.Count() > 1);
        }
    }
}
