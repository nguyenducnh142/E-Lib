using Microsoft.EntityFrameworkCore;

namespace SubjectService.Models
{
    [Keyless]
    public class StarSubject
    {
        public string UserId { get; set; }
        public string SubjectId { get; set; }
    }
}
