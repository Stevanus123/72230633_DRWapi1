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
        conn = _configuration.GetConnectionString("DefaultConnection");
    }

    public IEnumerable<ViewCourseWithCategory> GetCourses()
    {
        using (MySqlConnection conn = new MySqlConnection(this.conn))
        {
            string strConn = @"SELECT courseId, courseName, courseDescription, duration, a.categoryId, categoryName 
                            FROM course a inner join category b on a.categoryId = b.categoryId";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            List<ViewCourseWithCategory> courses = new List<ViewCourseWithCategory>();
            try
            {
                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ViewCourseWithCategory course = new ViewCourseWithCategory();
                    course.CourseId = Convert.ToInt32(dr["CourseId"]);
                    course.CourseName = dr["CourseName"].ToString();
                    course.CourseDescription = dr["CourseDescription"].ToString();
                    course.Duration = Convert.ToInt32(dr["Duration"]);
                    course.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    course.CateroryName = dr["CategoryName"].ToString();
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

    public ViewCourseWithCategory GetCourseById(int courseId)
    {
        using (MySqlConnection conn = new MySqlConnection(this.conn))
        {
            string strConn = @"SELECT courseId, courseName, courseDescription, duration, categoryId, categoryName 
                            FROM ViewCourseWithCategory WHERE courseId = @courseId";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            cmd.Parameters.AddWithValue("@courseId", courseId);
            ViewCourseWithCategory course = new ViewCourseWithCategory();
            try
            {
                conn.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    course.CourseId = Convert.ToInt32(dr["CourseId"]);
                    course.CourseName = dr["CourseName"].ToString();
                    course.CourseDescription = dr["CourseDescription"].ToString();
                    course.Duration = Convert.ToInt32(dr["Duration"]);
                    course.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                    course.CateroryName = dr["CategoryName"].ToString();
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
    public Course AddCourse(Course course)
    {
        using (MySqlConnection conn = new MySqlConnection(this.conn))
        {
            string strConn = @"INSERT INTO course (courseName, courseDescription, duration, categoryId) VALUES
            (@courseName, @courseDescription, @duration, @categoryId); SELECT LAST_INSERT_ID();";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@courseName", course.CourseName);
                cmd.Parameters.AddWithValue("@courseDescription", course.CourseDescription);
                cmd.Parameters.AddWithValue("@duration", course.Duration);
                cmd.Parameters.AddWithValue("@categoryId", course.CategoryId);
                int courseId = Convert.ToInt32(cmd.ExecuteScalar());
                course.CourseId = courseId;
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

    public Course UpdateCourse(Course course)
    {
        using (MySqlConnection conn = new MySqlConnection(this.conn))
        {
            string strConn = @"UPDATE course SET courseName = @courseName, courseDescription = @courseDescription, duration = @duration, categoryId = @categoryId WHERE courseId = @courseId";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@courseName", course.CourseName);
                cmd.Parameters.AddWithValue("@courseDescription", course.CourseDescription);
                cmd.Parameters.AddWithValue("@duration", course.Duration);
                cmd.Parameters.AddWithValue("@categoryId", course.CategoryId);
                cmd.Parameters.AddWithValue("@courseId", course.CourseId);
                cmd.ExecuteNonQuery();
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
    public void DeleteCourse(int courseId)
    {
        using(MySqlConnection conn = new MySqlConnection(this.conn)){
            string strConn = @"DELETE FROM course WHERE courseId = @courseId";
            MySqlCommand cmd = new MySqlCommand(strConn, conn);
            try{
                conn.Open();
                cmd.Parameters.AddWithValue("@courseId", courseId);
                int res = cmd.ExecuteNonQuery();
                if(res == 0){
                    throw new Exception("Yahh ga ditemuin???");
                }
            }
            catch (MySqlException ex) {
                throw new Exception(ex.Message);
            }
            finally{
                cmd.Dispose();
                conn.Close();
            }
        }
    }
}
