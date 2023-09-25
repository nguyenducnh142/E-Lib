using NotificationService.DBContexts;
using NotificationService.Models;

namespace NotificationService.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly NotificationContext _notificationContext;

        public NotificationRepository(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }
        private void Save()
        {
            _notificationContext.SaveChanges();
        }

        public void AddNoti(Notification notification)
        {
            _notificationContext.Add(notification);
            Save();
        }

        public void AddPersonalNoti(PersonalNotification notification)
        {
            _notificationContext.Add(notification);
            Save();
        }

        public void DeleteNoti(string notiId)
        {
            var noti = _notificationContext.Notifications.Find(notiId);
            if (noti != null)
            {
                _notificationContext.Notifications.Remove(noti);
            }
            else
            {
                var personalNoti = _notificationContext.PersonalNotifications.Find(notiId);
                _notificationContext.PersonalNotifications.Remove(personalNoti);
            }
            
            Save();

        }

        public IEnumerable<Notification> FindNoti(string notiDetail)
        {
            var noti = _notificationContext.Notifications.Where(e => _notificationContext.FuzzySearch(e.NotificaitonDetail) == _notificationContext.FuzzySearch(notiDetail)).ToList();
            return noti;
        }

        public IEnumerable<PersonalNotification> GetAccountNoti()
        {
            return _notificationContext.PersonalNotifications.ToList();
        }

        public IEnumerable<Notification> GetQuestionNoti()
        {
            return _notificationContext.Notifications.Where(e => e.NotificationTypeId == 2).ToList();
        }

        public IEnumerable<Notification> GetSubjectNoti()
        {
            return _notificationContext.Notifications.Where(e => e.NotificationTypeId == 1).ToList();
        }
    }
}
