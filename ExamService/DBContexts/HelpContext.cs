using Microsoft.EntityFrameworkCore;
using HelpService.Models;

namespace HelpService.DBContexts
{
    public class HelpContext : DbContext
    {
        public HelpContext(DbContextOptions<HelpContext> options):base(options)
        {
            
        }


        public DbSet<Help> Helps { get; set; }

    }
}
