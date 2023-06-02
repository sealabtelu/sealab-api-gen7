using System.Globalization;

namespace SealabAPI.Helpers
{
    public static class StringHelper
    {
        public static string RandomString(int length = 7)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";//0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string ToTitleCase(this string title)
        {
            return title != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower()) : null;
        }
    }
}
