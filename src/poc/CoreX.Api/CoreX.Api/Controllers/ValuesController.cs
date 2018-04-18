using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Runtime.Loader;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace CoreX.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public static bool DllGenerated = false;

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string fileName = "mylib.dll";
            string path = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            var output = new List<string>();

            if(!DllGenerated)
                {
            const string code = @"using System;
            using Microsoft.EntityFrameworkCore;
            namespace Test
            {
                public class Sample
                {
                    public int Id {get;set;}
                    public string Bio {get;set;}
                    public string Name {get;set;}
                    public DateTime PublishDate {get;set;}
                }

                public class DB : DbContext
                {
                    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    {
                        optionsBuilder.UseSqlServer(""Data Source=.;user id=sa;password=P@ssw0rd;Initial Catalog=AbpProjectNameDb"");
                        base.OnConfiguring(optionsBuilder);
                    }

                    public virtual DbSet<Sample> Samples{get;set; }
                }
            }";
            var tree = SyntaxFactory.ParseSyntaxTree(code);
            

            // A single, immutable invocation to the compiler
            // to produce a library
            var compilation = CSharpCompilation.Create(fileName)
              .WithOptions(
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
              .AddReferences(MetadataReference.CreateFromFile(typeof(Object).GetTypeInfo().Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(Object).GetTypeInfo().Assembly.Location.Replace("System.Private.CoreLib.dll","netstandard.dll")),
                            MetadataReference.CreateFromFile(typeof(Object).GetTypeInfo().Assembly.Location.Replace("System.Private.CoreLib.dll","System.Runtime.dll")),
                            MetadataReference.CreateFromFile(typeof(Object).GetTypeInfo().Assembly.Location.Replace("System.Private.CoreLib.dll","System.Data.Common.dll")),
                            MetadataReference.CreateFromFile(typeof(DbContext).GetTypeInfo().Assembly.Location),
                            MetadataReference.CreateFromFile(typeof(SqlServerDbContextOptionsExtensions).GetTypeInfo().Assembly.Location))
              .AddSyntaxTrees(tree);
            
            EmitResult compilationResult = compilation.Emit(path);
                if (compilationResult.Success)
            {
             DllGenerated = true;   
            }
            else
            {
                foreach (Diagnostic codeIssue in compilationResult.Diagnostics)
                {
                    string issue = $"ID: {codeIssue.Id}, Message: {codeIssue.GetMessage()}, Location: { codeIssue.Location.GetLineSpan()}, Severity: { codeIssue.Severity}";
                    output.Add(issue);
                }
            }
            }
            
            if(DllGenerated)
                {
            // Load the assembly
                Assembly asm = AssemblyLoadContext.Default.LoadFromAssemblyPath(path);
                var db = asm.GetType("Test.DB").GetConstructor(new Type[0]).Invoke(new object[0]);
                var result = db.GetType().GetProperty("Samples").GetValue(db);
                output.Add(JsonConvert.SerializeObject(result));
            }

            return output.ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
