namespace SubjectService.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionDetail { get; set; }
        public int LessonId { get; set; }
        public int SubjectId { get; set; }
    }
}
