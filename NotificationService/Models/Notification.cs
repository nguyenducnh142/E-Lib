using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Notification
    {
        [Key]
        public string NotificationId { get; set; }
        public int NotificationTypeId { get; set; }
        //NotificationType: 1=Subject, 2=Question
        public string NotificaitonDetail { get; set; }
    }
}
