using System.ComponentModel.DataAnnotations;

namespace ItCollege.Models.Auth;

public class RegistrationRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}