using System.Text;
using System.Text.RegularExpressions;

namespace _0_FrameWork.Application
{
    public static class SlugHelper
    {
        private static readonly Dictionary<char, string> Map = new()
        {
            // حروف فارسی/عربی
            ['ا'] = "a",
            ['آ'] = "a",
            ['ب'] = "b",
            ['پ'] = "p",
            ['ت'] = "t",
            ['ث'] = "s",
            ['ج'] = "j",
            ['چ'] = "ch",
            ['ح'] = "h",
            ['خ'] = "kh",
            ['د'] = "d",
            ['ذ'] = "z",
            ['ر'] = "r",
            ['ز'] = "z",
            ['ژ'] = "zh",
            ['س'] = "s",
            ['ش'] = "sh",
            ['ص'] = "s",
            ['ض'] = "z",
            ['ط'] = "t",
            ['ظ'] = "z",
            ['ع'] = "a",
            ['غ'] = "gh",
            ['ف'] = "f",
            ['ق'] = "gh",
            ['ک'] = "k",
            ['ك'] = "k",
            ['گ'] = "g",
            ['ل'] = "l",
            ['م'] = "m",
            ['ن'] = "n",
            ['و'] = "o",
            ['ه'] = "h",
            ['ی'] = "i",
            ['ي'] = "i",
            // همزه‌ها
            ['ء'] = "",
            ['ئ'] = "i",
            ['ؤ'] = "o",
            // ارقام فارسی/عربی
            ['۰'] = "0",
            ['۰'] = "0",
            ['۱'] = "1",
            ['۲'] = "2",
            ['۳'] = "3",
            ['۴'] = "4",
            ['۵'] = "5",
            ['۶'] = "6",
            ['۷'] = "7",
            ['۸'] = "8",
            ['۹'] = "9",
            ['٠'] = "0",
            ['١'] = "1",
            ['٢'] = "2",
            ['٣'] = "3",
            ['٤'] = "4",
            ['٥'] = "5",
            ['٦'] = "6",
            ['٧'] = "7",
            ['٨'] = "8",
            ['٩'] = "9",
            // فاصله‌ها
            [' '] = "-",
            ['\u200C'] = "-", // ZWNJ -> dash
                              // علائم متداول که بهتره حذف یا به خط‌تیره تبدیل شن
            ['_'] = "-",
            ['–'] = "-",
            ['—'] = "-",
            ['ـ'] = "-",
            ['٫'] = ".",
            ['٪'] = "percent"
        };

        // حروف لاتین مجاز + ارقام
        private static readonly Regex Allowed = new(@"[^a-z0-9\-\.]", RegexOptions.Compiled);
        private static readonly Regex Dashes = new(@"[-]{2,}", RegexOptions.Compiled);
        private static readonly Regex TrimDashes = new(@"(^-+|-+$)", RegexOptions.Compiled);

        public static string FaToEnSlug(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var sb = new StringBuilder(input.Length * 2);

            foreach (var ch in input.Normalize(NormalizationForm.FormC))
            {
                if (Map.TryGetValue(ch, out var rep))
                {
                    sb.Append(rep);
                }
                else if (char.IsLetterOrDigit(ch))
                {
                    // اگر لاتین بود، همون رو می‌ذاریم
                    sb.Append(ch);
                }
                else if (ch == '-' || ch == '.')
                {
                    sb.Append(ch);
                }
                // بقیه کاراکترها نادیده گرفته می‌شن
            }

            var slug = sb.ToString().ToLowerInvariant();

            // حذف هر چیزی غیر از a-z, 0-9, خط‌تیره و نقطه
            slug = Allowed.Replace(slug, "");
            // جمع کردن خط‌تیره‌های تکراری
            slug = Dashes.Replace(slug, "-");
            // حذف خط‌تیره از ابتدا/انتها
            slug = TrimDashes.Replace(slug, "");

            return slug;
        }
    }
}
