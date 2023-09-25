using System.ComponentModel.DataAnnotations;

namespace SubjectService.Models
{
    public class LessonFile
    {
        public string LessonFileId { get; set; }
        public string LessonFileName { get; set; }
        public string LessonFileDescription { get; set; }
        public string LessonId { get; set; }
        public bool Approve { get; set; }
        public string TeacherName {  get; set; }
        public string SubjectId { get; set; }
    }
}
