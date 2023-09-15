using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SubjectService.DBContexts;
using SubjectService.Migrations;
using SubjectService.Models;

namespace SubjectService.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly SubjectContext _dbContext;

        public TeacherRepository(SubjectContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateSubDes(string subjectDesciption, int subjectId)
        {
            var subDes = new Subject(){
                SubjectId = subjectId,
                SubjectDescription = subjectDesciption
            };

            _dbContext.Subjects.Attach(subDes);
            _dbContext.Subjects.Entry(subDes).Property(x => x.SubjectDescription).IsModified = true;
            Save();
            
        }
    }
}
