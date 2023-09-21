using System.ComponentModel.DataAnnotations;

namespace ExamService.Models
{
    public class TLQuestion
    {
        [Key]
        public string QuestionId { get; set; }
        public string QuestionDetail { get; set; }
        public string ExamId { get; set; }
    }
}
