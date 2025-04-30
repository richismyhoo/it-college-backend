using ItCollege.Data;
using ItCollege.Interfaces;
using ItCollege.Models.Recources;
using ItCollege.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ItCollege.Services;

public class CoursesService : ICoursesService
{
    private readonly ApplicationDbContext _context;

    public CoursesService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Course>> GetCourses()
    {
        var courses = await _context.Courses
            .AsNoTracking()
            .ToListAsync();
        return courses;
    }

    public async Task<Course> GetCourse(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Assignments)
            .Include(c => c.Enrollments)
            .Include(c => c.Modules)
            .FirstOrDefaultAsync(c => c.Id == id);
        return course ?? new Course();
    }

    public async Task<Course> CreateCourse(CourseRequest request, string username)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
        
        if (user.Role == Roles.Student)
            throw new UnauthorizedAccessException();

        var course = new Course
        {
            Category = request.Category,
            CreatedAt = DateTime.UtcNow,
            Description = request.Description,
            TeacherId = user.Id,
            Title = request.Title,
        };
        
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync();
        
        return course;
    }

    public async Task<Module> CreateModuleToCourse(ModuleRequest request, string username)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
        
        if (user.Role == Roles.Student)
            throw new UnauthorizedAccessException();

        var module = new Module
        {
            CourseId = request.CourseId,
            CreatedAt = DateTime.UtcNow,
            Description = request.Description,
            Title = request.Title,
            DueDate = request.DueDate,
        };
        
        await _context.Modules.AddAsync(module);
        await _context.SaveChangesAsync();

        return module;
    }

    public async Task<Assignment> CreateAssignmentToCourse(AssignmentRequest request, string username)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
        
        if (user.Role == Roles.Student)
            throw new UnauthorizedAccessException();

        var assignment = new Assignment
        {
            CourseId = request.CourseId,
            CreatedAt = DateTime.UtcNow,
            Description = request.Description,
            Title = request.Title,
            DueDate = request.DueDate
        };
        
        await _context.Assignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
        
        return assignment;
    }

    public async Task<Enrollment> CreateEnrollmentToCourse(int courseId, string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        
        var enrollment = new Enrollment
        {
            CourseId = courseId,
            EnrollmentDate = DateTime.UtcNow,
            Username = username,
            UserId = user.Id
        };
        
        await _context.Enrollments.AddAsync(enrollment);
        await _context.SaveChangesAsync();
        
        return enrollment;
    }

    public async Task<List<Assignment>> GetAssignmentsForCourse(int courseId)
    {
        var assignments = await _context.Assignments
            .AsNoTracking()
            .Where(a => a.CourseId == courseId)
            .ToListAsync();

        return assignments;
    }

    public async Task<List<Module>> GetModulesForCourse(int courseId)
    {
        var modules = await _context.Modules
            .AsNoTracking()
            .Where(m => m.CourseId == courseId)
            .ToListAsync();

        return modules;
    }
}