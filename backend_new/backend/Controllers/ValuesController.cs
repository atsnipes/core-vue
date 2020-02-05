using System;
using System.Collections.Generic;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/values")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("serPost")]
        public ActionResult SerPost(string text)
        {
            SerialService serService = new SerialService("/dev/ttyACM0");

            serService.write("/dev/ttyACM0",9600);
            Console.WriteLine("-------9600--------");
            serService.write("/dev/ttyACM0", 1200);
            Console.WriteLine("-------1200--------");
            serService.write("/dev/ttyACM0", 2400);
            Console.WriteLine("-------2400--------");
            serService.write("/dev/ttyACM0", 4800);
            Console.WriteLine("-------4800--------");
            serService.write("/dev/ttyACM0", 19200);
            Console.WriteLine("-------19200--------");
            serService.write("/dev/ttyACM0", 38400);
            Console.WriteLine("-------38400--------");
            serService.write("/dev/ttyACM0", 57600);
            Console.WriteLine("-------57600--------");
            serService.write("/dev/ttyACM0", 115200);
            Console.WriteLine("-------115200--------");

            return Ok(serService.PortNames);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
