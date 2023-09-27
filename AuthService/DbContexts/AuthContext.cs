using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.DbContexts
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {

        }

        public DbSet<AuthenticationToken> AuthenticationTokens{ get; set; }
        public DbSet<Login> Logins { get; set; }
        //public DbSet<UserLogin> UserLogins { get; set; }
    }
}
