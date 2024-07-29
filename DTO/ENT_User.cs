namespace Vsoft_share_document.DTO
{
    public class ENT_User
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}
