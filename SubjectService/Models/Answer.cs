using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SubjectService.Models
{
    public class Answer
    {
        public string AnswerId { get; set; }
        public string AnswerDetail { get; set; }
        public string QuestionId { get; set; }
    }
}
