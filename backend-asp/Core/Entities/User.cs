namespace Core.Entities;

public class User : BaseEntity
{
    public string? Name { get; init; }    
    public string? Surname { get; init; }
    public string? Password { get; init; }
}