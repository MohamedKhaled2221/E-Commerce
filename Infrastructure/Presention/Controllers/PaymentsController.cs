using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;

namespace Presention.Controllers
{
    #region Part 4 Payment Controller
    public class PaymentsController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpPost("{basketId}")] //POST : BaeUrl/api/payments/{basketId}
        public async Task<ActionResult<BasketDTO>> CreateOrUpdatePaymentntent(string basketId)
     => Ok(await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId));



    } 
    #endregion
}
