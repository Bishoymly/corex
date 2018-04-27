using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using CoreX.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;

namespace CoreX.Emit
{
    public class CodeGenerator : ICodeGenerator
    {
        public void GenerateCode(Model model)
        {
            EmitModels(model);
        }

        public static void EmitModels(Model model)
        {
            var path = "Generated\\CoreX.Generated.dll";
            var directory = Path.GetDirectoryName(path);
            var fileName = Path.GetFileName(path);

            if(!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var compilation = CSharpCompilation.Create(fileName)
              .WithOptions(
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
              .AddReferences(MetadataReference.CreateFromFile(typeof(Object).GetTypeInfo().Assembly.Location));

            foreach (var entity in model.Entities)
            {
                var code = GenerateEntity(model, entity);
                var tree = SyntaxFactory.ParseSyntaxTree(code);
                compilation = compilation.AddSyntaxTrees(tree);
            }
                        
            var compilationResult = compilation.Emit(path);

            if (compilationResult.Success == false)
            {
                var error = new StringBuilder();
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    error.AppendLine($"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()}, Location: { codeIssue.Location.GetLineSpan()}, Severity: { codeIssue.Severity}");
                }
                throw new Exception(error.ToString());
            }
        }

        public static string GenerateEntity(Model model, Entity entity)
        {
            var code = new StringBuilder();

            code.AppendLine("using System;");
            code.AppendLine();
            code.AppendLine("namespace CoreX.Generated.Models");
            code.AppendLine("{");
            code.AppendLine($"   public class {entity.Name}");
            code.AppendLine("   {");

            foreach (var property in entity.AllProperties)
            {
                code.AppendLine($"       public {ToCSharp(property.Type, !property.Required)} {property.Name} {{ get; set; }}");
            }

            code.AppendLine("   }");
            code.AppendLine("}");

            return code.ToString();
        }

        public static string ToCSharp(PropertyType type, bool isNullable = false)
        {
            switch (type)
            {
                case PropertyType.Boolean:
                    if (isNullable)
                        return "bool?";
                    else
                        return "bool";
                case PropertyType.String:
                    return "string";
                case PropertyType.Integer:
                    if (isNullable)
                        return "int?";
                    else
                        return "int";
                case PropertyType.Decimal:
                    if (isNullable)
                        return "decimal?";
                    else
                        return "decimal";
                case PropertyType.DateTime:
                    if (isNullable)
                        return "DateTime?";
                    else
                        return "DateTime";
                case PropertyType.UniqueIdentifier:
                    if (isNullable)
                        return "Guid?";
                    else
                        return "Guid";
                default:
                    return "string";
            }
        }
    }
}
