using ItCollege.Data;
using ItCollege.Interfaces;
using ItCollege.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ItCollege.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> BecomeTeacher(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        
        if (user is null)
            throw new KeyNotFoundException("Пользователь не найден");
        
        if (user.Role == Roles.Teacher)
            return false;

        user.Role = Roles.Teacher;
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> BecomeStudent(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        
        if (user is null)
            throw new KeyNotFoundException("Пользователь не найден");

        if (user.Role == Roles.Student)
            return false;
        
        user.Role = Roles.Student;
        await _context.SaveChangesAsync();

        return true;
    }
}