using System;

namespace WebApplication1.DTO;

public class CourseAddDTO
{
    public string CourseName { get; set; } = null!;

    public string CourseDescription { get; set; } = null!;

    public double Duration { get; set; }

    public int CategoryId { get; set; }
    
    public int InstructorId { get; set; }
}
