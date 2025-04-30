using System.Security.Claims;
using ItCollege.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItCollege.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [Authorize]
    [HttpPost("become/teacher")]
    public async Task<IActionResult> BecomeTeacher()
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        try
        {
            var result = await _userService.BecomeTeacher(username);
            if (!result)
                return BadRequest("Вы уже учитель");

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [Authorize]
    [HttpPost("become/student")]
    public async Task<IActionResult> BecomeStudent()
    {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        foreach (var claim in User.Claims)
        {
            Console.WriteLine($"{claim.Type}: {claim.Value}");
        }
        
        try
        {
            var result = await _userService.BecomeStudent(username);
            if (!result)
                return BadRequest("Вы уже студент");

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}