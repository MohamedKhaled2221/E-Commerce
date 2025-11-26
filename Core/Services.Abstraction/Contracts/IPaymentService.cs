using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.BasketModule;

namespace Services.Abstraction.Contracts
{
    public interface IPaymentService
    {
        public Task<BasketDTO> CreateOrUpdatePaymentIntentAsync(string basketId);

        public Task UpdateOrderPaymentStatusAsync(string json , string header);

    }
}
