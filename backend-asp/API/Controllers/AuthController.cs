using API.Errors;
using API.Services;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuthController(JwtService jwtService, IGenericRepository<User> usersRepo) : BaseApiController
{
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] UsersSpecParams usersSpecParams)
    {
        var users = await usersRepo.ListAllAsync();
        foreach (var user in users)
        {
            if (user.Name != usersSpecParams.Name || user.Password != usersSpecParams.Password)
            {
                continue;
            }
            var token = jwtService.GenerateToken(user.Id.ToString());
            return Ok(token);
        }
        return new ObjectResult(new ApiResponse(401));
    }


    [HttpPost("test")]
    [Authorize]
    public ActionResult<string> Test()
    {
        var userId = JwtService.GetUserId(User);
        return Ok($"User ID: {userId}");
    }
}