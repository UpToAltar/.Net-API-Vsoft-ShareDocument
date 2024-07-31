using Vsoft_share_document.DTO;

namespace Vsoft_share_document.InterfaceBUS
{
    public interface IDocumentWatchers_BUS
    {
        public Task<string> Create(ENT_CreateDocumentWatchers body);
        public Task<string> DeleteById(string id);

        public Task<List<ENT_DocumentWatchers>> GetAll();
        public Task<ENT_DocumentWatchers> GetById(string id);

        public Task<List<ENT_User>> GetAllUser();
        public Task<List<ENT_Document>> GetAllDocument();
    }
}
