using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class PersonalNotification
    {
        [Key]
        public string NotificationId { get; set; }
        public string NotificationDetail { get; set; }
        public string UserId { get; set; }
    }
}
