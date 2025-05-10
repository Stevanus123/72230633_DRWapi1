using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public interface ICourse
{
    List<Course> GetCourses();

    Course GetCourseById(int courseId);

    Course AddCourse(Course course);

    Course UpdateCourse(Course course);

    void DeleteCourse(int courseId);

    Course GetCoursesByIdCourse(int categoryId);

    IEnumerable<Course> GetAllCourses();
}