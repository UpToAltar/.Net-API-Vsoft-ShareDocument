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
    }
}
