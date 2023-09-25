using Microsoft.EntityFrameworkCore;
using NotificationService.Models;
using System.Collections.Generic;

namespace NotificationService.DBContexts
{
    public class NotificationContext : DbContext
    {
        public NotificationContext(DbContextOptions<NotificationContext> options) : base(options)
        {

        }

        [DbFunction(name: "SOUNDEX", IsBuiltIn = true)]
        public string FuzzySearch(string subjectName)
        {
            throw new NotImplementedException();
        }
        public DbSet<Notification> Notifications{ get; set; }
        public DbSet<PersonalNotification> PersonalNotifications { get; set; }

    }
}
