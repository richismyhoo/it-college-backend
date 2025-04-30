using System.Text.Json.Serialization;

namespace ItCollege.Models.Recources;

public class Enrollment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string Username { get; set; }
    
    [JsonIgnore]
    public virtual Course Course { get; set; }
}