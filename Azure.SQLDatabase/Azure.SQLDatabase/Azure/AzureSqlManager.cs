using Azure.SQLDatabase.Extensions;

namespace Azure.SQLDatabase.Azure
{
    public class AzureSqlManager : IAzureSqlManager, IDisposable
    {
        private readonly ISqlManagementClient _client;

        private readonly IOptions<AzureOptions> _azureOptions;

        public AzureSqlManager(IOptions<AzureOptions> azureOptions)
        {
            _azureOptions = azureOptions;

            var azureCreds = AzureTokenService.GetAzureCredentials(_azureOptions.Value.ClientId,
                                                                   _azureOptions.Value.AppSecret,
                                                                   _azureOptions.Value.TenantId,
                                                                   _azureOptions.Value.SubscriptionId);

            var restClient = RestClient.Configure().WithEnvironment(azureCreds.Environment).WithCredentials(azureCreds).Build();

            _client = new SqlManagementClient(restClient)
            {
                SubscriptionId = azureCreds.DefaultSubscriptionId
            };
        }

        public async Task<bool> CreateDatabaseAsync(string databaseName)
        {
            var dbInner = new DatabaseInner
            {
                ElasticPoolName = _azureOptions.Value.ElasticPoolName,
                Location        = Region.EuropeNorth.ToString(),
                Collation       = CatalogCollationType.SQLLatin1GeneralCP1CIAS.ToString(),
                Edition         = DatabaseEdition.GeneralPurpose,
                RequestedServiceObjectiveName = ServiceObjectiveName.ElasticPool,
            };

            var resultDbInner = await _client.Databases.CreateOrUpdateAsync(_azureOptions.Value.ResourceGroupName,
                                                                            _azureOptions.Value.ServerName,
                                                                             databaseName,
                                                                             dbInner);
            return !string.IsNullOrEmpty(resultDbInner.Name);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
