
namespace WufooSharp
{
    public static class Extensions
    {
        public static string FormatWith(this string s, params object[] args) {
            return string.Format(s, args);
        }
    }
}
