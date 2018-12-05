using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADHDTech.CiscoCrypt;
using System.IO;
using System.Runtime.InteropServices;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Core;
using System.Text.RegularExpressions;

namespace ADHDTech.Tester
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("\nTesting RemoteSupportPassphrase\n---------------------------------\n");
            RemoteSupportPassphrase passphrase = new RemoteSupportPassphrase();
            passphrase.Decode("BFHX9UBW92");
            Console.Write("{0}   ---> {1}\n", passphrase.Passphrase, passphrase.Password);
            passphrase.Decode("BFHX9UBW9202");
            Console.Write("{0} ---> {1}\n", passphrase.Passphrase, passphrase.Password);
            passphrase.Decode("BFHX9UBW9203");
            Console.Write("{0} ---> {1}\n", passphrase.Passphrase, passphrase.Password);
            passphrase.Decode("IDNNJIE0ZG03");
            Console.Write("{0} ---> {1}\n", passphrase.Passphrase, passphrase.Password);


            Console.Write("\nTesting PlatformConfigPassword\n------------------------------\n");
            string plainPass = "UCCE@dm1n!";
            string encryptedPass = null;
            //string encryptedPass = "BEDA6472791869342394638D1305EC39831812812AB2825C831812812AB2825C";

            PlatformConfigPassword pwd = new PlatformConfigPassword();
            encryptedPass = pwd.Encrypt(plainPass);
            Console.Write("Encrypted {0} -> {1}\n", plainPass, encryptedPass);
            //encryptedPass = "BEDA6472791869342394638D1305EC39831812812AB2825C831812812AB2825D";
            plainPass = pwd.Decrypt(encryptedPass);
            if (!pwd.Successful)
            {
                Console.Write("Could not decrypt platform config password!");
                Console.ReadKey();
            }
            Console.Write("Decrypted {0} <- {1}\n", plainPass, encryptedPass);


            Console.Write("\nTesting DRFComponentPassword\n------------------------------\n");
            string startingValue = "89babe930a6aeb06a8e278e50dbd632dcac63299ca4b236fdac2b92d419bbe9d";

            Console.Write("Starting SHA1  '{0}'\n", startingValue);
            DRFComponentValueCrypter pwd2 = new DRFComponentValueCrypter(encryptedPass, 1);
            var decryptedRandom = pwd2.Decrypt(startingValue);
            if (pwd2.Successful)
            {
                Console.Write("Decrypted SHA1 '{0}'\n", decryptedRandom);

                var encryptedRandom = pwd2.Encrypt(decryptedRandom);

                Console.Write("Encrypted SHA1 '{0}'\n", encryptedRandom);
            }
            else
            {
                Console.Write("Could not decrypt component password\n---> {0}\n", pwd2.ErrorMessage);
            }

            string sUCMBackupRootPath = @"Q:\UCMBackups";

            Console.Write("\nTesting GetRandomPasswordFromFile\n---------------------------------\n");
            //string fileName = @"c:\Temp\2017-07-02-01-00-02_EG-VVF-ADM-SUB1_UCM_CLM.tar";
            //string fileName = @"c:\Temp\2016-11-14-07-09-08_CUCMTEST115_UCM_CLM.tar";
            //string fileName = @"C:\Temp\2014-02-08-05-56-47_DEVCUCM912_UCM_CLM.tar";
            string fileName = sUCMBackupRootPath + @"\DevCUCM912 20140208\2014-02-08-05-56-47_DEVCUCM912_UCM_CLM.tar";
            var watch1 = System.Diagnostics.Stopwatch.StartNew();
            string randomPass = Functions.GetRandomPasswordFromFile(fileName);
            watch1.Stop();
            Console.Write("RandomPass: [{0}], {1}ms\n", randomPass, watch1.ElapsedMilliseconds);


            Console.Write("\nTesting SHA1Sum\n---------------------------------\n");
            string sha1Sum = Functions.GetSHA1Sum(sUCMBackupRootPath + @"\DevCUCM912 20140208\2014-02-08-05-56-47_DEVCUCM912_UCM_CCMDB.tar");
            Console.Write("SHA1Sum:           {0}\n", sha1Sum);
            string sha1SumEncrypted = pwd2.Encrypt(sha1Sum);
            Console.Write("SHA1Sum Encrypted: {0}\n", sha1SumEncrypted);


            Console.Write("\nTesting DecryptCCMPlatformValue\n---------------------------------\n");

            TestCCMPlatformDecrypt("717b204bd092a6a19f3346719a57fba3e49daeb76d9b5a5053a29ac97a95ac09", null, "drfComponent.xml <amIRestricted>");
            TestCCMPlatformDecrypt("e98a66b452beac44300b6c374955f18edc7dbf2931c3acdeda337035d1071809", null, "drfDevice.xml pw");
            TestCCMPlatformDecrypt("91b856078baf8fe08762ccd1b66a47b040350b78511f4137ed624910aae2b0c5", null, "drfComponent.xml <amIRestricted>");
            //TestCCMPlatformDecrypt("ab983513845bc5613dd46f84261b8e8342da5df42125b5970c9076e3ef8acd2f", "49c8182574a74ca2ddc4358024e98b6e02edfb5a54e3453b7a8e3db5a697bb19", "platformConfig.xml <ApplUserDbPwCrypt>");
            TestCCMPlatformDecrypt("ab983513845bc5613dd46f84261b8e8342da5df42125b5970c9076e3ef8acd2f", null, "platformConfig.xml <ApplUserDbPwCrypt>");
            //TestCCMPlatformDecrypt("a4bfca4a741c446b6478f7e6eca753290c28ed5e3b35a28f74cecef72a410330", "49c8182574a74ca2ddc4358024e98b6e02edfb5a54e3453b7a8e3db5a697bb19", "platformConfig.xml <LocalHostAdminPwCrypt>");

            //TestCCMPlatformDecrypt("a4bfca4a741c446b6478f7e6eca753290c28ed5e3b35a28f74cecef72a410330", "1aa83c978ec15ca9b5723e2624cdd3e9bc412b91204ad126eeec6e3205ee0bc079f991245f5f67f89b711a0c923be56dc7f2675bc45b8596c21712f96741b7b847ff645b096a292f40682e10730e8d2e84367422fea79511f0762afde2c98b22", "platformConfig.xml <LocalHostAdminPwCrypt>");


            Console.Write("\nTesting LoadDRFComponentFile\n---------------------------------\n");
            //var xmlFilePath = @"E:\UCMBackups\AZ 20171006\ADM\2017-10-02-02-00-02_MEM-VVF-ADM-PUB_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\Cisco Backups 20130909\AHSProd\CUCM\2013-09-09-00-15-19_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\Cisco Backups 20130909\AHSProd\UCON\Pub\2013-09-11-01-19-21_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\DevCUCM912 20140208\2014-02-08-05-56-47_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\TestCUCM912 20171021\2017-10-21-02-40-35_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\TestCUCM11-Backup-20150731\2015-07-31-15-15-04_TESTCUCM11_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\CUCMTest115 20161114\2016-11-14-07-09-08_CUCMTEST115_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\TestCUCM120 20171026\2017-10-26-22-58-07_CUCMTEST120_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\UCCX Backup\2013-12-11-14-38-14_drfComponent.xml";
            var xmlFilePath = sUCMBackupRootPath + @"\AZ 20171006\ADM\2017-10-02-02-00-02_MEM-VVF-ADM-PUB_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\AZ 20171006\CUC\2017-10-01-01-30-04_MEM-VVF-CUC-PRIM_drfComponent.xml";
            //var xmlFilePath = sUCMBackupRootPath + @"\NYK 20110713\2011-07-13-01-00-09_drfComponent.xml";

            string testPassword = @"toadwart61";
            //string testPassword = @"UCCE@dm1n!";
            DRSBackupSet myBackupSet = new DRSBackupSet(xmlFilePath);
            if (myBackupSet.XMLLoaded)
            {

                if (myBackupSet.IsEncrypted)
                {
                    bool isPasswordValid = myBackupSet.SetPassword(testPassword);
                    Console.Write("Security Password valid: '{0}' [{1}]\n\n", testPassword, isPasswordValid);
                    Console.Write("Have Random Backup Pass: [{0}]\n\n", myBackupSet.HaveRandomBackupPass);
                }
                /*
                if (!myBackupSet.IsEncrypted || myBackupSet.HaveRandomBackupPass) {
                    watch1.Restart();
                    myBackupSet.VerifyTarFiles();
                    watch1.Stop();
                    Console.Write("\nVerifyTarFiles time: {0}ms\n", watch1.ElapsedMilliseconds);
                }
                */
            }
            else
            {
                Console.Write("XML not loaded: {0}\n", myBackupSet.ErrorMsg);
            }

            //myBackupSet.ListFiles();

            /*
            JavaKeyHashConnector keyHashConnector = new JavaKeyHashConnector();
            keyHashConnector.ConnectorReadyHandler = delegate ()
            {
                Console.Write("Connector is ready!\n");

                String pwdEncoded = "BEDA6472791869342394638D1305EC39831812812AB2825C831812812AB2825C";
                keyHashConnector.SendData(pwdEncoded);                
            };
            keyHashConnector.ReceiveKeyHandler = delegate (string keyHash)
            {
                Console.Write("Received keyHash -> '{0}'\n", keyHash);
            };
            keyHashConnector.OpenConnector();

            // Sleep a few seconds
            */

            //LiveHostClient testClient = new LiveHostClient();

            //ADHDTech.CiscoSCP.LiveHostClient testClient = new ADHDTech.CiscoSCP.LiveHostClient("10.10.20.1", "myroot", "IDNNJIE0ZG03");
            ADHDTech.CiscoSCP.UCOSClientSFTP testClient = new ADHDTech.CiscoSCP.UCOSClientSFTP("10.10.20.1", "myroot", "IDNNJIE0ZG03");
            Dictionary<string, byte[]> filePack = testClient.GetSecurityFilePack();

            //ADHDTech.CiscoSCP.LiveHostClient testClient = new ADHDTech.CiscoSCP.LiveHostClient("192.168.5.90", "trogdor", "JKFMLJ4QFD03");

            //ADHDTech.CiscoSCP.LiveHostClient testClient = new ADHDTech.CiscoSCP.LiveHostClient("192.168.5.91", "trogdor", "5IPAEQFIUC02");

            //ADHDTech.CiscoSCP.LiveHostClient testClient = new ADHDTech.CiscoSCP.LiveHostClient("192.168.5.92", "trogdor", "6J7SB7EF5I03");

            //ADHDTech.CiscoSCP.LiveHostClient testClient = new ADHDTech.CiscoSCP.LiveHostClient("169.198.11.191", "trogdor", "WJS9VPUTUD03");
            //ADHDTech.CiscoSCP.LiveHostClientSFTP testClient = new ADHDTech.CiscoSCP.LiveHostClientSFTP("169.198.11.191", "trogdor", "WJS9VPUTUD03");

            //ADHDTech.CiscoSCP.Client testClient = new ADHDTech.CiscoSCP.Client("192.168.5.91", "trogdor", "5IPAEQFIUC02");
            //string sPlatformConfig = Functions.encoding.GetString(testClient.GetPlatformConfig());
            //Console.WriteLine("{0}", sPlatformConfig);

            //TestOntapeReader myReader = new TestOntapeReader(@"Q:\DRSOntapes\drf_ontape_backup_DCM");

            Console.Write("\nDone!\n");
            Console.ReadKey();
        }

        static void TestCCMPlatformDecrypt(string sEncryptedData, string sDKey, string sSource)
        {
            String sDecryptedCCMValue = Functions.DecryptCCMPlatformValue(sEncryptedData, sDKey);
            String sKeyType;
            if (sDKey == null)
            {
                sKeyType = "Static";
            }
            else
            {
                sKeyType = "dKey  ";
            }
            Console.Write("{0} | {1} Hex: {2}\tVal: {3}\n", sKeyType, sSource.PadRight(42), sEncryptedData, sDecryptedCCMValue);
        }
    }

    class TestOntapeReader
    {
        string _sCCMDBTarFile;
        string _sUnpackDir;
        string _sIdxFile;
        string _sOntapeFile;
        long _lFileLength;
        int _iPageSize;
        string[] _sIdxRecords;

        FileMarkers _oFileMarkers;
        static PageReader _oPageReader;
        OntapeHeader _oOntapeHeader;
        DBSpaceList _oDBSpaceList;
        FileStream _oInputStream;
        static List<DBSpace> _DBSpaces;
        RootDBSReserved _oRootDBSReserved;

        public unsafe TestOntapeReader(string sOntapeFile)
        {
            _iPageSize = 2048;

            _DBSpaces = new List<DBSpace>();

            // See if we have an index file; if so, it should be a specific size with respect to the tar file
            _sCCMDBTarFile = @"Q:\UCMBackups\Cisco Backups 20130909\DCM\2013-09-09-01-45-13_etnmem2cm01_ccm_ccmdb.tar";
            _sUnpackDir = @"C:\Temp\tar-out";
            ValidateUnpack(_sCCMDBTarFile, _sUnpackDir);

            // Parse Idx, get chunk start/stops and logical to physical page maps
            _oFileMarkers = new FileMarkers(_sIdxRecords);

            // Open file; this sets _lFileLength and _oInputStream
            //_sOntapeFile = sOntapeFile;
            OpenFile(_sUnpackDir + @"\" + _sOntapeFile);

            // Create a page reader
            _oPageReader = new PageReader(_oInputStream, 2048);

            ParseStream();
        }

        public bool ValidateUnpack(string sCCMDBTarFullPath, string sUnpackDir)
        {

            string pattern = @"^(\d{4}-\d{2}-\d{2}-\d{2}-\d{2}-\d{2}_[^_]+_)";
            Regex rgx = new Regex(pattern);

            // Verify source exists
            FileInfo oTARFileInfo = new FileInfo(sCCMDBTarFullPath);
            if (!oTARFileInfo.Exists)
            {
                throw new Exception("Source TAR does not exist");
            }

            // Verify filename has the expected format
            MatchCollection matches = rgx.Matches(oTARFileInfo.Name);
            if (matches.Count != 1)
            {
                throw new Exception("Source TAR does not follow naming convention");
            }

            // Set path to ontape within TAR
            string sTarOntapePath = @"/common/drf/db_drf_backup/drf_ontape_backup.gz";

            // Set target filenames
            string sFilenameBase = matches[0].Value;
            _sOntapeFile = sFilenameBase + @"ontapeCCM.dat";
            _sIdxFile = sFilenameBase + @"ontapeCCM.idx";

            FileInfo oIdxFileInfo = new FileInfo(sUnpackDir + @"\" + _sIdxFile);
            if (!oIdxFileInfo.Exists)
            {
                // The Idx file does not exist; let's unpack
                ExtractTarFileEntryUnGzipToFile(sCCMDBTarFullPath, sTarOntapePath, sUnpackDir, _sOntapeFile, _sIdxFile);
                _sIdxRecords = File.ReadAllLines(oIdxFileInfo.FullName);
            }
            else
            {
                // We have an Idx file; see if it completed
                _sIdxRecords = File.ReadAllLines(oIdxFileInfo.FullName);
                if (_sIdxRecords.Length > 0 && _sIdxRecords[_sIdxRecords.Length - 1].Equals(@"DONE"))
                {
                    // The Idx file was generated successfully
                }
                else
                {
                    // The Idx file was not generated successfully; let's unpack
                    ExtractTarFileEntryUnGzipToFile(sCCMDBTarFullPath, sTarOntapePath, sUnpackDir, _sOntapeFile, _sIdxFile);
                    _sIdxRecords = File.ReadAllLines(oIdxFileInfo.FullName);
                }
            }

            return true;
        }

        public unsafe void ParseStream()
        {
            // Get OntapeHeader
            Console.WriteLine("Reading Ontape header   @ 0x{0}", (_oPageReader.oInputStream.Position).ToString("x8"));
            _oOntapeHeader = new OntapeHeader(_oPageReader);

            // Advance to DBSpaceList
            while (_oPageReader.NextPage->Flags != 0x0014)
            {
                _oPageReader.ReadPage();
            }

            // Get DBSpaceList
            Console.WriteLine("Reading DBSpaceList     @ 0x{0}", (_oPageReader.oInputStream.Position - _iPageSize).ToString("x8"));
            _oDBSpaceList = new DBSpaceList(_oPageReader);

            // Advance to Reserved Pages
            while (_oPageReader.NextPage->Flags != 0x1800)
            {
                _oPageReader.ReadPage();
            }

            // Get Reserved Pages
            Console.WriteLine("Reading RootDBSReserved @ 0x{0}", (_oPageReader.oInputStream.Position - _iPageSize).ToString("x8"));
            _oRootDBSReserved = new RootDBSReserved(_oPageReader);

            /*
            // Advance to Log Pages
            while (_oPageReader.NextPage->Flags != 0x2014)
            {
                _oPageReader.ReadPage();
            }

            // Get Log Pages
            Console.WriteLine("Reading LogPages        @ 0x{0}", (_oPageReader.oInputStream.Position - _iPageSize).ToString("x8"));

            // Advance to DBSpace Pages; RootDBS will be first
            int pageCounter = 0;
            while (_oPageReader.oInputStream.Position != _oPageReader.oInputStream.Length)
            {
                _oPageReader.ReadPage();
                pageCounter++;
                if (_oPageReader.NextPage->Flags == 0x4010)
                {
                    // Get DBSpace
                    _oPageReader.ReadPage();
                    ushort targetChunkNum = BitConverter.ToUInt16(_oPageReader.GetRawPageData(0x28, 2), 0);
                    DBChunkRec thisChunkRec = _oRootDBSReserved.PAGE_1PCHUNK[targetChunkNum];
                    ulong usedPageCount = thisChunkRec.ChunkSize - thisChunkRec.ChunkFree;
                    Console.WriteLine("Reading DBSpace {0} @ 0x{1}, used pages: {2}, page counter: {3}", thisChunkRec.ChunkPath, (_oPageReader.oInputStream.Position - _iPageSize).ToString("x8"), usedPageCount, pageCounter);
                    pageCounter = 0;
                }
            }
            */

            
            foreach (KeyValuePair<uint, DBChunkMarker> thisPair in _oFileMarkers.DBChunkMarkers) {
                ParseSystables(thisPair.Value.chunkNum);
            }

            bool bTest = false;
        }

        public class OntapeHeader
        {
            string _sArchiveType;
            string _sProduct;
            string _sTimestamp;
            string _sUserName;
            byte _bUnknown1;
            int _iArchiveLevel;
            string _sInputSource;
            ushort _iTapeBlockSize;
            int _iUnknown2;
            int _iUnknown3;

            public unsafe OntapeHeader(PageReader oPageReader)
            {
                // We should be at the start of the stream; read the header page
                oPageReader.ReadPage();
                if (oPageReader.CurrentPage->Flags != 0x0005)
                {
                    // This isn't a header page - error out
                    throw new Exception("Tried to parse non-header page");
                }
                _sArchiveType = Encoding.UTF8.GetString(oPageReader.GetRecordData(0));
                _sProduct = Encoding.UTF8.GetString(oPageReader.GetRecordData(1));
                _sTimestamp = Encoding.UTF8.GetString(oPageReader.GetRecordData(2));
                _sUserName = Encoding.UTF8.GetString(oPageReader.GetRecordData(3));
                _bUnknown1 = oPageReader.GetRecordData(4)[0];
                _iArchiveLevel = BitConverter.ToInt32(oPageReader.GetRecordData(5), 0);
                _sInputSource = Encoding.UTF8.GetString(oPageReader.GetRecordData(6));
                _iTapeBlockSize = BitConverter.ToUInt16(oPageReader.GetRecordData(7), 0);
                _iUnknown2 = BitConverter.ToInt32(oPageReader.GetRecordData(8), 0);
                _iUnknown3 = BitConverter.ToInt32(oPageReader.GetRecordData(8), 0);
            }
        }

        public unsafe class DBSpaceList
        {
            string[] IncludedDBSpaces;
            public DBSpaceList(PageReader oPageReader)
            {
                // We should be at the start of the DBSpaces page
                oPageReader.ReadPage();
                if (oPageReader.CurrentPage->Flags != 0x0014)
                {
                    // This isn't a header page - error out
                    throw new Exception("Tried to parse non-dbspacelist page");
                }
                string sDBSpaceList = Encoding.UTF8.GetString(oPageReader.GetRecordData(0)).Trim();
                IncludedDBSpaces = sDBSpaceList.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // TO DO - Add logic to accomodate for multiple pages of DBSpace lists!
            }
        }

        public unsafe class RootDBSReserved
        {
            public static class PAGE_PZERO
            {
                public static string Copyright;
                public static string Version;
                public static ushort PageSize;
            }
            public Dictionary<String, String> PAGE_CONFIG;
            //public static class PAGE_CONFIG { }
            public static class PAGE_1CKPT { }
            public static class PAGE_2CKPT { }
            //public static class PAGE_1DBSP { }
            public Dictionary<uint, DBSpaceRec> PAGE_1DBSP;
            public static class PAGE_2DBSP { }
            public Dictionary<uint, DBChunkRec> PAGE_1PCHUNK;
            public static class PAGE_2PCHUNK { }
            public static class PAGE_1MCHUNK { }
            public static class PAGE_2MCHUNK { }
            public static class PAGE_1ARCH { }
            public static class PAGE_2ARCH { }
            public RootDBSReserved(PageReader oPageReader)
            {
                // We should be at the start of the stream; read the header page
                oPageReader.ReadPage();
                if (oPageReader.CurrentPage->ChunkNum != 0x0001 || oPageReader.CurrentPage->PageNumber != 0x00000000)
                {
                    // This isn't a header page - error out
                    throw new Exception("Failure trying to parse PAGE_PZERO");
                }

                PAGE_PZERO.Copyright = Encoding.UTF8.GetString(oPageReader.GetRawPageData(0x18, 76).ToArray()).Trim();
                PAGE_PZERO.PageSize = BitConverter.ToUInt16(oPageReader.GetRawPageData(0x6c, 2), 0);
                PAGE_PZERO.Version = Encoding.UTF8.GetString(oPageReader.GetRawPageData(0x80, 4).ToArray()).Trim();

                // Advance to PAGE_CONFIG
                oPageReader.ReadPage();
                if (oPageReader.CurrentPage->ChunkNum != 0x0001 || oPageReader.CurrentPage->PageNumber != 0x00000001)
                {
                    // This isn't a header page - error out
                    throw new Exception("Failure trying to parse PAGE_CONFIG");
                }
                PAGE_CONFIG = new Dictionary<string, string>();
                for (ushort i = 0; i < oPageReader.CurrentPage->RecordCount; i++)
                {
                    string sRecordData = Encoding.UTF8.GetString(oPageReader.GetRecordData(i)).TrimEnd('\0');
                    string[] sNameValPair = sRecordData.Split(' ');
                    PAGE_CONFIG[sNameValPair[0]] = sNameValPair[1];
                    //Console.WriteLine("PAGE_CONFIG value: '{0}' -> [{1}]", sNameValPair[0], sNameValPair[1]);
                }

                // Advance to PAGE_1DBSP
                while (oPageReader.NextPage->PageNumber != 0x00000004)
                {
                    oPageReader.ReadPage();
                }
                PAGE_1DBSP = new Dictionary<uint, DBSpaceRec>();
                while (oPageReader.CurrentPage->PageNumber != 0x00000005)
                {
                    oPageReader.ReadPage();
                    for (ushort i = 0; i < oPageReader.CurrentPage->RecordCount; i++)
                    {
                        DBSpaceRec thisDBSpace = (DBSpaceRec)Marshal.PtrToStructure(oPageReader.GetRecordPtr(i), typeof(DBSpaceRec));
                        PAGE_1DBSP[thisDBSpace.DBSpaceNum] = thisDBSpace;
                    }
                }

                // Advance to PAGE_1PCHUNK
                while (oPageReader.NextPage->PageNumber != 0x00000006)
                {
                    oPageReader.ReadPage();
                }
                PAGE_1PCHUNK = new Dictionary<uint, DBChunkRec>();
                while (oPageReader.CurrentPage->PageNumber != 0x00000007)
                {
                    oPageReader.ReadPage();
                    for (ushort i = 0; i < oPageReader.CurrentPage->RecordCount; i++)
                    {
                        DBChunkRec thisDBChunk = (DBChunkRec)Marshal.PtrToStructure(oPageReader.GetRecordPtr(i), typeof(DBChunkRec));
                        PAGE_1PCHUNK[thisDBChunk.ChunkNum] = thisDBChunk;
                    }
                }
            }
        }

        public class Chunk
        {
        }

        public class Extent
        {
        }

        public class DBSpace
        {
            public string Name;
            public string Owner;
            public string Filename;
            public int TableCount;
            public uint DBSpaceNum;
            public uint PageSize;
            public uint NumChunks;
            public uint FirstChunk;
            public uint Flags;
            public int MatchedPages;
            public int OrphanedPages;
            public int FragmentedPages;
            public int EarlyRecordTerms;
            public List<DBTableDetails> Tables;
            public SortedDictionary<String, DBTableDetails> TableDictionary;
        }

        public class TblSpace
        {
        }

        public class Database
        {
        }

        public class Table
        {
        }

        public class Page
        {
        }

        public class BlobPage
        {
        }

        public unsafe struct PageStruct
        {
            public uint PageNumber;
            public ushort ChunkNum;
            public ushort CheckSum;
            public ushort RecordCount;
            public ushort Flags;
            public ushort FreeOffset;
            public ushort FreeBytes;
            public uint NextPage;
            public uint PrevPage;
            public byte DataStart;
        }

        public unsafe class PageReader
        {
            public FileStream oInputStream;
            /*
            public uint PageNumber;
            public ushort ChunkNum;
            public ushort CheckSum;
            public ushort RecordCount;
            public ushort Flags;
            public ushort FreeOffset;
            public ushort FreeBytes;
            public uint NextPage;
            public uint PrevPage;
            */
            //public byte[][] RecordArrays;
            public long lTotalPagesRead;
            public int iPageSize;
            public long lBytesLastRead;
            public byte[] Buffer1;
            public byte[] Buffer2;
            bool bufferFlag;
            public bool HasOverrun;
            public PageStruct* CurrentPage;
            public PageStruct* NextPage;
            public PageReader(FileStream oNewInputStream, int iNewPageSize)
            {
                lTotalPagesRead = 0;
                iPageSize = iNewPageSize;
                oInputStream = oNewInputStream;
                Buffer1 = new byte[iPageSize];
                Buffer2 = new byte[iPageSize];
            }

            public void ReadPage(uint pageNum) {
                long lStartAddress = pageNum * iPageSize;
                oInputStream.Seek(lStartAddress, 0);
                bufferFlag = false;
                lTotalPagesRead = 0;
                ReadPage();
            }
            public void ReadPage()
            {
                if (bufferFlag)
                {
                    // Even
                    lBytesLastRead = oInputStream.Read(Buffer1, 0, iPageSize);
                    fixed (byte* pBuffer1 = Buffer1)
                    fixed (byte* pBuffer2 = Buffer2)
                    {
                        CurrentPage = (PageStruct*)pBuffer2;
                        NextPage = (PageStruct*)pBuffer1;
                    }
                    bufferFlag = false;
                    lTotalPagesRead++;
                }
                else
                {
                    // Odd
                    if (lTotalPagesRead == 0)
                    {
                        lBytesLastRead = oInputStream.Read(Buffer1, 0, iPageSize);
                    }
                    lBytesLastRead = oInputStream.Read(Buffer2, 0, iPageSize);
                    fixed (byte* pBuffer1 = Buffer1)
                    fixed (byte* pBuffer2 = Buffer2)
                    {
                        CurrentPage = (PageStruct*)pBuffer1;
                        NextPage = (PageStruct*)pBuffer2;
                    }
                    bufferFlag = true;
                    lTotalPagesRead++;
                }
                if (lBytesLastRead == 0)
                {
                    // No bytes read from stream; this must be the last page
                }
            }
            public byte[] GetRecordData(ushort iRecordNumber)
            {
                return GetRecordData(iRecordNumber, false);
            }
            public byte[] GetRecordData(ushort iRecordNumber, bool iNextPage)
            {
                PageStruct* targetPage;

                if (iNextPage)
                {
                    targetPage = NextPage;
                }
                else
                {
                    targetPage = CurrentPage;
                }

                byte[] returnArray = null;
                //if (iRecordNumber < targetPage->RecordCount)
                //{
                // Set the index Ptr to...
                // Address of DataPage + PageSize - Trailing Timestamp - Bytes of record indexes to skip
                //byte* tmpPtr = (byte*)pDataPage + PageSize - 4 - (iRecordNumber * 4);
                ushort* IndexPtr = (ushort*)((byte*)targetPage + iPageSize - 4 - (iRecordNumber * 4));
                IndexPtr--;
                ushort RecordLen = *IndexPtr;
                IndexPtr--;
                ushort RecordStartOffset = *IndexPtr;

                if (RecordStartOffset == 0)
                {
                    // The Data Start offset is not set.  This means the record has been deleted.
                    return returnArray;
                }

                if ((RecordLen & 0x8000) > 0)
                {
                    // We have an overrun and need to follow the breadcrumbs
                    this.HasOverrun = true;
                    RecordLen = (ushort)((uint)RecordLen & 0x00007FFF);
                    uint continuePageID = targetPage->NextPage;
                    if (continuePageID == 0)
                    {
                        // If the continuePageID is zero, the data continues on the next page number by default
                        continuePageID = targetPage->PageNumber + 1;
                    }

                    // See if the next page matches.  If so, read.  If not, we need to search.
                }
                returnArray = new byte[RecordLen];
                byte* PagePtr = (byte*)targetPage + *IndexPtr;
                Marshal.Copy((IntPtr)PagePtr, returnArray, 0, RecordLen);
                //}
                return returnArray;
            }
            public byte[] GetRawPageData(ushort StartPosition, ushort Length)
            {
                byte[] returnArray = new byte[Length];
                byte* PagePtr = (byte*)CurrentPage + StartPosition;
                Marshal.Copy((IntPtr)PagePtr, returnArray, 0, Length);
                return returnArray;
            }
            public IntPtr GetRecordPtr(ushort iRecordNumber)
            {
                return GetRecordPtr(iRecordNumber, false);
            }
            public IntPtr GetRecordPtr(ushort iRecordNumber, bool iNextPage)
            {
                PageStruct* targetPage;

                if (iNextPage)
                {
                    targetPage = NextPage;
                }
                else
                {
                    targetPage = CurrentPage;
                }

                ushort* IndexPtr = (ushort*)((byte*)targetPage + iPageSize - 4 - (iRecordNumber * 4));
                IndexPtr--;
                ushort RecordLen = *IndexPtr;
                IndexPtr--;
                ushort RecordStartOffset = *IndexPtr;
                byte* returnPtr = (byte*)targetPage + RecordStartOffset;
                return (IntPtr)returnPtr;
            }

            public uint GetCorrectedSequenceLength(int DBSpaceID, DBDataBlockRef checkBlockRef)
            {
                // Start with the DBPageCount
                uint CorrectedSequenceLength = checkBlockRef.DBPageCount;

                // Check current index pairs of tables in DBs
                for (int h = 0; h < _DBSpaces.Count(); h++)
                {
                    // See if this is the right DB
                    if (DBSpaceID == _DBSpaces[h].DBSpaceNum)
                    {
                        // Loop over tables
                        for (int i = 0; i < _DBSpaces[h].TableCount; i++)
                        {
                            // Loop over sequences in table
                            for (uint j = 0; j < _DBSpaces[h].Tables[i].DataBlockPairCount; j++)
                            {
                                DBDataBlockRef thisBlockRef = _DBSpaces[h].Tables[i].DataBlockMarkers[j];
                                // Does this sequence interrupt us?
                                if (thisBlockRef.DBPageStart > checkBlockRef.DBPageStart &&
                                    thisBlockRef.DBPageStart < checkBlockRef.DBPageEnd)
                                {
                                    // This is interleaved; is it the best interleaved?
                                    uint tmpSequenceLength = thisBlockRef.DBPageStart - checkBlockRef.DBPageStart;
                                    if (tmpSequenceLength < CorrectedSequenceLength)
                                    {
                                        CorrectedSequenceLength = tmpSequenceLength;
                                    }
                                }
                            }
                        }
                    }
                }
                /*
                if (CorrectedSequenceLength != checkBlockRef.DBPageCount)
                {
                    Console.Write("Correcting sequence length for block...\n\tStart\t0x{0}\n\tOld Count\t0x{1}\n\tNew Count\t0x{2}\n",
                        checkBlockRef.DBPageStart.ToString("X8"),
                        checkBlockRef.DBPageCount.ToString("X8"),
                        CorrectedSequenceLength.ToString("X8"));
                }
                */
                return CorrectedSequenceLength;
            }
        }
        void OpenFile(string sFileName)
        {
            FileInfo oFileInfo = new FileInfo(sFileName);
            _lFileLength = oFileInfo.Length;
            _oInputStream = File.OpenRead(sFileName);
        }

        unsafe static public string BytePtrToString(byte* pBytePtr, int iMaxStringLen, bool bFixedLength, bool bTrimWhitespace)
        {
            string returnString = null;
            List<byte> bStringBytes = new List<byte>();
            for (int i = 0; i < iMaxStringLen; i++)
            {
                if (*pBytePtr == 0x00 && !bFixedLength)
                {
                    // We hit a null
                    break;
                }
                else
                {
                    // Add byte to array
                    bStringBytes.Add(*pBytePtr);
                    pBytePtr++;
                }
            }

            // Get string from byte array
            returnString = Encoding.UTF8.GetString(bStringBytes.ToArray());

            // Trim whitespace if requested
            if (bTrimWhitespace)
            {
                returnString = returnString.Trim();
            }

            return returnString;
        }

        public unsafe struct DBSpaceRec
        {
            public uint DBSpaceNum;
            public uint Flags;
            public uint FirstChunk;
            public uint NumChunks;
            public uint Unknown5;
            public uint Unknown6;
            public uint Unknown7;
            public uint PageSize;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 84)]
            public string Unknown9;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string DBSpaceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string DBOwnerName;
        }

        public unsafe struct DBChunkRec
        {
            public uint fffs;
            public ushort ChunkNum;
            public ushort NextChunk;
            public ulong ChunkSize;
            public ulong ChunkFree;
            public uint FirstPage;
            public uint Offset;
            public uint Flags;
            public ushort PageSize;
            public ushort DBSpaceNum;
            public ushort ChunkPathLen;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
            public string ChunkPath;
        }

        public class FileMarkers
        {
            public SortedDictionary<uint, DBChunkMarker> DBChunkMarkers;
            public FileMarkers(string[] headerArray)
            {
                DBChunkMarkers = new SortedDictionary<uint, DBChunkMarker>();

                bool isPage4k = false;
                ushort inChunk = 0;

                for (uint i = 0; i < headerArray.Length; i++)
                {
                    string thisRow = headerArray[i];
                    if (thisRow.Length == 24)
                    {
                        PageStruct thisPage = GetPageStructFromIdxRow(thisRow);

                        if (thisPage.Flags == 0x4020 || (thisPage.Flags == 0x4010 && inChunk > 0))
                        {
                            DBChunkMarkers[inChunk].pageCount = i - DBChunkMarkers[inChunk].startPage;
                            Console.WriteLine("Chunk [{2}] ended at physical page [{0}], total 2k pages [{1}]", i, DBChunkMarkers[inChunk].pageCount, inChunk);
                            inChunk = 0;
                            isPage4k = false;
                        }

                        if (thisPage.Flags == 0x4010)
                        {
                            // Check for 4K page size
                            uint startPage = i + 1;
                            PageStruct nextPage = GetPageStructFromIdxRow(headerArray[startPage]);
                            if (nextPage.ChunkNum == 0x0000)
                            {
                                isPage4k = true;
                                startPage = i + 2;
                                nextPage = GetPageStructFromIdxRow(headerArray[startPage]);
                            }
                            else
                            {
                                isPage4k = false;
                            }
                            inChunk = nextPage.ChunkNum;

                            DBChunkMarkers[inChunk] = new DBChunkMarker()
                            {
                                chunkNum = inChunk,
                                startPage = startPage,
                                LogicalPhysicalPageMap = new SortedDictionary<ulong, ulong>()
                            };

                            Console.WriteLine("Chunk [{2}] found at physical page [{0}], 4k [{1}]", i, isPage4k, inChunk);
                        } else if (inChunk > 0)
                        {
                            DBChunkMarkers[inChunk].LogicalPhysicalPageMap[thisPage.PageNumber] = i;
                        }

                        if (isPage4k)
                        {
                            // Advance an extra page if the chunk is 4k
                            i++;
                        }

                        bool bob = false;
                    }
                }
            }

            public PageStruct GetPageStructFromIdxRow(string sIdxRow)
            {
                // Get PageNum, ChunkNum, CheckSum, RecordCount, Flags
                PageStruct oReturnPage = new PageStruct();
                oReturnPage.PageNumber = BitConverter.ToUInt32(StringToByteArray(sIdxRow.Substring(0, 8)), 0);
                oReturnPage.ChunkNum = BitConverter.ToUInt16(StringToByteArray(sIdxRow.Substring(8, 4)), 0);
                oReturnPage.CheckSum = BitConverter.ToUInt16(StringToByteArray(sIdxRow.Substring(12, 4)), 0);
                oReturnPage.RecordCount = BitConverter.ToUInt16(StringToByteArray(sIdxRow.Substring(16, 4)), 0);
                oReturnPage.Flags = BitConverter.ToUInt16(StringToByteArray(sIdxRow.Substring(20, 4)), 0);
                return oReturnPage;
            }
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public class DBChunkMarker
        {
            public ushort chunkNum;
            public uint startPage;
            public uint pageCount;
            public SortedDictionary<ulong, ulong> LogicalPhysicalPageMap;
        }

        public static void ExtractTarFileEntryUnGzipToFile(string tarFileName, string getFileName, string targetPath, string targetOutFile, string targetIdxFile)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            using (FileStream fsIn = new FileStream(tarFileName, FileMode.Open, FileAccess.Read))
            {
                TarInputStream tarIn = new TarInputStream(fsIn);
                TarEntry tarEntry;
                while ((tarEntry = tarIn.GetNextEntry()) != null)
                {
                    if (!String.Equals(tarEntry.Name, getFileName))
                    {
                        continue;
                    }

                    if (tarEntry.IsDirectory)
                    {
                        continue;
                    }

                    using (MemoryStream outTmpStr = new MemoryStream())
                    {
                        // Write compressed contents to temp file
                        tarIn.CopyEntryContents(outTmpStr);

                        // Reset to beginning of compressed file
                        outTmpStr.Position = 0;
                        int iBufferSize = 2048;
                        byte[] dataBuffer = new byte[iBufferSize];
                        int bytesRead;

                        // Output uncompressed file
                        using (FileStream fsOut = File.Create(targetPath + @"\" + targetOutFile))
                        using (FileStream fsOutIdx = File.Create(targetPath + @"\" + targetIdxFile))
                        {
                            using (GZipInputStream zipInput = new GZipInputStream(outTmpStr))
                            using (BinaryReader reader = new BinaryReader(zipInput))
                            {
                                var watch1 = System.Diagnostics.Stopwatch.StartNew();

                                while ((bytesRead = reader.Read(dataBuffer, 0, iBufferSize)) > 0)
                                {
                                    fsOut.Write(dataBuffer, 0, bytesRead);
                                    string tmpString = BitConverter.ToString(dataBuffer.Take(12).ToArray()).Replace("-", "") + "\n";
                                    byte[] headerByteHexArray = encoding.GetBytes(tmpString);
                                    fsOutIdx.Write(headerByteHexArray, 0, headerByteHexArray.Length);
                                }
                                string sIdxLastLine = "DONE\n";
                                fsOutIdx.Write(encoding.GetBytes(sIdxLastLine), 0, sIdxLastLine.Length);
                                //StreamUtils.Copy(zipInput, fsOut, dataBuffer);
                                watch1.Stop();
                                Console.Write("Copy time: {0}ms\n", watch1.ElapsedMilliseconds);
                                int iTmp = 0;
                            }

                            // Close and delete temp file
                            outTmpStr.Close();
                            //                            File.Delete(tmpFileName);

                            // Close uncompressed output.  In the future, we could reset the Position to 0 then pass the stream handle to the database parser.
                            fsOut.Close();
                        }
                        //outTmpStr.Close();
                    }
                    //tarIn.Close();
                }
            }
        }

        public unsafe void ParseSystables(uint targetChunk) {

            DBChunkMarker oChunkMarker = _oFileMarkers.DBChunkMarkers[targetChunk];
            //long lStartAddress = oChunkMarker.startPage * _oPageReader.iPageSize;
            uint iMaxIterations = oChunkMarker.pageCount;
            //_oPageReader.oInputStream.Seek(lStartAddress, 0);

            _oPageReader.ReadPage(oChunkMarker.startPage);

            for (uint i = 0; i < iMaxIterations; i++) {

                PageStruct* dbPage = _oPageReader.CurrentPage;

                // Get Systables
                if (dbPage->Flags == 0x0802)
                {
                    // Process as Systables page
                    DBSystablePage* pSystablePage = (DBSystablePage*)dbPage;

                    DBTableDetails thisTable = GetTableDetails(pSystablePage, (uint)_oPageReader.iPageSize);
                    
                    string tmpTableName = thisTable.TableName;

                    //Console.Write("Found {0}.{1} ({2})\n", thisTable.TableDBInstance, tmpTableName, thisTable.PartNum);

                    if (tmpTableName == "device" || tmpTableName == "systables" || tmpTableName == "syscolumns" || tmpTableName == "sysdatabases") {
                    }
                    if (dbPage->ChunkNum == 0x0001)
                    {
                        bool breakLine = true;
                    }
                    if (thisTable.TableType == 0x0A)
                    {
                        Console.Write("Found {0}.{1} ({2})\n", thisTable.TableDBInstance, tmpTableName, thisTable.PartNum);
                    }
                }

                _oPageReader.ReadPage();
            }
        }

        public unsafe struct DBSystablePage
        {
            public uint PageNumber;
            public ushort ChunkNum;
            public ushort CheckSum;
            public ushort RecordCount;
            public ushort Flags;
            public ushort FreeOffset;
            public ushort FreeBytes;
            public uint NextPage;
            public uint PrevPage;
            public uint PartNum;
            public byte TableType1;
            public byte TableType2;
            public ushort Unknown6;
            public uint Rowsize;
            public ushort IndexPairs;
            public ushort SysRecPairs;
            public ushort DataBlockPairs;
            public fixed byte Unknown7[30];
            public uint Delimiter;
            public uint PartNum2;
            public uint TableRecords;
            public fixed byte Unknown8[32];
            public fixed byte NameData[148];
            public byte RecordStartBlock;
        }

        unsafe private DBTableDetails GetTableDetails(DBSystablePage* SystablesEntry, uint pageSize)
        {

            DBTableDetails CurrentTable = new DBTableDetails();

            byte* charPtr;
            byte* sysPageStart = (byte*)SystablesEntry;
            ushort* sysIndexPtr = (ushort*)(sysPageStart + pageSize - 4);

            // Skip first two pairs
            sysIndexPtr -= 2;

            // Get Name Data
            sysIndexPtr--;
            ushort RecordLen = *sysIndexPtr;
            sysIndexPtr--;
            charPtr = sysPageStart + *sysIndexPtr;

            // Get DBServerName
            string TableDBInstance = Marshal.PtrToStringAnsi((IntPtr)(charPtr));
            CurrentTable.TableDBInstance = TableDBInstance;
            charPtr += TableDBInstance.Length + 1;

            // Get Owner
            string TableOwner = Marshal.PtrToStringAnsi((IntPtr)(charPtr));
            CurrentTable.TableOwner = TableOwner;
            charPtr += TableOwner.Length + 1;

            // Get Table Name
            string TableName = Marshal.PtrToStringAnsi((IntPtr)(charPtr));
            CurrentTable.TableName = TableName;
            charPtr += TableName.Length + 1;

            CurrentTable.DataBlockPairIndex = 0;
            CurrentTable.DataBlockPairCount = SystablesEntry->DataBlockPairs;
            CurrentTable.TableRecords = SystablesEntry->TableRecords;
            CurrentTable.PartNum = SystablesEntry->PartNum;
            CurrentTable.DataBlockMarkers = new DBDataBlockRef[CurrentTable.DataBlockPairCount];
            CurrentTable.DataBlocksWritten = 0;

            // Skip next 2 pairs
            sysIndexPtr -= 4;

            // Get Data Blocks
            sysIndexPtr--;
            ushort DBDataBlockLen = *sysIndexPtr;
            sysIndexPtr--;
            charPtr = sysPageStart + *sysIndexPtr + 4;

            for (int i = 0; i < CurrentTable.DataBlockPairCount; i++)
            {
                uint* intPtr = (uint*)charPtr;
                CurrentTable.DataBlockMarkers[i].DBPageStart = *intPtr;
                charPtr += 4;
                intPtr = (uint*)charPtr;
                CurrentTable.DataBlockMarkers[i].DBPageCount = *intPtr;
                CurrentTable.DataBlockMarkers[i].DBPageEnd = *intPtr + CurrentTable.DataBlockMarkers[i].DBPageStart;
                charPtr += 4;
            }

            // Get Table Type
            CurrentTable.TableType = SystablesEntry->TableType1;

            return CurrentTable;
        }

        public unsafe struct DBDataBlockRef
        {
            public uint DBPageStart;
            public uint DBPageCount;
            public uint DBPageEnd;
        }

        public unsafe struct DBColumnRecord
        {
            public byte colnamelen;
            public string colname;
            public DBColumnDetails coldetails;
        }

        public unsafe struct DBColumnDetails
        {
            public uint TableID;
            public ushort colno;
            public ushort coltype;
            public ushort collength;
            public uint colmin;
            public uint colmax;
            public uint extended_id;
        }

        public unsafe struct DBDataBlockPtr
        {
            public byte[] DataChunk;
        }

        public unsafe class DBTableDetails
        {
            public uint PartNum;
            public uint TableID;
            public uint RecordsFound;
            public uint OverflowRecordsFound;
            public uint TotalBlockSize;
            public int DBSpaceID;
            public string TableName;
            public string TableDBInstance;
            public string TableOwner;
            public uint TableRecords;
            public uint Rowsize;
            public uint NumCols;
            public ushort TableType;
            public ushort DataBlockPairCount;
            public ushort DataBlockPairIndex;
            public uint DataBlockLen;
            public uint DataBlockEndMarker;
            public DBDataBlockRef[] DataBlockMarkers;
            public uint OverflowRecord;
            public uint OverflowRecordBlock;
            public ushort OverflowRecordOffset;
            public ushort OverflowLength;
            public uint CurRecordSize;
            public DBColumnRecord[] DBColumns;
            public DBDataBlockPtr[] DataBlocks;
            public List<long> DataPageList;
            public uint DataBlocksWritten;

            public DBTableDetails()
            {
                this.PartNum = 0;
                this.TableID = 0;
                this.DBSpaceID = 0;
                this.RecordsFound = 0;
                this.TotalBlockSize = 0;
                this.TableName = "";
                this.TableDBInstance = "";
                this.TableOwner = "";
                this.TableRecords = 0;
                this.TableType = 0;
                this.Rowsize = 0;
                this.NumCols = 0;
                this.DataBlockPairCount = 0;
                this.DataBlockPairIndex = 0;
                this.DataBlockLen = 0;
                this.DataBlockEndMarker = 0;
                this.OverflowRecord = 0;
                this.OverflowRecordBlock = 0;
                this.OverflowRecordOffset = 0;
                this.OverflowLength = 0;
                this.CurRecordSize = 0;
                this.DataBlocksWritten = 0;
            }

            public void GetPageList()
            {
                DataPageList = new List<long>();
                // Loop over block pointers for table
                foreach (DBDataBlockRef thisBlockRef in DataBlockMarkers)
                {

                    long startPageRefKey = (this.DBSpaceID * 0x100000000) + thisBlockRef.DBPageStart;
                    uint correctPageCount = _oPageReader.GetCorrectedSequenceLength(this.DBSpaceID, thisBlockRef);
                    long endPageRefKey = startPageRefKey + correctPageCount;
                    /*
                    int startPageRefIndex = _oPageReader._idxPageRefs.IndexOf(startPageRefKey);
                    int endPageRefIndex = _oPageReader._idxPageRefs.IndexOf(endPageRefKey);
                    long actualPageCount = endPageRefIndex - startPageRefIndex;
                    if (startPageRefIndex == -1)
                    {
                        Console.Write("Could not find starting block!\n");
                        return;
                    }
                    if (endPageRefIndex == -1)
                    {
                        Console.Write("Could not find ending block...\n\tStart\t0x{0}\n\tCount\t0x{1}\n\tEnd\t0x{2}\n",
                                        thisBlockRef.DBPageStart.ToString("X8"),
                                        thisBlockRef.DBPageCount.ToString("X8"),
                                        thisBlockRef.DBPageEnd.ToString("X8"));
                        return;
                    }
                    if (endPageRefIndex < startPageRefIndex)
                    {
                        Console.Write("Block end is before beginning!\n");
                        return;
                    }

                    // Loop until we get to the last block
                    for (int m = startPageRefIndex; m < endPageRefIndex; m++)
                    {
                        DataPageList.Add(_oPageReader._idxPageRefs.ElementAt(m));
                    }
                    */
                }
            }
            /*
            public delegate void RecHandlerCallBack(DBValue[] rowValues);

            public void GetRecords(int recordCount, int startOffset, RecHandlerCallBack callBack)
            {
                this.GetPageList();
                ParentOntapeReader._inputStream = File.OpenRead(ParentOntapeReader._OntapeFile);
                foreach (long fullPageID in DataPageList)
                {
                    ushort tmpDBSpaceID = (ushort)(fullPageID >> 32);
                    uint tmpDBPageNum = (uint)(fullPageID & 0xFFFFFFFF);
                    int dbsPageSize = (int)ParentOntapeReader._DBSpaces[tmpDBSpaceID].PageSize;
                    byte[] readBuffer = new byte[dbsPageSize];
                    long fileStartAt = ParentOntapeReader._dicPageRefs[fullPageID];
                    ParentOntapeReader._inputStream.Seek(fileStartAt, 0);
                    ParentOntapeReader._inputStream.Read(readBuffer, 0, dbsPageSize);
                    fixed (byte* tmpPtr = readBuffer)
                    {
                        byte* PagePtr = tmpPtr;

                        DBDataPageObj dataPageObj = new DBDataPageObj(PagePtr, dbsPageSize);
                        if (dataPageObj.Flags == 0x0801)
                        {
                            for (uint r = 0; r < dataPageObj.RecordCount; r++)
                            {
                                if (dataPageObj.HasOverrun)
                                {
                                    // Get Overrun
                                    bool haveOverrun = true;
                                    uint overRunDataPage = dataPageObj.NextPage;
                                    byte[] pageBuffer = new byte[dbsPageSize];
                                    while (haveOverrun)
                                    {
                                        long overrunFullPageID = (this.DBSpaceID * 0x100000000) + overRunDataPage;
                                        long pageFilePos = ParentOntapeReader._dicPageRefs[overrunFullPageID];
                                        ParentOntapeReader._inputStream.Seek(pageFilePos, 0);
                                        ParentOntapeReader._inputStream.Read(pageBuffer, 0, dbsPageSize);
                                        fixed (byte* OverrunRecStartPtr = pageBuffer)
                                        {
                                            DBDataPageObj dataPageOverrunObj = new DBDataPageObj(OverrunRecStartPtr, dbsPageSize);
                                            haveOverrun = dataPageOverrunObj.HasOverrun;
                                            Console.Write("Found overrun!\n");
                                            // Create a new Array that's as big as the old and the new
                                            int newArraySize = dataPageObj.RecordArrays[r].Length + dataPageOverrunObj.RecordArrays[0].Length;
                                            byte[] newRecordDataArray = new byte[newArraySize];
                                            Buffer.BlockCopy(dataPageObj.RecordArrays[r], 0, newRecordDataArray, 0, dataPageObj.RecordArrays[r].Length);
                                            Buffer.BlockCopy(dataPageOverrunObj.RecordArrays[0], 0, newRecordDataArray, dataPageObj.RecordArrays[r].Length, dataPageOverrunObj.RecordArrays[0].Length);
                                            dataPageObj.RecordArrays[r] = newRecordDataArray;
                                        }
                                    }
                                }
                                if (dataPageObj.RecordArrays[r].Length > 0)
                                {
                                    fixed (byte* RecStartPtr = dataPageObj.RecordArrays[r])
                                    {
                                        int rowStartOffset = 0;
                                        string verTenString = "IBM Informix Dynamic Server Version 10.";
                                        if (this.ParentOntapeReader._BackupVer.Length >= verTenString.Length && this.ParentOntapeReader._BackupVer.Substring(0, verTenString.Length) == verTenString)
                                        {
                                            rowStartOffset = 0;
                                        }
                                        else
                                        {
                                            rowStartOffset = 8;
                                        }
                                        byte* RecPtr = RecStartPtr + rowStartOffset;
                                        if (dataPageObj.HasOverrun)
                                        {
                                            RecPtr += 4;
                                        }
                                        DBValue[] rowData = new DBValue[NumCols];
                                        for (uint k = 0; k < (NumCols); k++)
                                        {
                                            rowData[k] = GetValue(DBColumns[k].coldetails.coltype,
                                                            DBColumns[k].coldetails.collength,
                                                            RecPtr);
                                            RecPtr = rowData[k].newPtr;
                                        }
                                        callBack(rowData);
                                    }
                                }
                            }
                        }
                    }
                }
                ParentOntapeReader._inputStream.Close();
            }
            */
        }
    }
}