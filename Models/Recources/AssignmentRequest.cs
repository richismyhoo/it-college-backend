namespace ItCollege.Models.Recources;

public class AssignmentRequest
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
}