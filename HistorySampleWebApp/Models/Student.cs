using AutoHistoryCore;

namespace HistorySampleWebApp.Models
{
    public class Student : HistoryBaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int age { get; set; }
    }
}
