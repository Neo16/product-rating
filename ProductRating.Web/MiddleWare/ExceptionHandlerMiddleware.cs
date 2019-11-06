using log4net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProductRating.Bll.Dtos;
using ProductRating.Bll.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProductRating.Web.MiddleWare
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private ILog logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILog logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BusinessLogicException bEx)
            {
                logger.Error("BusinessLogic exception: ", bEx);
                await WriteAsJsonAsync(context, (HttpStatusCode)bEx.ErrorCode, new ErrorDto
                {
                    ErrorCode = bEx.ErrorCode,
                    ErrorMessages = new string[] { bEx.Message }
                });
            }
            catch (Exception ex)
            {
                logger.Error("An exception happened: ", ex);
                await WriteAsJsonAsync(context, HttpStatusCode.InternalServerError, null);
            }
        }

        private async Task WriteAsJsonAsync(HttpContext context, HttpStatusCode statusCode, object payload)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var json = JsonConvert.SerializeObject(payload, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            await context.Response.WriteAsync(json);
        }
    }
}
