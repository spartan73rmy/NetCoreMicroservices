using BasketApi.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BasketApi.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache redis;

        public BasketRepository(IDistributedCache redis)
        {
            this.redis = redis;
        }

        public async Task DeleteBasket(string userName)
        {
            await redis.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await redis.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
                return null;
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await redis.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
