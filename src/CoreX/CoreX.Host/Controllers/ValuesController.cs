using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoreX.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreX.Host.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var model = Samples.CreateSampleModel();
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(model);
            System.IO.File.WriteAllText("SampleModel.json", result);

            var model2 = Newtonsoft.Json.JsonConvert.DeserializeObject<Model>(System.IO.File.ReadAllText("SampleModel.json"));
            model2.Initialize();
            var result2 = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            if (result2 != result)
                throw new Exception("Serialization Error");

            return new string[] { result };
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
