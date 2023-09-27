using Microsoft.EntityFrameworkCore;

namespace AuthService.Models
{
    [Keyless]
    public class Login
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }
}
