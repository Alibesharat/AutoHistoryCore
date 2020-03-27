using AutoHistoryCore;
using HistorySampleWebApp.Models;
using HistorySampleWebApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

      
        // [Route("api/test")]
        public IActionResult Index()
        {
            using (var _db = new DatabaseContext())
            {
                //Add Item
                _db.students.Add(new Student() { age = 25, Name = "John doe" });
                _db.students.Add(new Student() { age = 30, Name = "raul costa" });
                _db.students.Add(new Student() { age = 35, Name = "CR 7" });
                _db.SaveChangesWithHistory(HttpContext);
            }
            using (var _db = new DatabaseContext())
            {
                //Search Object AsNotracking where IsDelete Equals false and Etc ...
                var student = _db.students.Undelited().FirstOrDefault(c => c.age == 25);
                //Edit item
                student.Name = "Eli tailor";
                _db.Update(student);
                _db.SaveChangesWithHistory(HttpContext);
            }
            using (var _db = new DatabaseContext())
            {
                //Search Object AsNotracking where IsDelete Equals false and Etc ...
                var student = _db.students.Undelited().FirstOrDefault(c => c.age == 25);
                //Edit item
                student.Name = "other name";
                _db.Update(student);
                _db.SaveChangesWithHistory(HttpContext);
            }
            using (var _db = new DatabaseContext())
            {
                //Search Object AsNotracking where IsDelete Equals false and Etc ...
                var student = _db.students.Undelited().FirstOrDefault(c => c.age == 25);
                //Edit item
                student.Name = "David Beckham";
                _db.Update(student);
                _db.SaveChangesWithHistory(HttpContext);
            }
            using (var _db = new DatabaseContext())
            {
                //Search Object AsNotracking where IsDelete Equals false and Etc ...
                var student = _db.students.Undelited().FirstOrDefault(c => c.age == 25);
                _db.students.Remove(student);

                //soft-Delete(Logical Delete)
                _db.SaveChangesWithHistory(HttpContext);
            }
            using (var _db = new DatabaseContext())
            {
                var History = _db.students.FirstOrDefault(c=>c.age==25)?.Hs_Change;
                var data = JsonConvert.DeserializeObject<List<HistoryViewModel>>(History);

                return Json(data);
            }











        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
