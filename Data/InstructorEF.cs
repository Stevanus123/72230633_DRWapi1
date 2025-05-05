using System;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class InstructorEF : IInstructor
{
    private readonly ApplicationDbContext _context;
    public InstructorEF(ApplicationDbContext context)
    {
        _context = context;
    }

    public void DeleteInstructor(int instructorId)
    {
        var instructor = GetInstructorById(instructorId);
        if (instructor == null)
        {
            throw new Exception($"Instructor dengan ID {instructorId} gak ditemuin!.");
        }
        try
        {
            _context.Instructors.Remove(instructor);
            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal menghapus instructor: {ex.Message}");
        }
    }

    public Instructor GetInstructorById(int instructorId)
    {
        var instructor = _context.Instructors.FirstOrDefault(i => i.InstructorId == instructorId);
        if (instructor == null)
        {
            throw new Exception($"Instructor dengan ID {instructorId} gak ditemuin!.");
        }
        return instructor;
    }

    public List<Instructor> GetInstructors()
    {
        var instructors = _context.Instructors.OrderByDescending(i => i.InstructorId).ToList();
        return instructors;

    }

    public Instructor InsertInstructor(Instructor instructor)
    {
        try
        {
            _context.Instructors.Add(instructor);
            _context.SaveChanges();
            return instructor;
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal menambah instructor: {ex.Message}");
        }
    }

    public Instructor UpdateInstructor(Instructor instructor)
    {
        var existingInstructor = GetInstructorById(instructor.InstructorId);
        if (existingInstructor == null)
        {
            throw new Exception($"Instructor dengan ID {instructor.InstructorId} gak ditemuin!.");
        }
        try
        {
            _context.Entry(existingInstructor).CurrentValues.SetValues(instructor);
            _context.SaveChanges();
            return instructor;
        }
        catch (Exception ex)
        {
            throw new Exception($"Gagal mengupdate instructor: {ex.Message}");
        }
    }
}
