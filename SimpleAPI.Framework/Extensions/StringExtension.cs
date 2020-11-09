using System;
using System.Text;

namespace SimpleAPI.Framework.Extensions
{
    public static class StringExtension
    {
        public static bool IsNull(this string word)
        {
            return word == null;
        }

        public static bool IsEmpty(this string word)
        {
            return word.Replace(" ", "") == string.Empty;
        }

        public static bool IsNullOrEmpty(this string word)
        {
            return IsNull(word) || IsEmpty(word);
        }

        public static string ToKebabCase(this string str)
        {
            if (str.IsNullOrEmpty())
            {
                return str;
            }

            var sb = new StringBuilder();

            foreach (var ch in str.ToCharArray())
            {
                if (char.IsUpper(ch))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("-");
                    }

                    sb.Append(char.ToLower(ch));
                }
                else
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        public static bool StartsWithIgnoreCase(this string a, string b)
        {
            return a.StartsWith(b, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string ConcatStringIf(this string str, bool condition, string strToConcat)
        {
            if (condition)
            {
                str += strToConcat;
            }

            return str;
        }

        public static bool In(this string str, params string[] valores)
        {
            foreach (var item in valores)
            {
                if (str == item)
                    return true;
            }

            return false;
        }

        public static string RemoveBeginning(this string str, string strToRemove, StringComparison stringComparison = StringComparison.CurrentCulture)
        {
            if (str.StartsWith(strToRemove, stringComparison))
            {
                str = str.Remove(str.IndexOf(strToRemove, stringComparison), strToRemove.Length);
            }

            return str;
        }

        public static string RemoveFromEndIgnoreCase(this string str, params string[] suffixes)
        {
            return str.RemoveFromEnd(StringComparison.CurrentCultureIgnoreCase, suffixes);
        }

        public static string RemoveFromEnd(this string str, StringComparison stringComparison, params string[] suffixes)
        {
            for (int i = 0; i < suffixes.Length; i++)
            {
                if (str.EndsWith(suffixes[i], stringComparison))
                {
                    return str.Substring(0, str.Length - suffixes[i].Length);
                }
            }

            return str;
        }

        public static string ConcatString(this string str, string strConcat)
        {
            return str + strConcat;
        }
    }
}
