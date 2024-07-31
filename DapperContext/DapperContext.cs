using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Transactions;
using Vsoft_share_document.DTO;

namespace Vsoft_share_document.DatabaseHelper
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("SqlConnection");
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
  
    }
}
