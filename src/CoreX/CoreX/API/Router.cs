using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CoreX.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace CoreX.API
{
    public class Router
    {
        private readonly RequestDelegate _next;

        public Router(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Parse url
            var pathComponents = context.Request.Path.Value.Split('/');

            if (pathComponents.Length > 1 && pathComponents[1].Equals("CoreX", StringComparison.OrdinalIgnoreCase))
            {
                if (pathComponents.Length > 2)
                {
                    var model = Runtime.Model;
                    if (pathComponents[2].Equals("Swagger", StringComparison.OrdinalIgnoreCase))
                    {
                        // Swagger routes
                        var fileName = context.Request.Path.Value.Substring(context.Request.Path.Value.LastIndexOf('/') + 1);
                        if(fileName.Equals("Swagger", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Response.Redirect($"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}{context.Request.Path}/");
                            return;
                        }

                        if(string.IsNullOrWhiteSpace(fileName))
                        {
                            fileName = "index.html";
                        }

                        Stream stream;
                        if(fileName == "swagger.json")
                        {
                            stream = new FileStream("Generated//swagger.json", FileMode.Open);
                        }
                        else
                        {
                            stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("CoreX.wwwroot.swagger." + fileName);
                        }

                        using (stream)
                        {
                            context.Response.ContentType = MimeTypes.GetMimeType(fileName);
                            await stream.CopyToAsync(context.Response.Body);
                        }
                        return;
                    }
                    else
                    {
                        // API routes
                        var entityName = pathComponents[2];
                        var action = context.Request.Method;

                        //TODO: Validate entityName

                        if (pathComponents.Length > 3)
                        {
                            //TODO: Validate action
                            action = pathComponents[3];
                        }

                        await context.Response.WriteAsync($"--- CoreX --- Entity = {entityName}, Action = {action}");
                        return;
                    }
                }
            }

            // Call the next delegate/middleware in the pipeline
            await this._next(context);
        }
    }
}
