using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace MailSecure.Security
{
    public static class Utils
    {
        private static Random random = new Random();
        public static string RandomPassword(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetFileNameFromPath(string path)
        {
            return path.Substring(path.LastIndexOf('\\') + 1);
        }

        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null && data.Any();
        }

        public static string ConvertToUnsecureString(SecureString secureString)
        {
            if (secureString == null) {
                return string.Empty;
            }

            IntPtr unmanagedString = IntPtr.Zero;

            try {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
