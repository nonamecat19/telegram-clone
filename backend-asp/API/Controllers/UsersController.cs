using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IGenericRepository<User> usersRepo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return Ok(await usersRepo.ListAllAsync());
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUsers(int id)
    {
        return await usersRepo.GetByIdAsync(id);
    }
}