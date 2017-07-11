using System;
using System.IO;

namespace MailSecure.Core
{
    public static class DirectoryManager
    {
        #region Private Property
        private static string tempfolderPath = Environment.CurrentDirectory + "\\tmp\\";
        #endregion

        #region Public Static Methods
        public static bool CheckIfTempFolderExist()
        {
            return Directory.Exists(tempfolderPath);
        }

        public static void CreateTempFolder()
        {
            if (!CheckIfTempFolderExist())
            {
                Directory.CreateDirectory(tempfolderPath);
            }
        }

        public static void ClearTempFolder()
        {
            if (CheckIfTempFolderExist())
            {
                var dirInfoTemp = new DirectoryInfo(tempfolderPath);
                Array.ForEach(dirInfoTemp.GetFiles(), f => f.Delete());
                Array.ForEach(dirInfoTemp.GetDirectories(), f => f.Delete(true));
            }
        }

        public static void DeleteTempFolder()
        {
            if (CheckIfTempFolderExist())
            {
                Directory.Delete(tempfolderPath);
            }
        }
        #endregion
    }
}
