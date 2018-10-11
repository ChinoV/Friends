using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsWebApp.Data.Entities
{
    public class Person
    {
        public Person()
        {
            Friends = new List<Person>();
        }

        public int PersonId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public ICollection<Person> Friends { get; set; }
    }
}
