using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class UserNotification
    {
        [Key]
        public int NotificationId { get; set; }
        public string NotificationDetail{ get; set; }
        public string UserId { get; set; }
    }
}
