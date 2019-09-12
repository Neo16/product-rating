using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductRating.Bll.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ProductRating.Web.MiddleWare
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate next;
        private readonly List<string> urlWhiteList = new List<string>() {
            "localhost"
        };

        private readonly IApiKeyService apiKeyService;

        public ApiKeyMiddleware(RequestDelegate next, IApiKeyService apiKeyService)
        {
            this.next = next;
            this.apiKeyService = apiKeyService;
        }

        public async Task Invoke(HttpContext context)
        {
            string origin = context.Request.Headers["origin"].ToString();
            string siteBaseUrl = origin == "null" ? "" : new Uri(origin).Host;

            if (!urlWhiteList.Contains(siteBaseUrl))
            {
                if (context.Request.Query.TryGetValue("key", out var apiKeyTry))
                {
                    var apiKey = apiKeyTry.ToString();
                    var isValid = await apiKeyService.IsApiKeyValid(siteBaseUrl, apiKey);

                    if (!isValid)
                    {
                        await SendErrorResponse(context);
                        return;
                    }
                }
                else
                {
                    await SendErrorResponse(context);
                    return;
                }
            }

            await next(context);
        }

        private async Task SendErrorResponse(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.ContentType = "application/json";

            var json = JsonConvert.SerializeObject("Missing API key", new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await context.Response.WriteAsync(json);
        }
    }
}
