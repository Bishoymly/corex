using System;
using System.Collections.Generic;
using System.Text;
using CoreX.Models;

namespace CoreX.Emit.Swagger
{
    public class SwaggerGenerator : ICodeGenerator
    {
        public void GenerateCode(Model model)
        {
            var path = "Generated\\swagger.json";
            var root = new Rootobject
            {
                swagger = "2.0",
                basePath = "/corex/api/",
                info = new Info
                {
                    title = "CoreX API Swagger",
                    version = "1.0.0"
                },
                schemes = new string[] { "http", "https" },
                tags = new List<Tag1>()
            };

        }
    }
}
