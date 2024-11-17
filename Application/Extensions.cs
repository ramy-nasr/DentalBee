using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));

        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 100 * 1024 * 1024;
        });

        return services;
    }
}
