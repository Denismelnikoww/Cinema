using Microsoft.AspNetCore.CookiePolicy;
using Cinema.Extentions;
using Cinema.Infrastructure;
using Cinema.Options;
using FluentValidation;

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

            services.AddValidatorsFromAssemblyContaining<Program>();
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

            var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
            var authOptions = builder.Configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();

            Console.WriteLine($"JWT Secret: {jwtOptions?.SecretKey}");
            Console.WriteLine($"Auth Cookie: {authOptions?.CookieName}");

            if (string.IsNullOrEmpty(jwtOptions?.SecretKey))
                throw new Exception("JWT Secret Key is not configured");
            if (string.IsNullOrEmpty(authOptions?.CookieName))
                throw new Exception("Auth Cookie Name is not configured");

            app.Run();
        }
    }
}
