﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
namespace ADHDTech.CiscoCrypt
{
    public class drfComponentXML
    {

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]

        public partial class list
        {
            [XmlElement("FeatureObject")]
            public List<FeatureObject> FeatureObjects { get; set; }
            /*
            [XmlElement("FeatureObject")]
            private List<FeatureObject> featureObjectField;

            /// <remarks/>
            public List<FeatureObject> FeatureObject
            {
                get
                {
                    return this.featureObjectField;
                }
                set
                {
                    this.featureObjectField = value;
                }
            }
            */
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class FeatureObject
        {

            private string versionField;

            private string oSNameField;

            private string featureNameField;

            private string statusField;

            private byte percentageField;

            private string m__dStartTimeField;

            private string m__dEndTimeField;

            private bool m__bBackupFlagField;

            private string deploymentField;

            private object featureBackupSizeField;

            private string amIRestrictedField;

            private ServerObj[] vServerObjectField;

            /// <remarks/>
            public string Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            public string OSName
            {
                get
                {
                    return this.oSNameField;
                }
                set
                {
                    this.oSNameField = value;
                }
            }

            /// <remarks/>
            public string FeatureName
            {
                get
                {
                    return this.featureNameField;
                }
                set
                {
                    this.featureNameField = value;
                }
            }

            /// <remarks/>
            public string Status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            public byte Percentage
            {
                get
                {
                    return this.percentageField;
                }
                set
                {
                    this.percentageField = value;
                }
            }

            /// <remarks/>
            public string m__dStartTime
            {
                get
                {
                    return this.m__dStartTimeField;
                }
                set
                {
                    this.m__dStartTimeField = value;
                }
            }

            /// <remarks/>
            public string m__dEndTime
            {
                get
                {
                    return this.m__dEndTimeField;
                }
                set
                {
                    this.m__dEndTimeField = value;
                }
            }

            /// <remarks/>
            public bool m__bBackupFlag
            {
                get
                {
                    return this.m__bBackupFlagField;
                }
                set
                {
                    this.m__bBackupFlagField = value;
                }
            }

            /// <remarks/>
            public string Deployment
            {
                get
                {
                    return this.deploymentField;
                }
                set
                {
                    this.deploymentField = value;
                }
            }

            /// <remarks/>
            public object featureBackupSize
            {
                get
                {
                    return this.featureBackupSizeField;
                }
                set
                {
                    this.featureBackupSizeField = value;
                }
            }

            /// <remarks/>
            public string amIRestricted
            {
                get
                {
                    return this.amIRestrictedField;
                }
                set
                {
                    this.amIRestrictedField = value;
                }
            }

            /// <remarks/>
            public ServerObj[] vServerObject
            {
                get
                {
                    return this.vServerObjectField;
                }
                set
                {
                    this.vServerObjectField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class vServerObject
        {

            private ServerObj serverObjField;

            /// <remarks/>
            public ServerObj ServerObj
            {
                get
                {
                    return this.serverObjField;
                }
                set
                {
                    this.serverObjField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class ServerObj
        {

            private string versionField;

            private string serverNameField;

            private string statusField;

            private byte percentageField;

            private string m__dStartTimeField;

            private string m__dEndTimeField;

            private bool m__bConnectedField;

            private ComponentObject[] vComponentObjectField;

            /// <remarks/>
            public string Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            public string ServerName
            {
                get
                {
                    return this.serverNameField;
                }
                set
                {
                    this.serverNameField = value;
                }
            }

            /// <remarks/>
            public string Status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            public byte Percentage
            {
                get
                {
                    return this.percentageField;
                }
                set
                {
                    this.percentageField = value;
                }
            }

            /// <remarks/>
            public string m__dStartTime
            {
                get
                {
                    return this.m__dStartTimeField;
                }
                set
                {
                    this.m__dStartTimeField = value;
                }
            }

            /// <remarks/>
            public string m__dEndTime
            {
                get
                {
                    return this.m__dEndTimeField;
                }
                set
                {
                    this.m__dEndTimeField = value;
                }
            }

            /// <remarks/>
            public bool m__bConnected
            {
                get
                {
                    return this.m__bConnectedField;
                }
                set
                {
                    this.m__bConnectedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("ComponentObject", IsNullable = false)]
            public ComponentObject[] vComponentObject
            {
                get
                {
                    return this.vComponentObjectField;
                }
                set
                {
                    this.vComponentObjectField = value;
                }
            }
        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class ComponentObject
        {

            private string versionField;

            private string componentNameField;

            private string statusField;

            private byte percentageField;

            private string m__dStartTimeField;

            private string m__dEndTimeField;

            private string logLocationField;

            private DependentList dependentListField;

            private string prebackupField;

            private string dobackupField;

            private string postbackupField;

            private string prerestoreField;

            private string dorestoreField;

            private string postrestoreField;

            private string errormapField;

            private string directBackupField;

            private byte scriptPriorityField;

            private string rebootAfterRestoreField;

            private string encryptKeyField;

            private string encryptedSHA1CheckSumField;

            private byte tapeFileIndexField;

            /// <remarks/>
            public string Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            public string ComponentName
            {
                get
                {
                    return this.componentNameField;
                }
                set
                {
                    this.componentNameField = value;
                }
            }

            /// <remarks/>
            public string Status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            public byte Percentage
            {
                get
                {
                    return this.percentageField;
                }
                set
                {
                    this.percentageField = value;
                }
            }

            /// <remarks/>
            public string m__dStartTime
            {
                get
                {
                    return this.m__dStartTimeField;
                }
                set
                {
                    this.m__dStartTimeField = value;
                }
            }

            /// <remarks/>
            public string m__dEndTime
            {
                get
                {
                    return this.m__dEndTimeField;
                }
                set
                {
                    this.m__dEndTimeField = value;
                }
            }

            /// <remarks/>
            public string LogLocation
            {
                get
                {
                    return this.logLocationField;
                }
                set
                {
                    this.logLocationField = value;
                }
            }

            /// <remarks/>
            public DependentList DependentList
            {
                get
                {
                    return this.dependentListField;
                }
                set
                {
                    this.dependentListField = value;
                }
            }

            /// <remarks/>
            public string Prebackup
            {
                get
                {
                    return this.prebackupField;
                }
                set
                {
                    this.prebackupField = value;
                }
            }

            /// <remarks/>
            public string Dobackup
            {
                get
                {
                    return this.dobackupField;
                }
                set
                {
                    this.dobackupField = value;
                }
            }

            /// <remarks/>
            public string Postbackup
            {
                get
                {
                    return this.postbackupField;
                }
                set
                {
                    this.postbackupField = value;
                }
            }

            /// <remarks/>
            public string Prerestore
            {
                get
                {
                    return this.prerestoreField;
                }
                set
                {
                    this.prerestoreField = value;
                }
            }

            /// <remarks/>
            public string Dorestore
            {
                get
                {
                    return this.dorestoreField;
                }
                set
                {
                    this.dorestoreField = value;
                }
            }

            /// <remarks/>
            public string Postrestore
            {
                get
                {
                    return this.postrestoreField;
                }
                set
                {
                    this.postrestoreField = value;
                }
            }

            /// <remarks/>
            public string Errormap
            {
                get
                {
                    return this.errormapField;
                }
                set
                {
                    this.errormapField = value;
                }
            }

            /// <remarks/>
            public string DirectBackup
            {
                get
                {
                    return this.directBackupField;
                }
                set
                {
                    this.directBackupField = value;
                }
            }

            /// <remarks/>
            public byte ScriptPriority
            {
                get
                {
                    return this.scriptPriorityField;
                }
                set
                {
                    this.scriptPriorityField = value;
                }
            }

            /// <remarks/>
            public string RebootAfterRestore
            {
                get
                {
                    return this.rebootAfterRestoreField;
                }
                set
                {
                    this.rebootAfterRestoreField = value;
                }
            }

            /// <remarks/>
            public string EncryptKey
            {
                get
                {
                    return this.encryptKeyField;
                }
                set
                {
                    this.encryptKeyField = value;
                }
            }

            /// <remarks/>
            public string EncryptedSHA1CheckSum
            {
                get
                {
                    return this.encryptedSHA1CheckSumField;
                }
                set
                {
                    this.encryptedSHA1CheckSumField = value;
                }
            }

            /// <remarks/>
            public byte TapeFileIndex
            {
                get
                {
                    return this.tapeFileIndexField;
                }
                set
                {
                    this.tapeFileIndexField = value;
                }
            }

        }

        /// <remarks/>
        [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
        [System.SerializableAttribute()]
        [System.Diagnostics.DebuggerStepThroughAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class DependentList
        {

            private string stringField;

            private string classField;

            /// <remarks/>
            public string @string
            {
                get
                {
                    return this.stringField;
                }
                set
                {
                    this.stringField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string @class
            {
                get
                {
                    return this.classField;
                }
                set
                {
                    this.classField = value;
                }
            }
        }
    }
}