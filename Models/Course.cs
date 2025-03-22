using System;

namespace WebApplication1.Models;

public class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public string CourseDescription { get; set; } = null!;

    public double Duration { get; set; }

    public int CategoryId { get; set; }
}
