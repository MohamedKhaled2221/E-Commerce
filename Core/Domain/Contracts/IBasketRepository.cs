using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.BasketModule;

namespace Domain.Contracts
{
    public  interface IBasketRepository
    {
        // Get basket by  ID

        public Task<CustomerBasket?> GetBasketAsync(string id);

        // Delete basket
        public Task<bool> DeleteBasketAsync(string id);

        // Create Or Update Basket
        public Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null );

    }
}
