using Microsoft.EntityFrameworkCore;

namespace SystematicService.Models
{
    [PrimaryKey(nameof(UserId), nameof(ClassId))]
    public class StudentClass
    {
        public string UserId { get; set; }
        public string ClassId { get; set;}
    }
}
