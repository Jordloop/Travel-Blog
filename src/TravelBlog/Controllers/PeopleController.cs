﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelBlog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TravelBlog.Controllers
{
    public class PeopleController : Controller
    {
        //Instantiate database object
        private TravelBlogContext db = new TravelBlogContext();
        //Collects List of Experiences from DB
        public IActionResult Index()
        {
            return View(db.People.Include(people => people.Experience).ToList());
        }
        public IActionResult Details(int id)
        {
            var thisPeople = db.People.FirstOrDefault(people => people.PeopleId == id);
            return View(thisPeople);
        }
        //Create
        public IActionResult Create()
        {
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Story");
            return View();
        }
        [HttpPost]
        public IActionResult Create(People people)
        {
            db.People.Add(people);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Edit
        public IActionResult Edit(int id)
        {
            var thisPeople = db.People.FirstOrDefault(people => people.PeopleId == id);
            ViewBag.ExperienceId = new SelectList(db.Experiences, "ExperienceId", "Story");
            return View(thisPeople);
        }
        [HttpPost]
        public IActionResult Edit(People people)
        {
            db.Entry(people).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Delete
        public ActionResult Delete(int id)
        {
            var thisPeople = db.People.FirstOrDefault(people => people.PeopleId == id);
            return View(thisPeople);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisPeople = db.People.FirstOrDefault(people => people.PeopleId == id);
            db.People.Remove(thisPeople);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
