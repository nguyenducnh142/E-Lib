using Microsoft.EntityFrameworkCore;

namespace NotificationService.Models
{
    [Keyless]
    public class SubjectClass
    {
        public string ClassId { get; set; }
        public string SubjectId { get; set; }
    }
}
