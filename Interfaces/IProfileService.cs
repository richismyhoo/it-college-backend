using ItCollege.Models.User;

namespace ItCollege.Interfaces;

public interface IProfileService
{
    Task<ProfileDto> GetProfile(string username);
    Task<Profile> CreateProfile(ProfileRequest profile, string username);
    Task UpdateProfile(Profile profile);
}