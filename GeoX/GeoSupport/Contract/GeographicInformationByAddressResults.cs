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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", TypeName="GeographicInformationByAddressResults")]
    [System.ComponentModel.TypeConverterAttribute(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public partial class GeographicInformationByAddressResults
    {
        
        /// <remarks/>
        private string geoSupportFunctionName;
        
        /// <remarks/>
        private string highHouseNumber;
        
        /// <remarks/>
        private string lowHouseNumber;
        
        /// <remarks/>
        private string dCPLocallyValidStreetNames;
        
        /// <remarks/>
        private string lowCrossStreetCount;
        
        /// <remarks/>
        private System.Collections.Generic.List<Borough5DigitStreetCode> lowCrossStreets;
        
        /// <remarks/>
        private string highCrossStreetCount;
        
        /// <remarks/>
        private System.Collections.Generic.List<Borough5DigitStreetCode> highCrossStreets;
        
        /// <remarks/>
        private LionKey lionKey;
        
        /// <remarks/>
        private string specialCaseAddressIdentifier;
        
        /// <remarks/>
        private string sideOfStreetIndicator;
        
        /// <remarks/>
        private string x;
        
        /// <remarks/>
        private string y;
        
        /// <remarks/>
        private string reservedFORGeoSupportUSE;
        
        /// <remarks/>
        private string marbleHillRikersIslandFlag;
        
        /// <remarks/>
        private string dOTStreetLightContractorArea;
        
        /// <remarks/>
        private CommunityDistrict communityDistrict;
        
        /// <remarks/>
        private string zipCode;
        
        /// <remarks/>
        private string electionDistrict;
        
        /// <remarks/>
        private string assemblyDistrict;
        
        /// <remarks/>
        private string splitElectionDistrictFlag;
        
        /// <remarks/>
        private string congressionalDistrict;
        
        /// <remarks/>
        private string senateDistrict;
        
        /// <remarks/>
        private string schoolDistrict;
        
        /// <remarks/>
        private string splitSchoolDistrictFlag;
        
        /// <remarks/>
        private string censusTract2000;
        
        /// <remarks/>
        private string censusBlock2000;
        
        /// <remarks/>
        private string trueHouseNumber;
        
        /// <remarks/>
        private Borough7DigitStreetCode borough7DigitStreetCode;
        
        /// <remarks/>
        private ErrorMessage errorMessage;
        
        public GeographicInformationByAddressResults()
        {
        }
        
        public GeographicInformationByAddressResults(
                    string geoSupportFunctionName, 
                    string highHouseNumber, 
                    string lowHouseNumber, 
                    string dCPLocallyValidStreetNames, 
                    string lowCrossStreetCount, 
                    System.Collections.Generic.List<Borough5DigitStreetCode> lowCrossStreets, 
                    string highCrossStreetCount, 
                    System.Collections.Generic.List<Borough5DigitStreetCode> highCrossStreets, 
                    LionKey lionKey, 
                    string specialCaseAddressIdentifier, 
                    string sideOfStreetIndicator, 
                    string x, 
                    string y, 
                    string reservedFORGeoSupportUSE, 
                    string marbleHillRikersIslandFlag, 
                    string dOTStreetLightContractorArea, 
                    CommunityDistrict communityDistrict, 
                    string zipCode, 
                    string electionDistrict, 
                    string assemblyDistrict, 
                    string splitElectionDistrictFlag, 
                    string congressionalDistrict, 
                    string senateDistrict, 
                    string schoolDistrict, 
                    string splitSchoolDistrictFlag, 
                    string censusTract2000, 
                    string censusBlock2000, 
                    string trueHouseNumber, 
                    Borough7DigitStreetCode borough7DigitStreetCode, 
                    ErrorMessage errorMessage)
        {
            this.geoSupportFunctionName = geoSupportFunctionName;
            this.highHouseNumber = highHouseNumber;
            this.lowHouseNumber = lowHouseNumber;
            this.dCPLocallyValidStreetNames = dCPLocallyValidStreetNames;
            this.lowCrossStreetCount = lowCrossStreetCount;
            this.lowCrossStreets = lowCrossStreets;
            this.highCrossStreetCount = highCrossStreetCount;
            this.highCrossStreets = highCrossStreets;
            this.lionKey = lionKey;
            this.specialCaseAddressIdentifier = specialCaseAddressIdentifier;
            this.sideOfStreetIndicator = sideOfStreetIndicator;
            this.x = x;
            this.y = y;
            this.reservedFORGeoSupportUSE = reservedFORGeoSupportUSE;
            this.marbleHillRikersIslandFlag = marbleHillRikersIslandFlag;
            this.dOTStreetLightContractorArea = dOTStreetLightContractorArea;
            this.communityDistrict = communityDistrict;
            this.zipCode = zipCode;
            this.electionDistrict = electionDistrict;
            this.assemblyDistrict = assemblyDistrict;
            this.splitElectionDistrictFlag = splitElectionDistrictFlag;
            this.congressionalDistrict = congressionalDistrict;
            this.senateDistrict = senateDistrict;
            this.schoolDistrict = schoolDistrict;
            this.splitSchoolDistrictFlag = splitSchoolDistrictFlag;
            this.censusTract2000 = censusTract2000;
            this.censusBlock2000 = censusBlock2000;
            this.trueHouseNumber = trueHouseNumber;
            this.borough7DigitStreetCode = borough7DigitStreetCode;
            this.errorMessage = errorMessage;
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=0, ElementName="GeoSupportFunctionName")]
        public string GeoSupportFunctionName
        {
            get
            {
                return this.geoSupportFunctionName;
            }
            set
            {
                if ((this.geoSupportFunctionName != value))
                {
                    this.geoSupportFunctionName = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=1, ElementName="HighHouseNumber")]
        public string HighHouseNumber
        {
            get
            {
                return this.highHouseNumber;
            }
            set
            {
                if ((this.highHouseNumber != value))
                {
                    this.highHouseNumber = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=2, ElementName="LowHouseNumber")]
        public string LowHouseNumber
        {
            get
            {
                return this.lowHouseNumber;
            }
            set
            {
                if ((this.lowHouseNumber != value))
                {
                    this.lowHouseNumber = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=3, ElementName="DCPLocallyValidStreetNames")]
        public string DCPLocallyValidStreetNames
        {
            get
            {
                return this.dCPLocallyValidStreetNames;
            }
            set
            {
                if ((this.dCPLocallyValidStreetNames != value))
                {
                    this.dCPLocallyValidStreetNames = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=4, ElementName="LowCrossStreetCount")]
        public string LowCrossStreetCount
        {
            get
            {
                return this.lowCrossStreetCount;
            }
            set
            {
                if ((this.lowCrossStreetCount != value))
                {
                    this.lowCrossStreetCount = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlArrayAttribute(Order=5, ElementName="LowCrossStreets")]
        public System.Collections.Generic.List<Borough5DigitStreetCode> LowCrossStreets
        {
            get
            {
                return this.lowCrossStreets;
            }
            set
            {
                if ((this.lowCrossStreets != value))
                {
                    this.lowCrossStreets = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=6, ElementName="HighCrossStreetCount")]
        public string HighCrossStreetCount
        {
            get
            {
                return this.highCrossStreetCount;
            }
            set
            {
                if ((this.highCrossStreetCount != value))
                {
                    this.highCrossStreetCount = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlArrayAttribute(Order=7, ElementName="HighCrossStreets")]
        public System.Collections.Generic.List<Borough5DigitStreetCode> HighCrossStreets
        {
            get
            {
                return this.highCrossStreets;
            }
            set
            {
                if ((this.highCrossStreets != value))
                {
                    this.highCrossStreets = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=8, ElementName="LionKey")]
        public LionKey LionKey
        {
            get
            {
                return this.lionKey;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("LionKey");
                }
                if ((this.lionKey != value))
                {
                    this.lionKey = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=9, ElementName="SpecialCaseAddressIdentifier")]
        public string SpecialCaseAddressIdentifier
        {
            get
            {
                return this.specialCaseAddressIdentifier;
            }
            set
            {
                if ((this.specialCaseAddressIdentifier != value))
                {
                    this.specialCaseAddressIdentifier = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=10, ElementName="SideOfStreetIndicator")]
        public string SideOfStreetIndicator
        {
            get
            {
                return this.sideOfStreetIndicator;
            }
            set
            {
                if ((this.sideOfStreetIndicator != value))
                {
                    this.sideOfStreetIndicator = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=11, ElementName="X")]
        public string X
        {
            get
            {
                return this.x;
            }
            set
            {
                if ((this.x != value))
                {
                    this.x = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=12, ElementName="Y")]
        public string Y
        {
            get
            {
                return this.y;
            }
            set
            {
                if ((this.y != value))
                {
                    this.y = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=13, ElementName="ReservedFORGeoSupportUSE")]
        public string ReservedFORGeoSupportUSE
        {
            get
            {
                return this.reservedFORGeoSupportUSE;
            }
            set
            {
                if ((this.reservedFORGeoSupportUSE != value))
                {
                    this.reservedFORGeoSupportUSE = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=14, ElementName="MarbleHillRikersIslandFlag")]
        public string MarbleHillRikersIslandFlag
        {
            get
            {
                return this.marbleHillRikersIslandFlag;
            }
            set
            {
                if ((this.marbleHillRikersIslandFlag != value))
                {
                    this.marbleHillRikersIslandFlag = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=15, ElementName="DOTStreetLightContractorArea")]
        public string DOTStreetLightContractorArea
        {
            get
            {
                return this.dOTStreetLightContractorArea;
            }
            set
            {
                if ((this.dOTStreetLightContractorArea != value))
                {
                    this.dOTStreetLightContractorArea = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=16, ElementName="CommunityDistrict")]
        public CommunityDistrict CommunityDistrict
        {
            get
            {
                return this.communityDistrict;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("CommunityDistrict");
                }
                if ((this.communityDistrict != value))
                {
                    this.communityDistrict = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=17, ElementName="ZipCode")]
        public string ZipCode
        {
            get
            {
                return this.zipCode;
            }
            set
            {
                if ((this.zipCode != value))
                {
                    this.zipCode = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=18, ElementName="ElectionDistrict")]
        public string ElectionDistrict
        {
            get
            {
                return this.electionDistrict;
            }
            set
            {
                if ((this.electionDistrict != value))
                {
                    this.electionDistrict = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=19, ElementName="AssemblyDistrict")]
        public string AssemblyDistrict
        {
            get
            {
                return this.assemblyDistrict;
            }
            set
            {
                if ((this.assemblyDistrict != value))
                {
                    this.assemblyDistrict = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=20, ElementName="SplitElectionDistrictFlag")]
        public string SplitElectionDistrictFlag
        {
            get
            {
                return this.splitElectionDistrictFlag;
            }
            set
            {
                if ((this.splitElectionDistrictFlag != value))
                {
                    this.splitElectionDistrictFlag = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=21, ElementName="CongressionalDistrict")]
        public string CongressionalDistrict
        {
            get
            {
                return this.congressionalDistrict;
            }
            set
            {
                if ((this.congressionalDistrict != value))
                {
                    this.congressionalDistrict = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=22, ElementName="SenateDistrict")]
        public string SenateDistrict
        {
            get
            {
                return this.senateDistrict;
            }
            set
            {
                if ((this.senateDistrict != value))
                {
                    this.senateDistrict = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=23, ElementName="SchoolDistrict")]
        public string SchoolDistrict
        {
            get
            {
                return this.schoolDistrict;
            }
            set
            {
                if ((this.schoolDistrict != value))
                {
                    this.schoolDistrict = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=24, ElementName="SplitSchoolDistrictFlag")]
        public string SplitSchoolDistrictFlag
        {
            get
            {
                return this.splitSchoolDistrictFlag;
            }
            set
            {
                if ((this.splitSchoolDistrictFlag != value))
                {
                    this.splitSchoolDistrictFlag = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=25, ElementName="CensusTract2000")]
        public string CensusTract2000
        {
            get
            {
                return this.censusTract2000;
            }
            set
            {
                if ((this.censusTract2000 != value))
                {
                    this.censusTract2000 = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=26, ElementName="CensusBlock2000")]
        public string CensusBlock2000
        {
            get
            {
                return this.censusBlock2000;
            }
            set
            {
                if ((this.censusBlock2000 != value))
                {
                    this.censusBlock2000 = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=27, ElementName="TrueHouseNumber")]
        public string TrueHouseNumber
        {
            get
            {
                return this.trueHouseNumber;
            }
            set
            {
                if ((this.trueHouseNumber != value))
                {
                    this.trueHouseNumber = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=28, ElementName="Borough7DigitStreetCode")]
        public Borough7DigitStreetCode Borough7DigitStreetCode
        {
            get
            {
                return this.borough7DigitStreetCode;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("Borough7DigitStreetCode");
                }
                if ((this.borough7DigitStreetCode != value))
                {
                    this.borough7DigitStreetCode = value;
                }
            }
        }
        
        [System.Xml.Serialization.XmlElementAttribute(Order=29, ElementName="ErrorMessage")]
        public ErrorMessage ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                if ((value == null))
                {
                    throw new System.ArgumentNullException("ErrorMessage");
                }
                if ((this.errorMessage != value))
                {
                    this.errorMessage = value;
                }
            }
        }
    }
}
