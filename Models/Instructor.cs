using System;

namespace WebApplication1.Models;

public class Instructor
{
    public int InstructorIdku { get; set; }
    public string InstructorNameku { get; set; } = null!;
    public string InstructorEmailku { get; set; } = null!;
    public string InstructorPhoneku { get; set; } = null!;
    public string InstructorAddressku { get; set; } = null!;
    public string InstructorCityku { get; set; } = null!;
}
