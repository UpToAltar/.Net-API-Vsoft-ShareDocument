using System.ComponentModel;

namespace Vsoft_share_document.DTO
{
    public class ENT_DocumentWatchers
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid? UserId { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime ExpiredIn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime EditedOn { get; set; }

        public Guid? CreatedBy { get; set; }
        public Guid? EditedBy { get; set;}
        public string? CheckSum { get; set; }

        public int SecurityLevel { get; set; }

        private int status;

        public int Status
        {
            get => status;
            set
            {
                status = value;
                StatusMessage = EnumHelper.GetDescription((EDocumentStatus)value);
            }
        }
        public string DocumentSummary { get; set; }

        public string StatusMessage { get; set; }
    }

    public enum EDocumentStatus
    {
        [Description("Đang xử lý")]
        Processing = 0,

        [Description("Đã hoàn thành")]
        Completed = 1,

        [Description("Bản nháp")]
        Draft = 2,

        [Description("Bị thay thế")]
        Replaced = 3,

        [Description("Hết hiệu lực")]
        Expired = 4
    }
}
