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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", TypeName="CityInfo")]
    [System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public partial class CityInfo
    {
        
        /// <remarks/>
        private string bBL;
        
        /// <remarks/>
        private string bIN;
        
        /// <remarks/>
        private string hurricaneZone;
        
        /// <remarks/>
        private string latitude;
        
        /// <remarks/>
        private string longitude;
        
        /// <remarks/>
        private CityInfoFDNY fDNY;
        
        /// <remarks/>
        private CityInfoDeptOfHealth deptOfHealth;
        
        /// <remarks/>
        private CityInfoNeighborhoodTabulationArea neighborhoodTabulationArea;
        
        /// <remarks/>
        private CityInfoNYPD nYPD;
        
        /// <remarks/>
        private CityInfoDeptOfSanitation deptOfSanitation;
        
        /// <remarks/>
        private string uSPSCityName;
        
        public CityInfo()
        {
        }
        
        public CityInfo(string bBL, string bIN, string hurricaneZone, string latitude, string longitude, CityInfoFDNY fDNY, CityInfoDeptOfHealth deptOfHealth, CityInfoNeighborhoodTabulationArea neighborhoodTabulationArea, CityInfoNYPD nYPD, CityInfoDeptOfSanitation deptOfSanitation, string uSPSCityName)
        {
            this.bBL = bBL;
            this.bIN = bIN;
            this.hurricaneZone = hurricaneZone;
            this.latitude = latitude;
            this.longitude = longitude;
            this.fDNY = fDNY;
            this.deptOfHealth = deptOfHealth;
            this.neighborhoodTabulationArea = neighborhoodTabulationArea;
            this.nYPD = nYPD;
            this.deptOfSanitation = deptOfSanitation;
            this.uSPSCityName = uSPSCityName;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="token", Order=0, ElementName="BBL")]
        public string BBL
        {
            get
            {
                return this.bBL;
            }
            set
            {
                if ((this.bBL != value))
                {
                    this.bBL = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="token", Order=1, ElementName="BIN")]
        public string BIN
        {
            get
            {
                return this.bIN;
            }
            set
            {
                if ((this.bIN != value))
                {
                    this.bIN = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="token", Order=2, ElementName="HurricaneZone")]
        public string HurricaneZone
        {
            get
            {
                return this.hurricaneZone;
            }
            set
            {
                if ((this.hurricaneZone != value))
                {
                    this.hurricaneZone = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=3, ElementName="Latitude")]
        public string Latitude
        {
            get
            {
                return this.latitude;
            }
            set
            {
                if ((this.latitude != value))
                {
                    this.latitude = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=4, ElementName="Longitude")]
        public string Longitude
        {
            get
            {
                return this.longitude;
            }
            set
            {
                if ((this.longitude != value))
                {
                    this.longitude = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=5, ElementName="FDNY")]
        public CityInfoFDNY FDNY
        {
            get
            {
                return this.fDNY;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("FDNY");
                }
                if ((this.fDNY != value))
                {
                    this.fDNY = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=6, ElementName="DeptOfHealth")]
        public CityInfoDeptOfHealth DeptOfHealth
        {
            get
            {
                return this.deptOfHealth;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("DeptOfHealth");
                }
                if ((this.deptOfHealth != value))
                {
                    this.deptOfHealth = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=7, ElementName="NeighborhoodTabulationArea")]
        public CityInfoNeighborhoodTabulationArea NeighborhoodTabulationArea
        {
            get
            {
                return this.neighborhoodTabulationArea;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("NeighborhoodTabulationArea");
                }
                if ((this.neighborhoodTabulationArea != value))
                {
                    this.neighborhoodTabulationArea = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=8, ElementName="NYPD")]
        public CityInfoNYPD NYPD
        {
            get
            {
                return this.nYPD;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("NYPD");
                }
                if ((this.nYPD != value))
                {
                    this.nYPD = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=9, ElementName="DeptOfSanitation")]
        public CityInfoDeptOfSanitation DeptOfSanitation
        {
            get
            {
                return this.deptOfSanitation;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("DeptOfSanitation");
                }
                if ((this.deptOfSanitation != value))
                {
                    this.deptOfSanitation = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(DataType="normalizedString", Order=10, ElementName="USPSCityName")]
        public string USPSCityName
        {
            get
            {
                return this.uSPSCityName;
            }
            set
            {
                if ((this.uSPSCityName != value))
                {
                    this.uSPSCityName = value;
                }
            }
        }
    }
}