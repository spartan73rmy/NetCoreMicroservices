using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Commands.CheckoutOrder;
using Ordering.Application.Features.Queries.GetOrdersList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.api.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]/[Action]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{userName}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<OrdersViewModel>>> GetOrdersByUserName(string userName)
        {
            var orders = await mediator.Send(new GetOrdersListQuery(userName));
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CheckoutOrder([FromBody] CheckoutOrderCommand command)
        {
            return Ok(await mediator.Send(command));
        }
    }
}
