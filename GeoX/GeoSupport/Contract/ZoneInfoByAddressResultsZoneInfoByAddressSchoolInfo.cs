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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", TypeName="ZoneInfoByAddressResultsZoneInfoByAddressSchoolInfo")]
    [System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public partial class ZoneInfoByAddressResultsZoneInfoByAddressSchoolInfo
    {
        
        /// <remarks/>
        private System.Collections.Generic.List<SchoolInfo> elementarySchools;
        
        /// <remarks/>
        private System.Collections.Generic.List<SchoolInfo> middleSchools;
        
        public ZoneInfoByAddressResultsZoneInfoByAddressSchoolInfo()
        {
        }
        
        public ZoneInfoByAddressResultsZoneInfoByAddressSchoolInfo(System.Collections.Generic.List<SchoolInfo> elementarySchools, System.Collections.Generic.List<SchoolInfo> middleSchools)
        {
            this.elementarySchools = elementarySchools;
            this.middleSchools = middleSchools;
        }
        
        [System.Xml.Serialization.XmlArrayAttribute(Order=0, ElementName="ElementarySchools")]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public System.Collections.Generic.List<SchoolInfo> ElementarySchools
        {
            get
            {
                return this.elementarySchools;
            }
            set
            {
                if ((this.elementarySchools != value))
                {
                    this.elementarySchools = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlArrayAttribute(Order=1, ElementName="MiddleSchools")]
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public System.Collections.Generic.List<SchoolInfo> MiddleSchools
        {
            get
            {
                return this.middleSchools;
            }
            set
            {
                if ((this.middleSchools != value))
                {
                    this.middleSchools = value;
                }
            }
        }
    }
}
