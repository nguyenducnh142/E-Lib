using Microsoft.EntityFrameworkCore;

namespace NotificationService.Models
{
    [Keyless]
    public class StudentClass
    {
        public string UserId { get; set; }
        public string ClassId{ get; set; }
    }
}
