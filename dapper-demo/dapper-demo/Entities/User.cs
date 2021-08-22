using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dapper_demo.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public List<Note> Notes { get; set; }
        public User()
        {
            this.Notes = new List<Note>();
        }
    }
}
