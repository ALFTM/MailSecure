using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure
{
    public static class AppConst
    {
        // This must be use with Environment.ExpandEnvironmentVariables() to have the full path
        public static readonly string APP_DATA_FOLDER = "%AppData%\\MailSecure";
    }
}
