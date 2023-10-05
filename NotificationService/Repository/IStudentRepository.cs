using NotificationService.Models;

namespace NotificationService.Repository
{
    public interface IStudentRepository
    {
        IEnumerable<Notification> FindNoti(string notiDetail, string userId);
        IEnumerable<UserNotification> GetAccountNoti(string userId);
        IEnumerable<Notification> GetQuestionNoti(string userId);
        IEnumerable<Notification> GetSubjectNoti(string userId);
    }
}
