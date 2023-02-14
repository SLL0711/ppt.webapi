using log4net.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using wt.basic.service.Common;

namespace wt.basic.webapi.Extension.ErrorHandler
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context, wtJsonResult jsonResult, ILogger<ErrorHandlingMiddleware> logger)
        {
            string contentType = "application/json;charset=utf-8";
            try
            {
                context.Response.ContentType = contentType;
                await _next.Invoke(context);
            }
            catch (System.Exception ex)
            {
                jsonResult.success = false;
                jsonResult.msg = $"服务器错误：{ex.Message}";
                logger.LogError($"服务器错误：{ex.Message}");
            }
            finally
            {
                try
                {
                    if (context.Response.ContentType == contentType)
                    {
                        jsonResult.code = context.Response.StatusCode;
                        var jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(jsonResult);
                        logger.LogWarning($"jsonResult:{jsonStr}");
                        await context.Response.WriteAsync(jsonStr);
                    }
                    else
                    {
                        logger.LogWarning($"ContentType 被修改：Path:{context.Request.Path.Value}   ContentType:{context.Response.ContentType}");
                    }
                }
                catch (System.Exception ex)
                {
                    logger.LogError($"服务器错误2：{ex.Message}");
                }
            }
        }
    }
}
