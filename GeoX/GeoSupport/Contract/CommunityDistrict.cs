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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", TypeName="CommunityDistrict")]
    [System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public partial class CommunityDistrict
    {
        
        /// <remarks/>
        private string boroughCode;
        
        /// <remarks/>
        private string districtNumber;
        
        public CommunityDistrict()
        {
        }
        
        public CommunityDistrict(string boroughCode, string districtNumber)
        {
            this.boroughCode = boroughCode;
            this.districtNumber = districtNumber;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0, ElementName="BoroughCode")]
        public string BoroughCode
        {
            get
            {
                return this.boroughCode;
            }
            set
            {
                if ((this.boroughCode != value))
                {
                    this.boroughCode = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1, ElementName="DistrictNumber")]
        public string DistrictNumber
        {
            get
            {
                return this.districtNumber;
            }
            set
            {
                if ((this.districtNumber != value))
                {
                    this.districtNumber = value;
                }
            }
        }
    }
}
