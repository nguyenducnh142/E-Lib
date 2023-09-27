using ExamService.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamService.DbContexts
{
    public class ExamContext : DbContext
    {
        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {

        }
        [DbFunction(name: "SOUNDEX", IsBuiltIn = true)]
        public string FuzzySearch(string subjectName)
        {
            throw new NotImplementedException();
        }
        public DbSet<Exam> Exams{ get; set; }
        public DbSet<TLQuestion> TLQuestions { get; set; }
        public DbSet<TNQuestion> TNQuestions { get; set; }
        public DbSet<TNQuestionExam> TNQuestionExams { get; set; }
    }
}
