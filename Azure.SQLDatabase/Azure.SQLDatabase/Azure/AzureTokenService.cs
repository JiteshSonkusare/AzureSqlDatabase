using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;

namespace Azure.SQLDatabase.Azure
{
    public class AzureTokenService
    {
        public static AzureCredentials GetAzureCredentials(string clientId, string appSecret, string tenantId, string subscriptionId)
        {
            var credentials = new AzureCredentialsFactory().FromServicePrincipal(clientId, appSecret, tenantId, AzureEnvironment.AzureGlobalCloud);

            return credentials.WithDefaultSubscription(subscriptionId);
        }
    }
}
