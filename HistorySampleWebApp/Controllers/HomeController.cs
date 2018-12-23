using AutoHistoryCore;
using HistorySampleWebApp.Models;
using HistorySampleWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
           // _db.students.Add(new Student() { age = 25, Name = "Kamran" });
            // _db.SaveChanges();
           // _db.SaveChangesWithHistory();
            var student = _db.students.FirstOrDefault();
            //_db.Entry<Student>(student).State = Microsoft.EntityFrameworkCore.EntityState.Detached;

            student.Name = "aLIA";
            student.IsDeleted = false;
           _db.Update(student);
            //_db.students.Remove(student);
           // string ip =HttpContext.Connection.RemoteIpAddress.ToString();
            _db.SaveChangesWithHistory(HttpContext);
           /// _db.Entry<Student>(student).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
           
           

            //_db.students.Remove(student);
            //_db.SaveChangesWithHistory();
            var History = _db.students.FirstOrDefault().Hs_Change;
            var data = JsonConvert.DeserializeObject<List<HistoryViewModel>>(History);
            //_db.teachers.Add(new Teacher() { Level = 20, Name = "John Doe" });
            //_db.SaveChangesWithHistory();
            return Json(data);
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
