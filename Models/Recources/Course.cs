namespace ItCollege.Models.Recources;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TeacherId { get; set; }
    public string Category { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public virtual List<Module> Modules { get; set; }
    public virtual List<Enrollment> Enrollments { get; set; }
    public virtual List<Assignment> Assignments { get; set; }
}