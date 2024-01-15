using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UsersRepository(StoreContext context) : IUsersRepository
{
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<IReadOnlyList<User>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }
}