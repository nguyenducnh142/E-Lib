using System.ComponentModel.DataAnnotations;

namespace NotificationService.Models
{
    public class Notification
    {
        [Key]
        public int NotificationId { get; set; }
        public string NotificationType { get; set; }
        //NotificationType: subject/question
        public DateTime DateTime { get; set; }
        public string SubjectId { get; set; }
        public string NotificaitonDetail { get; set; }
    }
}
