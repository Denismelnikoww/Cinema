
using Microsoft.EntityFrameworkCore;
using Pet.Infrastructure;
using Pet.Models;
using Pet.Repositories;
using Pet.Services;

namespace Pet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddScoped<HallRepository>();
            builder.Services.AddScoped<MovieRepository>();
            builder.Services.AddScoped<UserRepository>();


            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<JwtProvider>();
            builder.Services.AddScoped<PasswordHasher>();

            builder.Services.Configure<JwtOptions>(builder
                .Configuration.GetSection(nameof(JwtOptions)));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration.AddUserSecrets<Program>();

            builder.Services.AddDbContext<AppDbContext>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
