using Microsoft.EntityFrameworkCore;
using SubjectService.Models;

namespace SubjectService.DBContexts
{
    public class SubjectContext : DbContext
    {
        public SubjectContext(DbContextOptions<SubjectContext> options):base(options)
        {
            
        }

        [DbFunction(name:"SOUNDEX",IsBuiltIn =true)]
        public string FuzzySearch(string subjectName)
        {
            throw new NotImplementedException();
        }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonFile> LessonsFiles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<SubjectNotification> SubjectNotifications { get; set; }
        public DbSet<SubjectClass> SubjectClasses { get; set; }
        public DbSet<StarSubject> StarSubjects { get; set; }
    }
}
