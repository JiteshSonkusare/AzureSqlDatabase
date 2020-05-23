using Azure.SQLDatabase.Azure;
using Azure.SQLDatabase.Application;

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
