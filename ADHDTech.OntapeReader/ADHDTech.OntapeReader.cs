using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace ADHDTech.Informix
{
    public unsafe class OntapeReader
    {
        public unsafe struct DBValue
        {
            public int dataType;
            public uint dataLen;
            public byte isNull;
            public byte* newPtr;
            public byte[] dataVal;
        }

        unsafe struct OnTapePreHeader
        {
            public uint PageNumber;
            public ushort ChunkNum;
            public ushort BlockNum;
            public ushort RecCount;
            public fixed byte Unknown[14];
            public byte ArchiveInfoStart;
        }

        unsafe struct DBDef
        {
            public uint DBNumber1;
            public fixed byte Unknown1[4];
            public uint DbID;
            public fixed byte Unknown2[104];
            public fixed byte DBName[160];
        }

        unsafe struct DBDataHeader
        {
            public uint PageNumber;
            public ushort ChunkNum;
            public ushort BlockNum;
            public uint BlockType1;
            public uint BlockType2;
            public fixed byte Unknown1[24];
            public DBDef DBHeader;
        }

        public unsafe struct DBDataPage
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

        public unsafe struct DBSpaceListPage
        {
            public uint PageNumber;
            public ushort ChunkNum;
            public ushort chksum;
            public ushort unknown1;
            public ushort unknown2;
            public ushort FreeOffset;
            public ushort FreeBytes;
            public uint RecordCount;
            public uint unknown3;
            public byte DataStart;
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

        public unsafe struct DBColumnRecord
        {
            public byte colnamelen;
            public string colname;
            public DBColumnDetails coldetails;
        }

        public unsafe struct DBDataBlockPtr
        {
            public byte[] DataChunk;
        }

        public unsafe struct DBDataBlockRef
        {
            public uint DBPageStart;
            public uint DBPageCount;
            public uint DBPageEnd;
        }

        public unsafe struct TableRecDetails
        {
            public uint PartNum;
            public uint TableID;
            public ushort RowSize;
            public ushort NumCols;
            public ushort NumIndexes;
            public uint NumRows;
            public fixed byte Unknown1[40];
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
            public fixed byte Unknown9[84];
            public fixed byte DBSpaceName[128];
            public fixed byte DBOwnerName[32];
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
            public fixed byte ChunkPath[256];
        }

        public unsafe class DBDataPageObj
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
            public int PageSize;
            public byte[][] RecordArrays;
            public bool HasOverrun;
            public DBDataPage* pDataPage;
            public DBDataPageObj(byte* pPagePtr, int iPageSize)
            {
                this.pDataPage = (DBDataPage*)pPagePtr;
                this.ChunkNum = pDataPage->ChunkNum;
                this.PageNumber = pDataPage->PageNumber;
                this.CheckSum = pDataPage->CheckSum;
                this.RecordCount = pDataPage->RecordCount;
                this.Flags = pDataPage->Flags;
                this.FreeOffset = pDataPage->FreeOffset;
                this.FreeBytes = pDataPage->FreeBytes;
                this.NextPage = pDataPage->NextPage;
                this.PrevPage = pDataPage->PrevPage;
                this.PageSize = iPageSize;
                this.HasOverrun = false;

                uint PageStart = (uint)pPagePtr;
                ushort* IndexPtr = (ushort*)(PageStart + PageSize - 4);
                this.RecordArrays = new byte[this.RecordCount][];

                for (ushort r = 0; r < this.RecordCount; r++)
                {
                    this.RecordArrays[r] = GetRecordData(r);
                }
            }
            public byte[] GetRecordData(ushort iRecordNumber)
            {
                byte[] returnArray = null;
                if (iRecordNumber < RecordCount)
                {
                    // Set the index Ptr to...
                    // Address of DataPage + PageSize - Trailing Timestamp - Bytes of record indexes to skip
                    //byte* tmpPtr = (byte*)pDataPage + PageSize - 4 - (iRecordNumber * 4);
                    ushort* IndexPtr = (ushort*)((byte*)pDataPage + PageSize - 4 - (iRecordNumber * 4));
                    IndexPtr--;
                    ushort RecordLen = *IndexPtr;
                    IndexPtr--;
                    ushort RecordStartOffset = *IndexPtr;

                    if (RecordStartOffset == 0)
                    {
                        // The Data Start offset is not set.  This means the record has been deleted.
                    }

                    if ((RecordLen & 0x8000) > 0)
                    {
                        this.HasOverrun = true;
                        RecordLen = (ushort)((uint)RecordLen & 0x00007FFF);
                        if (this.NextPage == 0)
                        {
                            this.NextPage = this.PageNumber + 1;
                        }
                    }
                    returnArray = new byte[RecordLen];
                    byte* PagePtr = (byte*)pDataPage + *IndexPtr;
                    Marshal.Copy((IntPtr)PagePtr, returnArray, 0, RecordLen);
                }
                return returnArray;
            }
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
            public OntapeReader ParentOntapeReader;
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
                    uint correctPageCount = this.ParentOntapeReader.GetCorrectedSequenceLength(this.DBSpaceID, thisBlockRef);
                    long endPageRefKey = startPageRefKey + correctPageCount;
                    int startPageRefIndex = ParentOntapeReader._idxPageRefs.IndexOf(startPageRefKey);
                    int endPageRefIndex = ParentOntapeReader._idxPageRefs.IndexOf(endPageRefKey);
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
                        DataPageList.Add(ParentOntapeReader._idxPageRefs.ElementAt(m));
                    }
                }
            }

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
        }

        public unsafe struct DBProfile
        {
            public string Name;
            public string Filename;
            public int TableCount;
            public uint DbID;
            public int MatchedPages;
            public int OrphanedPages;
            public int FragmentedPages;
            public int EarlyRecordTerms;
            public DBTableDetails[] Tables;
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

        public class DBChunk
        {
            public ushort ChunkNum;
            public ushort NextChunk;
            public ulong ChunkSize;
            public ulong ChunkFree;
            public uint FirstPage;
            public uint Offset;
            public uint Flags;
            public ushort PageSize;
            public ushort DBSpaceNum;
            public string ChunkPath;
        }

        private string _OntapeFile;
        public string OntapeFile
        {
            get { return _OntapeFile; }
            set { _OntapeFile = value; }
        }

        private int _archBlockSize = 32768;
        public int archBlockSize
        {
            get { return _archBlockSize; }
            set { _archBlockSize = value; }
        }

        private uint _archPageSize = 2048;
        public uint archPageSize
        {
            get { return _archPageSize; }
            set { _archPageSize = value; }
        }

        private string _BackupVer;
        public string BackupVer
        {
            get { return _BackupVer; }
            set { _BackupVer = value; }
        }

        private string _BackupDate;
        public string BackupDate
        {
            get { return _BackupDate; }
            set { _BackupDate = value; }
        }

        private int _BlockReadCount = 0;
        private bool _HaveArchiveInfo = false;
        private uint _FoundDBCount = 0;
        private string _sArchInfoText;

        private byte[] _archBlockData;
        private FileStream _inputStream;

        private DBProfile[] _DBProfiles;
        public List<DBSpace> _DBSpaces;
        public List<DBChunk> _DBChunks;
        public List<String> _DBSpacesIncluded;
        public OntapeHeaderObj _OntapeHeader;

        public BackgroundWorker OntapeWorker;

        ASCIIEncoding _encoding = new ASCIIEncoding();

        //private Dictionary<long, long> _PageRefs = new Dictionary<long, long>();
        private Dictionary<long, long> _dicPageRefs = new Dictionary<long, long>();
        private List<long> _idxPageRefs = new List<long>();

        /// <summary>
        /// Calculated - Flag that tracks whether or not we're already reading
        /// </summary>
        public bool InSeq = false;
        /// <summary>
        /// Matched - Index of DB in DB array
        /// </summary>
        public int DBIdx = 0;
        /// <summary>
        /// Matched - Index of Table in DB->table array
        /// </summary>
        public int TableIdx = 0;
        /// <summary>
        ///  Matched - Index of Sequence in DB->table->sequence array
        /// </summary>
        public uint SeqIdx = 0;
        /// <summary>
        ///  Real - Database ID of DB assigned to DBIdx
        /// </summary>
        public uint DbID = 0;
        /// <summary>
        ///  Real - Beginning DbPageID of sequence
        /// </summary>
        public uint SeqPageStart = 0;
        /// <summary>
        ///  Real - Number of DbPages in sequence
        /// </summary>
        public uint SeqPageCount = 0;
        /// <summary>
        ///  Cursor - Number of DbPages already read in sequence
        /// </summary>
        public uint SeqPageCursor = 0;
        /// <summary>
        ///  Matched - Name of DB in DB array
        /// </summary>
        public string DBName = "";
        /// <summary>
        ///  Matched - Name of Table in DB->table array
        /// </summary>
        public string TableName = "";

        public Dictionary<string, int> _dMaxLengths = new Dictionary<string, int>()
        {
            { "DBSpaceName", 128 },
            { "DBOwnerName", 32 }
        };

        public string BytePtrToString(byte* pBytePtr, int iMaxStringLen, bool bFixedLength, bool bTrimWhitespace)
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
            returnString = _encoding.GetString(bStringBytes.ToArray());

            // Trim whitespace if requested
            if (bTrimWhitespace)
            {
                returnString = returnString.Trim();
            }

            return returnString;
        }

        private void PageReaderReset()
        {
            this.InSeq = false;
            this.DBIdx = 0;
            this.TableIdx = 0;
            this.SeqIdx = 0;
            this.DbID = 0;
            this.SeqPageStart = 0;
            this.SeqPageCount = 0;
            this.SeqPageCursor = 0;
            this.DBName = "";
            this.TableName = "";
        }

        private void FullReaderReset()
        {
            _BlockReadCount = 0;
            _HaveArchiveInfo = false;
            _FoundDBCount = 0;
            _sArchInfoText = "";
            _BackupDate = "";
            _BackupVer = "";

            _DBProfiles = new DBProfile[0];
        }

        private DBTableDetails GetTableDetails(DBSystablePage* SystablesEntry, uint pageSize)
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

        //        public PageReader()
        //        {
        //            this.Reset();
        //        }

        /// <summary>
        ///  Resets PageReader state
        /// </summary>

        public bool FirstPage(uint checkDbID, uint checkPageID)
        {
            if (this.DbID == checkDbID && this.SeqPageStart == checkPageID)
                return true;
            else
                return false;
        }

        public bool NextPage(uint checkDbID, uint checkPageID)
        {
            //if (this.DbID == checkDbID && (this.SeqPageStart + this.SeqPageCursor) == checkPageID)
            if (this.DbID == checkDbID && checkPageID < (this.SeqPageStart + this.SeqPageCount))
                return true;
            else
                return false;
        }

        public bool LastPage(uint checkDbID, uint checkPageID)
        {
            if (this.DbID == checkDbID && (this.SeqPageStart + this.SeqPageCount) == (checkPageID + 1))
                return true;
            else
                return false;
        }

        public bool PassedLastPage(uint checkDbID, uint checkPageID)
        {
            if (this.DbID == checkDbID && (this.SeqPageStart + this.SeqPageCount) == checkPageID)
                return true;
            else
                return false;
        }

        public bool FindSequenceStart(uint checkDbID, uint checkPageID)
        {
            bool StartNewSequence = false;

            // Check current index pairs of tables in DBs
            for (int h = 0; h < _DBSpaces.Count(); h++)
            {
                // See if this is the right DB
                if (checkDbID == _DBSpaces[h].DBSpaceNum)
                {
                    for (int i = 0; i < _DBSpaces[h].TableCount; i++)
                    {
                        uint dbPairIndex = _DBSpaces[h].Tables[i].DataBlockPairIndex;
                        if (dbPairIndex < _DBSpaces[h].Tables[i].DataBlockPairCount && _DBSpaces[h].Tables[i].DataBlockMarkers[dbPairIndex].DBPageStart == checkPageID)
                        {
                            if (_DBSpaces[h].Tables[i].TableName == "systables" || _DBSpaces[h].Tables[i].TableName == "syscolumns" || _DBSpaces[h].Tables[i].TableName == "sysdatabases")
                            {
                                if (_DBSpaces[h].Tables[i].TableName != this.TableName)
                                {
                                    this.DBIdx = h;
                                    this.DbID = _DBSpaces[h].DBSpaceNum;
                                    this.TableIdx = i;
                                    this.SeqIdx = _DBSpaces[h].Tables[i].DataBlockPairIndex;
                                    this.DBName = _DBSpaces[h].Name;
                                    this.TableName = _DBSpaces[h].Tables[i].TableName;
                                    this.SeqPageStart = _DBSpaces[h].Tables[i].DataBlockMarkers[SeqIdx].DBPageStart;
                                    this.SeqPageCount = _DBSpaces[h].Tables[i].DataBlockMarkers[SeqIdx].DBPageCount;
                                    this.SeqPageCursor = 0;
                                    this.InSeq = true;
                                    StartNewSequence = true;
                                }
                            }
                            else
                            {
                                // Copy block markers, not data
                            }
                        }
                        if (dbPairIndex + 1 < _DBSpaces[h].Tables[i].DataBlockPairCount && _DBSpaces[h].Tables[i].DataBlockMarkers[dbPairIndex + 1].DBPageStart == checkPageID)
                        {
                            //Console.Write("Index missed increment!\n");
                        }
                    }
                }
            }

            return StartNewSequence;
        }

        /// <summary>
        /// Checks all systable records for sequences that begin within the current sequence's range.
        /// </summary>
        /// <param name="checkDbID"></param>
        /// <param name="checkPageID"></param>
        /// <returns>Corrected sequence length</returns>
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

        public bool DeepDive(uint checkDbID, uint checkPageID)
        {
            bool foundFragment = false;

            // Check current index pairs of tables in DBs
            for (int h = 0; h < _DBSpaces.Count(); h++)
            {
                // See if this is the right DB
                if (checkDbID == _DBSpaces[h].DBSpaceNum)
                {
                    for (int i = 0; i < _DBSpaces[h].TableCount; i++)
                    {
                        for (int j = 0; j < _DBSpaces[h].Tables[i].DataBlockPairCount; j++)
                        {
                            if (checkPageID >= _DBSpaces[h].Tables[i].DataBlockMarkers[j].DBPageStart && checkPageID < _DBSpaces[h].Tables[i].DataBlockMarkers[j].DBPageEnd)
                            {
                                //Console.Write("Found fragmented page [{0:X}] {1:X} belonging to [{5}] {2} {3:X}->{4:X}\n", checkDbID, checkPageID, _DBSpaces[h].Tables[i].TableName, _DBSpaces[h].Tables[i].DataBlockMarkers[j].DBPageStart, _DBSpaces[h].Tables[i].DataBlockMarkers[j].DBPageEnd, _DBSpaces[h].DbID);
                                foundFragment = true;
                            }
                        }
                    }
                }
            }
            return foundFragment;
        }

        public void WritePageData(DBDataPage* dbPage)
        {
            bool doWrite = true;

            /*            
                        if (this.FirstPage(dbPage->DbID, dbPage->DBPageID))
                        {
                            // Found Sequence Start
                            Console.Write("<start of " + this.DBName + "." + this.TableName + ">");
                            Console.Write("\t[{0}] {1:X}, {2} | {3} : {4} | run {5:X}\n", this.DbID, this.SeqPageStart, this.SeqPageCursor, this.SeqIdx, _DBSpaces[this.DBIdx].Tables[this.TableIdx].DataBlockPairIndex, this.SeqPageCount);
                        }
             */

            switch (this.TableName)
            {
                case "systables":
                    doWrite = true;
                    //if (this.FirstPage(dbPage->DbID, dbPage->DBPageID)) {
                    //    Console.Write("\nWriting to [{1:X4}] systables from [{1:X4}] {2:X8}\n", this.DbID, dbPage->DbID, dbPage->DBPageID);
                    //}
                    //                    Console.Write("+");
                    break;
                case "syscolumns":
                    doWrite = true;
                    //                    Console.Write("-");
                    break;
                case "sysdatabases":
                    doWrite = true;
                    //                    Console.Write("*");
                    break;
                default:
                    break;
            }
            if (doWrite)
            {
                DBTableDetails thisTable = _DBSpaces[this.DBIdx].Tables[this.TableIdx];
                if (thisTable.DataBlocksWritten == 0)
                {
                    // Need to initialize data storage
                    uint totalPageCount = 0;
                    for (uint i = 0; i < thisTable.DataBlockPairCount; i++)
                    {
                        // In 10.5 backups, the second syscolumns DataBlockMarker is way over 30,000,000
                        totalPageCount += thisTable.DataBlockMarkers[i].DBPageCount;
                    }
                    if (totalPageCount > 1000000)
                    {
                        string test = "Test";
                    }
                    thisTable.DataBlocks = new DBDataBlockPtr[totalPageCount];
                }
                thisTable.DataBlocks[thisTable.DataBlocksWritten].DataChunk = new byte[2048];
                Marshal.Copy((IntPtr)dbPage, thisTable.DataBlocks[thisTable.DataBlocksWritten].DataChunk, 0, 2048);
                thisTable.DataBlocksWritten++;
            }
        }

        private int ParseOntapeHeader()
        {
            _archBlockData = new byte[_archBlockSize];
            _inputStream.Read(_archBlockData, 0, _archBlockSize);

            fixed (byte* pBlockPtr = _archBlockData)
            {
                _OntapeHeader = new OntapeHeaderObj(pBlockPtr);
                //ParseOntapeHeaderPage(pBlockPtr);
            }
            return 0;
        }

        public unsafe class OntapeHeaderObj
        {
            public string sArchiveType;
            public string sProduct;
            public string sTimestamp;
            public string sUserName;
            public byte bUnknown1;
            public int iUnknown1;
            public string sPipeType;
            public int iHeaderLen;
            public int iUnknown2;
            public int iUnknown3;
            public int iUnknown4;
            public byte bUnknown2;
            public int iPageSize = 2048;
            public int iRecordCount;
            public int iFreeOffset;
            public int iPageType;
            public DBDataPageObj oDataPageObj;

            public OntapeHeaderObj(byte* pPagePtr)
            {
                oDataPageObj = new DBDataPageObj(pPagePtr, iPageSize);
                if (oDataPageObj.ChunkNum == 0xffff)
                {
                    if (oDataPageObj.CheckSum == 0x0000)
                    {
                        // This is an Ontape Top Header page
                        iPageType = 1;
                        sArchiveType = Encoding.UTF8.GetString(oDataPageObj.RecordArrays[0]);
                        sProduct = Encoding.UTF8.GetString(oDataPageObj.RecordArrays[1]);
                        sTimestamp = Encoding.UTF8.GetString(oDataPageObj.RecordArrays[2]);
                        sUserName = Encoding.UTF8.GetString(oDataPageObj.RecordArrays[3]);
                        bUnknown1 = oDataPageObj.RecordArrays[4][0];
                        iUnknown1 = BitConverter.ToInt32(oDataPageObj.RecordArrays[5], 0);
                        sPipeType = Encoding.UTF8.GetString(oDataPageObj.RecordArrays[6]);
                        iHeaderLen = BitConverter.ToInt32(oDataPageObj.RecordArrays[7], 0);
                        iRecordCount = oDataPageObj.RecordCount;
                        iFreeOffset = oDataPageObj.FreeOffset;
                    }
                    else if (oDataPageObj.CheckSum == 0xffff)
                    {
                        // This is an Ontape trailer page
                        iPageType = 2;
                    }
                    else
                    {
                        // This is an Ontape header page
                        iPageType = 3;
                    }
                }
                else if (oDataPageObj.ChunkNum > 0)
                {
                    // This is a chunk page
                    iPageType = 4;
                    iRecordCount = oDataPageObj.RecordCount;
                    iFreeOffset = oDataPageObj.FreeOffset;
                }
                else
                {
                    // This is an empty page
                    iPageType = 0;
                }
            }
        }

        /*
        private int ParseOntapeHeaderPage(byte* pBlockPtr)
        {
            OnTapePreHeader* OPHeader = (OnTapePreHeader*)pBlockPtr;
            // Get text starting at ArchiveInfoStart, ending with 0x00
            byte* bytePtr = &OPHeader->ArchiveInfoStart;
            int archInfoLen = 0;
            List<byte> bArchInfoText = new List<byte>();
            while (*bytePtr != 0x00)
            {
                bArchInfoText.Add(*bytePtr);
                archInfoLen++;
                bytePtr++;
            }

            // Copy byte array to string in _sArchInfoText
            _sArchInfoText = Encoding.UTF8.GetString(bArchInfoText.ToArray());

            var regex = new Regex(@"Archive Backup Tape(.*)(\w{3} \w{3} .{2} \d{2}:\d{2}:\d{2} \d{4})informix\?$");
            var match = regex.Match(_sArchInfoText);
            _BackupVer = match.Groups[1].Value;
            _BackupVer.Trim();
            _BackupDate = match.Groups[2].Value;
            _BackupDate.Trim();

            Console.Write("Backup Version: " + _BackupVer + "\n");
            Console.Write("Backup Date   : " + _BackupDate + "\n");

            _HaveArchiveInfo = true;
            return 0;
        }
        */

        private int ParseDBSpaceList()
        {
            // Get the list of DBSpaces actually included in the backup.  Temp DBSpaces and associated chunks
            // may be referenced later but not actually included in the backup.
            //this._inputStream.Seek(0, SeekOrigin.Begin);
            _DBSpacesIncluded = new List<string>();
            bool foundEnd = false;
            do
            {
                _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
                fixed (byte* pBlockPtr = _archBlockData)
                {
                    DBSpaceListPage* dPage = (DBSpaceListPage*)pBlockPtr;
                    if (dPage->PageNumber == 0xFFFFFFFF && dPage->ChunkNum == 0xFFFF && dPage->RecordCount > 0)
                    {
                        for (int i = 0; i < dPage->RecordCount; i++)
                        {
                            // Set the pointer to the beginning of the name
                            byte* bytePtr = &dPage->DataStart + (i * _dMaxLengths["DBSpaceName"]);
                            string sDBSpaceName = BytePtrToString(bytePtr, _dMaxLengths["DBSpaceName"], false, true);
                            _DBSpacesIncluded.Add(sDBSpaceName);
                        }
                    }
                    else
                    {
                        foundEnd = true;
                    }
                }
            } while (foundEnd == false);

            return 0;
        }

        private int ParseRootDBSHeader()
        {
            // RootDBS Page 1 - Informix version info
            _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
            fixed (byte* pBlockPtr = _archBlockData)
            {
                // Create page object
                DBDataPage* dPage = (DBDataPage*)pBlockPtr;
                for (uint j = 0; j < dPage->RecordCount; j++)
                {
                    ushort* tmpIndexPtr = (ushort*)((byte*)dPage + _archPageSize - 4);
                    tmpIndexPtr -= j * 2;
                    tmpIndexPtr--;
                    ushort RecordSize = *tmpIndexPtr;
                    tmpIndexPtr--;
                    byte* PagePtr = (byte*)dPage + *tmpIndexPtr;

                    if (*tmpIndexPtr == 0) continue;

                    // We have the start position for the data.
                    string archInfo = Marshal.PtrToStringAnsi((IntPtr)PagePtr);
                    Console.Write("Archive Info: {0}\n", archInfo);
                    //PagePtr += archInfo.Length + 1;

                    // Get Archive ID ?
                }
            }

            // RootDBS Page 2 - Config Values
            _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
            fixed (byte* pBlockPtr = _archBlockData)
            {
                DBDataPage* dPage = (DBDataPage*)pBlockPtr;
                for (uint j = 0; j < dPage->RecordCount; j++)
                {
                    ushort* tmpIndexPtr = (ushort*)((byte*)dPage + _archPageSize - 4);
                    tmpIndexPtr -= j * 2;
                    tmpIndexPtr--;
                    ushort RecordSize = *tmpIndexPtr;
                    tmpIndexPtr--;
                    byte* PagePtr = (byte*)dPage + *tmpIndexPtr;

                    if (*tmpIndexPtr == 0) continue;

                    // We have the start position for the data.
                    string archCfgVal = Marshal.PtrToStringAnsi((IntPtr)PagePtr);
                    Console.Write("Config Val: {0}\n", archCfgVal);
                }
            }

            // RootDBS Pages 3 & 4 - Lengths?
            _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
            _inputStream.Read(_archBlockData, 0, (int)_archPageSize);

            // RootDBS Pages 5-8 - DBSpaces
            ParseDBSpaceDefs();

            // RootDBS Pages 9-12 - Chunks
            ParseDBChunkDefs();
            /*
            _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
            _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
            _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
            _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
            */

            bool foundEnd = false;
            do
            {
                _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
                fixed (byte* pBlockPtr = _archBlockData)
                {
                    DBDataPage* dPage = (DBDataPage*)pBlockPtr;
                    if (dPage->PageNumber == 0xFFFFFFFF && dPage->ChunkNum == 0xFFFF)
                    {
                        foundEnd = true;
                    }
                }
            } while (foundEnd == false);

            return 0;
        }

        private int ParseDBSpaceDefs()
        {
            bool doneDBSdefs = false;
            int totalDBSpaces = 0;
            _DBSpaces = new List<DBSpace>();
            do
            {
                _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
                fixed (byte* pBlockPtr = _archBlockData)
                {
                    DBDataPage* dPage = (DBDataPage*)pBlockPtr;
                    for (uint j = 0; j < dPage->RecordCount; j++)
                    {
                        ushort* tmpIndexPtr = (ushort*)((byte*)dPage + _archPageSize - 4);
                        tmpIndexPtr -= j * 2;
                        tmpIndexPtr--;
                        ushort RecordSize = *tmpIndexPtr;
                        tmpIndexPtr--;
                        byte* PagePtr = (byte*)dPage + *tmpIndexPtr;

                        if (*tmpIndexPtr == 0) continue;

                        // Cast record as struct
                        DBSpaceRec* thisDBSpaceDef = (DBSpaceRec*)PagePtr;

                        // Get DBSpace Name
                        string sDBSpaceName = BytePtrToString(thisDBSpaceDef->DBSpaceName, _dMaxLengths["DBSpaceName"], false, true);

                        // Get DBSpace Owner
                        string sDBOwnerName = BytePtrToString(thisDBSpaceDef->DBOwnerName, _dMaxLengths["DBOwnerName"], false, true);

                        DBSpace newSpace = new DBSpace
                        {
                            TableCount = 0,
                            Tables = new List<DBTableDetails>(),
                            Name = sDBSpaceName,
                            Owner = sDBOwnerName,
                            DBSpaceNum = thisDBSpaceDef->DBSpaceNum,
                            PageSize = thisDBSpaceDef->PageSize,
                            FirstChunk = thisDBSpaceDef->FirstChunk,
                            Flags = thisDBSpaceDef->Flags
                        };
                        _DBSpaces.Add(newSpace);

                        Console.Write("Space: {0}.{1}, page size {2}\n", sDBOwnerName, sDBSpaceName, thisDBSpaceDef->PageSize);
                        totalDBSpaces++;
                    }

                    if (dPage->NextPage == 0x00000000)
                    {
                        doneDBSdefs = true;
                    }
                }
            } while (doneDBSdefs == false);

            return 0;
        }

        private int ParseDBChunkDefs()
        {
            bool doneDBSdefs = false;
            int totalDBSpaces = 0;
            bool bReachedChunks = false;
            _DBChunks = new List<DBChunk>();
            do
            {
                _inputStream.Read(_archBlockData, 0, (int)_archPageSize);
                fixed (byte* pBlockPtr = _archBlockData)
                {
                    DBDataPageObj dPageObj = new DBDataPageObj(pBlockPtr, (int)_archPageSize);
                    if (!bReachedChunks)
                    {
                        if (dPageObj.PageNumber == 0x000006 & dPageObj.ChunkNum == 0x0001)
                        {
                            bReachedChunks = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else {
                        if (dPageObj.PageNumber == 0x000007 & dPageObj.ChunkNum == 0x0001) {
                            break;
                        }
                    }

                    for (uint j = 0; j < dPageObj.RecordCount; j++)
                    {
                        fixed (byte* RecStartPtr = dPageObj.RecordArrays[j])
                        {
                            // Cast record as struct
                            DBChunkRec* thisDBChunkDef = (DBChunkRec*)RecStartPtr;

                            // Get DBChunk Name
                            string sDBChunkPath = BytePtrToString(thisDBChunkDef->ChunkPath, thisDBChunkDef->ChunkPathLen, false, true);

                            DBChunk newChunk = new DBChunk
                            {
                                ChunkNum = thisDBChunkDef->ChunkNum,
                                NextChunk = thisDBChunkDef->NextChunk,
                                ChunkSize = thisDBChunkDef->ChunkSize,
                                ChunkFree = thisDBChunkDef->ChunkFree,
                                FirstPage = thisDBChunkDef->FirstPage,
                                Offset = thisDBChunkDef->Offset,
                                Flags = thisDBChunkDef->Flags,
                                PageSize = thisDBChunkDef->PageSize,
                                DBSpaceNum = thisDBChunkDef->DBSpaceNum,
                                ChunkPath = sDBChunkPath
                            };
                            _DBChunks.Add(newChunk);
                            Console.Write("Chunk: [{0}] {1}, page size {2}\n", newChunk.ChunkNum, newChunk.ChunkPath, newChunk.PageSize);
                        }
                    }
                    /*
                    DBDataPage* dPage = (DBDataPage*)pBlockPtr;
                    for (uint j = 0; j < dPage->RecordCount; j++)
                    {
                        ushort* tmpIndexPtr = (ushort*)((byte*)dPage + _archPageSize - 4);
                        tmpIndexPtr -= j * 2;
                        tmpIndexPtr--;
                        ushort RecordSize = *tmpIndexPtr;
                        tmpIndexPtr--;
                        byte* PagePtr = (byte*)dPage + *tmpIndexPtr;

                        if (*tmpIndexPtr == 0) continue;

                        // Cast record as struct
                        DBChunkRec* thisDBChunkDef = (DBChunkRec*)PagePtr;

                        // Get DBChunk Name
                        string sDBChunkPath = BytePtrToString(thisDBChunkDef->ChunkPath, thisDBChunkDef->ChunkPathLen, false, true);

                        DBChunk newChunk = new DBChunk
                        {
                            ChunkNum = thisDBChunkDef->ChunkNum,
                            NextChunk = thisDBChunkDef->NextChunk,
                            ChunkSize = thisDBChunkDef->ChunkSize,
                            ChunkFree = thisDBChunkDef->FirstPage,
                            Offset = thisDBChunkDef->Offset,
                            Flags = thisDBChunkDef->Flags,
                            PageSize = thisDBChunkDef->PageSize,
                            DBSpaceNum = thisDBChunkDef->DBSpaceNum,
                            ChunkPath = sDBChunkPath
                        };
                        _DBChunks.Add(newChunk);

                        Console.Write("Chunk: [{0}] {1}, page size {2}\n", newChunk.ChunkNum, newChunk.ChunkPath, newChunk.PageSize);
                        totalDBSpaces++;
                    }
                    */

                    if (dPageObj.NextPage == 0x00000000)
                    {
                        doneDBSdefs = true;
                    }
                }
            } while (doneDBSdefs == false);
            _inputStream.Seek(0x18000, SeekOrigin.Begin);
            return 0;
        }

        private int ParseTblSpaces()
        {
            int bytesRead = 0;
            long totalBytesRead = 0;
            uint curPageSize = _archPageSize;
            bool gotRootTables = false;
            bool gotMatch = false;
            int matchIdx = 0;

            // Go to beginning of file (should this be the beginning of the chunk instead?)
            //_inputStream.Seek(0, SeekOrigin.Begin);
            //while (!gotRootTables && (bytesRead = _inputStream.Read(_archBlockData, 0, _archBlockSize)) > 0)
            while ((bytesRead = _inputStream.Read(_archBlockData, 0, _archBlockSize)) > 0)
            {
                totalBytesRead += bytesRead;
                int pageCounter = 0;
                fixed (byte* pBlockPtr = _archBlockData)
                {
                    DBDataHeader* BlockHeader = (DBDataHeader*)pBlockPtr;

                    if (BlockHeader->PageNumber == 0xFFFFFFFF && BlockHeader->ChunkNum == 0xFFFF && BlockHeader->BlockNum == 0xFFFF)
                    {
                        curPageSize = _archPageSize;
                    }

                    if (BlockHeader->PageNumber == 0xFFFFFFFF && BlockHeader->ChunkNum == 0xFFFF && (BlockHeader->BlockType1 & 0xFFFF00FF) == 0x40100003 && (BlockHeader->BlockType2 & 0x07FFFFFF) == 0x05880268)
                    {
                        // Found a database
                        if (_FoundDBCount > 0)
                        { // This is temporary; to bypass databases other than rootdbs
                            gotRootTables = true;
                            //continue;
                        }
                        byte* bytePtr = BlockHeader->DBHeader.DBName;
                        List<byte> bDBSpaceName = new List<byte>();
                        while (*bytePtr != 0x20)
                        {
                            bDBSpaceName.Add(*bytePtr);
                            bytePtr++;
                        }

                        string dbSpaceName = _encoding.GetString(bDBSpaceName.ToArray());

                        // Let's see which of the dbspaces this matches.  Need to apply the page size.
                        gotMatch = false;

                        for (int l = 0; l < _DBSpaces.Count(); l++)
                        {
                            if (_DBSpaces[l].Name == dbSpaceName)
                            {
                                gotMatch = true;
                                matchIdx = l;
                            }
                        }
                        if (gotMatch)
                        {

                            //bDBName.Add(0x00);
                            //_DBSpaces[i].Name = ascEncoding.GetString(bDBName.ToArray());
                            // Copy byte array to string in DB Name array
                            //_DBSpaces[matchIdx].Name = Encoding.UTF8.GetString(bDBSpaceName.ToArray());
                            //_DBProfiles[_FoundDBCount].Name = ascEncoding.GetString(bDBName.ToArray());
                            DBSpace matchedSpace = _DBSpaces[(int)matchIdx];
                            matchedSpace.DBSpaceNum = BlockHeader->DBHeader.DbID;
                            matchedSpace.MatchedPages = 0;
                            matchedSpace.EarlyRecordTerms = 0;
                            matchedSpace.OrphanedPages = 0;
                            matchedSpace.FragmentedPages = 0;

                            //Console.Write("\nFound start of database '" + _DBProfiles[_FoundDBCount].Name + "' at block " + _BlockReadCount + "\n");
                            Console.Write("\nFound start of DBSpace '{0}'\n", dbSpaceName);
                            if (matchedSpace.PageSize > curPageSize)
                            {
                                curPageSize = matchedSpace.PageSize;
                            }
                        }
                        else
                        {
                            Console.Write("No match for DBSpace '{0}'\n", dbSpaceName);
                        }
                        _FoundDBCount++;
                    }

                    while (pageCounter < (_archBlockSize / curPageSize))
                    {
                        /*
                           Calculate the page's start position in the file.
                           pageStartInStream = currentStreamPosition - last read count + (page index in block * page size)
                        */
                        long pageStartInStream = _inputStream.Position - bytesRead + (pageCounter * curPageSize);

                        // Set pDataPage to address of current page
                        DBDataPage* pDataPage = (DBDataPage*)(pBlockPtr + (pageCounter * curPageSize));

                        long blockIndex = pDataPage->ChunkNum * 0x100000000 + pDataPage->PageNumber;
                        _idxPageRefs.Add(blockIndex);
                        _dicPageRefs[blockIndex] = pageStartInStream;
                        if (pDataPage->PageNumber == 0x000005c6)
                        {
                            Console.Write("");
                        }
                        // Parse page Data
                        ReadPage(matchIdx, pDataPage, curPageSize, (uint)pageCounter);
                        pageCounter++;
                    }
                }
                if ((_inputStream.Position & 0xFFFFF) == 0)
                {
                    int readProgress = (int)((_inputStream.Position * 100) / _inputStream.Length);
                    OntapeWorker.ReportProgress(readProgress);
                }
            }
            return 0;
        }

        private void ReadPage(int dbIndex, DBDataPage* dbPage, uint pageSize, uint pageIndex)
        {
            //            if (dbPage->DbID == 0x0002 && (dbPage->DBPageID == 0x00000227 || dbPage->DBPageID == 0x00000AFB))
            //            {
            //                Console.Write("Found {5:X8} @ 0x0002.  InSeq->[{0}], curTable='{1}'[{2:X8}][{3:X8}] -> {4:X8}", InSeq, this.TableName, this.SeqPageStart, this.SeqPageCursor, this.SeqPageCount, dbPage->DBPageID);
            //            }

            //if ((dbPage->PageType & 0x00FF) == 0x02)
            if (dbPage->Flags == 0x0802)
            {
                // Process as Systables page
                DBSystablePage* pSystablePage = (DBSystablePage*)dbPage;

                while (_DBSpaces[this.DBIdx].Tables.Count < _DBSpaces[dbIndex].TableCount + 1)
                {
                    _DBSpaces[this.DBIdx].Tables.Add(new DBTableDetails());
                }
                _DBSpaces[dbIndex].Tables[_DBSpaces[dbIndex].TableCount] = GetTableDetails(pSystablePage, pageSize);

                DBTableDetails tmpTable = _DBSpaces[dbIndex].Tables[_DBSpaces[dbIndex].TableCount];

                string tmpTableName = tmpTable.TableName;
                //if (tmpTableName == "device" || tmpTableName == "systables" || tmpTableName == "syscolumns" || tmpTableName == "sysdatabases") {
                if (dbPage->ChunkNum == 0x0001) {
                    bool breakLine = true;
                }
                if (tmpTable.TableType == 0x0A)
                {
                    Console.Write("Found {0}.{1} ({2}) size {3} @ {4}, {5}\n", tmpTable.TableDBInstance, tmpTableName, tmpTable.PartNum, pageSize, _inputStream.Position - _archBlockSize, pageIndex);
                }
                //Console.Write("\t[{0}] {1:X}, {2} | {3} : {4} | run {5:X}\n", this.DbID, this.SeqPageStart, this.SeqPageCursor, this.SeqIdx, _DBSpaces[this.DBIdx].Tables[this.TableIdx].DataBlockPairIndex, this.SeqPageCount);
                //}

                _DBSpaces.ElementAt(0).TableCount++;
            }

            if (dbPage->PageNumber == 0xFFFFFFFF)
            {
                while (_DBSpaces[this.DBIdx].Tables.Count < this.TableIdx + 1)
                {
                    _DBSpaces[this.DBIdx].Tables.Add(new DBTableDetails());
                }
                _DBSpaces[this.DBIdx].Tables[this.TableIdx].DataBlockPairIndex++;
                PageReaderReset();
                _DBSpaces[0].EarlyRecordTerms++;
            }

            // Normal page processing
            if (this.InSeq && PassedLastPage(dbPage->ChunkNum, dbPage->PageNumber))
            {
                _DBSpaces[this.DBIdx].Tables[this.TableIdx].DataBlockPairIndex++;
                PageReaderReset();
            }

            //if (! this.InSeq) {

            if (this.InSeq)
            {
                int oldTableIdx = this.TableIdx;
                int oldDBIdx = this.DBIdx;
                uint oldSeqPageStart = this.SeqPageStart;
                uint oldSeqPageCursor = this.SeqPageCursor;
                if (this.FindSequenceStart(dbPage->ChunkNum, dbPage->PageNumber))
                {
                    Console.Write("Broke sequence [{0:X}] {1:X},{2:X} in favor of [{0:X}]  {3:X},{4:X}\n", dbPage->ChunkNum, oldSeqPageStart, oldSeqPageCursor, this.SeqPageStart, this.SeqPageCursor);
                    _DBSpaces[oldDBIdx].Tables[oldTableIdx].DataBlockPairIndex++;
                    //if (dbPage->DbID == 0x0002 && dbPage->DBPageID == 0x00000AFB)
                    //{
                    //    Console.Write("Manual find of systables\n");
                    //}
                }
                // Found Sequence Start
                //Console.Write("<start of " + this.DBName + "." + this.TableName + ">");
                //Console.Write("\t[{0}] {1:X}, {2} | {3} : {4} | run {5:X}\n", this.DbID, this.SeqPageStart, this.SeqPageCursor, this.SeqIdx, _DBSpaces[this.DBIdx].Tables[this.TableIdx].DataBlockPairIndex, this.SeqPageCount);
            }
            else
            {
                this.FindSequenceStart(dbPage->ChunkNum, dbPage->PageNumber);
                //if (dbPage->DbID == 0x0002 && dbPage->DBPageID == 0x00000AFB)
                //{
                //Console.Write("Could not start reading systables @ 0x00000AFB!\n");
                //}
            }
            //}

            if (this.InSeq)
            {
                if (!this.NextPage(dbPage->ChunkNum, dbPage->PageNumber))
                {
                    // Prematurely ended sequence!
                    _DBSpaces[this.DBIdx].Tables[this.TableIdx].DataBlockPairIndex++;
                    //if (this.FindSequenceStart(_DBSpaces, dbPage->DbID, dbPage->DBPageID))
                    //{
                    //    Console.Write("\tPicked up at {0}[{1}] -> {2} @ {3:X}, {4:X}\n", this.TableName, this.SeqIdx, this.SeqPageCursor, this.SeqPageStart, dbPage->DBPageID);
                    //} else {
                    string tblName = this.TableName;
                    uint seqPageStart = this.SeqPageStart;
                    uint seqPageCursor = this.SeqPageCursor;

                    PageReaderReset();
                    if (this.FindSequenceStart(dbPage->ChunkNum, dbPage->PageNumber))
                    {
                        //Console.Write("\tPicked up at {0}[{1}] -> {2} @ {3:X}, {4:X}\n", this.TableName, this.SeqIdx, this.SeqPageCursor, this.SeqPageStart, dbPage->DBPageID);
                    }
                    else
                    {
                        _DBSpaces[0].EarlyRecordTerms++;
                        //Console.Write("\tPrematurely ended without restart... {2} {0:X} != {1:X}\n", seqPageStart + seqPageCursor, dbPage->DBPageID, tblName);
                    }
                    //}
                }

                // Is this page the next in the sequence?
                if (this.NextPage(dbPage->ChunkNum, dbPage->PageNumber))
                {

                    // Write data to target

                    // See if this is the first or last page
                    if (this.FirstPage(dbPage->ChunkNum, dbPage->PageNumber))
                    {
                        // Beginning of sequence
                        this.WritePageData(dbPage);
                    }
                    else if (this.LastPage(dbPage->ChunkNum, dbPage->PageNumber))
                    {
                        // End of sequence
                        this.WritePageData(dbPage);
                        _DBSpaces[this.DBIdx].Tables[this.TableIdx].DataBlockPairIndex++;
                        PageReaderReset();
                    }
                    else
                    {
                        // Mid sequence
                        this.WritePageData(dbPage);
                    }

                    this.SeqPageCursor++;
                    _DBSpaces[0].MatchedPages++;
                }
            }
            else
            {

                //                if (this.DeepDive(dbPage->DbID, dbPage->DBPageID))
                //                {
                //                    _DBSpaces[0].FragmentedPages++;
                //                    //Console.Write("Found fragmented page -> [{0:X}] {1:X}\n", dbPage->DbID, dbPage->DBPageID);
                //                }
                //                else
                //                {
                //                    // Could not find in a sequence - orphaned data?
                //                    _DBSpaces[0].OrphanedPages++;
                //                }                
            }

        }

        private void ProcessDBStructures()
        {
            // Loop over DBs
            for (int i = 0; i < _DBSpaces.Count(); i++)
            {
                _DBSpaces[i].TableDictionary = new SortedDictionary<string, DBTableDetails>();
                // Loop over Tables - look for all "systables"
                for (int j = 0; j < _DBSpaces[i].TableCount; j++)
                {
                    if (_DBSpaces[i].Tables[j].TableName == "sysdatabases")
                    {
                    }

                    if (_DBSpaces[i].Tables[j].TableName == "systables")
                    {
                        // Get the DB Instance then loop over all tables again.
                        // Each systables is associated with an instance.
                        DBTableDetails thisSysTables = _DBSpaces[i].Tables[j];
                        string checkInstance = thisSysTables.TableDBInstance;

                        bool gotColTable = false;
                        DBTableDetails colTable = thisSysTables;

                        // Change to FOR loop and assign colTable as _DBProfiles[i].Tables[k]
                        for (int k = 0; k < _DBSpaces[i].TableCount; k++)
                        {
                            _DBSpaces[i].Tables[k].ParentOntapeReader = this;
                            //if (_DBSpaces[i].Tables[k].TableDBInstance == checkInstance && _DBSpaces[i].Tables[k].TableName == "syscolumns")
                            if (_DBSpaces[i].Tables[k].TableName == "syscolumns")
                            {
                                colTable = _DBSpaces[i].Tables[k];
                                gotColTable = true;
                            }
                        }

                        if (gotColTable)
                        {
                            foreach (DBTableDetails checkTable in _DBSpaces[i].Tables)
                            {
                                if (checkTable.TableDBInstance == checkInstance)
                                {
                                    //Console.Write(".");
                                    if (GetSystableInfo(thisSysTables, checkTable, _DBSpaces[i].PageSize))
                                    {
                                        GetSyscolumnInfo(colTable, checkTable, _DBSpaces[i].PageSize);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool GetSystableInfo(DBTableDetails sysTable, DBTableDetails checkTable, uint pageSize)
        {
            bool gotDetails = false;
            for (uint i = 0; i < sysTable.DataBlocksWritten; i++)
            {
                fixed (byte* tmpPtr = sysTable.DataBlocks[i].DataChunk)
                {
                    DBDataPage* sysPage = (DBDataPage*)tmpPtr;
                    if (((sysPage->Flags & 0x00FF) == 0x0001 || (sysPage->Flags & 0x00FF) == 0x0009))
                    {
                        //Console.Write(".");
                        for (uint j = 0; j < sysPage->RecordCount; j++)
                        {
                            ushort* tmpIndexPtr = (ushort*)((byte*)sysPage + pageSize - 4);
                            tmpIndexPtr -= j * 2;
                            tmpIndexPtr--;
                            ushort RecordSize = *tmpIndexPtr;
                            tmpIndexPtr--;
                            byte* PagePtr = (byte*)sysPage + *tmpIndexPtr;

                            if (*tmpIndexPtr == 0) continue;

                            uint TableNameLen = *PagePtr;
                            PagePtr += 1;			// Add tablename length byte
                            PagePtr += TableNameLen;		// Add tablename length
                            PagePtr += 8;			// Add 'informix'
                            PagePtr += 24;			// Add 24 spaces
                            TableRecDetails* TableRec = (TableRecDetails*)PagePtr;
                            PagePtr += sizeof(TableRecDetails);
                            PagePtr -= 2;
                            uint tmpNum = ReverseInt32(TableRec->PartNum);
                            //                        if (checkTable.TableName == "device" && sysPage->DBPageID == 0x00004B09)
                            //                        {
                            //                            uint bob = 1;
                            //                        }
                            if (tmpNum == checkTable.PartNum)
                            {
                                checkTable.DBSpaceID = sysPage->ChunkNum;
                                checkTable.TableID = TableRec->TableID;
                                checkTable.NumCols = (uint)ReverseInt16(TableRec->NumCols);
                                //Console.Write("Found systables entry for {0}.{1} TableID[{2:X}], PartNum[{3:X}]\n", checkTable.TableDBInstance, checkTable.TableName, checkTable.TableID, checkTable.PartNum);
                                gotDetails = true;
                                checkTable.DBColumns = new DBColumnRecord[checkTable.NumCols];
                                //printf("\tID's\t[0x%08X]\t[0x%08X]\t[0x%08X]\t", TableRec->PartNum, tmpNum, TableRec->TableID);
                                //printf("Cols: %d\tBlock: [0x%08X]\tNameLen: [0x%08X]\n", checkTable.NumCols, sysPage->DBPageID, TableNameLen);
                            }
                        }
                    }
                }
            }
            return gotDetails;
        }

        private bool GetSyscolumnInfo(DBTableDetails colTable, DBTableDetails checkTable, uint pageSize)
        {
            bool gotDetails = true;
            for (uint i = 0; i < colTable.DataBlocksWritten; i++)
            {
                fixed (byte* tmpPtr = colTable.DataBlocks[i].DataChunk)
                {
                    DBDataPage* colPage = (DBDataPage*)tmpPtr;
                    if ((colPage->Flags & 0x00FF) == 0x0001 &&
                        colPage->RecordCount > 0 &&
                        checkTable.NumCols > 0)
                    {
                        //Console.Write("*");
                        for (uint j = 0; j < colPage->RecordCount; j++)
                        {
                            ushort* tmpIndexPtr = (ushort*)((byte*)colPage + pageSize - 4);
                            tmpIndexPtr -= j * 2;
                            tmpIndexPtr--;
                            ushort RecordSize = *tmpIndexPtr;
                            tmpIndexPtr--;
                            byte* PagePtr = (byte*)colPage + *tmpIndexPtr;

                            if (*tmpIndexPtr == 0) continue;

                            int itemOffset = (int)PagePtr - (int)colPage;

                            byte ColNameLen = *PagePtr;

                            PagePtr += 1;                      // Add tablename length byte

                            byte[] bColname = new byte[ColNameLen];
                            Marshal.Copy((IntPtr)PagePtr, bColname, 0, ColNameLen);
                            string ColName = Encoding.UTF8.GetString(bColname);

                            PagePtr += ColNameLen;           // Add tablename length

                            DBColumnDetails* ColRec = (DBColumnDetails*)PagePtr;
                            PagePtr += sizeof(DBColumnDetails);
                            PagePtr -= 2;

                            //Console.Write("Cols on DbPageID {0:X}[{3:X}] - Reading items {4} (@{5}) for TableID [{1}] -> '{2}'\n", colPage->DBPageID, colTable.TableID, ColName, colPage->RecordCount, j, itemOffset);
                            if (colPage->PageNumber == 0x00005081 && colPage->ChunkNum == 0x0006)
                            {
                                Console.Write("");
                            }
                            //if (ReverseInt32(ColRec->TableID) == checkTable.TableID)
                            if (ColRec->TableID == checkTable.TableID)
                            {
                                ushort colIndex = ReverseInt16(ColRec->colno);
                                colIndex--;
                                if (colPage->PageNumber == 0x00007D88 && colPage->ChunkNum == 0x0006)
                                {
                                    Console.Write("");
                                }
                                if (colIndex < checkTable.NumCols)
                                {
                                    checkTable.OverflowRecord = 0;
                                    //checkTable.DBColumns[colIndex] = new DBColumnRecord();
                                    checkTable.DBColumns[colIndex].colname = ColName;
                                    checkTable.DBColumns[colIndex].coldetails.colno = ReverseInt16(ColRec->colno);
                                    checkTable.DBColumns[colIndex].coldetails.coltype = ReverseInt16(ColRec->coltype);
                                    checkTable.DBColumns[colIndex].coldetails.collength = ReverseInt16(ColRec->collength);
                                    checkTable.DBColumns[colIndex].coldetails.colmin = ReverseInt32(ColRec->colmin);
                                    checkTable.DBColumns[colIndex].coldetails.colmax = ReverseInt32(ColRec->colmax);
                                    checkTable.DBColumns[colIndex].coldetails.extended_id = ReverseInt32(ColRec->extended_id);
                                    //printf("\tnextRecStart[0x%04X]\t--> (%d) %s \n", ptrOffset, ColNameLen, ColName);
                                    //printf("\tColno: [%04d] Type: <0x%04X> ModType: <0x%04X> Len: {%06d} %s\n", ReverseInt16(ColRec->colno), ReverseInt16(ColRec->coltype), ReverseInt16(ColRec->coltype) & 0xFEFF, ReverseInt16(ColRec->collength), ColName);
                                    //Console.Write("Cols on DbPageID {0:X}[{3:X}] - Reading items {4} (@{5}) for TableID [{1:X}] -> '{2}'\n", colPage->DBPageID, colTable.TableID, ColName, colPage->RecordCount, j, itemOffset);
                                    //                                    if (checkTable.TableName == "device")
                                    //                                    {
                                    //                                        Console.Write("");
                                    //                                    }
                                }
                                else Console.Write("Table {0} has too many columns!\n", checkTable.TableName);
                            }
                        }
                    }
                }
            }
            return gotDetails;
        }

        public static DBValue GetValue(ushort colType, ushort colLength, byte* dataPtr)
        {
            DBValue thisDBVal = new DBValue();
            thisDBVal.isNull = 0;
            thisDBVal.dataLen = 0;
            uint dataLen = 0;
            // Get Data Length
            switch (colType & 0xFEFF)
            {
                case 0x0000:
                    {
                        // CHAR
                        dataLen = colLength;
                        thisDBVal.dataType = 0;
                        break;
                    }
                case 0x0001:
                    {
                        // SMALLINT
                        dataLen = colLength;
                        thisDBVal.dataType = 1;
                        break;
                    }
                case 0x0002:
                    {
                        // INTEGER
                        dataLen = colLength;
                        thisDBVal.dataType = 2;
                        break;
                    }
                case 0x0003:
                    {
                        // FLOAT
                        dataLen = colLength;
                        thisDBVal.dataType = 3;
                        break;
                    }
                case 0x0004:
                    {
                        // SMALLFLOAT
                        dataLen = colLength;
                        thisDBVal.dataType = 4;
                        break;
                    }
                case 0x0005:
                    {
                        // DECIMAL
                        dataLen = colLength;
                        thisDBVal.dataType = 5;
                        break;
                    }
                case 0x0006:
                    {
                        // SERIAL
                        dataLen = colLength;
                        thisDBVal.dataType = 2;
                        break;
                    }
                case 0x0007:
                    {
                        // DATE
                        dataLen = colLength;
                        thisDBVal.dataType = 7;
                        break;
                    }
                case 0x0008:
                    {
                        // MONEY
                        dataLen = colLength;
                        thisDBVal.dataType = 8;
                        break;
                    }
                case 0x0009:
                    {
                        // NULL
                        dataLen = colLength;
                        thisDBVal.dataType = 9;
                        break;
                    }
                case 0x000A:
                    {
                        // DATETIME
                        dataLen = colLength;
                        thisDBVal.dataType = 10;
                        break;
                    }
                case 0x000B:
                    {
                        // BYTE
                        break;
                    }
                case 0x000C:
                    {
                        // TEXT
                        break;
                    }
                case 0x000D:
                    {
                        // VARCHAR
                        thisDBVal.dataType = 0;
                        dataLen = *dataPtr;
                        dataPtr++;
                        break;
                    }
                case 0x000E:
                    {
                        // INTERVAL
                        break;
                    }
                case 0x000F:
                    {
                        // NCHAR
                        dataLen = colLength;
                        break;
                    }
                case 0x0010:
                    {
                        // NVARCHAR
                        dataLen = *dataPtr;
                        dataPtr++;
                        break;
                    }
                case 0x0011:
                    {
                        // INT8
                        dataLen = colLength;
                        break;
                    }
                case 0x0012:
                    {
                        // SERIAL8
                        dataLen = colLength;
                        break;
                    }
                case 0x0013:
                    {
                        // SET
                        break;
                    }
                case 0x0014:
                    {
                        // MULTISET
                        break;
                    }
                case 0x0015:
                    {
                        // LIST
                        break;
                    }
                case 0x0016:
                    {
                        // Unnamed ROW
                        break;
                    }
                case 0x0028:
                    {
                        // Variable-Length
                        //printf("\tStart: 0x%08X", PagePtr - PageStart);
                        thisDBVal.dataType = 0;
                        dataLen |= (uint)*dataPtr << 8;
                        dataPtr++;
                        dataLen |= *dataPtr;
                        dataPtr++;
                        thisDBVal.isNull = *dataPtr;
                        dataPtr++;
                        if (dataLen > 0) dataLen--;
                        break;
                    }
                case 0x0029:
                    {
                        if (colLength == 1)
                        {
                            // BOOL
                            thisDBVal.dataType = 0x0029;
                        }
                        else
                        {
                            // BINARY FILE POINTER DATA
                            thisDBVal.dataType = 0x1029;
                        }
                        dataLen = colLength;
                        thisDBVal.isNull = *dataPtr;
                        dataPtr++;
                        break;
                    }
                case 0x0034:
                    {
                        // LONG
                        thisDBVal.dataType = 0x0034;
                        dataLen = colLength;
                        break;
                    }
                case 0x1016:
                    {
                        // Named ROW
                        break;
                    }
                default:
                    {
                        // Unknown data type!
                        break;
                    }
            }

            // Get Data
            thisDBVal.dataVal = new byte[dataLen];
            Marshal.Copy((IntPtr)dataPtr, thisDBVal.dataVal, 0, (int)dataLen);
            dataPtr += dataLen;

            thisDBVal.dataLen = dataLen;
            thisDBVal.newPtr = dataPtr;

            return thisDBVal;
        }

        public static uint ReverseInt32(uint ReverseNum)
        {
            uint ReverseBytes = 0;
            ReverseBytes |= (ReverseNum & 0xFF000000) >> 24;
            ReverseBytes |= (ReverseNum & 0x00FF0000) >> 8;
            ReverseBytes |= (ReverseNum & 0x0000FF00) << 8;
            ReverseBytes |= (ReverseNum & 0x000000FF) << 24;
            return (ReverseBytes);
        }

        public static ushort ReverseInt16(ushort ReverseNum)
        {
            uint tmpReverseBytes = 0;
            tmpReverseBytes |= ((uint)ReverseNum & 0x0000FF00) >> 8;
            tmpReverseBytes |= ((uint)ReverseNum & 0x000000FF) << 8;
            ushort ReverseBytes = Convert.ToUInt16(tmpReverseBytes);
            return (ReverseBytes);
        }

        public static ulong ReverseInt64(ulong value)
        {
            return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40 |
                   (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8 |
                   (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24 |
                   (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
        }

        public int LoadBackup()
        {
            FullReaderReset();
            Console.Write("Loading Backup...\n");
            using (this._inputStream = File.OpenRead(_OntapeFile))
            {
                this.ParseOntapeHeader();
                // Should be at 32KB
                this.ParseDBSpaceList();
                // Should be at 36KB
                this.ParseRootDBSHeader();
                // Should be at 74KB
                this.ParseTblSpaces();
            }

            //Console.Write("Matched {0}\nEarlyTerm {1}\nOrphaned: {2}\nFragmented: {3}\n", _DBProfiles[0].MatchedPages, _DBProfiles[0].EarlyRecordTerms, _DBProfiles[0].OrphanedPages, _DBProfiles[0].FragmentedPages);
            Console.Write("\nDone reading backup file.  Processing results...\n");

            ProcessDBStructures();

            Console.Write("\nDone processing results structures.  Found...\n");

            foreach (DBSpace dbProfile in _DBSpaces)
            {
                uint TotalCols = 0;
                uint TotalPages = 0;
                if (dbProfile.Name != null)
                {
                    foreach (DBTableDetails thisTable in dbProfile.Tables)
                    {
                        TotalCols += thisTable.NumCols;
                        TotalPages += thisTable.DataBlocksWritten;
                        dbProfile.TableDictionary[thisTable.TableName] = thisTable;
                    }
                    Console.Write("DBName: {0}\n", dbProfile.Name);
                    Console.Write("+-------------------+\n");
                    Console.Write("|Tables: {0:D10} |\n", dbProfile.TableCount);
                    Console.Write("|Cols  : {0:D10} |\n", TotalCols);
                    Console.Write("|Pages : {0:D10} |\n", TotalPages);
                    Console.Write("+-------------------+\n\n\n");
                }
            }

            //DumpData();

            Console.Write("\nAll finished.  Press any key to close window.\n");

            return 0;
        }
    }
}
