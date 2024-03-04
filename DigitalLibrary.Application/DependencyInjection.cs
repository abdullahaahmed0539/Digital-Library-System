
using DigitalLibrary.Application.Common.Interfaces.Services;
using DigitalLibrary.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalLibrary.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IBookService, BookService>();
            return services;
        }
    }
}


