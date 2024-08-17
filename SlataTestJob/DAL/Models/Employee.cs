using System.ComponentModel.DataAnnotations;

namespace SlataTestJob.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string JobPosition { get; set; }
        public string Department { get; set; }
    }
}
