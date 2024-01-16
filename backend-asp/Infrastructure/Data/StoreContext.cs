using System.Reflection;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<ChatRoom> ChatRooms { get; init; }
    public DbSet<ChatMessage> ChatMessages { get; init; }
    public DbSet<ChatMember> ChatMembers { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
               var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));

               foreach (var property in properties)
               {
                   modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion<double>();
               }
            }
        }
    }
}