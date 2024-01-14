using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _repo;

    public UsersController(IUsersRepository repo)
    {
        _repo = repo;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        var users = await _repo.GetUsersAsync();
        return Ok(users);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUsers(int id)
    {
        var user = await _repo.GetUserByIdAsync(id);
        return user;
    }
}