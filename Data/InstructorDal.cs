using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class InstructorDal : IInstructor
{
    private List<Instructor> _instructors = new List<Instructor>();

    public InstructorDal()
    {
        _instructors = new List<Instructor>
        {
            new Instructor
            {
                InstructorIdku = 1,
                InstructorNameku = "John Doe",
                InstructorEmailku = "john-doe@gmail.com",
                InstructorPhoneku = "123-456-7890",
                InstructorAddressku = "123 Main St",
                InstructorCityku = "New York"
            },
            new Instructor
            {
                InstructorIdku = 2,
                InstructorNameku = "Jane Doe",
                InstructorEmailku = "jane-doe@gmail.com",
                InstructorPhoneku = "123-456-7890",
                InstructorAddressku = "123 Main St",
                InstructorCityku = "New York"
            },
            new Instructor
            {
                InstructorIdku = 3,
                InstructorNameku = "John Smith",
                InstructorEmailku = "john-smith@gmail.com",
                InstructorPhoneku = "123-456-7890",
                InstructorAddressku = "123 Main St",
                InstructorCityku = "New York"
            }
        };
    }

    public void DeleteInstructor(int instructorId)
    {
        var instructor = GetInstructorById(instructorId);
        if (instructor != null)
        {
            _instructors.Remove(instructor);
        }
    }

    public Instructor GetInstructorById(int instructorId)
    {
        var instructor = _instructors.FirstOrDefault(x => x.InstructorIdku == instructorId);
        if (instructor == null)
        {
            throw new Exception("Instructor not found");
        }
        return instructor;
    }

    public List<Instructor> GetInstructors()
    {
        return _instructors;
    }

    public Instructor InsertInstructor(Instructor instructor)
    {
        _instructors.Add(instructor);
        return instructor;
    }

    public Instructor UpdateInstructor(Instructor instructor)
    {
        var existingInstructor = GetInstructorById(instructor.InstructorIdku);
        if (existingInstructor != null)
        {
            existingInstructor.InstructorNameku = instructor.InstructorNameku;
            existingInstructor.InstructorEmailku = instructor.InstructorEmailku;
            existingInstructor.InstructorPhoneku = instructor.InstructorPhoneku;
            existingInstructor.InstructorAddressku = instructor.InstructorAddressku;
            existingInstructor.InstructorCityku = instructor.InstructorCityku;
        }
        return existingInstructor;
    }
}
