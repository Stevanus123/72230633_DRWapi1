using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public interface IInstructor
{
    List<Instructor> GetInstructors();

    Instructor GetInstructorById(int instructorId);

    Instructor InsertInstructor(Instructor instructor);

    Instructor UpdateInstructor(Instructor instructor);

    void DeleteInstructor(int instructorId);
}
