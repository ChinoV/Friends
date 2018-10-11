using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FriendsWebApp.Data;
using FriendsWebApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FriendsWebApp.Controllers
{
    public class PersonController : Controller
    {
        private readonly Context _context;

        public PersonController(Context context)
        {
            _context = context;
        }

        //get
        public IActionResult Index()
        {
            //_context.AddPerson(new Person
            //{
            //    Name = "James",
            //    LastName = "Howlett"
            //});

            //_context.AddPerson(new Person
            //{
            //    Name = "Eric",
            //    LastName = "Lehnsherr"
            //});

            //_context.AddPerson(new Person
            //{
            //    Name = "Charles",
            //    LastName = "Xavier"
            //});

            //_context.LinkFriends(1, 2);
            //_context.LinkFriends(1, 3);
            //_context.LinkFriends(2, 3);
            IList<Person> PeopleList = _context.GetPeople();
            return View();
        }

        public IActionResult GetPeople()
        {
            IList<Person> PeopleList = _context.GetPeople();
            return Json(PeopleList);
        }

        //get
        public IActionResult Create()
        {
            return Json(null);
        }

        //post
        [HttpPost]
        public IActionResult Create([Bind("Name,LastName")] Person person)
        {
            if (_context.AddPerson(person))
            {
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}