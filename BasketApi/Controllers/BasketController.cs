using AutoMapper;
using BasketApi.Entities;
using BasketApi.Repositories;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BasketApi.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]/[Action]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository repository;
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publish;

        public BasketController(IBasketRepository repository, IMapper mapper, IPublishEndpoint publish)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.publish = publish;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
            => Ok(await repository.GetBasket(userName) ?? new ShoppingCart(userName));

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await repository.DeleteBasket(userName);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart cart)
        => Ok(await repository.UpdateBasket(cart));

        [HttpPost]
        public async Task<IActionResult> CheckOut([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await repository.GetBasket(basketCheckout.UserName);
            if (basket == null)
                return NotFound();

            var eventMessage = mapper.Map<BasketCheckoutEvent>(basketCheckout);

            eventMessage.TotalPrice = basket.TotalPrice;

            await publish.Publish(eventMessage);

            await repository.DeleteBasket(basket.UserName);

            return Ok(eventMessage);
        }
    }
}
