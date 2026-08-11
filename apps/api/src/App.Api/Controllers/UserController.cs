using App.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Application.Interfaces;

namespace App.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet]
    public async Task<List<UserResponse>> GetUsers()
    {
        return await userService.GetAllUsersAsync();
    }
    [HttpGet("{id:Guid}")]
    public async Task<UserResponse?> GetUserById(Guid id)
    {
        return await userService.GetUserByIdAsync(id);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<CreateUserResponse> CreateUser(CreateUserRequest value)
    {
        return await userService.CreateUserAsync(value);
    }
    [HttpDelete("{id:Guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await userService.DeleteUserAsync(id);

        return NoContent();
    }
}
