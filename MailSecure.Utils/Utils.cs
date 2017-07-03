using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure
{
    public class Utils
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
    }
}
