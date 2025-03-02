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
                InstructorId = 1,
                InstructorName = "John Doe",
                InstructorEmail = "john-doe@gmail.com",
                InstructorPhone = "123-456-7890",
                InstructorAddress = "123 Main St",
                InstructorCity = "New York"
            },
            new Instructor
            {
                InstructorId = 2,
                InstructorName = "Jane Doe",
                InstructorEmail = "jane-doe@gmail.com",
                InstructorPhone = "123-456-7890",
                InstructorAddress = "123 Main St",
                InstructorCity = "New York"
            },
            new Instructor
            {
                InstructorId = 3,
                InstructorName = "John Smith",
                InstructorEmail = "john-smith@gmail.com",
                InstructorPhone = "123-456-7890",
                InstructorAddress = "123 Main St",
                InstructorCity = "New York"
            }
        };
    }

    public Instructor DeleteInstructor(int instructorId)
    {
        var instructor = GetInstructorById(instructorId);
        if (instructor != null)
        {
            _instructors.Remove(instructor);
        }
        return instructor;
    }

    public Instructor GetInstructorById(int instructorId)
    {
        var instructor = _instructors.FirstOrDefault(x => x.InstructorId == instructorId);
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
        var existingInstructor = GetInstructorById(instructor.InstructorId);
        if (existingInstructor != null)
        {
            existingInstructor.InstructorName = instructor.InstructorName;
            existingInstructor.InstructorEmail = instructor.InstructorEmail;
            existingInstructor.InstructorPhone = instructor.InstructorPhone;
            existingInstructor.InstructorAddress = instructor.InstructorAddress;
            existingInstructor.InstructorCity = instructor.InstructorCity;
        }
        return existingInstructor;
    }
}
