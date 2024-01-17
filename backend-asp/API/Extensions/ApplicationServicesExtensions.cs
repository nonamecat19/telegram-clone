using System.Security.Claims;
using System.Text;
using API.Errors;
using API.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions;

public static class ApplicationServicesExtensions
{
   public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
   {
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.AddDbContext<StoreContext>(opt =>
      {
         opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
      });
      services.AddScoped<IUsersRepository, UsersRepository>();
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.Configure<ApiBehaviorOptions>(options =>
      {
         options.InvalidModelStateResponseFactory = actionContext =>
         {
            var errors = actionContext.ModelState
               .Where(e => e.Value is { Errors.Count: > 0 })
               .SelectMany(x => x.Value?.Errors!)
               .Select(x => x.ErrorMessage).ToArray();

            var errorResponse = new ApiValidationErrorResponse
            {
               Errors = errors
            };

            return new BadRequestObjectResult(errorResponse);
         };
      });
      services.AddScoped<JwtService>();
      services.AddCors(opt =>
      {
         opt.AddPolicy("CorsPolicy", policy =>
         {
            policy.AllowAnyHeader().AllowAnyHeader().AllowAnyOrigin();
         });
      });
      
      services.AddAuthentication(x =>
      {
         x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
         x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      }).AddJwtBearer(x =>
      {
         x.TokenValidationParameters = new TokenValidationParameters
         {
            ValidIssuer = config["JwtSettings:Issuer"],
            ValidAudience = config["JwtSettings:Issuer"],
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"] ?? string.Empty)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
         };
         x.Events = new JwtBearerEvents
         {
            OnTokenValidated = context =>
            {
               var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
               return Task.CompletedTask;
            }
         };
      });
      
      services.AddAuthorization();
      
      return services;
   }
}