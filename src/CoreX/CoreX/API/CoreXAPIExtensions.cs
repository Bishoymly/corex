using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    public static class CoreXAPIExtensions
    {
        public static IApplicationBuilder UseCoreX(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CoreX.API.Router>();
        }
    }
}
