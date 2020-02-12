using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Azure.SQLDatabase.Azure;
using Azure.SQLDatabase.Extensions;
using Microsoft.Extensions.Options;

namespace Azure.SQLDatabase.Application
{
    public class AzureRequestCommand : IRequest<bool>
    {
        public string DbName { get; set; }

        public class AsyncHandler : IRequestHandler<AzureRequestCommand, bool>
        {
            private readonly IAzureSqlManager _azureSqlManager;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="azureSqlManager"></param>
            public AsyncHandler(IAzureSqlManager azureSqlManager)
            {
                _azureSqlManager = azureSqlManager;
            }

            /// <summary>
            /// Command handler method
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<bool> Handle(AzureRequestCommand request, CancellationToken cancellationToken)
            {
                return await _azureSqlManager.CreateDatabaseAsync(request.DbName);
            }
        }
    }
}
