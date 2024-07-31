namespace Vsoft_share_document.DTO
{
    public class ENT_Document
    {
        public Guid Id { get; set; }
        public string FKey { get; set; }
        public string Code { get; set; }
        public int SecurityLevel { get; set; }

        public bool IsActive { get; set; }
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
}
