namespace ItCollege.Interfaces;

public interface IUserService
{
    Task<bool> BecomeTeacher(string username);
    Task<bool> BecomeStudent(string username);
}