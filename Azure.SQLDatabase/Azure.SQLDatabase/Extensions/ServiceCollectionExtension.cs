using MediatR;
using Azure.SQLDatabase.Azure;
using Azure.SQLDatabase.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Azure.SQLDatabase.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddClientProvisioning(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AzureOptions>(configuration.GetSection(nameof(AzureOptions)));

            services.AddMediatR(typeof(AzureRequestCommand));
            services.AddTransient<IAzureSqlManager, AzureSqlManager>();
        }
    }
}
