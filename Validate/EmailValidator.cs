using System.Text.RegularExpressions;

namespace Vsoft_share_document.Validate
{
    public class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Sử dụng biểu thức chính quy để kiểm tra tính hợp lệ của email
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

                return regex.IsMatch(email);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu cần thiết (ví dụ: ghi log)
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
