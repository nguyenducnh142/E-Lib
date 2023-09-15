using System.ComponentModel.DataAnnotations;

namespace SubjectService.Models
{
    public class LessonFile
    {
        public int LessonFileId { get; set; }
        public string LessonFileName { get; set; }
        public string LessonFileDescription { get; set; }
        public int LessonId { get; set; }
    }
}
