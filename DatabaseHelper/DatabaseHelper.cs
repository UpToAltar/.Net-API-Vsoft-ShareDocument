using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using Vsoft_share_document.DTO;

namespace Vsoft_share_document.DatabaseHelper
{
    public class DatabaseHelperNew
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DatabaseHelperNew(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<int> ExcuteNonQueryAsync(string sprocedureName, DynamicParameters parameters)
        {
            using (var connection = this.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int affectedRows = await connection.ExecuteAsync(
                            sprocedureName,
                            parameters,
                            transaction,
                            commandType: CommandType.StoredProcedure
                        );

                        transaction.Commit();
                        return affectedRows;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }

        public async Task<T> ExcuteReturnDataAsync<T>(string sprocedureName, DynamicParameters parameters)
        {
            try
            {
                using (var connection = this.CreateConnection())
                {
                    var obj = await connection.QueryFirstOrDefaultAsync<T>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
                    return obj;
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
                return default(T);
            }
        }

        

        public async Task<List<T>> ExcuteReturnListOfDataAscync<T>(string sprocedureName, DynamicParameters parameters)
        {

            try
            {
                using (var connection = this.CreateConnection())
                {

                    var users = await connection.QueryAsync<T>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
                    return users.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
