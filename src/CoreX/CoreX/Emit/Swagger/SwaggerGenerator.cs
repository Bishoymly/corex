using System;
using System.Collections.Generic;
using System.Text;
using CoreX.Models;
using Newtonsoft.Json;

namespace CoreX.Emit.Swagger
{
    public class SwaggerGenerator : ICodeGenerator
    {
        public void GenerateCode(Model model)
        {
            var path = "Generated\\swagger.json";
            
            var doc = new SwaggerDocument
            {
                BasePath = "/corex/api/",
                Info = new Info
                {
                    Title = "CoreX API Swagger",
                    Version = "1.0.0"
                },
                Schemes = new string[] { "http", "https" },
                Tags = new List<Tag>(),
                Paths = new Dictionary<string, PathItem>()
            };

            foreach (var entity in model.Entities)
            {
                var tag = new Tag
                {
                    Name = entity.Name
                };

                doc.Tags.Add(tag);
            }

            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(doc));
        }
    }
}
