using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCalTechTalkTodo.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly InMemoryDatabase _database;

        public TodoController(InMemoryDatabase database)
        {
            _database = database;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(_database.Data.Values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            var found = _database.Data.TryGetValue(id, out TodoItem value);
            if (!found)
                return new NoContentResult();
            return new JsonResult(value);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]TodoItem value)
        {
            bool createdSuccesfully = false;
            //add the new item to the database!
            if (createdSuccesfully)
                return new OkResult();
            else
                throw new ArgumentException("The content is invalid");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
    }
}
