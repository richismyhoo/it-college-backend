using ItCollege.Data;
using ItCollege.Interfaces;
using ItCollege.Models.User;
using Microsoft.EntityFrameworkCore;

namespace ItCollege.Services;

public class ProfileService : IProfileService
{
    private readonly ApplicationDbContext _context;

    public ProfileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Profile> GetProfile(string username)
    {
        var user = await _context.Profiles.FirstOrDefaultAsync(p => p.Username == username);
        
        return user ?? new Profile();
    }

    public async Task<Profile> CreateProfile(ProfileRequest request, string username)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username);
        
        var newProfile = new Profile
        {
            Username = username,
            Bio = request.Bio,
            Skills = request.Skills,
            UserId = user.Id,
        };
        
        await _context.Profiles.AddAsync(newProfile);
        await _context.SaveChangesAsync();

        return newProfile;
    }

    public async Task UpdateProfile(Profile profile)
    {
        _context.Profiles.Update(profile);
        await _context.SaveChangesAsync();
    }
}