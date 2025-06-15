using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Cinema.Options;
using System.Text;
using Cinema.Interfaces;
using Cinema.Repositories;
using Cinema.Infrastucture.Auth;
using Cinema.Infrastucture.Repositories;
using Microsoft.AspNetCore.Authorization;

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

                    if (string.IsNullOrEmpty(jwtOptions?.AcсessSecretKey))
                        throw new ArgumentNullException(nameof(jwtOptions.AcсessSecretKey));

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtOptions.AcсessSecretKey)),
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request
                                .Cookies[authOptions?.AcсessTokenName ?? "AuthToken"];
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
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddHttpContextAccessor(); 
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
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