using ItCollege.Models.Recources;

namespace ItCollege.Interfaces;

public interface ICoursesService
{
    Task<List<Course>> GetCourses();
    Task<Course> GetCourse(int id);
    Task<Course> CreateCourse(CourseRequest request, string username);
    Task<Module> CreateModuleToCourse(ModuleRequest request, string username);
    Task<Assignment> CreateAssignmentToCourse(AssignmentRequest request, string username);
    Task<Enrollment> CreateEnrollmentToCourse(int id, string username);
    Task<List<Assignment>> GetAssignmentsForCourse(int courseId);
    Task<List<Module>> GetModulesForCourse(int courseId);
}