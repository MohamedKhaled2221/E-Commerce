using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Services.Abstraction.Contracts;
using Shared.Dtos.BasketModule;


namespace Presention.Controllers
{

    #region Part 15 Basket Controller
    [Authorize]
    public class BasketController(IServiceManager serviceManager) : ApiControllerBase
    {
        [HttpGet("{id}")] //GET : BaseUrl/api/Basket/{id}
        public async Task<ActionResult<BasketDTO>> GetBasketById(string id)

      => Ok(await serviceManager.BasketService.GetBasketAsync(id));

        [HttpPost] //POST : BaseUrl/api/Basket
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basket)
            => Ok(await serviceManager.BasketService.CreateOrUpdateBasketAsync(basket));

        [HttpDelete("{id}")] //DELETE : BaseUrl/api/Basket/{id}
        public async Task<ActionResult> Delete(string id)
        {
            await serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent(); // 204
        }

    } 
    #endregion
}
