using Microsoft.AspNetCore.Builder;

namespace wt.basic.webapi.Extension.ErrorHandler
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
