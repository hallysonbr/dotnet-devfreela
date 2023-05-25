using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DevFreela.Core.Extensions
{
    public static class StringExtensions
    {
        public static string Format(this string value)
        {
            if (value == null)
                return null;

            return value.Trim().ToLower(CultureInfo.CurrentCulture);
        }

        public static string FormatToUpper(this string value)
        {
            if (value is null)
                return null;

            return value.Trim().ToUpper(CultureInfo.CurrentCulture);
        }

        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return (s.Length % 4 == 0) && Regex.IsMatch(s, @"^[a-zA-Z0-9\+\/]*={0,3}$", RegexOptions.None);
        }

        public static string ToBase64String(this string s)
        {
            var bytes = Encoding.UTF8.GetBytes(s);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static string ToCurrency(this decimal value)
        {
            var format = new NumberFormatInfo
            {
                CurrencySymbol = "",
                CurrencyGroupSeparator = ".",
                CurrencyDecimalSeparator = ",",
                CurrencyDecimalDigits = 2,
                CurrencyNegativePattern = 1,
            };
            return value.ToString("C", format);
        }

        public static string ToAmericaStandardTime(this DateTime date)
        {
            var utcDate = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTime(utcDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static string MinifyXmlString(this string content)
        {
            content = content.Replace("\r", "").Trim();

            while (true)
            {
                var startIndex = content.IndexOf('\n');
                if (startIndex == -1) return content;

                var count = 1;
                for (int i = startIndex + 1; i < content.Length && content[i] == ' '; i++) count++;

                content = content.Remove(startIndex, count);
            }
        }

        public static bool IsAnalyst(this string username)
        {
            return username.Format().Substring(0, 2).Equals("TR".Format())
                || username.Format().Substring(0, 2).Equals("CS".Format());
        }

        public static string FormatProfile(this string profile)
        {
            return profile.Contains("Operador") ? "POPL.Operador" : profile;
        }

        public static string ToCNPJ(this string cnpj) 
        {
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }
            
        public static string ToCEP(this string cep)
        {
            return Convert.ToUInt64(cep).ToString(@"00000\-000");
        }

        public static string ToPhone(this string phone)
        {
            return phone.Length == 11 ?
                Convert.ToUInt64(phone).ToString(@"\(00\) 00000\-0000") :
                Convert.ToUInt64(phone).ToString(@"\(00\) 0000\-0000");
        }
    }
}
