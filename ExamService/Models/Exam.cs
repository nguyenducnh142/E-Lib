using System.ComponentModel.DataAnnotations;

namespace ExamService.Models
{
    public class Exam
    {
        [Key]
        public string ExamId { get; set; }
        public string ExamName { get; set;}
        public string ExamFormal { get; set; }
        //TN / TL
        public string SubjectId { get; set; }
        public int TimeToLearn {  get; set; }
        public string TeacherId { get; set; }
        public bool Approve { get; set; } = false;

    }
}
