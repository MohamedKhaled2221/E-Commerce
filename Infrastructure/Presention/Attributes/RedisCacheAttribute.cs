using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.Contracts;

namespace Presention.Attributes
{
    #region Part 4 Cache Attribute
    internal class RedisCacheAttribute(int durationInSecondes = 60) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;
            // Data Cached Or Not ==> Cache Key

            string cacheKey = GenerateCacheKey(context.HttpContext.Request);

            var result = await cacheService.GetCachedValueAsync(cacheKey);

            // Data Already Cached [ Return Response WithOut Entering The Endpoint ]
            if (result is not null)
            {
                // Return From Cache
                context.Result = new ContentResult()
                {
                    Content = result,
                    ContentType = "Application/Json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }

            var contextResult = await next.Invoke();

            if (contextResult.Result is OkObjectResult okObject)
                // Store Data In Cache
                await cacheService.SetCacheValueAsync(cacheKey, okObject, TimeSpan.FromMinutes(durationInSecondes));

        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            // Request.Path ==> /api/products

            // Request.Query ==> ?brandId=1&typeId=2&pageIndex=1&pageSize=10&sort=priceAsc


            keyBuilder.Append(request.Path);

            foreach (var item in request.Query.OrderBy(q => q.Key))
            {
                keyBuilder.Append($"|{item.Key}-{item.Value}");
            }
            return keyBuilder.ToString();
        }
    } 
    #endregion
}
