using System.Threading.Tasks;

namespace Azure.SQLDatabase.Azure
{
    public interface IAzureSqlManager
    {
        Task<bool> CreateDatabaseAsync(string dbName);
    }
}
