using System.ComponentModel.DataAnnotations;

namespace WebApplication4.Models
{
    public class Course
    {
        [Key]
        public int COURSE_ID { get; set; }
        public string  COURSE_NAME { get; set; }
    }
}
