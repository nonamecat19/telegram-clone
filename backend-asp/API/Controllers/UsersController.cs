using API.Dto;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IGenericRepository<User> usersRepo) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<UsersToReturnDto>>> GetUsers()
    {
        var users = await usersRepo.ListAllAsync();
        return users.Select(user => new UsersToReturnDto
        {
            Id = user.Id,
            Name = user.Name,
            Surname = user.Surname
        }).ToList();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsersToReturnDto>> GetUsersById(int id)
    {
        var user = await usersRepo.GetByIdAsync(id);
        return new UsersToReturnDto
        {
             Id   = user.Id,
             Name =  user.Name,
             Surname = user.Surname
        };
    }
}