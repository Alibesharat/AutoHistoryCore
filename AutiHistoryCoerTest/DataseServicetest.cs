using AutoHistoryCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace AutiHistoryCoerTest
{

    public class testdb : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "testdb");
            base.OnConfiguring(optionsBuilder);


        }

        public DbSet<Student> Students { get; set; }
    }


    public class Student : HistoryBaseModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string LastName { get; set; }
    }

    public class DataseServicetest
    {

        private readonly ITestOutputHelper _testOutputHelper;

        public DataseServicetest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void Write_Read_data()
        {
            testdb db = new testdb();
            Student st = new Student()
            {
                Name = "jhon",
                LastName = "Doe"
            };
            db.Add(st);
            db.SaveChangesWithHistory(null);
        
            var savedStudent = db.Students.FirstOrDefault();
            _testOutputHelper.WriteLine(savedStudent.Hs_Change);
            Assert.NotNull(savedStudent.Hs_Change);
        }
    }
}

