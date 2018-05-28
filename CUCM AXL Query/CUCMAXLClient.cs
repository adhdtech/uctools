using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using ADHDTech.UCOSClients;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CUCM_AXL_Query
{
    [DataContract]
    public class AXLSQLApp
    {
        public MainForm mainForm;
        [DataMember]
        public Dictionary<string, CUCMAXLProfile> AXLProfiles;
        public CUCMAXLProfile currentAXLProfile;
        public string configFile;
        DataContractJsonSerializer JSONser;

        public AXLSQLApp(MainForm originForm)
        {
            JSONser = new DataContractJsonSerializer(typeof(Dictionary<string, CUCMAXLProfile>));
            AXLProfiles = new Dictionary<string, CUCMAXLProfile>();
            mainForm = originForm;
            configFile = @"AXLSQLConfig.json";
        }

        public void SaveConfig()
        {
            MemoryStream stream1 = new MemoryStream();
            JSONser.WriteObject(stream1, AXLProfiles);

            stream1.Position = 0;
            StreamReader sr = new StreamReader(stream1);
            //Console.Write("JSON form of Person object: ");
            //Console.WriteLine(sr.ReadToEnd());
            File.WriteAllText(configFile, sr.ReadToEnd());
        }

        public void LoadConfig()
        {
            if (File.Exists(configFile))
            {
                using (FileStream stream = File.Open(configFile, FileMode.Open))
                {
                    //Bitmap originalBMP = new Bitmap(stream);
                    AXLProfiles = (Dictionary<string, CUCMAXLProfile>)JSONser.ReadObject(stream);
                }
                //string readText = File.ReadAllText(configFile);
            }
        }
    }

    public class CUCMAXLClient
    {
        public List<Dictionary<string, string>> ReturnDataSet;
        public String ErrorMsg;
        public AXLAPIService AXLAPIClient;

        public CUCMAXLClient(string targetHost, string userName, string userPass)
        {
            AXLAPIClient = new AXLAPIService(targetHost, userName, userPass);
        }

        public bool RunQuery(string sqlQuery)
        {
            ErrorMsg = null;
            ReturnDataSet = new List<Dictionary<string, string>>();
            ExecuteSQLQueryReq sqlReq = new ExecuteSQLQueryReq();
            sqlReq.sql = sqlQuery;
            try
            {
                ExecuteSQLQueryRes sqlRes = AXLAPIClient.executeSQLQuery(sqlReq);
                for (int i = 0; i < sqlRes.@return.Length; i++)
                {
                    System.Xml.XmlNode[] columnNodes = (System.Xml.XmlNode[])sqlRes.@return[i];
                    Dictionary<string, string> fieldValues = new Dictionary<string, string>();
                    for (int j = 0; j < columnNodes.Length; j++)
                    {
                        if (columnNodes[j].FirstChild != null)
                        {
                            fieldValues[columnNodes[j].Name] = columnNodes[j].FirstChild.Value;
                        }
                        else
                        {
                            fieldValues[columnNodes[j].Name] = null;
                        }
                    }
                    ReturnDataSet.Add(fieldValues);
                    //string tmpVal = rootNode.Attributes["devicecount"].Value;
                }
                return false;
            }
            catch (Exception ex)
            {
                ErrorMsg = ex.Message;
                //Console.WriteLine(ex.Message);
                return true;
            }
        }
    }

    [DataContract]
    public class CUCMAXLProfile
    {
        [DataMember]
        public string AXLHost;
        [DataMember]
        public string AXLUser;
        [DataMember]
        public string AXLPass;
        public bool Validated;
        public string AXLError;

        public CUCMAXLProfile(string targetHost, string userName, string userPass)
        {
            AXLHost = targetHost;
            AXLUser = userName;
            AXLPass = userPass;
            AXLError = null;
            Validated = Validate();
        }

        public bool Validate()
        {
            bool isValid = false;
            CUCMAXLClient testClient = new CUCMAXLClient(AXLHost, AXLUser, AXLPass);

            if (testClient.RunQuery("select count(*) from applicationuser as appusercount"))
            {
                // We received an error
                AXLError = testClient.ErrorMsg;
            }
            else
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
