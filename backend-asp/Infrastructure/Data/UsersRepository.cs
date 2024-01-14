using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UsersRepository(StoreContext context) : IUsersRepository
{
    private readonly StoreContext _context = context;

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<IReadOnlyList<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
}