using NotificationService.DBContexts;
using NotificationService.Models;

namespace NotificationService.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly NotificationContext _dbContext;
        public StudentRepository(NotificationContext notificationContext)
        {
            _dbContext = notificationContext;
        }

        public async Task<IEnumerable<Notification>> FindNoti(string notiDetail, string userId)
        {
            var studentClass = _dbContext.studentClasses.Where(e => e.UserId == userId).FirstOrDefault();
            if (studentClass != null)
            {
                var subjectsClass = _dbContext.subjectClasses.Where(e => e.ClassId == studentClass.ClassId).ToList();
                var notifications = new List<Notification>();
                foreach (var subject in subjectsClass)
                {
                    notifications.Add(_dbContext.Notifications.Where(e => e.SubjectId == subject.SubjectId &&_dbContext.FuzzySearch(e.NotificaitonDetail) == _dbContext.FuzzySearch(notiDetail)).FirstOrDefault());
                }
                return notifications;
            }
            return null;
        }

        public async Task<IEnumerable<UserNotification>> GetAccountNoti(string userId)
        {
            return _dbContext.UserNotifications.Where(e=>e.UserId==userId).ToList();
        }

        public async Task<IEnumerable<Notification>> GetQuestionNoti(string userId)
        {
            var studentClass = _dbContext.studentClasses.Where(e => e.UserId == userId).FirstOrDefault();
            if (studentClass != null)
            {
                var subjectsClass = _dbContext.subjectClasses.Where(e => e.ClassId == studentClass.ClassId).ToList();
                var notifications = new List<Notification>();
                foreach (var subject in subjectsClass)
                {
                    notifications.Add(_dbContext.Notifications.Where(e => e.SubjectId == subject.SubjectId && e.NotificationType == "question").FirstOrDefault());
                }
                return notifications;
            }
            return null;
        }

        public async Task<IEnumerable<Notification>> GetSubjectNoti(string userId)
        {
            var studentClass = _dbContext.studentClasses.Where(e=>e.UserId == userId).FirstOrDefault();
            if (studentClass != null)
            {
                var subjectsClass = _dbContext.subjectClasses.Where(e => e.ClassId == studentClass.ClassId).ToList();
                var notifications = new List<Notification>();
                foreach (var subject in subjectsClass)
                {
                    notifications.Add(_dbContext.Notifications.Where(e=>e.SubjectId==subject.SubjectId && e.NotificationType=="subject").FirstOrDefault());
                }
                return notifications;
            }
            return null;
        }
    }
}
