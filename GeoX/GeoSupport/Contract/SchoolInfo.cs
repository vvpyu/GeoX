#region WSCF
//------------------------------------------------------------------------------
// <autogenerated code>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated code>
//------------------------------------------------------------------------------
// File time 09-01-13 01:01 PM
//
// This source code was auto-generated by WsContractFirst, Version=0.7.6319.1
#endregion


namespace GeoSupportService
{
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5420")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", TypeName="SchoolInfo")]
    [System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public partial class SchoolInfo
    {
        
        /// <remarks/>
        private string locationCode;
        
        /// <remarks/>
        private string remarks;
        
        public SchoolInfo()
        {
        }
        
        public SchoolInfo(string locationCode, string remarks)
        {
            this.locationCode = locationCode;
            this.remarks = remarks;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="token", Order=0, ElementName="LocationCode")]
        public string LocationCode
        {
            get
            {
                return this.locationCode;
            }
            set
            {
                if ((this.locationCode != value))
                {
                    this.locationCode = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="normalizedString", Order=1, ElementName="Remarks")]
        public string Remarks
        {
            get
            {
                return this.remarks;
            }
            set
            {
                if ((this.remarks != value))
                {
                    this.remarks = value;
                }
            }
        }
    }
}