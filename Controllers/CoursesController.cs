using System.Security.Claims;
using ItCollege.Interfaces;
using ItCollege.Models.Recources;
using Microsoft.AspNetCore.Mvc;

namespace ItCollege.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService _coursesService;

    public CoursesController(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _coursesService.GetCourses();
        return Ok(courses);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CourseRequest request)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            var course = await _coursesService.CreateCourse(request, username);
            return Ok(course);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCourse(int id)
    {
        try
        {
            var course = await _coursesService.GetCourse(id);
            return Ok(course);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("{id}/modules")]
    public async Task<IActionResult> CreateCourseModule(ModuleRequest request)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            var module = await _coursesService.CreateModuleToCourse(request, username);
            return Ok(module);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("{id}/assignments")]
    public async Task<IActionResult> CreateCourseAssignment(AssignmentRequest request)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            var assignment = await _coursesService.CreateAssignmentToCourse(request, username);
            return Ok(assignment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost("{id}/enroll")]
    public async Task<IActionResult> EnrollStudentToCourse(int id)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            var enrollment = await _coursesService.CreateEnrollmentToCourse(id, username);
            return Ok(enrollment);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{id}/assignments")]
    public async Task<IActionResult> GetCourseAssignments(int id)
    {
        var assignments = await _coursesService.GetAssignmentsForCourse(id);
        return Ok(assignments);
    }

    [HttpGet("{id}/modules")]
    public async Task<IActionResult> GetCourseModules(int id)
    {
        var modules = await _coursesService.GetModulesForCourse(id);
        return Ok(modules);
    }
}