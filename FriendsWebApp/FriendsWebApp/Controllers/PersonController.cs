using FriendsWebApp.Data;
using FriendsWebApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;

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

            return View();
        }

        //get graph
        public IActionResult GetGraph()
        {
            List<JPerson> PeopleList = _context.GetGraph(1);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < PeopleList.Count; i++)
            {
                sb.Append("{ \"id\": ").Append(PeopleList[i].Id).Append(" \"text\": ")
                    .Append(PeopleList[i].Text)
                    .Append(", \"position\": { \"left\": \"248.859px\", \"top\": \"22px\" }, \"neighbors\": [ \"3, 2, 4\" ] },");
            }
            sb.Length--;
            return Json(null);
        }

        //get
        public IActionResult GetPeople()
        {
            IList<Person> PeopleList = _context.GetPeople();
            return Json(PeopleList);
        }

        [HttpPost]
        public IActionResult GetNoneFriends([FromBody]int personId)
        {
            IList<Person> PeopleList = _context.GetNoneFriends(personId);
            return Json(PeopleList);
        }

        [HttpPost]
        public IActionResult GetFriends([FromBody]int personId)
        {
            try
            {
                IList<Person> PeopleList = _context.GetFriends(personId);
                return Json(PeopleList);
            }
            catch (System.Exception)
            {
                return Json(new { success = false });
            }
           
        }

        [HttpPost]
        public IActionResult RemoveFriendship([FromBody]DoubleParam personIds)
        {
            try
            {                
                if (_context.RemoveFriendship(personIds.Id1, personIds.Id2))
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false }); 
            }
            catch (System.Exception)
            {
                return Json(new { success = false });
            }            
        }

        [HttpPost]
        public IActionResult LinkFriends([FromBody]DoubleParam personIds)
        {
            try
            {
                if (_context.LinkFriends(personIds.Id1, personIds.Id2))
                {
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch (System.Exception)
            {
                return Json(new { success = false });
            }
        }

        
        [HttpPost]
        public Person Create([FromBody]Person person)
        {
            if (_context.AddPerson(person))
            {
                person = _context.GetLastPersonInsert();
                return person;
            }
            return null;
        }

        //get
        [HttpDelete]
        public IActionResult Delete([FromBody]int personId)
        {
            try
            {
                if (_context.RemovePerson(personId))
                {
                    return Json(new { success = true });
                }

                return Json(new { success = false });
            }
            catch (System.Exception)
            {
                return Json(new { success = false });
            }
        }

        
        [HttpPut]
        public IActionResult Edit([FromBody]Person person)
        {
            try
            {
                if (_context.EditPerson(person))
                {
                    return Json(person);
                }
                return Json(new { success = false });
            }
            catch (System.Exception)
            {
                throw;
            }
            
        }
    }
}