using Microsoft.EntityFrameworkCore;
using SubjectService.Models;

namespace SubjectService.DBContexts
{
    public class SubjectContext : DbContext
    {
        public SubjectContext(DbContextOptions<SubjectContext> options):base(options)
        {
            
        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<SubjectType> Types { get; set; }
    }
}
