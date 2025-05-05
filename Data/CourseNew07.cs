using System;
using MySql.Data.MySqlClient;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class CourseNew07 : ICourse
{
    private readonly IConfiguration _configuration;

    public string conn = String.Empty;

    public CourseNew07(IConfiguration configuration)
    {
        _configuration = configuration;
        conn = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }

    public Course AddCourse(Course course)
    {
        throw new NotImplementedException();
    }

    public void DeleteCourse(int courseId)
    {
        using (MySqlConnection conn = new MySqlConnection(this.conn))
        {
            string strConn = @"DELETE FROM course WHERE courseId = @courseId";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@courseId", courseId);
                int res = cmd.ExecuteNonQuery();
                if (res == 0)
                {
                    throw new Exception("Yahh ga ditemuin???");
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    public Course GetCourseById(int courseId)
    {
        using (MySqlConnection conn = new MySqlConnection(this.conn))
        {
            string strConn = @"SELECT * FROM course WHERE courseId = @courseId";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            Course course = new Course();
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@courseId", courseId);
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    course = new Course()
                    {
                        CourseId = reader.GetInt32("courseId"),
                        CourseName = reader.GetString("courseName"),
                        CourseDescription = reader.GetString("courseDescription"),
                        Duration = reader.GetDouble("duration"),
                        CategoryId = reader.GetInt32("categoryId")
                    };
                }
                return course;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    public List<Course> GetCourses()
    {
        using (MySqlConnection conn = new MySqlConnection(this.conn))
        {
            string strConn = @"SELECT * FROM course ORDER BY courseId DESC";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            List<Course> courses = new List<Course>();
            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Course course = new Course()
                    {
                        CourseId = reader.GetInt32("courseId"),
                        CourseName = reader.GetString("courseName"),
                        CourseDescription = reader.GetString("courseDescription"),
                        Duration = reader.GetDouble("duration"),
                        CategoryId = reader.GetInt32("categoryId")
                    };
                    courses.Add(course);
                }
                return courses;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }

    public Course UpdateCourse(Course course)
    {
        using (MySqlConnection conn = new MySqlConnection(this.conn))
        {
            string strConn = @"UPDATE course SET courseName = @courseName, courseDescription = @courseDescription, duration = @duration, categoryId = @categoryId WHERE courseId = @courseId";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@courseId", course.CourseId);
                cmd.Parameters.AddWithValue("@courseName", course.CourseName);
                cmd.Parameters.AddWithValue("@courseDescription", course.CourseDescription);
                cmd.Parameters.AddWithValue("@duration", course.Duration);
                cmd.Parameters.AddWithValue("@categoryId", course.CategoryId);
                int res = cmd.ExecuteNonQuery();
                if (res == 0)
                {
                    throw new Exception("Kamu mau update data yang mana? orang ga ada??");
                }
                return course;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                conn.Close();
            }
        }
    }
}
