using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pet.Extentions;
using Pet.Infrastructure;
using Pet.Middleware;
using Pet.Models;
using Pet.Options;
using Pet.Repositories;
using Pet.Services;

namespace Pet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            services.AddScoped<HallRepository>();
            services.AddScoped<MovieRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<SessionRepository>();
            services.AddScoped<UserService>();
            services.AddScoped<JwtProvider>();
            services.AddScoped<PasswordHasher>();

            builder.Configuration.AddUserSecrets<Program>();

            services.Configure<JwtOptions>(builder
                .Configuration.GetSection(nameof(JwtOptions)));

            services.Configure<AuthOptions>(builder
                .Configuration.GetSection(nameof(AuthOptions)));

            builder.Services.AddProblemDetails();

            services.AddApiAuthentication();

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<AppDbContext>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); 
            }
            else
            {
                app.UseExceptionHandler("/error"); 
            }


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
