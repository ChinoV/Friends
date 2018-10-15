﻿using FriendsWebApp.Data;
using FriendsWebApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        //get
        public IActionResult GetPeople()
        {
            IList<Person> PeopleList = _context.GetPeople();
            return Json(PeopleList);
        }

        //get
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //post
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
        public IActionResult Delete(int id)
        {
            try
            {
                if (_context.RemovePerson(id))
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

        //get
        //public IActionResult Edit(int Id)
        //{
        //    Person person = new Person();
        //    person = _context.GetPerson(Id);
        //    return View(Json(person));
        //}

        //post
        [HttpPost]
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