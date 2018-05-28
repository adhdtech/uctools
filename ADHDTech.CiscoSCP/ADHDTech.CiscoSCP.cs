using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADHDTech.CiscoCrypt;

namespace ADHDTech.CiscoSCP
{
    public class Client
    {
        Renci.SshNet.ConnectionInfo scpConnInfo;
        Renci.SshNet.ScpClient scpClient;
        public Client(string sHostName, string sRemoteSupportUser, string sRemoteSupportPassphrase)
        {
            RemoteSupportPassphrase oReportSupportPassphrase = new RemoteSupportPassphrase();
            String sUserPassword = oReportSupportPassphrase.Decode(sRemoteSupportPassphrase);
            Renci.SshNet.AuthenticationMethod authMethod = new Renci.SshNet.PasswordAuthenticationMethod(sRemoteSupportUser, sUserPassword);
            scpConnInfo = new Renci.SshNet.ConnectionInfo(sHostName, sRemoteSupportUser, new[] { authMethod });
            scpClient = new Renci.SshNet.ScpClient(scpConnInfo);
        }

        public Dictionary<String, byte[]> GetSecurityFilePack()
        {
            String[] SecurityFileNames = new String[] {
                "/usr/local/platform/conf/platformConfig.xml",
                "/usr/local/platform/.security/CCMEncryption/keys/dkey.txt"
            };
            Dictionary<String, byte[]> oSecurityFilePack = new Dictionary<String, byte[]>();
            scpClient.Connect();
            if (scpClient.IsConnected)
            {
                foreach (String sFileName in SecurityFileNames)
                {
                    MemoryStream xmlStream = new MemoryStream();

                    scpClient.Download(sFileName, xmlStream);

                    byte[] xmlDataBytes = new byte[xmlStream.Length];
                    xmlStream.Seek(0, SeekOrigin.Begin);
                    xmlStream.Read(xmlDataBytes, 0, (int)xmlStream.Length);
                    oSecurityFilePack[sFileName] = xmlDataBytes;
                    //Console.Write("{0}\n", Functions.encoding.GetString(xmlDataBytes));
                }
                scpClient.Disconnect();
            }
            else
            {
                // Error - not connected
            }
            return oSecurityFilePack;
        }

        public byte[] GetPlatformConfig()
        {
            byte[] bPlatformConfigData = new byte[0];
            scpClient.Connect();
            if (scpClient.IsConnected)
            {
                Console.WriteLine("SCP connected");
                //MemoryStream xmlStream = new MemoryStream();
                FileInfo newFile = new FileInfo(@"C:\Temp\platformConfig.xml.xml");
                //newFile.DirectoryName = @"C:\Temp";
                scpClient.Download(@"/usr/local/platform/conf/platformConfig.xml", newFile);
                /*
                bPlatformConfigData = new byte[xmlStream.Length];
                xmlStream.Seek(0, SeekOrigin.Begin);
                xmlStream.Read(bPlatformConfigData, 0, (int)xmlStream.Length);
                */
                scpClient.Disconnect();
                //Console.Write("PlatformConfig...\n----------\n{0}----------\n", Functions.encoding.GetString(bPlatformConfigData));
                
            }
            else
            {
                // Error - not connected
                Console.WriteLine("SCP Not connected");
            }
            return bPlatformConfigData;
        }
    }

    public class LiveHostClient
    {
        Renci.SshNet.ConnectionInfo scpConnInfo;
        Renci.SshNet.ScpClient scpClient;
        public LiveHostClient(string sHostName, string sRemoteSupportUser, string sRemoteSupportPassphrase)
        {
            RemoteSupportPassphrase oReportSupportPassphrase = new RemoteSupportPassphrase();
            String sUserPassword = oReportSupportPassphrase.Decode(sRemoteSupportPassphrase);
            Renci.SshNet.AuthenticationMethod authMethod = new Renci.SshNet.PasswordAuthenticationMethod(sRemoteSupportUser, sUserPassword);
            scpConnInfo = new Renci.SshNet.ConnectionInfo(sHostName, sRemoteSupportUser, new[] { authMethod });
            scpClient = new Renci.SshNet.ScpClient(scpConnInfo);

            MemoryStream xmlStream = new MemoryStream();

            scpClient.Connect();
            //System.Threading.Thread.Sleep(3000);
            try
            {
                scpClient.Download(@"/usr/local/platform/conf/platformConfig.xml", xmlStream);
            } catch (Exception ex) {
                string bob = ex.Message;
            }

            scpClient.Disconnect();

            byte[] xmlDataBytes = new byte[xmlStream.Length];
            xmlStream.Seek(0, SeekOrigin.Begin);
            xmlStream.Read(xmlDataBytes, 0, (int)xmlStream.Length);

            Console.Write("{0}\n", Functions.encoding.GetString(xmlDataBytes));
        }
    }

    public class LiveHostClientSFTP
    {
        Renci.SshNet.ConnectionInfo scpConnInfo;
        Renci.SshNet.SftpClient sftpClient;
        public LiveHostClientSFTP(string sHostName, string sRemoteSupportUser, string sRemoteSupportPassphrase)
        {
            RemoteSupportPassphrase oReportSupportPassphrase = new RemoteSupportPassphrase();
            String sUserPassword = oReportSupportPassphrase.Decode(sRemoteSupportPassphrase);
            Renci.SshNet.AuthenticationMethod authMethod = new Renci.SshNet.PasswordAuthenticationMethod(sRemoteSupportUser, sUserPassword);
            scpConnInfo = new Renci.SshNet.ConnectionInfo(sHostName, sRemoteSupportUser, new[] { authMethod });
            sftpClient = new Renci.SshNet.SftpClient(scpConnInfo);

            MemoryStream xmlStream = new MemoryStream();

            sftpClient.Connect();
            try
            {
                sftpClient.BeginDownloadFile(@"/usr/local/platform/conf/platformConfig.xml", xmlStream);
            }
            catch (Exception ex)
            {
                string bob = ex.Message;
            }
            sftpClient.Disconnect();

            byte[] xmlDataBytes = new byte[xmlStream.Length];
            xmlStream.Seek(0, SeekOrigin.Begin);
            xmlStream.Read(xmlDataBytes, 0, (int)xmlStream.Length);

            Console.Write("{0}\n", Functions.encoding.GetString(xmlDataBytes));
        }
    }
}
