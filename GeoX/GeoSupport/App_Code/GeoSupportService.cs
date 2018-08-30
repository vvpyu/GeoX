using System.Diagnostics;
using System.Web.Services;
using System.ComponentModel;
using System.Web.Services.Protocols;
using System;
using System.Xml.Serialization;
using System.Web;
using GeoSupportService;

/// <summary>
/// Summary description for GeoSupportService
/// </summary>
[WebService(Name = "GeoSupportServiceSoap", Namespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1, Name = "GeoSupportServiceSoap", Namespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0")]
public partial class GeoSupportServiceSoap : System.Web.Services.WebService, IGeoSupportServiceSoap
{
    private static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public GeoSupportServiceSoap()
    {
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("GeographicInformationbyAddress", RequestNamespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", ResponseNamespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Binding = "GeoSupportServiceSoap")]
    [return: System.Xml.Serialization.XmlElementAttribute("GeographicInformationbyAddressResult", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public virtual GeographicInformationByAddressResults[] GeographicInformationbyAddress([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "HouseNumber")] string HouseNumber, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "StreetName")] string StreetName, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "BoroughOrZipCode")] string BoroughOrZipCode)
    {
        return ServiceLogic.GeographicInformationbyAddress(HouseNumber, StreetName, BoroughOrZipCode);

    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("PropertyLevelInformationByAddressResults", RequestNamespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", ResponseNamespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Binding = "GeoSupportServiceSoap")]
    [return: System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public virtual PropertyLevelInformationByAddressResults PropertyLevelInformationbyAddress([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "HouseNumber")] string HouseNumber, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "StreetName")] string StreetName, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Borough")] string Borough)
    {
        return ServiceLogic.PropertyLevelInformationbyAddress(HouseNumber, StreetName, Borough);

    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("GeographicInformationbyIntersection", RequestNamespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", ResponseNamespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Binding = "GeoSupportServiceSoap")]
    [return: System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public virtual GeographicInformationByIntersectionResults GeographicInformationbyIntersection([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "StreetName1")] string StreetName1, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Borough1")] string Borough1, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "StreetName2")] string StreetName2, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "Borough2")] string Borough2)
    {
        return ServiceLogic.GeographicInformationbyIntersection(StreetName1, Borough1, StreetName2, Borough2);

    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:ZoneInformationByAddress", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare, Binding = "GeoSupportServiceSoap")]
    [return: System.Xml.Serialization.XmlArrayAttribute("ZoneInformationByAddressResponse", Namespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("ZoneInfoByAddress", IsNullable = false)]
    public virtual ZoneInfoByAddressResultsZoneInfoByAddress[] ZoneInformationByAddress([System.Xml.Serialization.XmlElementAttribute("ZoneInformationByAddress", Namespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0")] AddressInfo zoneInformationByAddress1)
    {
        Log.Debug(string.Format("ZoneInformationByAddress params: {0} --- {1} --- {2}", zoneInformationByAddress1.HouseNumber, zoneInformationByAddress1.StreetName, zoneInformationByAddress1.BoroughOrZipCode));
        ZoneInfoByAddressResultsZoneInfoByAddress[] results = ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress(zoneInformationByAddress1.HouseNumber, zoneInformationByAddress1.StreetName, zoneInformationByAddress1.BoroughOrZipCode);
        Log.Debug("Service Logic Successful");
        return results;

    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:EnrollmentInformationByAddress", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare, Binding = "GeoSupportServiceSoap")]
    [return: System.Xml.Serialization.XmlArrayAttribute("EnrollmentInformproxyationByAddressResponse", Namespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("EnrollmentInfoByAddress", IsNullable = false)]
    public virtual EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[] EnrollmentInformationByAddress([System.Xml.Serialization.XmlElementAttribute("EnrollmentInformationByAddress", Namespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0")] AddressInfo enrollmentInformationByAddress1)
    {
        Log.Debug(string.Format("EnrollmentInformationByAddress params: {0} --- {1} --- {2}", enrollmentInformationByAddress1.HouseNumber, enrollmentInformationByAddress1.StreetName, enrollmentInformationByAddress1.BoroughOrZipCode));
        EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[] results = ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress(enrollmentInformationByAddress1.HouseNumber, enrollmentInformationByAddress1.StreetName, enrollmentInformationByAddress1.BoroughOrZipCode);
        return results;
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("urn:NYCInformationByAddress", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Bare, Binding = "GeoSupportServiceSoap")]
    [return: System.Xml.Serialization.XmlArrayAttribute("NYCInformationByAddressResponse", Namespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0")]
    [return: System.Xml.Serialization.XmlArrayItemAttribute("NYCInformationByAddress", IsNullable = false)]
    public virtual NYCInformationByAddressResultsNYCInformationByAddress[] NYCInformationByAddress([System.Xml.Serialization.XmlElementAttribute("NYCInformationByAddress", Namespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0")] AddressInfo nYCInformationByAddress1)
    {
        Log.Debug(string.Format("NYCInformationByAddress params: {0} --- {1} --- {2}", nYCInformationByAddress1.HouseNumber, nYCInformationByAddress1.StreetName, nYCInformationByAddress1.BoroughOrZipCode));
        NYCInformationByAddressResultsNYCInformationByAddress[] results = ServiceLogic.NYCInformationByAddressResultsNYCInformationByAddress(nYCInformationByAddress1.HouseNumber, nYCInformationByAddress1.StreetName, nYCInformationByAddress1.BoroughOrZipCode);
        return results;
    }

}