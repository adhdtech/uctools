using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADHDTech;
using ADHDTech.CiscoCrypt;

namespace DRSBackupDecrypter
{
    /// <summary>
    /// Contains global variables for project.
    /// </summary>
    public static class DRSD
    {

        public static DRSBackupSet myBackupSet;
        /// <summary>
        /// Global variable that is constant.
        /// </summary>
        public const string GlobalString = "Important Text";

        /// <summary>
        /// Static value protected by access routine.
        /// </summary>
        static int _globalValue;

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static int GlobalValue
        {
            get
            {
                return _globalValue;
            }
            set
            {
                _globalValue = value;
            }
        }

        /// <summary>
        /// Global static field.
        /// </summary>
        public static bool GlobalBoolean;

        public static string sFilenameEncrypted = "";
        public static string sFilenameDecryptTo = "";
        public static string sClusterSecurityPass = "";
        public static string sClusterSecurityPassEncHex = "";
        public static string sRandomBackupPass = "";
        public static string sXMLEncryptKey = "";
        public static string sOutputDirectory = "";
        public static string sBackupSetXMLFilename = "";
        public static string sBackupSetDirectory = "";
        public static string sUCVersion = "";
        public static int iDecryptFileCount = 0;
        public static int iDecryptFilesProcessed = 0;
        public static long lCurrentFileSize = 0;
        public static long lCurrentFileProcessed = 0;
        public static bool bTermEarly = false;
        public static string[] sFilesToDecrypt;
    }
}
