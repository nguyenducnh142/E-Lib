using Microsoft.EntityFrameworkCore;

namespace ExamService.Models
{
    [PrimaryKey(nameof(ExamId), nameof(QuestionId))]
    public class TNQuestionExam
    {
        public string ExamId { get; set; }
        public string QuestionId { get; set; }
    }
}
