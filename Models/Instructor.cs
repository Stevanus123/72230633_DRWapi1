using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Instructor
{
    [Key]
    public int InstructorId { get; set; }
    public string InstructorName { get; set; } = null!;
    public string InstructorEmail { get; set; } = null!;
    public string InstructorPhone { get; set; } = null!;
    public string InstructorAddress { get; set; } = null!;
    public string InstructorCity { get; set; } = null!;
}
