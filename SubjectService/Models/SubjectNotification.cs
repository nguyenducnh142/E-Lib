namespace SubjectService.Models
{
    public class SubjectNotification
    {
        public int SubjectNotificationId { get; set; }
        public string TeacherName { get; set; }
        public string SubjectNotificationDetail { get; set; }
        public int SubjectId { get; set; }
    }
}
