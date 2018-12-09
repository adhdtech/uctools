using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ADHDTech.CiscoCrypt
{
    public class Functions
    {
        public static ASCIIEncoding encoding = new ASCIIEncoding();
        public static JavaKeyHashConnector KeyHashConnector = new JavaKeyHashConnector();
        public static byte[] runningMarker = encoding.GetBytes("You are running ");
        public static byte[] plainPassMarker = encoding.GetBytes("-salt -k ");
        public static byte[] HexStringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public static int GetHexVal(char hex)
        {
            int val = (int)hex;
            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        public static string GetHexCharString(byte[] checkByteArray)
        {
            var returnString = "";
            if (checkByteArray.Length == 0 || checkByteArray.Length % 2 == 1)
                return null;

            for (int j = 0; j < checkByteArray.Length; j++)
            {
                byte checkByte = checkByteArray[j];
                if ((checkByte >= 0x30 && checkByte <= 0x39) ||
                    (checkByte >= 0x41 && checkByte <= 0x5A) ||
                    (checkByte >= 0x61 && checkByte <= 0x7A))
                {
                    // This is a valid hex character
                    returnString += Convert.ToChar(checkByte);
                }
                else
                {
                    return null;
                }
            }
            return returnString;
        }

        public static bool IsPrintableString(string checkString)
        {
            if (checkString.Length == 0)
                return false;

            for (int j = 0; j < checkString.Length; j++)
            {
                char checkByte = checkString[j];
                if (checkByte >= 0x21 && checkByte <= 0x7E)
                {
                    // This is a valid hex character
                }
                else
                {
                    // This is not a valid character
                    return false;
                }
            }
            return true;
        }

        public static string GetRandomPasswordFromFile(string encryptedFileName)
        {
            string sRandomPassword = null;

            try
            {
                using (FileStream fsInput = File.OpenRead(encryptedFileName))
                {

                    int bytesRead = 0;
                    int iTailSize = 2048;
                    byte[] chunkData = new byte[iTailSize];

                    // Seek to almost end of file
                    FileInfo oFileInfo = new FileInfo(encryptedFileName);
                    long lSeekTo = oFileInfo.Length - iTailSize;
                    fsInput.Seek(lSeekTo, 0);

                    bytesRead = fsInput.Read(chunkData, 0, iTailSize);

                    int matchLen = 0;

                    for (int p = 0; p < bytesRead; p++)
                    {
                        // See if this byte is part of the "-salt -k " marker
                        if (plainPassMarker[matchLen] == chunkData[p])
                        {
                            matchLen++;
                            if (matchLen == 9)
                            {
                                // The next 20 characters are the random backup password
                                byte[] passwordBytes = new byte[20];
                                System.Buffer.BlockCopy(chunkData, p + 1, passwordBytes, 0, 20);
                                sRandomPassword = Functions.encoding.GetString(passwordBytes);
                                break;
                            }
                        }
                        else
                        {
                            // Not a match, reset matchLen to 0
                            matchLen = 0;
                        }
                    }
                    fsInput.Close();
                }
            }
            catch
            {
                // Could not open file
            }
            return sRandomPassword;
        }

        public static int GetTrailingTextLength(string encryptedFileName)
        {
            int iTrailingTextLength = 0;
            try
            {
                using (FileStream fsInput = File.OpenRead(encryptedFileName))
                {

                    int bytesRead = 0;
                    int iTailSize = 2048;
                    byte[] chunkData = new byte[iTailSize];

                    // Seek to almost end of file
                    FileInfo oFileInfo = new FileInfo(encryptedFileName);
                    long lSeekTo = oFileInfo.Length - iTailSize;
                    fsInput.Seek(lSeekTo, 0);

                    // Get the tail of the file
                    bytesRead = fsInput.Read(chunkData, 0, iTailSize);

                    // If the last character is not a newline, we have no trailing text
                    if (chunkData[iTailSize - 1] != 0x0a) return iTrailingTextLength;

                    // Check for text

                    for (int p = 0; p < bytesRead; p++)
                    {

                        // If we're at zero, the string must begin with one of the following formats...
                        // 'start <scriptname.py>'
                        // 'You are running Linux OS'
                        // '2017/10/26 23:00: slm_do_backup.py :INFO:Start'

                        byte testByte = chunkData[p];
                        if (iTrailingTextLength == 0)
                        {
                            // If we're within the last 8 bytes and have found nothing, break
                            if (p + 8 == iTailSize) return iTrailingTextLength;

                            // 
                            if (chunkData[p] == 's' && chunkData[p + 1] == 't' && chunkData[p + 5] == ' ' ||
                                chunkData[p] == 'Y' && chunkData[p + 1] == 'o' && chunkData[p + 3] == ' ' ||
                                chunkData[p] == '2' && chunkData[p + 4] == '/' && chunkData[p + 7] == '/')
                            {
                                // This is the start of a text block
                                iTrailingTextLength++;
                            }
                        }
                        else
                        {

                            // Verify the rest is printable text
                            if ((testByte >= 0x20 && testByte <= 0x7e) || testByte == 0x0a)
                            {
                                iTrailingTextLength++;
                            }
                            else
                            {
                                // Not a match, reset counter to 0
                                iTrailingTextLength = 0;
                            }
                        }
                    }
                    fsInput.Close();
                }
            }
            catch
            {
                // Could not open file
            }
            return iTrailingTextLength;
        }

        public static string GetSHA1Sum(string fileName)
        {
            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Open))
                using (BufferedStream bs = new BufferedStream(fs))
                {
                    using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        byte[] hash = sha1.ComputeHash(bs);
                        StringBuilder formatted = new StringBuilder(2 * hash.Length);
                        foreach (byte b in hash)
                        {
                            formatted.AppendFormat("{0:x2}", b);
                        }
                        return formatted.ToString();
                    }
                }
            }
            catch
            {
                // Could not open file
            }
            return null;
        }

        public static string DecryptCCMPlatformValue(string hexEncodedCryptData, string dKeyValue, bool dKeyRaw)
        {
            byte[] staticKey = Functions.encoding.GetBytes("smetsysocsiccni");

            byte[] key = new byte[16];
            byte[] iv = new byte[16];

            byte[] encryptedData = Functions.HexStringToByteArray(hexEncodedCryptData);
            byte[] dataToDecrypt = null;

            bool bTruncateFirstBlock = false;

            if (dKeyValue == null)
            {
                // Use static key
                System.Buffer.BlockCopy(staticKey, 0, key, 0, staticKey.Length);
                System.Buffer.BlockCopy(staticKey, 0, iv, 0, staticKey.Length);
                dataToDecrypt = encryptedData;
                bTruncateFirstBlock = true;
            }
            else
            {
                // Use install specific dKeyValue
                if (dKeyRaw)
                {
                    key = Encoding.UTF8.GetBytes(dKeyValue);
                }
                else {
                    key = Functions.HexStringToByteArray(dKeyValue);
                }
                
                System.Buffer.BlockCopy(encryptedData, 0, iv, 0, 16);
                dataToDecrypt = new byte[encryptedData.Length - 16];
                System.Buffer.BlockCopy(encryptedData, 16, dataToDecrypt, 0, encryptedData.Length - 16);
            }

            RijndaelManaged cipher = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = key.Length * 8,
                BlockSize = iv.Length * 8,
                Key = key,
                IV = iv
            };

            byte[] plainText = null;
            String PlaintextString = "";

            try
            {
                plainText = cipher.CreateDecryptor().TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
                PlaintextString = Functions.encoding.GetString(plainText);
            }
            catch (Exception ex)
            {
                // Bad decrypt
            }

            if (bTruncateFirstBlock) {
                PlaintextString = PlaintextString.Substring(16, PlaintextString.Length - 16);
            }

            return PlaintextString;
        }

        public static platformConfigXML.PlatformData LoadPlatformConfigFile(string sBackupXMLPath)
        {
            platformConfigXML.PlatformData oPlatformConfig = null;

            // If the file doesn't exist, return
            if (!File.Exists(sBackupXMLPath))
            {
                return oPlatformConfig;
            }

            // File exists; let's read it
            XmlSerializer mySerializer = new XmlSerializer(typeof(platformConfigXML.PlatformData));
            string sBackupXMLText = File.ReadAllText(sBackupXMLPath);
            //FileStream myFileStream = new FileStream(sBackupXMLPath, FileMode.Open);
            //_oBackupSetDef = (drfComponentXML.list)mySerializer.Deserialize(myFileStream);
            using (TextReader reader = new StringReader(sBackupXMLText))
            {
                oPlatformConfig = (platformConfigXML.PlatformData)mySerializer.Deserialize(reader);
            }

            return oPlatformConfig;
        }

        public static platformConfigXML.PlatformData LoadPlatformConfigBytes(byte[] xmlFileBytes)
        {
            platformConfigXML.PlatformData oPlatformConfig = null;
            XmlSerializer mySerializer = new XmlSerializer(typeof(platformConfigXML.PlatformData));
            string sBackupXMLText = Encoding.UTF8.GetString(xmlFileBytes);
            using (TextReader reader = new StringReader(sBackupXMLText))
            {
                oPlatformConfig = (platformConfigXML.PlatformData)mySerializer.Deserialize(reader);
            }

            return oPlatformConfig;
        }

        public static string Guess(string sUnknownText)
        {
            // Let's see if we can guess the type of text using Regex
            //
            // PlatformConfig                     -> BEDA6472791869342394638D1305EC39831812812AB2825C831812812AB2825C
            // drfComponent EncryptKey            -> c6038dfe0dcaa26a8fd4220134985575de5949d824dd072b225bc2a9e946be43
            // drfComponent EncryptedSHA1CheckSum -> 329af12f76b56e612a201f06dd0343d746ac1efa2b20057879d27b8bd39f53980a7e94190efcad67b97adf62d79207a0
            // RemoteSupport v1                   -> BFHX9UBW92
            // RemoteSupport v2+                  -> BFHX9UBW9203

            // Is it a Component
            return null;
        }

    }

    public class JavaKeyHashConnector
    {
        public delegate void ConnectorReadyCallback();
        public delegate void ConnectorExitedCallback();
        public delegate void KeyHashCallback(string sKeyHash);
        private bool _bHasInitialized;
        private bool _bReady;
        private int _iExitCode;
        private string _sExitMessage;
        private string _sJavaExecutable = "java";
        private string _sJarFile = "KeyHashGenerator.jar";
        private ConnectorReadyCallback _fConnectorReadyCallback;
        private ConnectorExitedCallback _fConnectorExitedCallback;
        private KeyHashCallback _fKeyHashCallback;
        private Process _oJavaProcess;

        public ConnectorReadyCallback ConnectorReadyHandler
        {
            get { return _fConnectorReadyCallback; }
            set { _fConnectorReadyCallback = value; }
        }

        public ConnectorExitedCallback ConnectorExitedHandler
        {
            get { return _fConnectorExitedCallback; }
            set { _fConnectorExitedCallback = value; }
        }

        public KeyHashCallback ReceiveKeyHandler
        {
            get { return _fKeyHashCallback; }
            set { _fKeyHashCallback = value; }
        }

        public bool IsReady
        {
            get { return _bReady; }
            set { }
        }

        public bool IsClosed
        {
            get { return (_iExitCode > 0 || _sExitMessage != null); }
            set { }
        }

        public bool HasInitialized
        {
            get { return _bHasInitialized; }
            set { }
        }

        public string ErrorMsg
        {
            get { return _sExitMessage; }
            set { }
        }

        public JavaKeyHashConnector()
        {
            //OpenConnector();
        }

        public void Initialize()
        {
            _bHasInitialized = true;
            ManualResetEvent mreConnector = new ManualResetEvent(false);

            if (!File.Exists(_sJarFile))
            {
                _sExitMessage = "Cannot find JAR file: " + _sJarFile;
                return;
            }
            _oJavaProcess = new Process();
            _oJavaProcess.StartInfo.RedirectStandardError = true;
            _oJavaProcess.StartInfo.RedirectStandardOutput = true;
            _oJavaProcess.StartInfo.RedirectStandardInput = true;
            _oJavaProcess.StartInfo.UseShellExecute = false;
            _oJavaProcess.StartInfo.CreateNoWindow = true;
            _oJavaProcess.StartInfo.FileName = _sJavaExecutable;
            _oJavaProcess.StartInfo.Arguments = " -jar " + _sJarFile;
            //p.StartInfo.WorkingDirectory = @"P:\Development\Java\Cisco DRF Password Encryption";

            _oJavaProcess.OutputDataReceived += new DataReceivedEventHandler(
                (s, e) =>
                {
                    //Process javaProcess = (Process)s;
                    //Console.Write("JAVACONNECTOR RECV -> {0}\n", e.Data.Trim());
                    if (_oJavaProcess.HasExited)
                    {
                        _iExitCode = _oJavaProcess.ExitCode;
                        _sExitMessage = _oJavaProcess.StandardError.ReadToEnd();
                        return;
                    }
                    //Console.WriteLine(e.Data);
                    if (_bReady)
                    {
                        // Process incoming message
                        _fKeyHashCallback?.Invoke(e.Data.Trim());
                    }
                    else
                    {
                        if (e.Data.Equals("READY"))
                        {
                            _bReady = true;
                            mreConnector.Set();
                            _fConnectorReadyCallback?.Invoke();
                            //outputString = process.StandardOutput.ReadToEnd();
                        }
                        else
                        {
                            mreConnector.Set();
                            _oJavaProcess.Kill();
                            _sExitMessage = "Received unexpected data";
                        }
                    }
                }
            );
            _oJavaProcess.ErrorDataReceived += new DataReceivedEventHandler((s, e) => { }); //Console.WriteLine(e.Data); });

            _oJavaProcess.Exited += new EventHandler((s, e) =>
            {
                _bReady = false;
                mreConnector.Set();
                _fConnectorExitedCallback?.Invoke();
                /*
                if (_sExitMessage.Equals("Error: Unable to access jarfile " + _sJarFile + "\n"))
                {
                    Console.Write("Could not find JAR file: {0}\n", _sJarFile);
                }
                else
                {
                    Console.Write("Java exited with code {0} -> '{1}'\n", _iExitCode, _sExitMessage);
                }
                */
            });

            try
            {
                _oJavaProcess.Start();
                _oJavaProcess.BeginOutputReadLine();
                mreConnector.WaitOne();
            }
            catch (Exception ex)
            {
                // Could not run Java
                _sExitMessage = ex.Message;
            }
        }

        public void SendData(string sDataToSend)
        {
            _oJavaProcess.StandardInput.WriteLine(sDataToSend);
        }

    }

    public class PlatformConfigPassword
    {
        private int _keySize = 64;
        private byte[] _cryptKey = { 0xEF, 0x00, 0xFF, 0x02, 0xFB, 0x00, 0xFF, 0x42 };
        private CipherMode _desMode = CipherMode.ECB;
        private PaddingMode _paddingMode = PaddingMode.None;
        private DESCryptoServiceProvider _des = null;
        public bool Successful = false;
        public string ErrorMessage = null;
        public string PlaintextString;
        public string EncryptedHexString;

        public PlatformConfigPassword()
        {
            _des = new DESCryptoServiceProvider
            {
                KeySize = _keySize,
                Key = _cryptKey,
                Mode = _desMode,
                Padding = _paddingMode
            };
        }

        public string Encrypt(string sPlaintextPassword)
        {
            // Initialize variables
            byte[] uPassword = new byte[32];
            string hexEncPass = "";
            Successful = false;
            PlaintextString = sPlaintextPassword;

            // Copy plaintext bytes to byte array
            System.Buffer.BlockCopy(Functions.encoding.GetBytes(sPlaintextPassword), 0, uPassword, 0, sPlaintextPassword.Length);

            // Create encryptor
            ICryptoTransform ic = _des.CreateEncryptor();

            // Iterate 4 times, encrypt 8 bytes at a time
            for (int i = 0; i < 4; i++)
            {
                // Encrypt text
                byte[] encBytes = ic.TransformFinalBlock(uPassword, i * 8, 8);

                // Convert encrypted text to hex string
                for (int j = 0; j < encBytes.Length; j++)
                    hexEncPass += encBytes[j].ToString("X2");
            }
            Successful = true;
            EncryptedHexString = hexEncPass;
            return hexEncPass;
        }

        public string Decrypt(string sEncryptedPassword)
        {
            // Initialize variables
            string sPlaintextPassword = "";
            Successful = false;
            EncryptedHexString = sEncryptedPassword;

            // Convert hex string to byte array
            byte[] encBytes = Functions.HexStringToByteArray(sEncryptedPassword);

            // Create decryptor
            ICryptoTransform ic = _des.CreateDecryptor();

            // Iterate 4 times, encrypt 8 bytes at a time
            for (int i = 0; i < 4; i++)
            {
                // Decrypt block
                byte[] decBytes = ic.TransformFinalBlock(encBytes, i * 8, 8);
                sPlaintextPassword += Functions.encoding.GetString(decBytes);
            }
            sPlaintextPassword = sPlaintextPassword.Trim('\0');
            Successful = Functions.IsPrintableString(sPlaintextPassword);
            return sPlaintextPassword;
        }
    }
    public class DRFComponentValueCrypter
    {
        private byte[] _salt = { 0xA9, 0x87, 0xC8, 0x32, 0x56, 0xA5, 0xE3, 0xB2 };
        private RijndaelManaged _rijndaelCipher;
        public JavaKeyHashConnector KeyHashConnector = Functions.KeyHashConnector;
        public bool Successful = false;
        public string ErrorMessage = null;
        public string PlaintextString;
        public string EncryptedHexString;

        public DRFComponentValueCrypter(string sEncryptedClusterSecurityPassHex, int iHashType)
        {
            byte[] cipherBytes = null;
            int iKeySize = 0;
            int iCipherMode = 0;
            byte[] keyBytes = null;
            byte[] ivBytes = null;

            if (iHashType == 1)
            {
                iKeySize = 128;
                iCipherMode = (int)CipherMode.ECB;
                // Hashtype 1 = MD5 Digest until we get replacement for PBEWithHMACSHA1andDESede
                MD5 md5 = new MD5CryptoServiceProvider();

                // Could use LINQ for code readability, but we'll concatenate them this way for performance
                // hashBytes = passBytes + saltBytes
                byte[] passBytes = Functions.encoding.GetBytes(sEncryptedClusterSecurityPassHex);
                System.Buffer.BlockCopy(Encoding.UTF8.GetBytes(sEncryptedClusterSecurityPassHex), 0, passBytes, 0, passBytes.Length);
                byte[] hashBytes = new byte[passBytes.Length + _salt.Length];
                System.Buffer.BlockCopy(passBytes, 0, hashBytes, 0, passBytes.Length);
                System.Buffer.BlockCopy(_salt, 0, hashBytes, passBytes.Length, _salt.Length);

                // Iterate a total of 1024 times
                md5.ComputeHash(hashBytes);
                for (int q = 1; q < 1024; q++)
                {
                    md5.ComputeHash(md5.Hash);
                }

                // Final result goes to cipherBytes
                cipherBytes = md5.Hash;

                // Set Key & IV to the first 16 bytes of cipherBytes
                keyBytes = cipherBytes.Take(iKeySize / 8).ToArray();
                ivBytes = keyBytes;
            }
            else if (iHashType == 2)
            {
                if (KeyHashConnector == null || !KeyHashConnector.IsReady)
                {
                    return;
                }

                iKeySize = 192;

                iCipherMode = (int)CipherMode.CBC;

                ManualResetEvent mrePassHash = new ManualResetEvent(false);
                KeyHashConnector.ReceiveKeyHandler = delegate (string keyHash)
                {
                    //Console.Write("Received keyHash -> '{0}'\n", keyHash);
                    cipherBytes = Functions.HexStringToByteArray(keyHash);
                    mrePassHash.Set();
                };
                KeyHashConnector.SendData(sEncryptedClusterSecurityPassHex);
                mrePassHash.WaitOne();

                // Set Key & IV to the first x bytes of cipherBytes
                keyBytes = cipherBytes.Take(iKeySize / 8).ToArray();
                ivBytes = new byte[16];
            }
            else if (iHashType == 3)
            {
                iKeySize = 128;
                iCipherMode = (int)CipherMode.CBC;
                byte[] passBytes = Functions.encoding.GetBytes(sEncryptedClusterSecurityPassHex);
                cipherBytes = Functions.encoding.GetBytes(Functions.GetHexCharString(passBytes));
                keyBytes = cipherBytes.Take(iKeySize / 8).ToArray();
                ivBytes = new byte[16];
            }
            else if (iHashType == 4)
            {
                iKeySize = 128;
                iCipherMode = (int)CipherMode.CBC;
                byte[] passBytes = Functions.encoding.GetBytes(sEncryptedClusterSecurityPassHex);
                cipherBytes = Functions.encoding.GetBytes(Functions.GetHexCharString(passBytes));

                cipherBytes = PBKDF2Sha256GetBytes(iKeySize, passBytes, _salt, 1024);

                keyBytes = cipherBytes.Take(iKeySize / 8).ToArray();
                ivBytes = new byte[16];
            }

            // Create AES-128-CBC object
            _rijndaelCipher = new RijndaelManaged
            {
                Mode = (CipherMode)iCipherMode,
                Padding = PaddingMode.PKCS7,
                KeySize = iKeySize,
                BlockSize = 128
            };

            _rijndaelCipher.Key = keyBytes;
            _rijndaelCipher.IV = ivBytes;
        }

        public string Encrypt(string sPlaintextValue)
        {
            Successful = false;
            if (sPlaintextValue == null) return null;
            PlaintextString = sPlaintextValue;
            EncryptedHexString = "";
            byte[] plaintextData = Functions.encoding.GetBytes(sPlaintextValue);

            // Encrypt data
            byte[] encryptedData = _rijndaelCipher.CreateEncryptor().TransformFinalBlock(plaintextData, 0, plaintextData.Length);
            for (int j = 0; j < encryptedData.Length; j++)
                EncryptedHexString += encryptedData[j].ToString("x2");

            Successful = true;
            return EncryptedHexString;
        }
        public string Decrypt(string sEncryptedValueHex)
        {
            Successful = false;
            if (sEncryptedValueHex == null) return null;
            PlaintextString = "";
            EncryptedHexString = sEncryptedValueHex;

            byte[] encryptedData = Functions.HexStringToByteArray(sEncryptedValueHex);

            // Decrypt data
            try
            {
                byte[] plainText = _rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
                PlaintextString = Functions.GetHexCharString(plainText);
                if (PlaintextString.Length > 0)
                {
                    Successful = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return PlaintextString;
        }

        public static byte[] PBKDF2Sha256GetBytes(int dklen, byte[] password, byte[] salt, int iterationCount)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(password))
            {
                int hashLength = hmac.HashSize / 8;
                if ((hmac.HashSize & 7) != 0)
                    hashLength++;
                int keyLength = dklen / hashLength;
                if ((long)dklen > (0xFFFFFFFFL * hashLength) || dklen < 0)
                    throw new ArgumentOutOfRangeException("dklen");
                if (dklen % hashLength != 0)
                    keyLength++;
                byte[] extendedkey = new byte[salt.Length + 4];
                Buffer.BlockCopy(salt, 0, extendedkey, 0, salt.Length);
                using (var ms = new System.IO.MemoryStream())
                {
                    for (int i = 0; i < keyLength; i++)
                    {
                        extendedkey[salt.Length] = (byte)(((i + 1) >> 24) & 0xFF);
                        extendedkey[salt.Length + 1] = (byte)(((i + 1) >> 16) & 0xFF);
                        extendedkey[salt.Length + 2] = (byte)(((i + 1) >> 8) & 0xFF);
                        extendedkey[salt.Length + 3] = (byte)(((i + 1)) & 0xFF);
                        byte[] u = hmac.ComputeHash(extendedkey);
                        Array.Clear(extendedkey, salt.Length, 4);
                        byte[] f = u;
                        for (int j = 1; j < iterationCount; j++)
                        {
                            u = hmac.ComputeHash(u);
                            for (int k = 0; k < f.Length; k++)
                            {
                                f[k] ^= u[k];
                            }
                        }
                        ms.Write(f, 0, f.Length);
                        Array.Clear(u, 0, u.Length);
                        Array.Clear(f, 0, f.Length);
                    }
                    byte[] dk = new byte[dklen];
                    ms.Position = 0;
                    ms.Read(dk, 0, dklen);
                    ms.Position = 0;
                    for (long i = 0; i < ms.Length; i++)
                    {
                        ms.WriteByte(0);
                    }
                    Array.Clear(extendedkey, 0, extendedkey.Length);
                    return dk;
                }
            }
        }
    }

    public class TARFileObj
    {
        public bool _bFileExists;
        public bool _bVerifiedDecryptable;
        public bool _bValidated;
        public bool _bEncrypted;
        public bool _bTermEarly;
        public string _sFileName;
        public string _sFileDirectory;
        public string _sEncryptedSha1Sum;
        public string _sPlaintextSha1Sum;
        public string _sDecryptError;
        public string _sPlaintextBackupPass;
        public string _sEncryptedBackupPass;
        public string _sErrorMsg;
        public long _lFileSize;
        public long _lFileDecryptProgress;
        public int _iHashTypeTAR;
        public DRFComponentValueCrypter _oDRFComponentValueCrypter;
        public DRSBackupSet _oDRSBackupSet;
        public drfComponentXML.ComponentObject _oComponentObject;
        public BackgroundWorker _oBackgroundWorker;

        public TARFileObj(string sFileName, string sFileDirectory, DRSBackupSet oDRSBackupSet, drfComponentXML.ComponentObject oComponentObject)
        {
            _sFileName = sFileName;
            _sFileDirectory = sFileDirectory;
            _oDRSBackupSet = oDRSBackupSet;
            _oDRFComponentValueCrypter = oDRSBackupSet._oComponentValueCrypter;
            _oComponentObject = oComponentObject;
            _sPlaintextBackupPass = oDRSBackupSet._sRandomBackupPass;
            _iHashTypeTAR = _oDRSBackupSet._iHashTypeTAR;

            Validate();
        }

        public TARFileObj(string sFileName, string sFileDirectory, string sPlaintextBackupPass, int iHashTypeTAR)
        {
            _sFileName = sFileName;
            _sFileDirectory = sFileDirectory;
            _sPlaintextBackupPass = sPlaintextBackupPass;
            _iHashTypeTAR = iHashTypeTAR;

            Validate();
        }

        public void Validate()
        {
            if (File.Exists(FullFilePath))
            {
                _bFileExists = true;
                _lFileSize = new FileInfo(FullFilePath).Length;
                _bEncrypted = CheckEncrypted();
                if (_bEncrypted)
                {
                    _bVerifiedDecryptable = VerifyDecryptable(_sPlaintextBackupPass, _iHashTypeTAR);
                }
            }
            _bValidated = true;

            Console.Write("exists [{2}]\tisDecryptable [{1}]\t{0}\n", _sFileName, _bVerifiedDecryptable, _bFileExists);
        }

        public string FullFilePath
        {
            get { return _sFileDirectory + @"\" + _sFileName; }
            set { }
        }

        public void AssignToSet(DRSBackupSet _oDRSBackupSet)
        {
            _bVerifiedDecryptable = VerifyDecryptable(_oDRSBackupSet._sRandomBackupPass, _oDRSBackupSet._iHashTypeTAR);
        }

        public void GetHash()
        {
            string sSHA1Sum = Functions.GetSHA1Sum(FullFilePath);
            string _sEncryptedHash = _oDRFComponentValueCrypter.Encrypt(sSHA1Sum);
        }

        public bool CheckEncrypted()
        {
            bool isEncrypted = false;
            try
            {
                using (FileStream fsInput = File.OpenRead(FullFilePath))
                {
                    // First 8 bytes should be "Salted__"
                    byte[] header = new byte[8];
                    fsInput.Read(header, 0, 8);
                    if (Functions.encoding.GetString(header).Equals("Salted__"))
                    {
                        isEncrypted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Could not read file
            }
            return isEncrypted;
        }

        public bool VerifyDecryptable(string sPassword, int iHashType)
        {
            return RunDecrypt(null, sPassword, iHashType, true);
        }

        public bool Decrypt(string decryptedFileName, string sPassword, int iHashType)
        {
            return RunDecrypt(decryptedFileName, sPassword, iHashType, false);
        }

        public bool Decrypt(string decryptedFileName, string sPassword, int iHashType, BackgroundWorker worker)
        {
            _oBackgroundWorker = worker;
            return RunDecrypt(decryptedFileName, sPassword, iHashType, false);
        }

        private bool RunDecrypt(string decryptedFileName, string sPassword, int iHashType, bool checkOnly)
        {
            if (sPassword == null || sPassword.Length != 20)
            {
                _sErrorMsg = "Must supply a 20 char password to decrypt";
                return false;
            }

            byte[] password = new byte[sPassword.Length];
            System.Buffer.BlockCopy(Encoding.UTF8.GetBytes(sPassword), 0, password, 0, password.Length);
            bool encounteredError = false;

            // Get file size
            long lTarFileSize = new FileInfo(FullFilePath).Length;

            // Get trailing text length
            int iTrailingTextLength = Functions.GetTrailingTextLength(FullFilePath);

            long lCalculatedEncryptedLength = lTarFileSize - 16 - iTrailingTextLength;

            if (iTrailingTextLength > 0)
            {
                //Console.Write("File '{0}' has [{1}] bytes of trailing text, calculated encrypted size of [{2}] bytes\n", FullFilePath, iTrailingTextLength, lCalculatedEncryptedLength);
            }

            try
            {
                using (FileStream fsInput = File.OpenRead(FullFilePath))
                {
                    long bytesReadTotal = 0;
                    int cycleCount = 0;

                    // First 8 bytes should be "Salted__"
                    byte[] header = new byte[8];
                    fsInput.Read(header, 0, 8);
                    if (!(Functions.encoding.GetString(header).Equals("Salted__")))
                    {
                        _sDecryptError = "Encrypted file does not begin with 'Salted__'";
                        return false;
                    }

                    // Next 8 bytes are the Salt
                    byte[] salt = new byte[8];
                    fsInput.Read(salt, 0, 8);

                    // Create TAR file decrypter
                    TARFileCrypt tarCrypt = new TARFileCrypt(salt, iHashType, password);

                    if (checkOnly)
                    {
                        // If running a checkOnly, we decrypt the first 128 bytes of the TAR file and look for
                        // a filename in the first 100 bytes
                        using (MemoryStream checkStream = new MemoryStream())
                        {
                            try
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(checkStream, tarCrypt.Decrypter, CryptoStreamMode.Write))
                                {
                                    int bytesRead = 0;
                                    int chunkSize = 16;
                                    for (int y = 0; y < 8; y++)
                                    {
                                        byte[] chunkData = new byte[chunkSize];
                                        bytesRead = fsInput.Read(chunkData, 0, chunkSize);
                                        cryptoStream.Write(chunkData, 0, bytesRead);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // We are going to hit this due to closing the cryptostream
                                // prior to reaching the padding at the end of the file.
                                long streamLength = checkStream.Length;
                                if (streamLength > 100)
                                    streamLength = 100;
                                byte[] streamContents = new byte[streamLength];
                                checkStream.Seek(0, SeekOrigin.Begin);
                                checkStream.Read(streamContents, 0, (int)streamLength);

                                // We will look for one of two conditions.  If it's an uncompressed TAR,
                                // the first 100 bytes will be a filename.  If it's a GZ, the first two
                                // bytes of data will be [1F 8B].  The TFTP file does this.

                                // Check for a filename in the TAR header
                                string pathRegexPattern = @"\/[\/\-_.A-Za-z0-9]+\x00+$";
                                string checkString = Functions.encoding.GetString(streamContents);
                                Regex pathRegex = new Regex(pathRegexPattern);
                                if (pathRegex.IsMatch(checkString)) return true;

                                // No filename; let's check for a GZ magic number
                                if (streamContents[0] == 0x1F && streamContents[1] == 0x8B) return true;

                                // Neither case applied.  Can't validate the decrypt.
                                //string thisString = Functions.encoding.GetString(streamContents);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (decryptedFileName == null || decryptedFileName.Length < 1)
                        {
                            _sErrorMsg = "Decrypt file target not specified";
                            return false;
                        }

                        // Run a normal full decrypt
                        using (FileStream fsOutput = File.OpenWrite(decryptedFileName))
                        {

                            try
                            {
                                using (CryptoStream cryptoStream = new CryptoStream(fsOutput, tarCrypt.Decrypter, CryptoStreamMode.Write))
                                {
                                    int chunkSize = 16;

                                    long lTotalChunkCount = lCalculatedEncryptedLength / chunkSize;

                                    byte[] chunkData = new byte[chunkSize];
                                    int bytesRead = 0;
                                    for (long lProcessedChunkCount = 0; lProcessedChunkCount < lTotalChunkCount; lProcessedChunkCount++)
                                    {
                                        bytesRead = fsInput.Read(chunkData, 0, chunkSize);

                                        cryptoStream.Write(chunkData, 0, bytesRead);

                                        bytesReadTotal = bytesReadTotal + bytesRead;

                                        cycleCount++;
                                        if (cycleCount == 100000)
                                        {
                                            cycleCount = 0;
                                            _lFileDecryptProgress = bytesReadTotal;
                                            if (_oBackgroundWorker != null)
                                            {
                                                int progress = (int)(_lFileDecryptProgress * 100 / _lFileSize);
                                                _oBackgroundWorker.ReportProgress(progress);
                                                if (_bTermEarly)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                if (!_bTermEarly)
                                {
                                    _sDecryptError = "Error decrypting: " + ex.Message;
                                }
                                else
                                {
                                    _sDecryptError = "Decryption cancelled";
                                }

                                encounteredError = true;
                                _sDecryptError = ex.Message;
                            }
                            fsOutput.Close();
                        }
                    }
                    fsInput.Close();
                }
            }
            catch (Exception ex)
            {
                encounteredError = true;
                _sDecryptError = ex.Message;
            }
            return !encounteredError;
        }
    }

    public class RemoteSupportPassphrase
    {
        public bool Successful = false;
        public string ErrorMessage = null;
        public string Passphrase;
        public string Password;
        private string[] _magicStrings = {
                "",
                "T#A@O!S# *D#e?c>o<d*e~r% )f(o9r#&xpa*s&s$p%h^rase",
                "O3+v1aVjYGjy0VMLEqrx0GfgdhdoXdee+wJBAL4FIqwmzQeVngwEswSIwKguS3ac",
                "GTGK+/UNjnBxejpm2aiwz3EvESgQNrhSqoEwARF+cPcLfLHBKX9TXniD0vkD+LV"
            };
        private string[] _ivStrings = {
                "",
                "s&o)m9e1 #v*e%r0",
                "VC7Nb255puGE7vC1",
                "tcYuu+IDtwj6xDG5"
            };

        public string Decode(string sPassphrase)
        {
            int decoderVersion = 0;
            int iPassphraseLen = 10;
            Passphrase = sPassphrase;
            Password = null;

            // Figure out decoder version
            int iPassphraseTotalLen = sPassphrase.Length;
            if (iPassphraseTotalLen != 0x0c)
            {
                if (iPassphraseTotalLen == 0x0a)
                {
                    // Length 10 -> Version 1
                    decoderVersion = 1;
                }
                else
                {
                    ErrorMessage = "Passphrase must be 10 or 12 characters";
                    return null;
                }
            }
            else
            {
                // Length 12 -> Version 2/3
                decoderVersion = Convert.ToInt16(sPassphrase.Substring(10));
                if (decoderVersion < 2 || decoderVersion > 3)
                {
                    ErrorMessage = "Unknown decoder version";
                    return null;
                }
            }

            // Get the actual passphrase portion of the string (excluding version)
            byte[] passphraseBytes = Functions.encoding.GetBytes(sPassphrase.Substring(0, 10));

            // Create string to store fuzzed data
            byte[] partOneFuzzed = new byte[iPassphraseLen];

            // Fuzz and encode passphrase
            FuzzString(passphraseBytes, partOneFuzzed, false, iPassphraseLen);
            int partOneEncodedLength = ((iPassphraseLen + 3 - 1) / 3) * 4;
            string partOneEncoded = Convert.ToBase64String(partOneFuzzed);

            // Concatenate fuzzed/encoded passphrase with magic string
            int partTwoLength = partOneEncodedLength + _magicStrings[decoderVersion].Length;
            string partTwo = String.Concat(partOneEncoded + _magicStrings[decoderVersion]);

            // Add a byte denoting encoder version
            string partTwoFull = partTwo + (char)(0x10 + decoderVersion);

            // Fuzz and encode combined string
            byte[] partTwoFuzzed = new byte[partTwoLength + 1];
            FuzzString(Functions.encoding.GetBytes(partTwoFull), partTwoFuzzed, true, partTwoLength + 1);
            string partTwoEncoded = Convert.ToBase64String(partTwoFuzzed);

            // Create digest and encode
            HMACSHA1 myHmac = new HMACSHA1(Functions.encoding.GetBytes(_ivStrings[decoderVersion]));
            myHmac.Initialize();
            byte[] rawDigest = myHmac.ComputeHash(Functions.encoding.GetBytes(partTwoEncoded));
            string encodedDigest = Convert.ToBase64String(rawDigest.Take(16).ToArray());

            // Get cipherKey and IV
            byte[] cipherKey = Functions.encoding.GetBytes(encodedDigest).Take(16).ToArray();
            byte[] cipherIV = Functions.encoding.GetBytes(_ivStrings[decoderVersion]);

            // Run encrypt
            byte[] encryptedText = RunEncrypt(Functions.encoding.GetBytes(partTwoEncoded), cipherKey, cipherIV, decoderVersion);

            // Convert encrypted data to Base64 
            string encodedFinal = Convert.ToBase64String(encryptedText);
            byte[] encodedFinalBytes = Functions.encoding.GetBytes(encodedFinal);

            // Sanitize string
            SanitizeString(encodedFinalBytes);

            // Return 10 characters starting at position 14
            Password = Functions.encoding.GetString(encodedFinalBytes).Substring(0xe, 10);
            Successful = true;

            return Password;
        }

        private byte[] RunEncrypt(byte[] dataToEncrypt, byte[] cipherKey, byte[] cipherIV, int decodeVersion)
        {
            byte[] encryptedText = null;

            if (decodeVersion <= 2)
            {
                BlowFishCS.BlowFish myBF = new BlowFishCS.BlowFish(cipherKey);
                myBF.IV = cipherIV.Take(8).ToArray();
                encryptedText = myBF.Encrypt_CBC(dataToEncrypt);
            }
            else if (decodeVersion == 3)
            {
                // AES-128-CBC
                using (RijndaelManaged myRijndael = new RijndaelManaged())
                {
                    myRijndael.Key = cipherKey;
                    myRijndael.IV = cipherIV;
                    // Encrypt the string to an array of bytes. 
                    ICryptoTransform encryptor = myRijndael.CreateEncryptor(myRijndael.Key, myRijndael.IV);

                    // Create the streams used for encryption. 
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                //Write all data to the stream.
                                swEncrypt.Write(Functions.encoding.GetString(dataToEncrypt));
                            }
                            encryptedText = msEncrypt.ToArray();
                        }
                    }
                }
            }
            else
            {
                // Unknown version
            }
            return encryptedText;
        }

        void SanitizeString(byte[] dirtyString)
        {
            for (int byteCount = 0xe; byteCount <= 0x17; byteCount++)
            {
                byte currentByte = dirtyString[byteCount];
                if (currentByte == 0x4f)
                {
                    dirtyString[byteCount] = 0x38;
                }
                else if (currentByte == 0x6f)
                {
                    dirtyString[byteCount] = 0x53;
                }
                else if (currentByte == 0x2b)
                {
                    dirtyString[byteCount] = 0x4a;
                }
                else if (currentByte == 0x2f)
                {
                    dirtyString[byteCount] = 0x50;
                }
                else if (currentByte >= 0x61 && currentByte <= 0x7a)
                {
                    dirtyString[byteCount] -= 0x20;
                }
            }
        }

        int FuzzString(byte[] stringToFuzz, byte[] fuzzedString, bool addFour, int stringLen)
        {
            for (int i = 0x0; i < stringLen; i++)
            {
                fuzzedString[i] = (byte)((stringToFuzz[i] * 8) & 0xFF);
                if (addFour)
                {
                    fuzzedString[i] += 4;
                }
            }
            return 0;
        }
    }

    public class DRSBackupSet
    {
        public int _iHashTypeTAR;
        public int _iHashTypeXML;
        public string _sBackupDirectory;
        public string _sBackupXMLFile;
        public string _sBackupXMLText;
        public string _sRestoreDirectory;
        public string _sBackupTimestamp;
        public string _sClusterSecurityPw;
        public string _sClusterSecurityPwCrypt;
        public string _sRandomBackupPass;
        public string _sRandomBackupPassCrypt;
        public string _sDecryptError;
        public string _sErrorMsg;
        public bool _bAllFilesReadyForDecrypt;
        public bool _bTARFilesVerified;
        public DRFComponentValueCrypter _oComponentValueCrypter;
        public drfComponentXML.list _oBackupSetDef;
        public JavaKeyHashConnector KeyHashConnector = Functions.KeyHashConnector;
        public Dictionary<String, TARFileObj> _dTARFiles = new Dictionary<string, TARFileObj>();

        public bool IsEncrypted
        {
            get { return (_iHashTypeTAR > 0); }
            set { }
        }

        public bool HaveClusterSecurityPw
        {
            get { return (_sClusterSecurityPwCrypt != null); }
            set { }
        }

        public bool HaveRandomBackupPass
        {
            get { return (_sRandomBackupPass != null); }
            set { }
        }

        public bool XMLLoaded
        {
            get { return (_oBackupSetDef != null); }
            set { }
        }

        public string ErrorMsg
        {
            get { return _sErrorMsg; }
            set { }
        }

        public DRSBackupSet(string sBackupXMLPath)
        {
            LoadDRFComponentFile(sBackupXMLPath);
        }
        void LoadDRFComponentFile(string sBackupXMLPath)
        {
            // If the file doesn't exist, return
            if (!File.Exists(sBackupXMLPath))
            {
                _sErrorMsg = "XML file not found: " + sBackupXMLPath;
                return;
            }

            // File exists; let's read it
            XmlSerializer mySerializer = new XmlSerializer(typeof(drfComponentXML.list));
            _sBackupXMLText = File.ReadAllText(sBackupXMLPath);
            //FileStream myFileStream = new FileStream(sBackupXMLPath, FileMode.Open);
            //_oBackupSetDef = (drfComponentXML.list)mySerializer.Deserialize(myFileStream);
            using (TextReader reader = new StringReader(_sBackupXMLText))
            {
                _oBackupSetDef = (drfComponentXML.list)mySerializer.Deserialize(reader);
            }

            // Set paths, timestamp and HashType
            _sBackupDirectory = Path.GetDirectoryName(sBackupXMLPath);
            _sBackupXMLFile = Path.GetFileName(sBackupXMLPath);
            _sBackupTimestamp = _sBackupXMLFile.Substring(0, 19);
            _iHashTypeTAR = GetTarCryptHashTypeFromUCVersion(_oBackupSetDef.FeatureObjects.First().Version);
            _iHashTypeXML = GetXmlCryptHashTypeFromUCVersion(_oBackupSetDef.FeatureObjects.First().Version);

            // See if we can get a random password (older UC versions only)
            //string randomPass = Functions.GetRandomPasswordFromFile(fileName);

            if (IsEncrypted)
            {
                // Load new connector if we're dealing with an 11.5 set
                if (_iHashTypeXML == 2)
                {

                    // Has it initialized?  If not, start it and wait for a response
                    if (!KeyHashConnector.HasInitialized)
                    {
                        KeyHashConnector.Initialize();
                    }

                    if (!KeyHashConnector.IsReady)
                    {
                        // The Java KeyHashConnector isn't ready.  Is it closed?
                        if (KeyHashConnector.IsClosed)
                        {
                            // Closed
                            //Console.Write("KeyHashConnector is closed... [{0}]\n", KeyHashConnector.ErrorMsg);
                        }
                        else
                        {
                            // Unknown condition
                            //Console.Write("KeyHashConnector has error... [{0}]\n", KeyHashConnector.ErrorMsg);
                        }
                    }
                }

                // Get the product feature key
                string[] allowedProducts = new string[] { "CUC", "UCM", "UCCX" };
                drfComponentXML.FeatureObject featureObj = _oBackupSetDef.FeatureObjects.Where(f => allowedProducts.Contains(f.FeatureName)).First();

                // Get the first host - doesn't matter which since all have _CLM files
                drfComponentXML.ServerObj serverObj = featureObj.vServerObject.First();

                // Get the encrypted random backup password
                _sRandomBackupPassCrypt = serverObj.vComponentObject.First().EncryptKey;

                // Get the object
                string drsTARFileName = _sBackupTimestamp + "_" + serverObj.ServerName + "_" + featureObj.FeatureName + "_CLM.tar";
                string randomPass = Functions.GetRandomPasswordFromFile(_sBackupDirectory + @"\" + drsTARFileName);

                // If we found a random password, store it
                if (randomPass != null)
                {
                    _sRandomBackupPass = randomPass;
                }
                else
                {
                    // See if it's in the TCT
                    drsTARFileName = _sBackupTimestamp + "_" + serverObj.ServerName + "_" + featureObj.FeatureName + "_SLM.tar";
                    randomPass = Functions.GetRandomPasswordFromFile(_sBackupDirectory + @"\" + drsTARFileName);

                    // If we found a random password, store it
                    if (randomPass != null)
                    {
                        _sRandomBackupPass = randomPass;
                    }
                }
            }
        }

        public void VerifyTarFiles()
        {
            _bAllFilesReadyForDecrypt = true;
            foreach (drfComponentXML.FeatureObject featureObj in _oBackupSetDef.FeatureObjects)
            {
                string featureName = featureObj.FeatureName;
                foreach (drfComponentXML.ServerObj serverObj in featureObj.vServerObject)
                {
                    foreach (drfComponentXML.ComponentObject componentObj in serverObj.vComponentObject)
                    {
                        string drsTARFileName = _sBackupTimestamp + "_" + serverObj.ServerName + "_" + featureObj.FeatureName + "_" + componentObj.ComponentName + ".tar";
                        _dTARFiles[drsTARFileName] = new TARFileObj(drsTARFileName, _sBackupDirectory, this, componentObj);
                        if (!_dTARFiles[drsTARFileName]._bFileExists || (_dTARFiles[drsTARFileName]._bEncrypted && !_dTARFiles[drsTARFileName]._bVerifiedDecryptable))
                        {
                            _bAllFilesReadyForDecrypt = false;
                        }
                    }
                }
            }
        }

        public void DecryptAllTarFiles(string sOutputDirectory)
        {
            if (!_bAllFilesReadyForDecrypt)
            {
                Console.Write("All files are not ready for decryption!\n");
                return;
            }

            if (!Directory.Exists(sOutputDirectory))
            {
                Console.Write("Target directory does not exist! -> '{0}'\n", sOutputDirectory);
                return;
            }
            foreach (KeyValuePair<string, TARFileObj> oTARRef in _dTARFiles)
            {
                // Decrypt the <EncryptedSHA1CheckSum> value
                TARFileObj oTarFileObj = oTARRef.Value;
                string sDecryptToFilePath = sOutputDirectory + @"\" + oTarFileObj._sFileName;
                oTARRef.Value.Decrypt(sDecryptToFilePath, oTarFileObj._sPlaintextBackupPass, oTarFileObj._iHashTypeTAR);
            }
        }

        public bool SetPassword(string sClusterSecurityPw)
        {
            if (!XMLLoaded)
            {
                _sErrorMsg = "Tried to set password before loading drfComponent.xml";
                return false;
            }

            // Encrypt cluster security password
            string sClusterSecurityPwCrypt = new PlatformConfigPassword().Encrypt(sClusterSecurityPw);

            // Create DRFComponentValue decrypter
            DRFComponentValueCrypter componentValObj = null;

            // Loop over both hash types, see which one matches
            componentValObj = new DRFComponentValueCrypter(sClusterSecurityPwCrypt, _iHashTypeXML);
            componentValObj.Decrypt(_sRandomBackupPassCrypt);
            if (componentValObj.Successful)
            {
                _sClusterSecurityPw = sClusterSecurityPw;
                _sClusterSecurityPwCrypt = sClusterSecurityPwCrypt;
                _sRandomBackupPass = componentValObj.PlaintextString;
                _oComponentValueCrypter = componentValObj;
            }
            return componentValObj.Successful;
        }

        public int GetTarCryptHashTypeFromUCVersion(string sUCVersion)
        {
            int iHashType = 0;

            string sUCMVersionMajor = sUCVersion.Substring(0, 3);

            if (String.Equals(sUCMVersionMajor, "8.0")) iHashType = 1;
            else if (String.Equals(sUCMVersionMajor, "8.5")) iHashType = 1;
            else if (String.Equals(sUCMVersionMajor, "8.6")) iHashType = 2;
            else if (String.Equals(sUCMVersionMajor, "9.0")) iHashType = 2;
            else if (String.Equals(sUCMVersionMajor, "9.1")) iHashType = 2;
            else if (String.Equals(sUCMVersionMajor, "10.")) iHashType = 2;
            else if (String.Equals(sUCMVersionMajor, "11.")) iHashType = 2;
            else if (String.Equals(sUCMVersionMajor, "12.")) iHashType = 2;
            return iHashType;
        }

        public int GetXmlCryptHashTypeFromUCVersion(string sUCVersion)
        {
            int iHashType = 0;

            string sUCMVersionMajor = sUCVersion.Substring(0, 3);

            if (String.Equals(sUCMVersionMajor, "8.0")) iHashType = 1;
            else if (String.Equals(sUCMVersionMajor, "8.5")) iHashType = 1;
            else if (String.Equals(sUCMVersionMajor, "8.6")) iHashType = 1;
            else if (String.Equals(sUCMVersionMajor, "9.0")) iHashType = 1;
            else if (String.Equals(sUCMVersionMajor, "9.1")) iHashType = 1;
            else if (String.Equals(sUCMVersionMajor, "10.")) iHashType = 1;

            sUCMVersionMajor = sUCVersion.Substring(0, 4);
            if (String.Equals(sUCMVersionMajor, "11.0")) iHashType = 2;
            else if (String.Equals(sUCMVersionMajor, "11.5")) iHashType = 3;
            else if (String.Equals(sUCMVersionMajor, "12.0")) iHashType = 4;

            return iHashType;
        }

        public void ListFiles()
        {
            // Create DRFComponentValue Object for encrypt/decrypt
            foreach (drfComponentXML.FeatureObject featureObj in _oBackupSetDef.FeatureObjects)
            {
                string featureName = featureObj.FeatureName;
                foreach (drfComponentXML.ServerObj serverObj in featureObj.vServerObject)
                {
                    foreach (drfComponentXML.ComponentObject componentObj in serverObj.vComponentObject)
                    {
                        string drsTARFile = _sBackupTimestamp + "_" + serverObj.ServerName + "_" + featureObj.FeatureName + "_" + componentObj.ComponentName + ".tar";
                        string drsTARFilePath = _sBackupDirectory + "\\" + drsTARFile;
                        if (File.Exists(drsTARFilePath))
                        {
                            // Let's get the SHA1Sum and encrypt it to compare to the XML
                            bool hashGood = false;
                            if (HaveClusterSecurityPw)
                            {
                                string sSHA1Sum = Functions.GetSHA1Sum(drsTARFilePath);
                                string sSHA1SumCrypt = _oComponentValueCrypter.Encrypt(sSHA1Sum);
                                hashGood = sSHA1SumCrypt.Equals(componentObj.EncryptedSHA1CheckSum);
                            }

                            Console.Write(" O {0} [{1}]\n", drsTARFile, hashGood);
                        }
                        else
                        {
                            Console.Write(" X {0}\n", drsTARFile);
                        }
                    }
                }
            }
            return;
        }

        public bool ReadyToUpdateXMLSecurityPassword()
        {
            // Verify we have the random backup password
            if (!HaveRandomBackupPass)
            {
                _sErrorMsg = "Missing random backup password";
                return false;
            }

            if (!HaveClusterSecurityPw)
            {
                // Verify that all XMLs can be decrypted; this tells us our random backup password is valid
                if (!_bAllFilesReadyForDecrypt)
                {
                    _sErrorMsg = "When missing the cluster security password, all files in the backup set must be present for sha1 hash calculation";
                    return false;
                }
            }

            return true;
        }

        public bool UpdateXMLSecurityPassword(string sPassword, string sNewXMLFilePath)
        {
            // Verify that our password meets complexity requirements
            if (sPassword == null || sPassword.Length < 10 || sPassword.Length > 20)
            {
                _sErrorMsg = "Password must be between 10 and 20 characters";
                return false;
            }

            // Replace all EncryptKey instances with new ones 
            String sNewClusterSecurityPwCrypt = new PlatformConfigPassword().Encrypt(sPassword);
            String sOldRandomBackupPassCrypt = System.String.Copy(_sRandomBackupPassCrypt);
            String sNewRandomBackupPassCrypt = new DRFComponentValueCrypter(sNewClusterSecurityPwCrypt, _iHashTypeXML).Encrypt(_sRandomBackupPass);
            String sNewBackupXMLText = System.String.Copy(_sBackupXMLText);
            sNewBackupXMLText = sNewBackupXMLText.Replace(sOldRandomBackupPassCrypt, sNewRandomBackupPassCrypt);

            // Create a new ComponentValue encrypter with new password
            DRFComponentValueCrypter newCrypter = new DRFComponentValueCrypter(sNewClusterSecurityPwCrypt, _iHashTypeXML);

            if (HaveClusterSecurityPw)
            {

                // We have the current cluster security password, so we can decrypt
                // the current sha1sum and re-encrypt without the files being present

                // Iterate over each file...
                foreach (KeyValuePair<string, TARFileObj> oTARRef in _dTARFiles)
                {
                    // Decrypt the <EncryptedSHA1CheckSum> value
                    string sSHA1CheckSumCryptOld = oTARRef.Value._oComponentObject.EncryptedSHA1CheckSum;
                    string sSHA1CheckSum = _oComponentValueCrypter.Decrypt(sSHA1CheckSumCryptOld);
                    string sSHA1CheckSumCryptNew = newCrypter.Encrypt(sSHA1CheckSum);
                    sNewBackupXMLText = sNewBackupXMLText.Replace(sSHA1CheckSumCryptOld, sSHA1CheckSumCryptNew);
                }
            }
            else
            {

                // We don't have the current cluster security password, so
                // we have to get the sha1sum for each file

                // Iterate over each file...
                foreach (KeyValuePair<string, TARFileObj> oTARRef in _dTARFiles)
                {
                    // Get current sha1sum for replacement
                    string sSHA1CheckSumCryptOld = oTARRef.Value._oComponentObject.EncryptedSHA1CheckSum;
                    // Calculate sha1sum
                    string sSHA1CheckSum = Functions.GetSHA1Sum(oTARRef.Value.FullFilePath);
                    // Encrypt
                    string sSHA1CheckSumCryptNew = newCrypter.Encrypt(sSHA1CheckSum);
                    // Replace instance with new value
                    sNewBackupXMLText = sNewBackupXMLText.Replace(sSHA1CheckSumCryptOld, sSHA1CheckSumCryptNew);
                }

            }

            // Debug
            //Console.Write("\n{0}\n", _sBackupXMLText);
            //Console.Write("\n{0}\n", sNewBackupXMLText);

            // Write new xml to new file
            File.WriteAllText(sNewXMLFilePath, sNewBackupXMLText);

            return true;
        }
    }
    public class TARFileCrypt
    {

        private RijndaelManaged _cipher;
        private ICryptoTransform _decrypter;

        public ICryptoTransform Decrypter
        {
            get { return _decrypter; }
            set { }
        }
        public TARFileCrypt(byte[] salt, int iHashType, byte[] password)
        {


            byte[] finalKey = new byte[16];
            byte[] finalIV = new byte[16];

            if (iHashType == 1)
            {
                // 8.0 to 8.5
                MD5 md5 = MD5.Create();

                int preKeyLength = password.Length + salt.Length;
                byte[] preKey = new byte[preKeyLength];

                Buffer.BlockCopy(password, 0, preKey, 0, password.Length);
                Buffer.BlockCopy(salt, 0, preKey, password.Length, salt.Length);

                finalKey = md5.ComputeHash(preKey);

                int preIVLength = finalKey.Length + preKeyLength;
                byte[] preIV = new byte[preIVLength];

                Buffer.BlockCopy(finalKey, 0, preIV, 0, finalKey.Length);
                Buffer.BlockCopy(preKey, 0, preIV, finalKey.Length, preKey.Length);

                finalIV = md5.ComputeHash(preIV);

                md5.Clear();
                md5 = null;
            }
            else if (iHashType == 2)
            {
                // 8.6 to 11.5
                SHA1Managed sha1 = new SHA1Managed();

                int preKeyLength = password.Length + salt.Length;

                byte[] someBytes = new byte[preKeyLength];
                System.Buffer.BlockCopy(password, 0, someBytes, 0, password.Length);
                System.Buffer.BlockCopy(salt, 0, someBytes, password.Length, salt.Length);

                byte[] keyBytes = sha1.ComputeHash(someBytes);

                byte[] someMoreBytes = new byte[keyBytes.Length + preKeyLength];
                System.Buffer.BlockCopy(keyBytes, 0, someMoreBytes, 0, keyBytes.Length);
                System.Buffer.BlockCopy(someBytes, 0, someMoreBytes, keyBytes.Length, someBytes.Length);

                byte[] ivBytes = sha1.ComputeHash(someMoreBytes);

                System.Buffer.BlockCopy(keyBytes, 0, finalKey, 0, 16);
                System.Buffer.BlockCopy(keyBytes, 16, finalIV, 0, 4);
                System.Buffer.BlockCopy(ivBytes, 0, finalIV, 4, 12);

                sha1.Clear();
                sha1 = null;
            }
            else return;

            _cipher = new RijndaelManaged
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = finalKey,
                IV = finalIV
            };

            _decrypter = _cipher.CreateDecryptor();
        }
    }

    public class UCOSClientSSH
    {
        string sHostName;
        string sUserName;
        string sUserPassword;
        bool Validated = false;

        Renci.SshNet.ConnectionInfo sshConnInfo;

        public UCOSClientSSH(string hostName, string userName, string userPassword)
        {
            sHostName = hostName;
            sUserName = userName;
            sUserPassword = userPassword;
            Renci.SshNet.AuthenticationMethod authMethod = new Renci.SshNet.PasswordAuthenticationMethod(sUserName, sUserPassword);
            sshConnInfo = new Renci.SshNet.ConnectionInfo(sHostName, sUserName, new[] { authMethod });
        }

        public void Validate()
        {
            Renci.SshNet.SftpClient sftpClient = new Renci.SshNet.SftpClient(sshConnInfo);
            sftpClient.Connect();
            Validated = true;
            sftpClient.Disconnect();
        }

        public Dictionary<string, byte[]> GetFilePack(String[] sFileNames)
        {
            Renci.SshNet.SftpClient sftpClient = new Renci.SshNet.SftpClient(sshConnInfo);

            Dictionary<String, byte[]> oFilePack = new Dictionary<String, byte[]>();

            sftpClient.Connect();
            try
            {
                foreach (String sFileName in sFileNames)
                {
                    MemoryStream memStream = new MemoryStream();

                    sftpClient.DownloadFile(sFileName, memStream);

                    byte[] fileBytes = new byte[memStream.Length];
                    memStream.Seek(0, SeekOrigin.Begin);
                    memStream.Read(fileBytes, 0, (int)memStream.Length);
                    oFilePack[sFileName] = fileBytes;
                    //Console.Write("{0}\n", Functions.encoding.GetString(xmlDataBytes));

                }
            }
            catch (Exception ex)
            {
                //sftpClient.Disconnect();
                //throw ex;
            }
            sftpClient.Disconnect();

            return oFilePack;
        }

        public Dictionary<string, byte[]> GetSecurityFilePack()
        {
            String[] SecurityFileNames = new String[] {
                @"/usr/local/platform/conf/platformConfig.xml",
                @"/usr/local/platform/.security/CCMEncryption/keys/dkey.txt"
            };

            return GetFilePack(SecurityFileNames);
        }
    }
}
