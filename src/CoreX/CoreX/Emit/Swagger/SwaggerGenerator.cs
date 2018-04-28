using System;
using System.Collections.Generic;
using System.Text;
using CoreX.Models;
using Newtonsoft.Json;
using Humanizer;

namespace CoreX.Emit.Swagger
{
    public class SwaggerGenerator : ICodeGenerator
    {
        public void GenerateCode(Model model)
        {
            var filePath = "Generated\\swagger.json";
            
            var doc = new SwaggerDocument
            {
                Host = "localhost",
                BasePath = "/corex/api/",
                Info = new Info
                {
                    Title = "CoreX API Swagger",
                    Version = "1.0.0"
                },
                Schemes = new string[] { "http", "https" },
                Tags = new List<Tag>(),
                Paths = new Dictionary<string, PathItem>(),
                Responses = new Dictionary<string, Response>(),
                Definitions = new Dictionary<string, Schema>()
            };

            foreach (var entity in model.Entities)
            {
                string name = entity.Name.ToLower();

                var tag = new Tag
                {
                    Name = entity.Name
                };

                doc.Tags.Add(tag);

                var schema = new Schema
                {
                    Type = "object",
                    Properties = new Dictionary<string, Schema>(),
                    Xml = new Xml
                    {
                        Name = entity.Name.Camelize()
                    }
                };

                foreach (var property in entity.AllProperties)
                {
                    var type = new Schema();
                    type.Type = GetJavascriptType(property.Type);
                    schema.Properties.Add(property.Name.Camelize(), type);
                }

                doc.Definitions.Add(entity.Name.Camelize(), schema);

                foreach (var action in entity.AllActions)
                {
                    var url = $"/{name}/{action.Name.ToLower()}";

                    if(action.Input == ActionSchema.Id)
                    {
                        url += "/{id}";
                    }

                    if (!doc.Paths.ContainsKey(url))
                    {
                        doc.Paths.Add(url, new PathItem());
                    }

                    var path = doc.Paths[url];
                    var operation = new Operation
                    {
                        OperationId = action.Name,
                        Summary = "",
                        Description = "",
                        Consumes = new List<string> { "application/json" },
                        Produces = new List<string> { "application/json" },
                        Parameters = new List<IParameter>(),
                        Responses = new Dictionary<string, Response>(),
                        Tags = new List<string> { entity.Name }
                    };

                    switch (action.Input)
                    {
                        case ActionSchema.Nothing:
                            break;
                        case ActionSchema.Id:
                            operation.Parameters.Add(new NonBodyParameter
                            {
                                Name = "id",
                                In = "path",
                                Required = true,
                                Type = GetJavascriptType(entity.PrimaryKeyType),
                                Format = GetJavascriptType(entity.PrimaryKeyType)
                            });
                            break;
                        case ActionSchema.Entity:
                            operation.Parameters.Add(new BodyParameter
                            {
                                In = "body",
                                Description = "",
                                Name = "body",
                                Required = true,
                                Schema = new Schema
                                {
                                    Ref = "#/definitions/" + entity.Name.Camelize()
                                }
                            });
                            break;
                        case ActionSchema.List:
                            operation.Parameters.Add(new BodyParameter
                            {
                                In = "body",
                                Description = "",
                                Name = "body",
                                Required = true,
                                Schema = new Schema
                                {
                                    Type = "array",
                                    Items = new Schema
                                    {
                                        Ref = "#/definitions/" + entity.Name.Camelize()
                                    }
                                }
                            });
                            break;
                    }

                    switch (action.Output)
                    {
                        case ActionSchema.Nothing:
                            operation.Responses.Add("200", new Response { Description = "Success" });
                            break;
                        case ActionSchema.Id:
                            operation.Responses.Add("200", new Response
                            {
                                Description = "Success",
                                Schema = new Schema { 
                                    Type = GetJavascriptType(entity.PrimaryKeyType),
                                    Format = GetJavascriptType(entity.PrimaryKeyType)
                                }
                            });
                            break;
                        case ActionSchema.Entity:
                            operation.Responses.Add("200", new Response
                            {
                                Description = "Success",
                                Schema = new Schema { Ref = "#/definitions/" + entity.Name.Camelize() }
                            });
                            break;
                        case ActionSchema.List:
                            operation.Responses.Add("200", new Response
                            {
                                Description = "Success",
                                Schema = new Schema
                                {
                                    Type = "array",
                                    Items = new Schema
                                    {
                                        Ref = "#/definitions/" + entity.Name.Camelize()
                                    }
                                }
                            });
                            break;
                    }

                    if(action.Name.StartsWith("get", StringComparison.OrdinalIgnoreCase))
                    {
                        path.Get = operation;
                    }
                    else if (action.Name.StartsWith("delete", StringComparison.OrdinalIgnoreCase))
                    {
                        path.Delete = operation;
                    }
                    else if (action.Name.StartsWith("put", StringComparison.OrdinalIgnoreCase)
                        || action.Name.StartsWith("update", StringComparison.OrdinalIgnoreCase))
                    {
                        path.Put = operation;
                    }
                    else
                    {
                        path.Post = operation;
                    }                    
                }
            }

            System.IO.File.WriteAllText(filePath, JsonConvert.SerializeObject(doc));
        }

        public static string GetJavascriptType(PropertyType type)
        {
            switch (type)
            {
                case PropertyType.Boolean:
                    return "boolean";
                case PropertyType.Integer:
                    return "integer";
                case PropertyType.Decimal:
                    return "number";
                default:
                    return "string";
            }
        }

        public static string GetJavascriptFormat(PropertyType type)
        {
            switch (type)
            {
                case PropertyType.Integer:
                    return "int32";
                case PropertyType.Decimal:
                    return "double";
                case PropertyType.DateTime:
                    return "date-time";
                default:
                    return null;
            }
        }
    }
}
