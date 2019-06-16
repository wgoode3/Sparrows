using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sparrows.Models;

namespace Sparrows.Controllers
{
    public class HomeController : Controller
    {
        
        private static DBContext context;
        public HomeController(DBContext DBContext)
        {
            context = DBContext;
        }
        
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.Notes = context.Notes;
            return View();
        }

        [HttpPost("note")]
        public IActionResult Create(Note n)
        {
            context.Notes.Add(n);
            context.SaveChanges();
            return Redirect("/");
        }

        [HttpGet("delete/{NoteId}")]
        public IActionResult Delete(int NoteId)
        {
            Note note = context.Notes.FirstOrDefault(n => n.NoteId == NoteId);
            context.Notes.Remove(note);
            context.SaveChanges();
            return Redirect("/");
        }

    }
}
