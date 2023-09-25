using System.ComponentModel.DataAnnotations;

namespace FileService.Models
{
    public class PersonalFile
    {
        [Key] 
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string SubjectId { get; set; }
        public int Size { get; set; } 
    }
}
