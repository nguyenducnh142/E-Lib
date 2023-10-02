using Microsoft.EntityFrameworkCore;

namespace SubjectService.Models
{
    [PrimaryKey(nameof(UserId), nameof(SubjectId))]
    public class StarSubject
    {
        public string UserId { get; set; }
        public string SubjectId { get; set; }
    }
}
