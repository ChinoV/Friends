using FriendsWebApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FriendsWebApp.Data.Initializer
{
    public class ContextInitializer
    {
        private readonly Context _context;

        public ContextInitializer(Context context)
        {
            _context = context;
        }

        public void InitializeDb()
        {
            _context.AddPerson(new Person {
                Name = "James",
                LastName= "Howlett"
            });

            _context.AddPerson(new Person
            {
                Name = "Eric",
                LastName = "Lehnsherr"
            });

            _context.AddPerson(new Person
            {
                Name = "Charles",
                LastName = "Xavier"
            });

            _context.LinkFriends(1, 2);
            _context.LinkFriends(1, 3);
            _context.LinkFriends(2, 3);            
        }


    }
}
