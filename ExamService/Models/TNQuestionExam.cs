using Microsoft.EntityFrameworkCore;

namespace ExamService.Models
{
    [Keyless]
    public class TNQuestionExam
    {
        public string ExamId { get; set; }
        public string QuestionId { get; set; }
    }
}
