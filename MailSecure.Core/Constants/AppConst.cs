namespace MailSecure.Core
{
    public static class AppConst
    {
        // This must be use with Environment.ExpandEnvironmentVariables() to have the full path
        public static readonly string APP_DATA_FOLDER = "%AppData%\\MailSecure";
        public static readonly string USER_CONFIG_FILE_NAME = "userapp.conf";
    }
}
