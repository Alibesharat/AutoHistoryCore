using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HistorySampleWebApp.Models;
using HistorySampleWebApp.Service;
using AutoHistoryCore;

namespace HistorySampleWebApp.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext _db;
        public HomeController(DatabaseContext databaseContext)
        {
            _db = databaseContext;
        }


        public IActionResult Index()
        {
            // _db.students.Add(new Student() { age = 25, Name = "Mina" });
            // // _db.SaveChanges();
            // _db.SaveChangesWithHistory();

            //var student= _db.students.Where(c=>c.IsDeleted==false).FirstOrDefault();
            // student.Name = "karim";
            // _db.Update(student);
            // _db.SaveChangesWithHistory();
            var data = _db.students.Undelited<Student>().Where(c=>c.Name=="Shima").ToList();

            //_db.teachers.Add(new Teacher() { Level = 20, Name = "John Doe" });
            //_db.SaveChangesWithHistory();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
