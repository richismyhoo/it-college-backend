using System.Security.Claims;
using ItCollege.Interfaces;
using ItCollege.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace ItCollege.Controllers;

[ApiController]
[Route("api/profile")]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSelfProfile()
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            var profile = await _profileService.GetProfile(username);
            return Ok(profile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetProfile(string username)
    {
        try
        {
            var profile = await _profileService.GetProfile(username);
            return Ok(profile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProfile([FromBody] ProfileRequest request)
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        try
        {
            var newProfile = await _profileService.CreateProfile(request, username);
            return Ok(newProfile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile([FromBody] Profile profile)
    {
        try
        {
            await _profileService.UpdateProfile(profile);
            return Ok(profile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}