namespace ItCollege.Interfaces;

public interface IAuthService
{
    Task<string> Register(string username, string email, string password);
    Task<string> Login(string username, string password);
}