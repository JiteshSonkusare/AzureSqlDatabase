# Create SQL Database in Azure Elastic Database Pool Programmatically Using Azure SDK for C#.NET

This solution uses the [SQL Database Library for .NET](https://msdn.microsoft.com/library/azure/mt349017.aspx) so you need to install the library. You can install by running the following command in the [package manager console](http://docs.nuget.org/Consume/Package-Manager-Console) in Visual Studio (**Tools** > **NuGet Package Manager** > **Package Manager Console**):

    PM> Install-Package Microsoft.Azure.Management.Sql.Fluent -Version 1.31.0

## Create a new database in a pool

Create a DatabaseInner instance, and set the properties of the new database. Then call the CreateOrUpdateAsync method with the resource group name, server name, and new database name.

        // Create a database: configure create or update parameters and properties explicitly
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
				
## MediatR Framework 

This solution is implemented using MediatR framework. so you need to install below nuget packages. 

	PM> Install-Package MediatR -Version 8.0.0

	PM> Install-Package MediatR.Extensions.Microsoft.DependencyInjection -Version 8.0.0
	
## How To Use

In .Net core web api 
Register ServiceCollectionExtension.AddClientProvisioning in startup class as below:
	
	services.AddClientProvisioning(Configuration);
	
Pass all the azure secrets through appsettings.json file. This solution uses safe storage of [app secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows) in development environment.
You need to install Microsoft.Extensions.Options.ConfigurationExtensions nuget package.

	PM> Install-Package Microsoft.Extensions.Options.ConfigurationExtensions -Version 3.1.1

Call command handler from web api controller:

	var command = new AzureRequestCommand("SampleDbName");
	var result = await Mediator.Send(command);


