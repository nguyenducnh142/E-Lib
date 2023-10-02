using NotificationService.Models;

namespace NotificationService.Repository
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Notification>> FindNoti(string notiDetail, string userId);
        Task<IEnumerable<UserNotification>> GetAccountNoti(string userId);
        Task<IEnumerable<Notification>> GetQuestionNoti(string userId);
        Task<IEnumerable<Notification>> GetSubjectNoti(string userId);
    }
}
