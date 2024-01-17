using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

    public async Task<EntityEntry<User>> AddUserAsync(User newUser)
    {
        return await context.Users.AddAsync(newUser);
    }

    public async Task<User> FindUserByCredentials(string name, string password)
    {
        return await context.Users
            .Where(u => u.Name == name && u.Password == password)
            .FirstOrDefaultAsync();
    }
}