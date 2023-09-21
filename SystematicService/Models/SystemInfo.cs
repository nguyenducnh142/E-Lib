using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SystematicService.Models
{
    public class SystemInfo 
    {
        [Key]
        public string SystemId { get; set; }
        public string SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Website {  get; set; }
        public string SchoolType { get; set; }
        public string SchoolMaster { get; set; }
        public string SystemName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        
    }
}
