using NotificationService.Models;

namespace NotificationService.Repository
{
    public interface INotificationRepository
    {
        public void AddNoti(Notification notification);
        public void AddPersonalNoti(PersonalNotification notification);
        public void DeleteNoti(string notiId);
        public IEnumerable<Notification> FindNoti(string notiDetail);
        public IEnumerable<PersonalNotification> GetAccountNoti();
        public IEnumerable<Notification> GetQuestionNoti();
        public IEnumerable<Notification> GetSubjectNoti();
    }
}
