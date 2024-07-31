using System;
using Vsoft_share_document.DAO;
using Vsoft_share_document.DatabaseHelper;
using Vsoft_share_document.DTO;
using Vsoft_share_document.InterfaceBUS;
using Vsoft_share_document.InterfaceDAO;
using Vsoft_share_document.Validate;

namespace Vsoft_share_document.BUS
{
    public class DocumentWatchers_BUS : IDocumentWatchers_BUS
    {
        private readonly IDocumentWatchers_DAO _documentWatchersDAO;
        public DocumentWatchers_BUS(IDocumentWatchers_DAO dao) {
            _documentWatchersDAO = dao;
        }
        public async Task<string> Create(ENT_CreateDocumentWatchers body)
        {
            // Kiểm tra các giá trị
            if (body == null)
            {
                throw new ArgumentException("Vui lòng nhập thông tin");
            }
            else if ((body.UserId == null || body.UserId.ToString() == "") && body.Email == "") {
                throw new ArgumentException("Vui lòng nhập UserId hoặc Email");
            } else if (body.DocumentId.ToString() == "")
            {
                return "Vui lòng nhập DocumentId";
            } else if(body.ExpiredIn == "" || !DateTimeValidator.IsValidDateFormat(body.ExpiredIn))
            {
                throw new ArgumentException("Vui lòng nhập ExpiredIn đúng định dạng dd-MM-yyyy");
            }
            //Check email hợp lệ
            if ( body.Email != "" && !EmailValidator.IsValidEmail(body.Email))
            {
                throw new ArgumentException("Email không hợp lệ");
            }

            // Tìm tài liệu
            var document = await _documentWatchersDAO.GetDocumentById(body.DocumentId);
            if (document == null) {
                throw new ArgumentException($"Không tồn tại tài liệu có Id = {body.DocumentId}");
            }
            //Kiểm tra tài liệu có được chia sẻ không
            if (document.Status != 1 && document.Status != 3 || document.SecurityLevel == 3)
            {
                throw new ArgumentException("Tài liệu không được phép chia sẻ");
            }


            // Kiểm tra có tồn tại người dùng với Id kia không
            if (body.UserId.HasValue)
            {
                var user = await _documentWatchersDAO.GetUserById(body.UserId);
                
                if (user == null) {
                    throw new ArgumentException($"Không tồn tại người dùng có Id = {body.UserId}");
                }

                //Kiểm tra tài liệu đã chia sẻ cho người dùng này chưa
                var docWatcher = await _documentWatchersDAO.GetDocumentWatchersByDocumentIdAndUserId(body.DocumentId, user.Id);
                if (docWatcher != null) {
                    throw new ArgumentException("Tài liệu đã được chia sẻ cho người dùng trước đó");
                }

                // Người dùng hệ thống được chia sẻ tài liệu sercurity 0-1-2
                if (document.SecurityLevel == 0 || document.SecurityLevel == 1 || document.SecurityLevel == 2)
                {
                    body.Email = user.Email;
                    int createSucess = await _documentWatchersDAO.CreateDocumentWatchers(body);
                    if (createSucess == 0)
                    {
                        throw new ArgumentException("Lỗi tạo mới chia sẻ tài liệu");
                    }

                }
            }
            else
            {
                
                // Người dùng ngoài hệ thống được chia sẻ tài liệu sercurity 0-1-2
                if (document.SecurityLevel == 0 || document.SecurityLevel == 1)
                {
                    body.UserId = Guid.Empty;
                    //Kiểm tra tài liệu đã chia sẻ cho người dùng này chưa
                    var docWatcher = await _documentWatchersDAO.GetDocumentWatchersByDocumentIdAndEmail(body.DocumentId, body.Email);
                    if (docWatcher != null)
                    {
                        throw new ArgumentException("Tài liệu đã được chia sẻ cho người dùng trước đó");
                    }

                    int createSucess = await _documentWatchersDAO.CreateDocumentWatchers(body);
                    if (createSucess == 0)
                    {
                        throw new ArgumentException("Lỗi tạo mới chia sẻ tài liệu");
                    }

                }
            }

            return "Chia sẻ tài liệu cho người dùng thành công"; 


        }

        public async Task<string> DeleteById(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                var docWatcher = await _documentWatchersDAO.GetDocumentWatchersById(guid);

                if (docWatcher == null)
                {
                    throw new ArgumentException($"Không tồn tại chia sẻ tài liệu có Id = {guid}");
                }
                int deleteSucess = await _documentWatchersDAO.DeleteDocumentById(guid);
                if (deleteSucess == 0)
                {
                    throw new ArgumentException("Lỗi xóa chia sẻ tài liệu");
                }
            }
            else
            {
                throw new ArgumentException("Chuỗi không phải là một Guid hợp lệ.");
            }
            return "Xóa chia sẻ tài liệu thành công";
        }

        public async Task<List<ENT_DocumentWatchers>> GetAll()
        {
            var data = await _documentWatchersDAO.GetAllDocumentWatchers();
            return data;
        }

        public async Task<List<ENT_Document>> GetAllDocument()
        {
            var data = await _documentWatchersDAO.GetAllUDocuments();
            return data;
        }

        public async Task<List<ENT_User>> GetAllUser()
        {
            var data = await _documentWatchersDAO.GetAllUser();
            return data;
        }

        public async Task<ENT_DocumentWatchers> GetById(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                var docWatcher = await _documentWatchersDAO.GetDocumentWatchersById(guid);
                return docWatcher;
            }
            else
            {
                return null;
            }
        }
    }
}
