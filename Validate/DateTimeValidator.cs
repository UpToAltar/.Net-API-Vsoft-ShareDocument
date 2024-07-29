using System.Globalization;
using System.Text.RegularExpressions;

namespace Vsoft_share_document.Validate
{
    public class DateTimeValidator
    {
        public static bool IsValidDateFormat(string date)
        {
            if (string.IsNullOrWhiteSpace(date))
                return false;

            string pattern = @"^\d{2}-\d{2}-\d{4}$";
            Regex regex = new Regex(pattern);

            if (!regex.IsMatch(date))
                return false;

            // Kiểm tra tính hợp lệ của ngày
            DateTime parsedDate;
            string format = "dd-MM-yyyy";
            bool isValidDate = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate);

            return isValidDate;
        }
    }
}
