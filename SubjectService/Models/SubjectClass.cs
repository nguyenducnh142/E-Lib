using Microsoft.EntityFrameworkCore;

namespace SubjectService.Models
{
    [Keyless]
    public class SubjectClass
    {
        public string ClassId { get; set; }
        public string SubjectId { get; set; }
    }
}
