using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace _0_FrameWork.Application
{
    public static class Slugify
    {
        public static string GenerateSlug(this string phrase)
        {
            var s = phrase.RemoveDiacritics().ToLower();

            // حذف کاراکترهای غیرمجاز (فارسی، انگلیسی، اعداد، فاصله و -)
            s = Regex.Replace(s, @"[^\u0600-\u06FF\ufb8a\u067e\u0686\u06af\u200c\u200fa-z0-9\s-]", "");

            // تبدیل چند فاصله پشت سر هم به یک فاصله
            s = Regex.Replace(s, @"\s+", " ").Trim();

            // کوتاه کردن متن (حداکثر 45 کاراکتر اگر بیشتر از 100 بود)
            s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim();

            // تبدیل فاصله‌ها به -
            s = Regex.Replace(s, @"\s", "-");

            // تبدیل نیم‌فاصله به -
            s = Regex.Replace(s, @"‌", "-");

            return s.ToLower();
        }

        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            var normalizedString = text.Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }


        #region Code 2
        //public static string GenerateSlug(string phrase)
        //{
        //    if (string.IsNullOrWhiteSpace(phrase))
        //        return "";

        //    // 1- به حروف کوچک تبدیل کن
        //    string str = phrase.ToLower().Trim();

        //    // 2- جایگزینی فاصله‌ها و _ با -
        //    str = Regex.Replace(str, @"[\s_]+", "-");

        //    // 3- حذف کاراکترهای غیر مجاز (اجازه به فارسی و انگلیسی و اعداد)
        //    str = Regex.Replace(str, @"[^a-z0-9\u0600-\u06FF\-]", "");

        //    // 4- حذف خط فاصله‌های تکراری
        //    str = Regex.Replace(str, @"-+", "-");

        //    // 5- حذف خط فاصله از ابتدا و انتها
        //    str = str.Trim('-');

        //    return str;
        //}
        #endregion

        #region Code 3
        //private static readonly Dictionary<char, string> PersianToEnglishMap = new()
        //{
        //    ['ا'] = "a",
        //    ['آ'] = "a",
        //    ['ب'] = "b",
        //    ['پ'] = "p",
        //    ['ت'] = "t",
        //    ['ث'] = "s",
        //    ['ج'] = "j",
        //    ['چ'] = "ch",
        //    ['ح'] = "h",
        //    ['خ'] = "kh",
        //    ['د'] = "d",
        //    ['ذ'] = "z",
        //    ['ر'] = "r",
        //    ['ز'] = "z",
        //    ['ژ'] = "zh",
        //    ['س'] = "s",
        //    ['ش'] = "sh",
        //    ['ص'] = "s",
        //    ['ض'] = "z",
        //    ['ط'] = "t",
        //    ['ظ'] = "z",
        //    ['ع'] = "a",
        //    ['غ'] = "gh",
        //    ['ف'] = "f",
        //    ['ق'] = "gh",
        //    ['ک'] = "k",
        //    ['گ'] = "g",
        //    ['ل'] = "l",
        //    ['م'] = "m",
        //    ['ن'] = "n",
        //    ['و'] = "v",
        //    ['ه'] = "h",
        //    ['ی'] = "y",
        //    ['ي'] = "y",
        //    ['ء'] = "",
        //    ['ٔ'] = "",
        //    ['ً'] = "",
        //    ['ٌ'] = "",
        //    ['ٍ'] = "",
        //    ['َ'] = "",
        //    ['ُ'] = "",
        //    ['ِ'] = "",
        //    ['ّ'] = "",
        //    ['ـ'] = ""
        //};

        //public static string GenerateSlug(string phrase)
        //{
        //    if (string.IsNullOrWhiteSpace(phrase))
        //        return "";

        //    phrase = phrase.Trim().ToLower();

        //    // 1. تبدیل حروف فارسی به انگلیسی
        //    var sb = new System.Text.StringBuilder();
        //    foreach (var ch in phrase)
        //    {
        //        if (PersianToEnglishMap.ContainsKey(ch))
        //            sb.Append(PersianToEnglishMap[ch]);
        //        else
        //            sb.Append(ch);
        //    }
        //    string result = sb.ToString();

        //    // 2. جایگزینی فاصله‌ها و _ با -
        //    result = Regex.Replace(result, @"[\s_]+", "-");

        //    // 3. حذف کاراکترهای غیرمجاز (فقط حروف انگلیسی، اعداد، و - باقی بماند)
        //    result = Regex.Replace(result, @"[^a-z0-9\-]", "");

        //    // 4. حذف خط فاصله‌های تکراری
        //    result = Regex.Replace(result, @"-+", "-");

        //    // 5. حذف - از ابتدا و انتها
        //    result = result.Trim('-');

        //    return result;
        //}
        #endregion

    }
}
