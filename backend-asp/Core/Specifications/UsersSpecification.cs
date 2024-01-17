using Core.Entities;

namespace Core.Specifications;

public class UsersSpecification : BaseSpecification<User>
{
   public UsersSpecification(UsersSpecParams usersSpecParams) 
      : base(x => 
      (usersSpecParams.Name.Length > 0 || x.Name == usersSpecParams.Name) && 
      (usersSpecParams.Password.Length > 0 || x.Password == usersSpecParams.Password)) 
   {
      Console.WriteLine(usersSpecParams);
      AddInclude(x => x.Name);
      AddInclude(x => x.Password);
      AddOrderBy(x => x.Id);
   }
}