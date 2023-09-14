namespace SubjectService.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; }
        public int TeacherId { get; set; }
        public int SubjectTypeId { get; set; }
        public DateTime Date { get; set; }
    }
}
