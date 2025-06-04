using Microsoft.AspNetCore.CookiePolicy;
using Cinema.Extentions;
using Cinema.Options;
using FluentValidation;
using Cinema.Interfaces;
using Cinema.Repositories;
using Cinema.Infrastucture.Infrastructure;
using Cinema.Validators;
using Cinema.Application.UseCases.Booking;

namespace Cinema.Services
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
            services.AddApiAuthentication(builder.Configuration);
            services.AddDbContext<AppDbContext>();

            services.AddScoped<CreateBookingUseCase>();
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            services.AddProblemDetails();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            builder.Configuration.AddUserSecrets<Program>();

            services.AddConfiguration(builder.Configuration);

            var app = builder.Build();


            app.UseMyExceptionMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI();

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
