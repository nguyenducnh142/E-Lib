namespace SubjectService.Models
{
    public class Subject
    {
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDescription { get; set; }
        public string TeacherName { get; set; }
        public DateTime Date { get; set; }
        public bool Star {  get; set; }
    }
}
