using System.ComponentModel.DataAnnotations;

namespace ExamService.Models
{
    public class TNQuestion
    {
        [Key]
        public string QuestionId { get; set;}
        public int QuestionType { get; set;}
        public string QuestionDetail {  get; set;}
        public string AnswerA { get; set;}
        public string AnswerB { get; set;}
        public string AnswerC { get; set;}
        public string AnswerD { get; set;}
        public string CorrectAnswer { get; set;}
        public bool QuestionUsed { get; set;} = false;
    }
}
