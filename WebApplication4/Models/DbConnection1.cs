using Microsoft.EntityFrameworkCore;

namespace WebApplication4.Models
{
    public class DbConnection1:DbContext
    {
        public DbConnection1(DbContextOptions<DbConnection1> options):base (options)
        {
            
        }
        public DbSet<Employee> Employee { get; set; }
        //public DbSet<ScannedDocument> scan_document { get; set; }
        //public DbSet<Course> CourseNew { get; set; }
    }
}
