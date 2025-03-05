using System.Text;

namespace XmlStorage.Utils.Extensions
{
    internal static class StringBuilderExtensions
    {
        public static string ToString(this StringBuilder builder, string str1, string str2, string str3)
        {
            builder.Clear();
            builder.Append(str1);
            builder.Append(str2);
            builder.Append(str3);

            return builder.ToString();
        }
    }
}
