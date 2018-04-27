using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CoreX.Emit;
using CoreX.Emit.Swagger;
using CoreX.Models;
using Newtonsoft.Json;

namespace CoreX.Services
{
    public class Runtime
    {
        private const string DefaultModelFile = "Generated\\Model.json";

        private static Model _model = null;
        public static Model Model
        {
            get
            {
                if(_model == null)
                {
                    // Load from file
                    if (File.Exists(DefaultModelFile))
                    {
                        _model = JsonConvert.DeserializeObject<Model>(File.ReadAllText(DefaultModelFile));
                        _model.Initialize();
                        CodeGenerator.EmitModels(_model);
                        var swagger = new SwaggerGenerator();
                        swagger.GenerateCode(_model);
                    }
                }

                return _model;
            }
        }
    }
}
