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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", TypeName="EnrollmentInformationByAddressResultsEnrollmentInfoByAddressSchoolInfo")]
    [System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public partial class EnrollmentInformationByAddressResultsEnrollmentInfoByAddressSchoolInfo
    {
        
        /// <remarks/>
        private ZonedSchools currentYear;
        
        /// <remarks/>
        private ZonedSchools nextYear;
        
        public EnrollmentInformationByAddressResultsEnrollmentInfoByAddressSchoolInfo()
        {
        }
        
        public EnrollmentInformationByAddressResultsEnrollmentInfoByAddressSchoolInfo(ZonedSchools currentYear, ZonedSchools nextYear)
        {
            this.currentYear = currentYear;
            this.nextYear = nextYear;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0, ElementName="CurrentYear")]
        public ZonedSchools CurrentYear
        {
            get
            {
                return this.currentYear;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("CurrentYear");
                }
                if ((this.currentYear != value))
                {
                    this.currentYear = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1, ElementName="NextYear")]
        public ZonedSchools NextYear
        {
            get
            {
                return this.nextYear;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("NextYear");
                }
                if ((this.nextYear != value))
                {
                    this.nextYear = value;
                }
            }
        }
    }
}
