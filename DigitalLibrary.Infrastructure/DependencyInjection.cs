
using DigitalLibrary.Application.Common.Interfaces.Persistence;
using DigitalLibrary.Application.Common.Interfaces.Utilities;
using DigitalLibrary.Infrastructure;
using DigitalLibrary.Infrastructure.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalLibrary.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IFileManagement, FileManagement>();
            return services;
        }
    }
}