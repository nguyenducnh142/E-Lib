using System.ComponentModel.DataAnnotations;

namespace ExamService.Models
{
    public class Exam
    {
        [Key]
        public string ExamId { get; set; }
        public string ExamName { get; set;}
        public bool ExamFormal { get; set; }
        public string SubjectId { get; set; }
        public int Time {  get; set; }
        public string TeacherId { get; set; }
        public bool Approve { get; set; }

    }
}
