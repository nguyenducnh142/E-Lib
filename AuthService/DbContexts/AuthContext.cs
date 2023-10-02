using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DbContexts
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        public DbSet<Account> Accounts { get; set; }
    }
}
