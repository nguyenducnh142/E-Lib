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
        private async Task Save()
        {
            _notificationContext.SaveChanges();
        }

        public async Task AddNoti(string subjectId, string notiDetail)
        {
            var noti = new Notification();
            noti.SubjectId = subjectId;
            noti.NotificaitonDetail = notiDetail;
            noti.NotificationType = "subject";
            noti.DateTime = DateTime.Now;
            _notificationContext.Add(noti);
            await Save();
        }

        public async Task AddPersonalNoti(string userId, string notiDetail)
        {
            var noti = new UserNotification();
            noti.UserId = userId;
            noti.NotificationDetail = notiDetail;
            _notificationContext.Add(noti);
            await Save();
        }

        public async Task DeleteNoti(int notiId)
        {
            var noti = _notificationContext.Notifications.Find(notiId);
            if (noti != null)
            {
                _notificationContext.Notifications.Remove(noti);
            }
            else
            {
                var personalNoti = _notificationContext.UserNotifications.Find(notiId);
                _notificationContext.UserNotifications.Remove(personalNoti);
            }
            
            Save();

        }

        public async Task<IEnumerable<Notification>> FindNoti(string notiDetail)
        {
            var noti = _notificationContext.Notifications.Where(e => _notificationContext.FuzzySearch(e.NotificaitonDetail) == _notificationContext.FuzzySearch(notiDetail)).ToList();
            return noti;
        }

        public async Task<IEnumerable<UserNotification>> GetAccountNoti(string userId)
        {
            return _notificationContext.UserNotifications.Where(e=>e.UserId==userId).ToList();
        }

        public async Task<IEnumerable<Notification>> GetQuestionNoti()
        {
            return _notificationContext.Notifications.Where(e => e.NotificationType == "question").ToList();
        }

        public async Task<IEnumerable<Notification>> GetSubjectNoti()
        {
            return _notificationContext.Notifications.Where(e => e.NotificationType == "subject").ToList();
        }
    }
}
