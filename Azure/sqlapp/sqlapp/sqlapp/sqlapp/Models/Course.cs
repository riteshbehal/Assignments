namespace sqlapp.Models
{
    public class Course
    {
        public int CourseID { get; set; }

        public string CourseName { get; set; }
        public decimal Rating { get; set; }

        public string ExamImage { get; set; }
    }
}
