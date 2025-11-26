using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using StackExchange.Redis;

namespace Presistence.Repositories
{
    #region Part 12 Basket Repository
    public class BasketRepository(IConnectionMultiplexer connectionMultiplexer) : IBasketRepository
    {
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var jsonBasket = JsonSerializer.Serialize(basket);
            var isCreatedorUpdate = await _database.StringSetAsync(basket.Id, jsonBasket, timeToLive ?? TimeSpan.FromDays(30));

            return isCreatedorUpdate ? await GetBasketAsync(basket.Id) : null;
        }

        public Task<bool> DeleteBasketAsync(string id)
    => _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var value = await _database.StringGetAsync(id);

            if (value.IsNullOrEmpty) return null;

            return JsonSerializer.Deserialize<CustomerBasket>(value!);
        }
    } 
    #endregion
}
