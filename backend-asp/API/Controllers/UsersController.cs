using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IUsersRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await repo.GetUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUsers(int id)
    {
        var user = await repo.GetUserByIdAsync(id);
        return user;
    }
}