namespace ItCollege.Models.User;

public class ProfileRequest
{
    public string Bio { get; set; }
    public List<string> Skills { get; set; } = new List<string>();
}