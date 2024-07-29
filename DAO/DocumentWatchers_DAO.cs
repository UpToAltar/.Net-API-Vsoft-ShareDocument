

using Dapper;
using Microsoft.SqlServer.Server;
using System.Data;
using System.Globalization;
using Vsoft_share_document.DatabaseHelper;
using Vsoft_share_document.DTO;
using Vsoft_share_document.InterfaceDAO;

namespace Vsoft_share_document.DAO
{
    public class DocumentWatchers_DAO : IDocumentWatchers_DAO
    {
        private readonly DatabaseHelperNew _db;
        public DocumentWatchers_DAO(DatabaseHelperNew db) {
            _db = db;
        }
        
        public async Task<int> CreateDocumentWatchers(ENT_CreateDocumentWatchers body)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", Guid.NewGuid(), DbType.Guid);
                parameters.Add("DocumentId", body.DocumentId, DbType.Guid);
                parameters.Add("UserId", body.UserId, DbType.Guid);
                parameters.Add("Email", body.Email, DbType.String);
                parameters.Add("ExpiredIn", DateTime.ParseExact(body.ExpiredIn, "dd-MM-yyyy", CultureInfo.InvariantCulture), DbType.DateTime);
                parameters.Add("CreatedBy", Guid.Empty, DbType.Guid);
                parameters.Add("CheckSum", "", DbType.String);

                int dt = await _db.ExcuteNonQueryAsync("Proc_DocumentWatchers_Insert", parameters);
                return dt;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<ENT_User> GetUserById(Guid? Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", Id, DbType.Guid);
                parameters.Add("UserId", new Guid(), DbType.Guid);
                ENT_User dt = await _db.ExcuteReturnDataAsync<ENT_User>("[dbo].[Proc_User_FindById]", parameters);
                return dt;
            }
            catch
            {
                return null;
            }
        }


        public async Task<ENT_Document> GetDocumentById(Guid Id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", Id, DbType.Guid);
                ENT_Document dt = await _db.ExcuteReturnDataAsync<ENT_Document>("[dbo].[Proc_Document_FindById]", parameters);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> DeleteDocumentById(Guid id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id",id, DbType.Guid);
                parameters.Add("EditedBy", Guid.Empty, DbType.Guid);
                int dt = await _db.ExcuteNonQueryAsync("[dbo].[Proc_DocumentWatchers_DeleteById]", parameters);
                return dt;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<ENT_DocumentWatchers> GetDocumentWatchersById(Guid id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("Id", id, DbType.Guid);
                ENT_DocumentWatchers dt = await _db.ExcuteReturnDataAsync<ENT_DocumentWatchers>("Proc_DocumentWatchers_FindById", parameters);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ENT_DocumentWatchers>> GetAllDocumentWatchers()
        {
            try
            {
                List<ENT_DocumentWatchers> dt = await _db.ExcuteReturnListOfDataAscync<ENT_DocumentWatchers>("Proc_DocumentWatchers_GetAll", new DynamicParameters());
                return dt;
            }
            catch
            {
                return new List<ENT_DocumentWatchers>();
            }
        }

        public async Task<ENT_DocumentWatchers> GetDocumentWatchersByDocumentIdAndUserId(Guid documentId, Guid userId)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("DocumentId", documentId, DbType.Guid);
                parameters.Add("UserId", userId, DbType.Guid);
                parameters.Add("Email", "", DbType.String);
                parameters.Add("Type", "UserId", DbType.String);
                ENT_DocumentWatchers dt = await _db.ExcuteReturnDataAsync<ENT_DocumentWatchers>("[dbo].[Proc_DocumentWatchers_FindByDocumentIdAndUserIdOrEmail]", parameters);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ENT_DocumentWatchers> GetDocumentWatchersByDocumentIdAndEmail(Guid documentId, string email)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("DocumentId", documentId, DbType.Guid);
                parameters.Add("UserId", Guid.Empty, DbType.Guid);
                parameters.Add("Email", email, DbType.String);
                parameters.Add("Type", "Email", DbType.String);
                ENT_DocumentWatchers dt = await _db.ExcuteReturnDataAsync<ENT_DocumentWatchers>("[dbo].[Proc_DocumentWatchers_FindByDocumentIdAndUserIdOrEmail]", parameters);
                return dt;
            }
            catch
            {
                return null;
            }
        }
    }
}
