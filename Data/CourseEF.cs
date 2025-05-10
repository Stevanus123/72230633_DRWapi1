using System;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class CourseEF : ICourse
{
    private readonly ApplicationDbContext _context;
    public CourseEF(ApplicationDbContext context)
    {
        _context = context;
    }

    public Course AddCourse(Course course)
    {
        try
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal menambah course: {ex.Message}");
        }
    }

    public void DeleteCourse(int courseId)
    {
        var course = GetCourseById(courseId);
        if (course == null)
        {
            throw new Exception($"Course dengan ID {courseId} gak ditemuin!.");
        }
        try
        {
            _context.Courses.Remove(course);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal menghapus course: {ex.Message}");
        }
    }

    public IEnumerable<Course> GetAllCourses()
    {
        var courses = _context.Courses
            .Include(c => c.Category)
            .Include(c => c.Instructor)
            .OrderByDescending(c => c.CourseId)
            .ToList();
        return courses;
    }

    public Course GetCourseById(int courseId)
    {
        var course = _context.Courses.FirstOrDefault(c => c.CourseId == courseId);
        if (course == null)
        {
            throw new Exception($"Course dengan ID {courseId} gak ditemuin!.");
        }
        return course;
    }

    public List<Course> GetCourses()
    {
        var courses = _context.Courses.OrderByDescending(c => c.CourseId).ToList();
        return courses;
    }

    public Course GetCoursesByIdCourse(int courseId)
    {
        var course = _context.Courses
            .Include(c => c.Category)
            .Include(c => c.Instructor)
            .FirstOrDefault(c => c.CourseId == courseId);
        if (course == null)
        {
            throw new Exception($"Course dengan ID {courseId} gak ditemuin!.");
        }
        return course;
    }

    public Course UpdateCourse(Course course)
    {
        var existingCourse = GetCourseById(course.CourseId);
        if (existingCourse == null)
        {
            throw new Exception($"Course dengan ID {course.CourseId} gak ditemuin!.");
        }
        try
        {
            existingCourse.CourseName = course.CourseName;
            existingCourse.CourseDescription = course.CourseDescription;
            existingCourse.Duration = course.Duration;
            existingCourse.CategoryId = course.CategoryId;
            existingCourse.InstructorId = course.InstructorId;

            _context.Courses.Update(existingCourse);
            _context.SaveChanges();
            return existingCourse;
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal mengubah course: {ex.Message}");
        }
    }
}
