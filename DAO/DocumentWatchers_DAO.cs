

using Dapper;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Globalization;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using Vsoft_share_document.DatabaseHelper;
using Vsoft_share_document.DTO;
using Vsoft_share_document.InterfaceDAO;

namespace Vsoft_share_document.DAO
{
    public class DocumentWatchers_DAO : IDocumentWatchers_DAO
    {
        private readonly DapperContext _db;
        public DocumentWatchers_DAO(DapperContext db) {
            _db = db;
        }
        
        public async Task<int> CreateDocumentWatchers(ENT_CreateDocumentWatchers body)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    string sprocedureName = "Proc_DocumentWatchers_Insert";
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", Guid.NewGuid(), DbType.Guid);
                    parameters.Add("DocumentId", body.DocumentId, DbType.Guid);
                    parameters.Add("UserId", body.UserId, DbType.Guid);
                    parameters.Add("Email", body.Email, DbType.String);
                    parameters.Add("ExpiredIn", DateTime.ParseExact(body.ExpiredIn, "dd-MM-yyyy", CultureInfo.InvariantCulture), DbType.DateTime);
                    parameters.Add("CreatedBy", Guid.Empty, DbType.Guid);
                    parameters.Add("CheckSum", "", DbType.String);
                    parameters.Add("IsActive", body.IsActive, DbType.Boolean);
                    int affectedRows = await connection.ExecuteAsync(sprocedureName, parameters, transaction, commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return affectedRows;
                }
            }
        }

        public async Task<ENT_User> GetUserById(Guid? Id)
        {
            using (var connection = _db.CreateConnection())
            {
                string sprocedureName = "[dbo].[Proc_User_FindById]";
                var parameters = new DynamicParameters();
                parameters.Add("Id", Id, DbType.Guid);
                parameters.Add("UserId", new Guid(), DbType.Guid);
                return await connection.QueryFirstOrDefaultAsync<ENT_User>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }


        public async Task<ENT_Document> GetDocumentById(Guid Id)
        {
            using (var connection = _db.CreateConnection())
            {
                string sprocedureName = "[dbo].[Proc_Document_FindById]";
                var parameters = new DynamicParameters();
                parameters.Add("Id", Id, DbType.Guid);
                return await connection.QueryFirstOrDefaultAsync<ENT_Document>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> DeleteDocumentById(Guid id)
        {
            using (var connection = _db.CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    string sprocedureName = "[dbo].[Proc_DocumentWatchers_DeleteById]";
                    var parameters = new DynamicParameters();
                    parameters.Add("Id", id, DbType.Guid);
                    parameters.Add("EditedBy", Guid.Empty, DbType.Guid);
                    int affectedRows = await connection.ExecuteAsync(sprocedureName,parameters,transaction,commandType: CommandType.StoredProcedure);
                    transaction.Commit();
                    return affectedRows;
                }
            }
        }

        public async Task<ENT_DocumentWatchers> GetDocumentWatchersById(Guid id)
        {
            using (var connection = _db.CreateConnection())
            {
                string sprocedureName = "Proc_DocumentWatchers_FindById";
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Guid);
                return await connection.QueryFirstOrDefaultAsync<ENT_DocumentWatchers>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
            }

        }

        public async Task<List<ENT_DocumentWatchers>> GetAllDocumentWatchers()
        {
            using (var connection = _db.CreateConnection())
            {
                string sprocedureName = "Proc_DocumentWatchers_GetAll";
                var parameters = new DynamicParameters();
                var dt = await connection.QueryAsync<ENT_DocumentWatchers>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
                return dt.ToList();
            }
        }

        public async Task<ENT_DocumentWatchers> GetDocumentWatchersByDocumentIdAndUserId(Guid documentId, Guid userId)
        {
            using (var connection = _db.CreateConnection())
            {
                string sprocedureName = "[dbo].[Proc_DocumentWatchers_FindByDocumentIdAndUserIdOrEmail]";
                var parameters = new DynamicParameters();
                parameters.Add("DocumentId", documentId, DbType.Guid);
                parameters.Add("UserId", userId, DbType.Guid);
                parameters.Add("Email", "", DbType.String);
                parameters.Add("Type", "UserId", DbType.String);
                return await connection.QueryFirstOrDefaultAsync<ENT_DocumentWatchers>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ENT_DocumentWatchers> GetDocumentWatchersByDocumentIdAndEmail(Guid documentId, string email)
        {
            using (var connection = _db.CreateConnection())
            {
                string sprocedureName = "[dbo].[Proc_DocumentWatchers_FindByDocumentIdAndUserIdOrEmail]";
                var parameters = new DynamicParameters();
                parameters.Add("DocumentId", documentId, DbType.Guid);
                parameters.Add("UserId", Guid.Empty, DbType.Guid);
                parameters.Add("Email", email, DbType.String);
                parameters.Add("Type", "Email", DbType.String);
                return await connection.QueryFirstOrDefaultAsync<ENT_DocumentWatchers>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<ENT_User>> GetAllUser()
        {
            using (var connection = _db.CreateConnection())
            {
                string sprocedureName = "[dbo].[Proc_User_GetAll]";
                var parameters = new DynamicParameters();
                var dt = await connection.QueryAsync<ENT_User>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
                return dt.ToList();
            }
        }

        public async Task<List<ENT_Document>> GetAllUDocuments()
        {
            using (var connection = _db.CreateConnection())
            {
                string sprocedureName = "Proc_Document_GetAll";
                var parameters = new DynamicParameters();
                var dt = await connection.QueryAsync<ENT_Document>(sprocedureName, parameters, commandType: CommandType.StoredProcedure);
                return dt.ToList();
            }
        }
    }
}
