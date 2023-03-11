using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ZeroGravity.Application.Extensions;

public static class JwtExtensions
{
     /// <summary>
     /// Configures the JWT token header and payload
     /// </summary>
     public static void AddJwtAuthentication(this WebApplicationBuilder builder)
     {
         var services = builder.Services;
         var configuration = builder.Configuration;
         
         services.AddAuthentication(o =>
         {
             o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
             o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
             o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
         }).AddJwtBearer(o =>
         {
             o.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = configuration["Jwt:Issuer"],
                 ValidAudience = configuration["Jwt:Audience"],
                 ClockSkew = TimeSpan.FromDays(28),
                 IssuerSigningKey = new SymmetricSecurityKey
                     (Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
             };
             
         });
    
         services.AddAuthorization();
     }
    
     public static void UseJwtAuthentication(this WebApplication app)
     {
         app.UseAuthentication();
         app.UseAuthorization();
     }
}