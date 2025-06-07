using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Cinema.Options;
using System.Text;
using Cinema.Interfaces;
using Cinema.Repositories;
using Cinema.Services;
using Cinema.Infrastucture.Auth;
using Cinema.Infrastucture.Repositories;

namespace Cinema.Extentions
{
    public static class ApiExtension
    {
        public static IServiceCollection AddApiAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();
                    var authOptions = configuration.GetSection(nameof(AuthOptions)).Get<AuthOptions>();

                    if (string.IsNullOrEmpty(jwtOptions?.SecretKey))
                        throw new ArgumentNullException(nameof(jwtOptions.SecretKey));

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request
                                .Cookies[authOptions?.CookieName ?? "AuthToken"];
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHallRepository, HallRepository>();
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            return services;
        }

        public static IServiceCollection AddConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<AuthOptions>(configuration.GetSection(nameof(AuthOptions)));
            services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));
            services.Configure<RolePermissionOptions>(
                configuration.GetSection(nameof(RolePermissionOptions)));

            return services;
        }
    }
}