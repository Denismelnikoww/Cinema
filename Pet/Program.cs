using Microsoft.AspNetCore.CookiePolicy;
using Cinema.Extentions;
using Cinema.Infrastucture.Infrastructure;
using Cinema.Application.UseCases.Booking;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastucture.Auth;
using Microsoft.AspNetCore.Authorization;

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

            services.Scan(scan => scan
                .FromAssemblyOf<CreateBookingUseCase>()
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("UseCase")))
                .AsSelf()
                .WithScopedLifetime());

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    builder.Configuration.GetConnectionString("Database"),
                    b => b.MigrationsAssembly("Cinema.Infrastucture")
                    )
                );

            services.AddHttpContextAccessor();

            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            
            services.AddAuthentication();
            services.AddAuthorization();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

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

            // “естова€ проверка (можно добавить временно)
            var provider = builder.Services.BuildServiceProvider();
            using var scope = provider.CreateScope();
            var authHandler = scope.ServiceProvider.GetService<IAuthorizationHandler>();
            var policyProvider = scope.ServiceProvider.GetService<IAuthorizationPolicyProvider>();

            if (authHandler == null || policyProvider == null)
            {
                throw new Exception("Authorization services not registered properly");
            }

            app.Run();
        }
    }
}
