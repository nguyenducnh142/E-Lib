using NotificationService.Models;

namespace NotificationService.Repository
{
    public interface INotificationRepository
    {
         Task AddNoti(string subjectId, string notiDetail);
         Task AddQuestionNoti(string subjectId, string notiDetail);
         Task AddPersonalNoti(string userId, string notiDetail);
         Task DeleteNoti(int notiId);
         Task<IEnumerable<Notification>> FindNoti(string notiDetail);
         Task<IEnumerable<UserNotification>> GetAccountNoti(string userId);
         Task<IEnumerable<Notification>> GetQuestionNoti();
         Task<IEnumerable<Notification>> GetSubjectNoti();
    }
}
