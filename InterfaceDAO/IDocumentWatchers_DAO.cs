using Vsoft_share_document.DTO;

namespace Vsoft_share_document.InterfaceDAO
{
    public interface IDocumentWatchers_DAO
    {
        public Task<int> CreateDocumentWatchers(ENT_CreateDocumentWatchers body);
        public Task<ENT_DocumentWatchers> GetDocumentWatchersById(Guid id);
        public Task<ENT_User> GetUserById(Guid? Id);

        public Task<List<ENT_User>> GetAllUser();
        public Task<List<ENT_Document>> GetAllUDocuments();
        public Task<ENT_Document> GetDocumentById(Guid id);

        public Task<int> DeleteDocumentById(Guid id);

        public Task<List<ENT_DocumentWatchers>> GetAllDocumentWatchers();
        public Task<ENT_DocumentWatchers> GetDocumentWatchersByDocumentIdAndUserId(Guid documentId, Guid userId);

        public Task<ENT_DocumentWatchers> GetDocumentWatchersByDocumentIdAndEmail(Guid documentId, string email);
    }
}
