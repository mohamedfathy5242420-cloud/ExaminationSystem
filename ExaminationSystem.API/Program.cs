using System.Text;
using ExaminationSystem.API.Authorization;
using ExaminationSystem.Application;
using ExaminationSystem.Infrastructure;
using ExaminationSystem.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ExaminationSystem.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var jwtSection = builder.Configuration.GetSection("Jwt");
            var jwtSecret = jwtSection["Secret"]!;

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSection["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = jwtSection["Audience"],
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    UserTypePolicies.AdminOnly,
                    policy => policy.RequireClaim("user_type", "Admin"));

                options.AddPolicy(
                    UserTypePolicies.StudentOnly,
                    policy => policy.RequireClaim("user_type", "Student"));

                options.AddPolicy(
                    UserTypePolicies.InstructorOnly,
                    policy => policy.RequireClaim("user_type", "Instructor"));
            });
            builder.Services.AddApplication();
            builder.Services.AddPersistence(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
