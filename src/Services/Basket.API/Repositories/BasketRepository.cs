using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<BasketCart> GetBasket(string userName)
    {
        var basket = await _redisCache.GetStringAsync(userName);
        if (string.IsNullOrWhiteSpace(basket))
        {
            return null;
        }

        return JsonConvert.DeserializeObject<BasketCart>(basket);
    }

    public async Task<BasketCart> UpdateBasket(BasketCart basket)
    {
        await _redisCache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));
        return await GetBasket(basket.Username);
    }

    public async Task<bool> DeleteBasket(string userName)
    {
        await _redisCache.RemoveAsync(userName);
        return true;
    }
}