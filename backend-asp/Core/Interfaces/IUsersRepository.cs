using Core.Entities;

namespace Core.Interfaces;

public interface IUsersRepository
{
    Task<User> GetUserByIdAsync(int id);
    Task<IReadOnlyList<User>> GetUsersAsync();
}