using sqlapp.Models;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace sqlapp.Services
{

    // This service will interact with our Product data in the SQL database
    public class CourseService
    {
        private static string data_source = "files/data.csv";        

        
        public List<Course> GetCourses()
        {
            List<Course> _course_lst = new List<Course>();

            StreamReader _reader = new StreamReader(File.OpenRead(data_source));
            
                while (!_reader.EndOfStream)
                {
                    string _line= _reader.ReadLine();
                    string[] _values = _line.Split(',');
                    Course _course = new Course()
                    {
                        CourseID = int.Parse(_values[0]),
                        ExamImage = _values[1],
                        CourseName = _values[2],
                        Rating = decimal.Parse(_values[3])
                    };

                    _course_lst.Add(_course);
                }                        
            return _course_lst;
        }

    }
}

