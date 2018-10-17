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
            return View();
        }

        public IActionResult RelationGraph()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetGraph([FromBody]int personId)
        {
            List<JPerson> PeopleList = _context.GetGraph(personId);

            int Top = 22;
            for (int i = 0; i < PeopleList.Count; i++)
            {
                StringBuilder neighbors = new StringBuilder();
                foreach (var s in PeopleList[i].Neighbors)
                {
                    neighbors.Append(s).Append(",");
                }

                if (i == 0)
                {
                    PeopleList[i].Left = "248.859px";
                }
                else if (i % 2 != 0)
                {
                    Top += 126;
                    PeopleList[i].Left = "80.8594px";
                }
                else
                {
                    PeopleList[i].Left = "416.859px";
                }
                PeopleList[i].Top = Top + "px";


                neighbors.Length--;
            }


            JPerson[] jPeople = new JPerson[PeopleList.Count];
            for (int i = 0; i < PeopleList.Count; i++)
            {
                jPeople[i] = PeopleList[i];
            }

            return Json(PeopleList);
        }

        [HttpPost]
        public IActionResult GetNewGraph([FromBody]int personId, List<JPerson> jPeople)
        {
            List<JPerson> PeopleList = _context.GetGraph(personId);

            var LastPerson = jPeople[jPeople.Count - 1];
            int Top = int.Parse(LastPerson.Top.Replace("px", "")) + 126;
            string LastLeft = LastPerson.Left;

            var FriendsToAdd = new List<JPerson>();

            for (int i = 0; i < PeopleList.Count; i++)
            {
                if (!jPeople.Exists(x => x.Id == PeopleList[i].Id))
                {
                    jPeople.Add(PeopleList[i]);
                    var Last = jPeople[FriendsToAdd.Count - 1];
                    if (LastLeft.Equals("248.859px") || LastLeft.Equals("416.859px"))
                    {
                        Top += 126;
                        Last.Left = "80.8594px";
                        LastLeft = "80.8594px";
                    }
                    else
                    {
                        Last.Left = "416.859px";
                    }
                    Last.Top += "px";
                    for (int j = 0; j < Last.Neighbors.Count - 1; j++)
                    {
                        var Neighbor = jPeople.Find(x => x.Id == Last.Neighbors[i]);
                        if (!Neighbor.Neighbors.Exists(x => x.Equals(Last.Id)))
                        {
                            Neighbor.Neighbors.Add(Last.Id);
                        }
                    }
                }
            }
            return Json(FriendsToAdd);
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