namespace Core.Entities;

public class User : BaseEntity
{
    public string? Name { get; set; }    
    public string? Surname { get; set; }
    public string? Password { get; set; }
}