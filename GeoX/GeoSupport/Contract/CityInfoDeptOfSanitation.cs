#region WSCF
//------------------------------------------------------------------------------
// <autogenerated code>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated code>
//------------------------------------------------------------------------------
// File time 27-10-15 01:27 PM
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.5485")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", TypeName="CityInfoDeptOfSanitation")]
    [System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public partial class CityInfoDeptOfSanitation
    {
        
        /// <remarks/>
        private string sanitationDistrict;
        
        /// <remarks/>
        private string sanitationSchdSctnAndSubsctn;
        
        public CityInfoDeptOfSanitation()
        {
        }
        
        public CityInfoDeptOfSanitation(string sanitationDistrict, string sanitationSchdSctnAndSubsctn)
        {
            this.sanitationDistrict = sanitationDistrict;
            this.sanitationSchdSctnAndSubsctn = sanitationSchdSctnAndSubsctn;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="token", Order=0, ElementName="SanitationDistrict")]
        public string SanitationDistrict
        {
            get
            {
                return this.sanitationDistrict;
            }
            set
            {
                if ((this.sanitationDistrict != value))
                {
                    this.sanitationDistrict = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="token", Order=1, ElementName="SanitationSchdSctnAndSubsctn")]
        public string SanitationSchdSctnAndSubsctn
        {
            get
            {
                return this.sanitationSchdSctnAndSubsctn;
            }
            set
            {
                if ((this.sanitationSchdSctnAndSubsctn != value))
                {
                    this.sanitationSchdSctnAndSubsctn = value;
                }
            }
        }
    }
}