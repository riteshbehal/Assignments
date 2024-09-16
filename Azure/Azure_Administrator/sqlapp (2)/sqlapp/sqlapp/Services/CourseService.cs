using MySql.Data.MySqlClient;
using sqlapp.Models;
using System.Data.SqlClient;

namespace sqlapp.Services
{

    // This service will interact with our Product data in the SQL database
    public class CourseService
    {
        private static string db_connectionstring = "server=containerdatabase.mysql.database.azure.com;user=sqladmin;password=Mithilove@123!;database=appdb";

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(db_connectionstring);
        }
        public List<Course> GetCourses()
        {
            List<Course> _lst = new List<Course>();
            string _statement = "SELECT CourseID,CourseName,rating from Course;";
            MySqlConnection _connection = GetConnection();
            // Let's open the connection
            _connection.Open();

            MySqlCommand _sqlcommand = new MySqlCommand(_statement, _connection);

            using (MySqlDataReader _reader = _sqlcommand.ExecuteReader())
            {
                while (_reader.Read())
                {
                    Course _course = new Course()
                    {
                        CourseID = _reader.GetInt32(0),
                        CourseName = _reader.GetString(1),
                        Rating = _reader.GetDecimal(2)
                    };

                    _lst.Add(_course);
                }
            }
            _connection.Close();
            return _lst;
        }


    }
}

