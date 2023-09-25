using FileService.Models;
using Microsoft.EntityFrameworkCore;

namespace FileService.DbContexts
{
    public class FileContext : DbContext
    {
        public FileContext(DbContextOptions<FileContext> options) : base(options)
        {

        }

        [DbFunction(name: "SOUNDEX", IsBuiltIn = true)]
        public string FuzzySearch(string value)
        {
            throw new NotImplementedException();
        }
        public DbSet<PersonalFile> PersonalFiles { get; set; }
    }
}
