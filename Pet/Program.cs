using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cinema.Extentions;
using Cinema.Infrastructure;
using Cinema.Middleware;
using Cinema.Models;
using Cinema.Options;
using Cinema.Repositories;
using FluentValidation;
using Cinema.Interfaces;
using Cinema.Services;

namespace Cinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            services.AddRepositories();
            services.AddServices();
            services.AddAuth();
            services.AddValidatorsFromAssemblyContaining<Program>();
            services.AddProblemDetails();
            services.AddApiAuthentication();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<AppDbContext>();

            builder.Configuration.AddUserSecrets<Program>();

            services.Configure<JwtOptions>(builder
                .Configuration.GetSection(nameof(JwtOptions)));

            services.Configure<AuthOptions>(builder
                .Configuration.GetSection(nameof(AuthOptions)));


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
