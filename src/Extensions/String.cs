using System;
using System.Linq;

namespace Vensha.Extensions
{
    public static class StringExtension
    {
        public static string toTitle(this string str) => String.Join(' ', str.Split(' ').Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower()).ToArray());

        public static string redactCredentials(this string str) => str.Replace(Program.Config.token, "[TOKEN REDACTED]");
    }
}