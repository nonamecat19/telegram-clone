using System.Text.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class StoreContextSeed
{

   public static async Task SeedAsync(StoreContext context)
   {
      await SeedHelper<User>(context, "users");
      await SeedHelper<ChatRoom>(context, "chatRooms");
      await SeedHelper<ChatMember>(context, "chatMembers");
      
      if (context.ChangeTracker.HasChanges())
      {
         await context.SaveChangesAsync();
      }
   }
   
   private static async Task SeedHelper<T>(DbContext context, string fileName) where T : class
   {
      if (!context.Set<T>().Any())
      {
         var jsonData = await File.ReadAllTextAsync($"../Infrastructure/Data/SeedData/{fileName}.json");
         var entities = JsonSerializer.Deserialize<List<T>>(jsonData);
         context.Set<T>().AddRange(entities);
      }
   }
}