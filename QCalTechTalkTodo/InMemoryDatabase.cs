using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCalTechTalkTodo
{
    public class InMemoryDatabase
    {
        static InMemoryDatabase()
        {

        }
        public Dictionary<long, TodoItem> Data { get; set; }
        
    }
}
