using MediaCompressor.Application.Services;
using MediaCompressor.Application.Services.Implementation;

namespace MediaCompressor.API.Extensions;

internal static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IImageService, ImageService>();
        return services;
    }
}
