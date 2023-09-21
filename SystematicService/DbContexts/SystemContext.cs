using Microsoft.EntityFrameworkCore;
using SystematicService.Models;

namespace SystematicService.DbContexts
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options)
        {
        }
        [DbFunction(name: "SOUNDEX", IsBuiltIn = true)]
        public string FuzzySearch(string subjectName)
        {
            throw new NotImplementedException();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<SystemInfo> SystemInfos { get; set; }
    }
}
