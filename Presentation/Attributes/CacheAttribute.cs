using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    class CacheAttribute(int DurationSec=90) :ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // create key
            var cacheKey = CreatCacheeKey(context.HttpContext.Request);
            // search if there is date stored with this key
            ICacheService cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheValue= await cacheService.GetCacheAsync(cacheKey);
            // check if there is data stored with key
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content=cacheValue,
                    ContentType="application/json",
                    StatusCode=StatusCodes.Status200OK
                };
                return;
             }

            // if no data stored with this key
            // call next end pint and store data to variable to cache it 

            var ExcutedData =await next.Invoke();
            if (ExcutedData.Result is OkObjectResult result)
            {
                // cache returned data 
               await cacheService.SetCacheAsync(cacheKey,result.Value,TimeSpan.FromSeconds(DurationSec));
            }

        }

        private string CreatCacheeKey(HttpRequest request)
        {
            // apend to path of end point
            // https://localhost:7117/api/Products/AllProducts
            StringBuilder key = new StringBuilder();
            key.Append(request.Path + '?');
            // sort to parameters 
            foreach (var item in request.Query.OrderBy(e=>e.Key))
            {
                //https://localhost:7117/api/Products/AllProducts?BrandId=1&TypeId=1
                // add parameters 
                key.Append($"{item.Key}={item.Value}&");
            }
            return key.ToString();
        }
    }
}
