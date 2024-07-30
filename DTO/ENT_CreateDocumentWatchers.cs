using System.ComponentModel.DataAnnotations;

namespace Vsoft_share_document.DTO
{
    public class ENT_CreateDocumentWatchers
    {
        [Required]
        public Guid DocumentId { get; set; }
        public Guid? UserId { get; set; }

        /// <example>Men's basketball shoes</example>
        public string? Email { get; set; } = "";
        public string ExpiredIn { get; set; } = "27-09-2024";

        public bool IsActive { get; set; } = true;
    }
}
