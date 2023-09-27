namespace AuthService.Models
{
    public record User(string UserId, string Password, string Role);
    /*public class UserLogin
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Role {  get; set; }
    }*/
}
