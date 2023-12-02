using AzureSQL_ServiceApp.Model;
using Microsoft.Data.SqlClient;

namespace AzureSQL_ServiceApp.DAL
{
    public class SQLConnectionClass
    {
        private static string db_source = "dinelab.database.windows.net";
        private static string db_usedId = "sqladmin";
        private static string db_password = "Azure@123";
        private static string db_database = "appDB";

        private SqlConnection GetConnectionString()
        {
            var _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = db_source;
            _builder.UserID = db_usedId;
            _builder.Password = db_password;
            _builder.InitialCatalog = db_database;
            return new SqlConnection(_builder.ConnectionString);
        }

        public List<Courses> GetCourses()
        {
            List<Courses> listCourses = new List<Courses>();
            SqlConnection conn = GetConnectionString();
            string query = "select * from Courses";

            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Courses course = new Courses();
                    course.CourseId = reader.GetInt32(0);
                    course.CourseName = reader.GetString(1);
                    course.CourseFees = reader.GetInt32(2);
                    listCourses.Add(course);
                }
            }
            conn.Close();
            return listCourses;
        }

    }
}
