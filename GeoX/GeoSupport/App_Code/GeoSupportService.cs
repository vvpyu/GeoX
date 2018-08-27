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

    public GeoSupportServiceSoap()
    {
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World, Peng";
    }

    [System.Web.Services.WebMethodAttribute()]
    [System.Web.Services.Protocols.SoapDocumentMethodAttribute("GeographicInformationbyAddress", RequestNamespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", ResponseNamespace = "http://schools.nyc.gov/DOE.Services.Geography/GeoSupportService_v1_0", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped, Binding = "GeoSupportServiceSoap")]
    [return: System.Xml.Serialization.XmlElementAttribute("GeographicInformationbyAddressResult", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public virtual GeographicInformationByAddressResults[] GeographicInformationbyAddress([System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "HouseNumber")] string HouseNumber, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "StreetName")] string StreetName, [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified, ElementName = "BoroughOrZipCode")] string BoroughOrZipCode)
    {
        return ServiceLogic.GeographicInformationbyAddress(HouseNumber, StreetName, BoroughOrZipCode);

    }

    public PropertyLevelInformationByAddressResults PropertyLevelInformationbyAddress(string HouseNumber, string StreetName, string Borough)
    {
        throw new NotImplementedException();
    }

    public GeographicInformationByIntersectionResults GeographicInformationbyIntersection(string StreetName1, string Borough1, string StreetName2, string Borough2)
    {
        throw new NotImplementedException();
    }

    public ZoneInfoByAddressResultsZoneInfoByAddress[] ZoneInformationByAddress(AddressInfo zoneInformationByAddress1)
    {
        throw new NotImplementedException();
    }

    public EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[] EnrollmentInformationByAddress(AddressInfo enrollmentInformationByAddress1)
    {
        throw new NotImplementedException();
    }
}