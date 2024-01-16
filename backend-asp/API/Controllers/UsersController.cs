using API.Dto;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController(IGenericRepository<User> usersRepo, IMapperBase mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UsersToReturnDto>>> GetUsers()
    {
        var users = await usersRepo.ListAllAsync();
        return Ok(mapper.Map<IReadOnlyList<User>, IReadOnlyList<UsersToReturnDto>>(users));
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsersToReturnDto>> GetUsersById(int id)
    {
        var user = await usersRepo.GetByIdAsync(id);
        return Ok(mapper.Map<User, UsersToReturnDto>(user));
    }
}