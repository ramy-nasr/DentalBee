using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Configuration;
using System.Text;


namespace Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StorageSettings>(configuration.GetSection("StorageSettings"));
        services.AddSingleton<IStorageSettings>(sp =>
            sp.GetRequiredService<IOptions<StorageSettings>>().Value);


        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            var jwtSettings = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "ValidIssuer",
                ValidAudience = "ValidAudience",
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourVeryLongAndStrongSecretKeyHere123"))
            };
        });

        services.AddAuthorization();


        services.Configure<DbSettings>(configuration.GetSection("DatabaseSettings"));
        services.AddSingleton<IDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<DbSettings>>().Value);

        services.AddDbContext<IAppDbContext, AppDbContext>((serviceProvider, options) =>
        {
            var dbSettings = serviceProvider.GetRequiredService<IDatabaseSettings>();
            options.UseNpgsql(dbSettings.ConnectionString);
        });

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<INoteRepository, NoteRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        return services;
    }
}
