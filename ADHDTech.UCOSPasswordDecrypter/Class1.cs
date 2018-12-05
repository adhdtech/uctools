using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// Contains global variables for project.
    /// </summary>
    public static class DRSD
    {
        public static string sFilenameEncrypted = "";
        public static string sBackupSetXMLFilename = "";
        public static string sBackupSetDirectory = "";
        public static string sUCVersion = "";
        public static int iDecryptFileCount = 0;
        public static int iDecryptFilesProcessed = 0;
        public static long lCurrentFileSize = 0;
        public static long lCurrentFileProcessed = 0;
        public static string sUCProduct = "";
        public static string sLocalHostName = "";
        public static string sLocalHostIP0 = "";
        public static string sLocalHostAdminName = "";
        public static string sLocalHostAdminPwCrypt = "";
        public static string sSftpPwCrypt = "";
        public static string sIPSecSecurityPwCrypt = "";
        public static string sApplUserUsername = "";
        public static string sApplUserPwCrypt = "";

    }

    public class UCOSHostCfg
    {
        public string sUCOSHost;
        public string sUCOSRemoteUser;
        public string sUCOSPassphrase;
    }
}
