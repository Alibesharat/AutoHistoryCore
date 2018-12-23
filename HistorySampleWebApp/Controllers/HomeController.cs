using AutoHistoryCore;
using HistorySampleWebApp.Models;
using HistorySampleWebApp.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace HistorySampleWebApp.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }


        public IActionResult Index()
        {
            using (var _db = new DatabaseContext())
            {
                //Add Item
                _db.students.Add(new Student() { age = 25, Name = "John doe" });
                _db.students.Add(new Student() { age = 30, Name = "raul costa" });
                _db.SaveChangesWithHistory(HttpContext);
            }
            using (var _db = new DatabaseContext())
            {
                //Search Object AsNotracking where IsDelete Equals false and Etc ...
                var student = _db.students.Undelited<Student>().FirstOrDefault(c => c.age == 25);
                //Edit item
                student.Name = "Eli tailor";
                student.age = 29;
                _db.Update(student);
                _db.SaveChangesWithHistory(HttpContext);
            }
            using (var _db = new DatabaseContext())
            {
                //Search Object AsNotracking where IsDelete Equals false and Etc ...
                var student = _db.students.Undelited<Student>().FirstOrDefault(c => c.age == 29);
                _db.students.Remove(student);

                //Safe-Delete(Logical Delete)
                _db.SaveChangesWithHistory(HttpContext);
            }
            using (var _db = new DatabaseContext())
            {
                var History = _db.students.FirstOrDefault().Hs_Change;
                var data = JsonConvert.DeserializeObject<List<HistoryViewModel>>(History);

                return Json(data);
            }











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
