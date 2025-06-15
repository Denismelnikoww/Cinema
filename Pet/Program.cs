using Microsoft.AspNetCore.CookiePolicy;
using Cinema.Extentions;
using Cinema.Infrastucture.Infrastructure;
using Cinema.Application.UseCases.Booking;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastucture.Auth;
using Microsoft.AspNetCore.Authorization;
using Serilog;

namespace Cinema.Services
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Host.UseSerilog();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                policy.WithOrigins(
                    builder.Configuration.GetSection("CorsOrigins").Get<string[]>())
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddRepositories();
            services.AddServices();
            services.AddAuth();
            services.AddConfiguration(builder.Configuration);
            services.AddApiAuthentication(builder.Configuration);

            services.Scan(scan => scan
                .FromAssemblyOf<CreateBookingUseCase>()
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("UseCase")))
                .AsSelf()
                .WithScopedLifetime());

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("Database"),
                    b => b.MigrationsAssembly("Cinema.Infrastucture")
                    )
                );

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration
                .GetConnectionString("Redis");
                options.InstanceName = "SampleInstance";
            });

            services.AddHttpContextAccessor();

            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication();
            services.AddAuthorization();

            services.AddProblemDetails();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            builder.Configuration.AddUserSecrets<Program>();

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

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
