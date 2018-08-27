using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DCP.Geosupport.DotNet.GeoX;
using GeoSupport.ClientProxy;
using System.Net;

namespace GeoSupportService
{
    public static class ServiceLogic
    {
        private static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Constants

        private const string DBN_KEY = "DBN";
        private const string REMARKS_KEY = "REMARKS";
        private const string ZONEDDIST_KEY = "ZONED_DIST";
        private const int CURRENT_ELEMENTARY_ZONE_LAYERID = 1;
        private const int CURRENT_MIDDLE_ZONE_LAYERID = 2;
        private const int CURRENT_HIGH_ZONE_LAYERID = 3;
        private const int NEXT_ELEMENTARY_ZONE_LAYERID = 5;
        private const int NEXT_MIDDLE_ZONE_LAYERID = 7;
        private const int NEXT_HIGH_ZONE_LAYERID = 6;
        private const string ELEMENTARY_POLYGON = "ESID_NO";
        private const string MIDDLE_POLYGON = "MSID_NO";
        private const string HIGH_POLYGON = "HSID_No";
        private const string ERROR_CODE_FAILED_TO_CONNECT = "XX";

        #endregion

        # region Functions

        public static GeographicInformationByAddressResults[] GeographicInformationbyAddress(string HouseNumber, string StreetName, string BoroughOrZipCode)
        {
            Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal Called");
            if (BoroughOrZipCode != null && BoroughOrZipCode.Length > 0)
            {
                Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal Borough Provided");
                GeographicInformationByAddressResults geo1 = ServiceLogic.GeographicInformationbyAddressInternal(HouseNumber, StreetName, BoroughOrZipCode);
                ArrayList list = new ArrayList();
                if (geo1.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT)
                {
                    //if the Error Code is XX then GeographicInformationbyAddressInternal returns the code & description, pass that to the results.
                    list.Add(geo1);
                }
                else if (HasValidCoordinates(geo1))
                {
                    list.Add(geo1);
                }
                return (GeographicInformationByAddressResults[])list.ToArray(typeof(GeographicInformationByAddressResults));                
            }
            else
            {
                Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal Borough Not Provided");
                return GeographicInformationbyAddress(HouseNumber, StreetName);
            }           
        }

        private static GeographicInformationByAddressResults[] GeographicInformationbyAddress(string HouseNumber, string StreetName)
        {
            GeographicInformationByAddressResults geo1 = ServiceLogic.GeographicInformationbyAddressInternal(HouseNumber, StreetName, "1");
            GeographicInformationByAddressResults geo2 = ServiceLogic.GeographicInformationbyAddressInternal(HouseNumber, StreetName, "2");
            GeographicInformationByAddressResults geo3 = ServiceLogic.GeographicInformationbyAddressInternal(HouseNumber, StreetName, "3");
            GeographicInformationByAddressResults geo4 = ServiceLogic.GeographicInformationbyAddressInternal(HouseNumber, StreetName, "4");
            GeographicInformationByAddressResults geo5 = ServiceLogic.GeographicInformationbyAddressInternal(HouseNumber, StreetName, "5");

            ArrayList list = new ArrayList();

            if (geo1.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT
                && geo2.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT
                && geo3.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT
                && geo4.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT
                && geo5.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT)
            {
                //if the Error Code is XX then GeographicInformationbyAddressInternal returns the code & description, pass that to the results.
                list.Add(geo1);
                return (GeographicInformationByAddressResults[])list.ToArray(typeof(GeographicInformationByAddressResults));
            }
            
            if (HasValidCoordinates(geo1))
            {
                list.Add(geo1);
            }
            if (HasValidCoordinates(geo2))
            {
                list.Add(geo2);
            }
            if (HasValidCoordinates(geo3))
            {
                list.Add(geo3);
            }
            if (HasValidCoordinates(geo4))
            {
                list.Add(geo4);
            }
            if (HasValidCoordinates(geo5))
            {
                list.Add(geo5);
            }
            return (GeographicInformationByAddressResults[])list.ToArray(typeof(GeographicInformationByAddressResults));
        }

        private static bool HasValidCoordinates(GeographicInformationByAddressResults result)
        {
            double x, y;
            return (double.TryParse(result.X, out x) && double.TryParse(result.Y, out y));
        }

        private static GeographicInformationByAddressResults GeographicInformationbyAddressInternal(string HouseNumber, string StreetName, string BoroughOrZipCode)
        {
            Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal Called");
            GeographicInformationByAddressResults results = new GeographicInformationByAddressResults();

            geo geosupport = new geo();
            Wa1 wa1 = new Wa1();
            Wa2F1 wa2f1 = new Wa2F1();

            wa1.Clear();
            wa1.in_func_code = "1";
            wa1.in_platform_ind = "C";

            try
            {
                string boroCode = ResolveBoroughNumber(BoroughOrZipCode);
                if (boroCode.Length > 0)
                {
                    wa1.in_b10sc1.boro = boroCode;
                }
                else if (BoroughOrZipCode.Length >= 5)
                {
                    wa1.in_zip_code = BoroughOrZipCode;
                }
                wa1.in_hnd = HouseNumber;
                wa1.in_stname1 = StreetName;

                Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal Invoking GeoCall");
                geosupport.GeoCall(ref wa1, ref wa2f1);

                if (wa1.out_grc != "00" && wa1.out_grc != "01")
                {
                    Log.Warn(string.Format("ServiceLogic.GeographicInformationbyAddressInternal GeoCall Failed Error Code: {0} Error Description: {1}", wa1.out_grc, wa1.out_error_message));
                    results.ErrorMessage = new ErrorMessage();
                    results.ErrorMessage.ErrorCode = wa1.out_grc;
                    results.ErrorMessage.ErrorDescription = wa1.out_error_message;
                    return results;
                }
                else 
                {
                    Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal GeoCall Successful");
                }
            }
            catch (WebException ex)
            {
                Log.Error("ServiceLogic.GeographicInformationbyAddressInternal GeoCall failed", ex);
                throw;
            }

            Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal Reformatting Data");
            Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal Error Code: " + wa1.out_grc.ToString());
            Log.Debug("ServiceLogic.GeographicInformationbyAddressInternal Error Message: " + wa1.out_error_message.ToString());
            
            results.HighHouseNumber = wa2f1.hi_hns;
            results.LowHouseNumber = wa2f1.lo_hns;
            results.DCPLocallyValidStreetNames = wa2f1.dcp_pref_lgc;
            results.LowCrossStreetCount = wa2f1.lo_x_sts_cnt;
            results.LowCrossStreets = new List<Borough5DigitStreetCode>();            
            for (int i = 0; i < wa2f1.lo_x_sts.Length; ++i)
            {
                if (wa2f1.lo_x_sts[i].boro.Trim().Length == 0 && wa2f1.lo_x_sts[i].sc5.Trim().Length == 0)
                {
                    break;
                }
                else
                {
                    Borough5DigitStreetCode StreetCode = new Borough5DigitStreetCode();
                    StreetCode.BoroughCode = wa2f1.lo_x_sts[i].boro;
                    StreetCode.FiveDigitStreetCode = wa2f1.lo_x_sts[i].sc5;
                    results.LowCrossStreets.Add(StreetCode);
                }
            }


            results.HighCrossStreetCount = wa2f1.hi_x_sts_cnt;
            results.HighCrossStreets = new List<Borough5DigitStreetCode>();            
            for (int i = 0; i < wa2f1.hi_x_sts.Length; ++i)
            {
                if (wa2f1.hi_x_sts[i].boro.Trim().Length == 0 && wa2f1.hi_x_sts[i].sc5.Trim().Length == 0)
                {
                    break;
                }
                else
                {
                    Borough5DigitStreetCode streetcode = new Borough5DigitStreetCode();
                    streetcode.BoroughCode = wa2f1.hi_x_sts[i].boro;
                    streetcode.FiveDigitStreetCode = wa2f1.hi_x_sts[i].sc5;
                    results.HighCrossStreets.Add(streetcode);
                }
            }

            results.LionKey = new GeoSupportService.LionKey();
            results.LionKey.BoroughCode = wa2f1.lion_key.boro;
            results.LionKey.BlockFaceCode = wa2f1.lion_key.face_code;
            results.LionKey.SequenceNumber = wa2f1.lion_key.sequence_number;

            results.SpecialCaseAddressIdentifier = wa2f1.spec_addr_flag;

            results.SideOfStreetIndicator = wa2f1.sos_ind;
            results.X = wa2f1.x_coord;
            results.Y = wa2f1.y_coord;
            results.ReservedFORGeoSupportUSE = wa2f1.res_gss;
            results.MarbleHillRikersIslandFlag = wa2f1.mh_ri_flag;
            results.DOTStreetLightContractorArea = wa2f1.dot_st_light_contract_area;
            results.CommunityDistrict = new CommunityDistrict();
            results.CommunityDistrict.BoroughCode = wa2f1.com_dist.boro;
            results.CommunityDistrict.DistrictNumber = wa2f1.com_dist.district_number;
            results.ElectionDistrict = wa2f1.ed;
            results.AssemblyDistrict = wa2f1.ad;
            results.SplitElectionDistrictFlag = wa2f1.split_ed;
            results.CongressionalDistrict = wa2f1.cd;
            results.SenateDistrict = wa2f1.sd;
            results.SchoolDistrict = wa2f1.school_dist;
            results.SplitSchoolDistrictFlag = wa2f1.split_school_dist_flag;
            results.CensusBlock2000 = wa2f1.census_block_2000;
            results.CensusTract2000 = wa2f1.census_tract_2000;
            results.TrueHouseNumber = wa2f1.true_hns;
            results.Borough7DigitStreetCode = new Borough7DigitStreetCode();
            results.Borough7DigitStreetCode.BoroughCode = wa2f1.real_b7sc.boro;
            results.Borough7DigitStreetCode.StreetCode = wa2f1.real_b7sc.sc5;
            results.Borough7DigitStreetCode.LocalGroupCode = wa2f1.real_b7sc.lgc;   
            
            results.ErrorMessage = new ErrorMessage();
            results.ErrorMessage.ErrorCode = wa1.out_grc;
            results.ErrorMessage.ErrorDescription = wa1.out_error_message;
            
            return results;
        }

 
        
        
        
        
        public static EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[] GeographicPoliticalInformationbyAddress(string HouseNumber, string StreetName, string BoroughOrZipCode)
        {
            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Called");
            if (BoroughOrZipCode != null && BoroughOrZipCode.Length > 0)
            {
                Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Borough Provided");
                EnrollmentInformationByAddressResultsEnrollmentInfoByAddress geo1e = ServiceLogic.GeoSupportFunction1EInternal(HouseNumber, StreetName, BoroughOrZipCode);
                ArrayList list = new ArrayList();
                if (geo1e.GeographicInfo.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT)
                {
                    //if the Error Code is XX then GeoSupportFunction1EInternal returns the code & description, pass that to the results.
                    list.Add(geo1e);
                }
                else if (HasValidCoordinates(geo1e.GeographicInfo))
                {
                    list.Add(geo1e);
                }
                return (EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[])list.ToArray(typeof(EnrollmentInformationByAddressResultsEnrollmentInfoByAddress));
            }
            else
            {
                Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Borough Not Provided");
                return GeographicPoliticalInformationbyAddress(HouseNumber, StreetName);
            }
        }
        
        private static EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[] GeographicPoliticalInformationbyAddress(string HouseNumber, string StreetName)
        {
            EnrollmentInformationByAddressResultsEnrollmentInfoByAddress geo1e = ServiceLogic.GeoSupportFunction1EInternal(HouseNumber, StreetName, "1");
            EnrollmentInformationByAddressResultsEnrollmentInfoByAddress geo2e = ServiceLogic.GeoSupportFunction1EInternal(HouseNumber, StreetName, "2");
            EnrollmentInformationByAddressResultsEnrollmentInfoByAddress geo3e = ServiceLogic.GeoSupportFunction1EInternal(HouseNumber, StreetName, "3");
            EnrollmentInformationByAddressResultsEnrollmentInfoByAddress geo4e = ServiceLogic.GeoSupportFunction1EInternal(HouseNumber, StreetName, "4");
            EnrollmentInformationByAddressResultsEnrollmentInfoByAddress geo5e = ServiceLogic.GeoSupportFunction1EInternal(HouseNumber, StreetName, "5");

            ArrayList list = new ArrayList();

            if (geo1e.GeographicInfo.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT
                && geo2e.GeographicInfo.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT
                && geo3e.GeographicInfo.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT
                && geo4e.GeographicInfo.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT
                && geo5e.GeographicInfo.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT)
            {
                //if the Error Code is XX then GeographicInformationbyAddressInternal returns the code & description, pass that to the results.
                list.Add(geo1e);
                return (EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[])list.ToArray(typeof(EnrollmentInformationByAddressResultsEnrollmentInfoByAddress));
            }

            if (HasValidCoordinates(geo1e.GeographicInfo))
            {
                list.Add(geo1e);
            }
            if (HasValidCoordinates(geo2e.GeographicInfo))
            {
                list.Add(geo2e);
            }
            if (HasValidCoordinates(geo3e.GeographicInfo))
            {
                list.Add(geo3e);
            }
            if (HasValidCoordinates(geo4e.GeographicInfo))
            {
                list.Add(geo4e);
            }
            if (HasValidCoordinates(geo5e.GeographicInfo))
            {
                list.Add(geo5e);
            }
            return (EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[])list.ToArray(typeof(EnrollmentInformationByAddressResultsEnrollmentInfoByAddress));
        }
        
        private static EnrollmentInformationByAddressResultsEnrollmentInfoByAddress GeoSupportFunction1EInternal(string HouseNumber, string StreetName, string BoroughOrZipCode)
        {
            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Called");
            EnrollmentInformationByAddressResultsEnrollmentInfoByAddress results = new EnrollmentInformationByAddressResultsEnrollmentInfoByAddress();

            results.GeographicInfo = new GeoSupportService.GeographicInformationByAddressResults();
            results.PoliticalInfo = new GeoSupportService.PoliticalInfo();

            geo geosupport = new geo();
            Wa1 wa1 = new Wa1();
            Wa2F1e wa2f1e = new Wa2F1e();

            wa1.Clear();
            wa1.in_func_code = "1E";
            wa1.in_platform_ind = "C";

            try
            {
                string boroCode = ResolveBoroughNumber(BoroughOrZipCode);
                if (boroCode.Length > 0)
                {
                    wa1.in_b10sc1.boro = boroCode;
                }
                else if (BoroughOrZipCode.Length >= 5)
                {
                    wa1.in_zip_code = BoroughOrZipCode;
                }
                wa1.in_hnd = HouseNumber;
                wa1.in_stname1 = StreetName;

                Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Invoking GeoCall");
                geosupport.GeoCall(ref wa1, ref wa2f1e);

                if (wa1.out_grc != "00" && wa1.out_grc != "01")
                {
                    Log.Warn(string.Format("ServiceLogic.GeoSupportFunction1EInternal GeoCall Failed Error Code: {0} Error Description: {1}", wa1.out_grc, wa1.out_error_message));

                    results.GeographicInfo.ErrorMessage = new ErrorMessage();
                    results.GeographicInfo.ErrorMessage.ErrorCode = wa1.out_grc;
                    results.GeographicInfo.ErrorMessage.ErrorDescription = wa1.out_error_message;
                    return results;
                }
                else
                {
                    Log.Debug("ServiceLogic.GeoSupportFunction1EInternal GeoCall Successful");
                }
            }
            catch (WebException ex)
            {
                Log.Error("ServiceLogic.GeoSupportFunction1EInternal GeoCall failed", ex);
                throw;
            }

            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Reformatting Data");
            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Error Code: " + wa1.out_grc.ToString());
            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Error Message: " + wa1.out_error_message.ToString());
           

            results.GeographicInfo.HighHouseNumber = wa2f1e.hi_hns;
            results.GeographicInfo.LowHouseNumber = wa2f1e.lo_hns;
            results.GeographicInfo.DCPLocallyValidStreetNames = wa2f1e.dcp_pref_lgc;
            results.GeographicInfo.LowCrossStreetCount = wa2f1e.lo_x_sts_cnt;
            results.GeographicInfo.LowCrossStreets = new List<Borough5DigitStreetCode>();
            for (int i = 0; i < wa2f1e.lo_x_sts.Length; ++i)
            {
                if (wa2f1e.lo_x_sts[i].boro.Trim().Length == 0 && wa2f1e.lo_x_sts[i].sc5.Trim().Length == 0)
                {
                    break;
                }
                else
                {
                    Borough5DigitStreetCode StreetCode = new Borough5DigitStreetCode();
                    StreetCode.BoroughCode = wa2f1e.lo_x_sts[i].boro;
                    StreetCode.FiveDigitStreetCode = wa2f1e.lo_x_sts[i].sc5;
                    results.GeographicInfo.LowCrossStreets.Add(StreetCode);
                }
            }


            results.GeographicInfo.HighCrossStreetCount = wa2f1e.hi_x_sts_cnt;
            results.GeographicInfo.HighCrossStreets = new List<Borough5DigitStreetCode>();
            for (int i = 0; i < wa2f1e.hi_x_sts.Length; ++i)
            {
                if (wa2f1e.hi_x_sts[i].boro.Trim().Length == 0 && wa2f1e.hi_x_sts[i].sc5.Trim().Length == 0)
                {
                    break;
                }
                else
                {
                    Borough5DigitStreetCode streetcode = new Borough5DigitStreetCode();
                    streetcode.BoroughCode = wa2f1e.hi_x_sts[i].boro;
                    streetcode.FiveDigitStreetCode = wa2f1e.hi_x_sts[i].sc5;
                    results.GeographicInfo.HighCrossStreets.Add(streetcode);
                }
            }

            results.GeographicInfo.LionKey = new GeoSupportService.LionKey();
            results.GeographicInfo.LionKey.BoroughCode = wa2f1e.lion_key.boro;
            results.GeographicInfo.LionKey.BlockFaceCode = wa2f1e.lion_key.face_code;
            results.GeographicInfo.LionKey.SequenceNumber = wa2f1e.lion_key.sequence_number;

            results.GeographicInfo.SpecialCaseAddressIdentifier = wa2f1e.spec_addr_flag;

            results.GeographicInfo.SideOfStreetIndicator = wa2f1e.sos_ind;
            results.GeographicInfo.X = wa2f1e.x_coord;
            results.GeographicInfo.Y = wa2f1e.y_coord;
            results.GeographicInfo.ReservedFORGeoSupportUSE = wa2f1e.res_gss;
            results.GeographicInfo.MarbleHillRikersIslandFlag = wa2f1e.mh_ri_flag;
            results.GeographicInfo.DOTStreetLightContractorArea = wa2f1e.dot_st_light_contract_area;
            results.GeographicInfo.CommunityDistrict = new CommunityDistrict();
            results.GeographicInfo.CommunityDistrict.BoroughCode = wa2f1e.com_dist.boro;
            results.GeographicInfo.CommunityDistrict.DistrictNumber = wa2f1e.com_dist.district_number;
            results.GeographicInfo.ElectionDistrict = wa2f1e.ed;
            results.GeographicInfo.AssemblyDistrict = wa2f1e.ad;
            results.GeographicInfo.SplitElectionDistrictFlag = wa2f1e.split_ed;
            results.GeographicInfo.CongressionalDistrict = wa2f1e.cd;
            results.GeographicInfo.SenateDistrict = wa2f1e.sd;
            results.GeographicInfo.SchoolDistrict = wa2f1e.school_dist;
            results.GeographicInfo.SplitSchoolDistrictFlag = wa2f1e.split_school_dist_flag;
            results.GeographicInfo.CensusBlock2000 = wa2f1e.census_block_2000;
            results.GeographicInfo.CensusTract2000 = wa2f1e.census_tract_2000;
            results.GeographicInfo.TrueHouseNumber = wa2f1e.true_hns;
            results.GeographicInfo.Borough7DigitStreetCode = new Borough7DigitStreetCode();
            results.GeographicInfo.Borough7DigitStreetCode.BoroughCode = wa2f1e.real_b7sc.boro;
            results.GeographicInfo.Borough7DigitStreetCode.StreetCode = wa2f1e.real_b7sc.sc5;
            results.GeographicInfo.Borough7DigitStreetCode.LocalGroupCode = wa2f1e.real_b7sc.lgc;

             
            Log.Debug("--------------------------------------------------------------------------------------------------------");
            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal wa1.display = \n" + wa1.Print()); 
            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal wa2f1e.display = \n" + wa2f1e.Print()); 
            Log.Debug("--------------------------------------------------------------------------------------------------------");

            //Political Info
            results.PoliticalInfo.ZipCode = wa2f1e.zip_code;
            results.PoliticalInfo.StreetCodeB10SC = wa1.out_b10sc1.ToString();
            results.PoliticalInfo.StandardizedStreetName = wa1.out_stname1;
            results.PoliticalInfo.SegmentID = wa2f1e.segment_id; 
            results.PoliticalInfo.MunicipalCourtDistrict = wa2f1e.mc; 
            results.PoliticalInfo.HouseCode = wa2f1e.true_hns;
            results.PoliticalInfo.CityCouncilDistrict = wa2f1e.co;
            results.PoliticalInfo.CensusTract2010 = wa2f1e.census_tract_2000;
            results.PoliticalInfo.CensusBlock2010 = wa2f1e.census_block_2000;
           
            results.GeographicInfo.ErrorMessage = new ErrorMessage();
            results.GeographicInfo.ErrorMessage.ErrorCode = wa1.out_grc;
            results.GeographicInfo.ErrorMessage.ErrorDescription = wa1.out_error_message;

            return results;
        }
   
        public static EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[] EnrollmentInformationByAddressResultsEnrollmentInfoByAddress(string HouseNumber, string StreetName, string BoroughOrZipCode)
        {
            //GeographicInformationbyAddress
            Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress Called");
            ArrayList list = new ArrayList();
            Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress calling GeographicPoliticalInformationbyAddress");
            EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[] GeoInfoPoliticByAddressResult = GeographicPoliticalInformationbyAddress(HouseNumber, StreetName, BoroughOrZipCode);
            Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress successfully called GeographicPoliticalInformationbyAddress");
            
            for (int i = 0; i < GeoInfoPoliticByAddressResult.Length; ++i)
            {
                EnrollmentInformationByAddressResultsEnrollmentInfoByAddressSchoolInfo enrollmentSchoolInfo = new EnrollmentInformationByAddressResultsEnrollmentInfoByAddressSchoolInfo();

                #region GeographicInformation

                GeographicInformationByAddressResults geographicInfo = new GeographicInformationByAddressResults();
                geographicInfo.AssemblyDistrict = GeoInfoPoliticByAddressResult[i].GeographicInfo.AssemblyDistrict;
                geographicInfo.Borough7DigitStreetCode = GeoInfoPoliticByAddressResult[i].GeographicInfo.Borough7DigitStreetCode;
                geographicInfo.CensusBlock2000 = GeoInfoPoliticByAddressResult[i].GeographicInfo.CensusBlock2000;
                geographicInfo.CensusTract2000 = GeoInfoPoliticByAddressResult[i].GeographicInfo.CensusTract2000;
                geographicInfo.CommunityDistrict = GeoInfoPoliticByAddressResult[i].GeographicInfo.CommunityDistrict;
                geographicInfo.CongressionalDistrict = GeoInfoPoliticByAddressResult[i].GeographicInfo.CongressionalDistrict;
                geographicInfo.DCPLocallyValidStreetNames = GeoInfoPoliticByAddressResult[i].GeographicInfo.DCPLocallyValidStreetNames;
                geographicInfo.DOTStreetLightContractorArea = GeoInfoPoliticByAddressResult[i].GeographicInfo.DOTStreetLightContractorArea;
                geographicInfo.ElectionDistrict = GeoInfoPoliticByAddressResult[i].GeographicInfo.ElectionDistrict;
                geographicInfo.HighCrossStreetCount = GeoInfoPoliticByAddressResult[i].GeographicInfo.HighCrossStreetCount;
                geographicInfo.HighCrossStreets = GeoInfoPoliticByAddressResult[i].GeographicInfo.HighCrossStreets;
                geographicInfo.HighHouseNumber = GeoInfoPoliticByAddressResult[i].GeographicInfo.HighHouseNumber;
                geographicInfo.LionKey = GeoInfoPoliticByAddressResult[i].GeographicInfo.LionKey;
                geographicInfo.LowCrossStreetCount = GeoInfoPoliticByAddressResult[i].GeographicInfo.LowCrossStreetCount;
                geographicInfo.LowCrossStreets = GeoInfoPoliticByAddressResult[i].GeographicInfo.LowCrossStreets;
                geographicInfo.LowHouseNumber = GeoInfoPoliticByAddressResult[i].GeographicInfo.LowHouseNumber;
                geographicInfo.MarbleHillRikersIslandFlag = GeoInfoPoliticByAddressResult[i].GeographicInfo.MarbleHillRikersIslandFlag;
                geographicInfo.ReservedFORGeoSupportUSE = GeoInfoPoliticByAddressResult[i].GeographicInfo.ReservedFORGeoSupportUSE;
                geographicInfo.SchoolDistrict = GeoInfoPoliticByAddressResult[i].GeographicInfo.SchoolDistrict;
                geographicInfo.SenateDistrict = GeoInfoPoliticByAddressResult[i].GeographicInfo.SenateDistrict;
                geographicInfo.SideOfStreetIndicator = GeoInfoPoliticByAddressResult[i].GeographicInfo.SideOfStreetIndicator;
                geographicInfo.SpecialCaseAddressIdentifier = GeoInfoPoliticByAddressResult[i].GeographicInfo.SpecialCaseAddressIdentifier;
                geographicInfo.SplitElectionDistrictFlag = GeoInfoPoliticByAddressResult[i].GeographicInfo.SplitElectionDistrictFlag;
                geographicInfo.SplitSchoolDistrictFlag = GeoInfoPoliticByAddressResult[i].GeographicInfo.SplitSchoolDistrictFlag;
                geographicInfo.TrueHouseNumber = GeoInfoPoliticByAddressResult[i].GeographicInfo.TrueHouseNumber;
                geographicInfo.X = GeoInfoPoliticByAddressResult[i].GeographicInfo.X;
                geographicInfo.Y = GeoInfoPoliticByAddressResult[i].GeographicInfo.Y;
                geographicInfo.ZipCode = GeoInfoPoliticByAddressResult[i].GeographicInfo.ZipCode;

                #endregion
                
                #region PoliticalInfo

                PoliticalInfo politicalInfo = new PoliticalInfo();
                politicalInfo.HouseCode = GeoInfoPoliticByAddressResult[i].PoliticalInfo.HouseCode;
                politicalInfo.ZipCode = GeoInfoPoliticByAddressResult[i].PoliticalInfo.ZipCode;
                politicalInfo.CensusTract2010 = GeoInfoPoliticByAddressResult[i].PoliticalInfo.CensusTract2010;
                politicalInfo.CensusBlock2010 = GeoInfoPoliticByAddressResult[i].PoliticalInfo.CensusBlock2010;
                politicalInfo.MunicipalCourtDistrict = GeoInfoPoliticByAddressResult[i].PoliticalInfo.MunicipalCourtDistrict;
                politicalInfo.CityCouncilDistrict = GeoInfoPoliticByAddressResult[i].PoliticalInfo.CityCouncilDistrict;
                politicalInfo.StreetCodeB10SC = GeoInfoPoliticByAddressResult[i].PoliticalInfo.StreetCodeB10SC;
                politicalInfo.StandardizedStreetName = GeoInfoPoliticByAddressResult[i].PoliticalInfo.StandardizedStreetName;
                politicalInfo.SegmentID = GeoInfoPoliticByAddressResult[i].PoliticalInfo.SegmentID;
                
                #endregion

                #region ZoneCheck

                HttpWebResponse response;
                try
                {
                    WebRequest wr = WebRequest.Create(ConfigurationManager.AppSettings["zoneCheckServer"]);
                    wr.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ZoneCheckTimeout"]);
                    Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress Status Check Called");
                    response = (HttpWebResponse)wr.GetResponse();
                    response.Close();
                }
                catch (WebException ex)
                {
                    Log.Error("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress GIS Server unavailable.", ex);
                    throw;
                }

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.Found || response.StatusCode == HttpStatusCode.Continue)
                {
                    Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress Status Check Success");
                    ZoneCheck_MapServer service = new ZoneCheck_MapServer();
                    Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress call GetServerInfo");
                    MapServerInfo mapinfo = service.GetServerInfo(service.GetDefaultMapName());
                    Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress GetServerInfo Successful");
                    MapDescription mapdesc = mapinfo.DefaultMapDescription;
                    //Set Results to not return Geometry
                    int LayerCount = mapdesc.LayerDescriptions.Count;
                    for (int j = 0; j < LayerCount; ++j)
                    {
                        LayerResultOptions layerResultOptions = new LayerResultOptions();
                        layerResultOptions.IncludeGeometry = false;
                        mapdesc.LayerDescriptions[i].LayerResultOptions = layerResultOptions;
                    }

                    ImageDisplay impDisp = new ImageDisplay();
                    impDisp.ImageWidth = 200;
                    impDisp.ImageHeight = 200;
                    impDisp.ImageDPI = 96;

                    PointN point = new PointN();
                    point.X = Convert.ToDouble(geographicInfo.X);
                    point.Y = Convert.ToDouble(geographicInfo.Y);

                    int[] layers = { CURRENT_ELEMENTARY_ZONE_LAYERID, CURRENT_MIDDLE_ZONE_LAYERID, CURRENT_HIGH_ZONE_LAYERID, NEXT_ELEMENTARY_ZONE_LAYERID, NEXT_MIDDLE_ZONE_LAYERID, NEXT_HIGH_ZONE_LAYERID };
                    Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress call Identify");
                    MapServerIdentifyResult[] MapServerresults = service.Identify(mapdesc, impDisp, point, 0, esriIdentifyOption.esriIdentifyAllLayers, layers);
                    Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress successfully called Identify");
                    if (MapServerresults != null)
                    {
                        Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress parse Identify");
                        enrollmentSchoolInfo.CurrentYear = new ZonedSchools();
                        enrollmentSchoolInfo.CurrentYear.SchoolYear = SchoolYear.ToString();
                        enrollmentSchoolInfo.NextYear = new ZonedSchools();
                        enrollmentSchoolInfo.NextYear.SchoolYear = (SchoolYear + 1).ToString();
                        for (int k = 0; k < MapServerresults.Length; ++k)
                        {
                            switch (MapServerresults[k].LayerID)
                            {
                                case CURRENT_ELEMENTARY_ZONE_LAYERID:
                                    enrollmentSchoolInfo.CurrentYear.ElementarySchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                    break;
                                case CURRENT_MIDDLE_ZONE_LAYERID:
                                    enrollmentSchoolInfo.CurrentYear.MiddleSchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                    break;
                                case CURRENT_HIGH_ZONE_LAYERID:
                                    enrollmentSchoolInfo.CurrentYear.HighSchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                    break;
                                case NEXT_ELEMENTARY_ZONE_LAYERID:
                                    enrollmentSchoolInfo.NextYear.ElementarySchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                    break;
                                case NEXT_MIDDLE_ZONE_LAYERID:
                                    enrollmentSchoolInfo.NextYear.MiddleSchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                    break;
                                case NEXT_HIGH_ZONE_LAYERID:
                                    enrollmentSchoolInfo.NextYear.HighSchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                    break;
                            }
                        }
                        Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress parse successful");
                    }
                }
                else
                {
                    Log.Error("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress Status Check Failed Response not 200");
                }

                #endregion

                EnrollmentInformationByAddressResultsEnrollmentInfoByAddress enrollmentInfoByAddress = new EnrollmentInformationByAddressResultsEnrollmentInfoByAddress(geographicInfo, politicalInfo, enrollmentSchoolInfo);
                list.Add(enrollmentInfoByAddress);
            }
            Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress Successful");
            return (EnrollmentInformationByAddressResultsEnrollmentInfoByAddress[])list.ToArray(typeof(EnrollmentInformationByAddressResultsEnrollmentInfoByAddress));
        }


        private static NYCInformationByAddressResultsNYCInformationByAddress[] GeographicPoliticalInformationbyAddressNY(string HouseNumber, string StreetName)
        {
            NYCInformationByAddressResultsNYCInformationByAddress address = GeoSupportFunction1EInternalNY(HouseNumber, StreetName, "1");
            NYCInformationByAddressResultsNYCInformationByAddress address2 = GeoSupportFunction1EInternalNY(HouseNumber, StreetName, "2");
            NYCInformationByAddressResultsNYCInformationByAddress address3 = GeoSupportFunction1EInternalNY(HouseNumber, StreetName, "3");
            NYCInformationByAddressResultsNYCInformationByAddress address4 = GeoSupportFunction1EInternalNY(HouseNumber, StreetName, "4");
            NYCInformationByAddressResultsNYCInformationByAddress address5 = GeoSupportFunction1EInternalNY(HouseNumber, StreetName, "5");
            ArrayList list = new ArrayList();
            if ((((address.GeographicInfo.ErrorMessage.ErrorCode == "XX") && (address2.GeographicInfo.ErrorMessage.ErrorCode == "XX")) && ((address3.GeographicInfo.ErrorMessage.ErrorCode == "XX") && (address4.GeographicInfo.ErrorMessage.ErrorCode == "XX"))) && (address5.GeographicInfo.ErrorMessage.ErrorCode == "XX"))
            {
                list.Add(address);
                return (NYCInformationByAddressResultsNYCInformationByAddress[])list.ToArray(typeof(NYCInformationByAddressResultsNYCInformationByAddress));
            }
            if (HasValidCoordinates(address.GeographicInfo))
            {
                list.Add(address);
            }
            if (HasValidCoordinates(address2.GeographicInfo))
            {
                list.Add(address2);
            }
            if (HasValidCoordinates(address3.GeographicInfo))
            {
                list.Add(address3);
            }
            if (HasValidCoordinates(address4.GeographicInfo))
            {
                list.Add(address4);
            }
            if (HasValidCoordinates(address5.GeographicInfo))
            {
                list.Add(address5);
            }
            return (NYCInformationByAddressResultsNYCInformationByAddress[])list.ToArray(typeof(NYCInformationByAddressResultsNYCInformationByAddress));
        }


        public static NYCInformationByAddressResultsNYCInformationByAddress[] GeographicPoliticalInformationbyAddressNY(string HouseNumber, string StreetName, string BoroughOrZipCode)
        {
            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Called");
            if ((BoroughOrZipCode != null) && (BoroughOrZipCode.Length > 0))
            {
                Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Borough Provided");
                NYCInformationByAddressResultsNYCInformationByAddress address = GeoSupportFunction1EInternalNY(HouseNumber, StreetName, BoroughOrZipCode);
                ArrayList list = new ArrayList();
                if (address.GeographicInfo.ErrorMessage.ErrorCode == ERROR_CODE_FAILED_TO_CONNECT)
                {
                    list.Add(address);
                }
                else if (HasValidCoordinates(address.GeographicInfo))
                {
                    list.Add(address);
                }
                return (NYCInformationByAddressResultsNYCInformationByAddress[])list.ToArray(typeof(NYCInformationByAddressResultsNYCInformationByAddress));
            }
            Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Borough Not Provided");
            return GeographicPoliticalInformationbyAddressNY(HouseNumber, StreetName);
        }

      
  private static NYCInformationByAddressResultsNYCInformationByAddress GeoSupportFunction1EInternalNY(string HouseNumber, string StreetName, string BoroughOrZipCode)
{
    int num;
    Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Called");
    NYCInformationByAddressResultsNYCInformationByAddress address = new NYCInformationByAddressResultsNYCInformationByAddress();
    address.GeographicInfo = new GeographicInformationByAddressResults();
    address.PoliticalInfo = new PoliticalInfo();
    address.CityInfo = new CityInfo();
    
    geo geo = new geo();
    Wa1 wa = new Wa1();
    Wa2F1e wafe = new Wa2F1e();
    Wa2F1ex wafex = new Wa2F1ex();
    Wa2F1ax wafax = new Wa2F1ax();
    Wa2F1a wafa = new Wa2F1a();
    wa.Clear();
    wa.in_func_code = "1E";
    wa.in_platform_ind = "C";

    Wa1 wa1 = new Wa1();
      wa1.Clear();
      wa1.in_func_code="1AX";
      wa1.in_platform_ind = "C";
   Wa1 wa2 = new Wa1();
      wa2.Clear();
      wa2.in_func_code = "1A";
      wa2.in_platform_ind = "C";
   Wa1 wa3 = new Wa1();
      wa3.Clear();
      wa3.in_func_code = "1EX";
      wa3.in_platform_ind = "C";
      wa3.in_mode_switch = "X";


    try
    {
        string str = ResolveBoroughNumber(BoroughOrZipCode);
        if (str.Length > 0)
        {
            wa.in_b10sc1.boro = str;
            wa1.in_b10sc1.boro = str;
            wa2.in_b10sc1.boro = str;
            wa3.in_b10sc1.boro = str;
        }
        else if (BoroughOrZipCode.Length >= 5)
        {
            wa.in_zip_code = BoroughOrZipCode;
            wa1.in_zip_code = BoroughOrZipCode;
            wa2.in_zip_code = BoroughOrZipCode;
            wa3.in_zip_code = BoroughOrZipCode;
            
        }
        wa.in_hnd = HouseNumber;
        wa.in_stname1 = StreetName;
        wa1.in_hnd = HouseNumber;
        wa1.in_stname1 = StreetName;
        wa2.in_hnd = HouseNumber;
        wa2.in_stname1 = StreetName;
        wa3.in_hnd = HouseNumber;
        wa3.in_stname1 = StreetName;
        Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Invoking GeoCall");
        geo.GeoCall(ref wa, ref wafe);
        geo.GeoCall(ref wa1, ref wafax);
        geo.GeoCall(ref wa3, ref wafex);
        geo.GeoCall(ref wa2, ref wafa);
        if ((wa.out_grc != "00") && (wa.out_grc != "01"))
        {
            Log.Warn(string.Format("ServiceLogic.GeoSupportFunction1EInternal GeoCall Failed Error Code: {0} Error Description: {1}", wa.out_grc, wa.out_error_message));
            address.GeographicInfo.ErrorMessage = new ErrorMessage();
            address.GeographicInfo.ErrorMessage.ErrorCode = wa.out_grc;
            address.GeographicInfo.ErrorMessage.ErrorDescription = wa.out_error_message;
            return address;
        }
        Log.Debug("ServiceLogic.GeoSupportFunction1EInternal GeoCall Successful");
    }
    catch (WebException exception)
    {
        Log.Error("ServiceLogic.GeoSupportFunction1EInternal GeoCall failed", exception);
        throw;
    }
    Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Reformatting Data");
    Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Error Code: " + wa.out_grc.ToString());
    Log.Debug("ServiceLogic.GeoSupportFunction1EInternal Error Message: " + wa.out_error_message.ToString());

    address.GeographicInfo.HighHouseNumber = wafe.hi_hns;
    address.GeographicInfo.LowHouseNumber = wafe.lo_hns;
    address.GeographicInfo.DCPLocallyValidStreetNames = wafe.dcp_pref_lgc;
    address.GeographicInfo.LowCrossStreetCount = wafe.lo_x_sts_cnt;
    address.GeographicInfo.LowCrossStreets = new List<Borough5DigitStreetCode>();
    for (num = 0; num < wafe.lo_x_sts.Length; num++)
    {
        if ((wafe.lo_x_sts[num].boro.Trim().Length == 0) && (wafe.lo_x_sts[num].sc5.Trim().Length == 0))
        {
            break;
        }
        else
        {
            Borough5DigitStreetCode item = new Borough5DigitStreetCode();

            item.BoroughCode = wafe.lo_x_sts[num].boro;
            item.FiveDigitStreetCode = wafe.lo_x_sts[num].sc5;
            address.GeographicInfo.LowCrossStreets.Add(item);
        }
    }
    address.GeographicInfo.HighCrossStreetCount = wafe.hi_x_sts_cnt;
    address.GeographicInfo.HighCrossStreets = new List<Borough5DigitStreetCode>();
    for (num = 0; num < wafe.hi_x_sts.Length; num++)
    {
        if ((wafe.hi_x_sts[num].boro.Trim().Length == 0) && (wafe.hi_x_sts[num].sc5.Trim().Length == 0))
        {
            break;
        }
        else
         {
            Borough5DigitStreetCode code2 = new Borough5DigitStreetCode ();

            code2.BoroughCode = wafe.hi_x_sts[num].boro;
            code2.FiveDigitStreetCode = wafe.hi_x_sts[num].sc5;
            address.GeographicInfo.HighCrossStreets.Add(code2);
        }
    }
    address.GeographicInfo.LionKey = new LionKey();
    address.GeographicInfo.LionKey.BoroughCode = wafe.lion_key.boro;
    address.GeographicInfo.LionKey.BlockFaceCode = wafe.lion_key.face_code;
    address.GeographicInfo.LionKey.SequenceNumber = wafe.lion_key.sequence_number;
    address.GeographicInfo.SpecialCaseAddressIdentifier = wafe.spec_addr_flag;
    address.GeographicInfo.SideOfStreetIndicator = wafe.sos_ind;
    address.GeographicInfo.X = wafe.x_coord;
    address.GeographicInfo.Y = wafe.y_coord;
    address.GeographicInfo.ReservedFORGeoSupportUSE = wafe.res_gss;
    address.GeographicInfo.MarbleHillRikersIslandFlag = wafe.mh_ri_flag;
    address.GeographicInfo.DOTStreetLightContractorArea = wafe.dot_st_light_contract_area;
    address.GeographicInfo.CommunityDistrict = new CommunityDistrict();
    address.GeographicInfo.CommunityDistrict.BoroughCode = wafe.com_dist.boro;
    address.GeographicInfo.CommunityDistrict.DistrictNumber = wafe.com_dist.district_number;
    address.GeographicInfo.ElectionDistrict = wafe.ed;
    address.GeographicInfo.AssemblyDistrict = wafe.ad;
    address.GeographicInfo.SplitElectionDistrictFlag = wafe.split_ed;
    address.GeographicInfo.CongressionalDistrict = wafe.cd;
    address.GeographicInfo.SenateDistrict = wafe.sd;
    address.GeographicInfo.SchoolDistrict = wafe.school_dist;
    address.GeographicInfo.SplitSchoolDistrictFlag = wafe.split_school_dist_flag;
    address.GeographicInfo.CensusBlock2000 = wafe.census_block_2000;
    address.GeographicInfo.CensusTract2000 = wafe.census_tract_2000;
    address.GeographicInfo.TrueHouseNumber = wafe.true_hns;
    address.GeographicInfo.Borough7DigitStreetCode = new Borough7DigitStreetCode();
    address.GeographicInfo.Borough7DigitStreetCode.BoroughCode = wafe.real_b7sc.boro;
    address.GeographicInfo.Borough7DigitStreetCode.StreetCode = wafe.real_b7sc.sc5;
    address.GeographicInfo.Borough7DigitStreetCode.LocalGroupCode = wafe.real_b7sc.lgc;
    Log.Debug("--------------------------------------------------------------------------------------------------------");
    Log.Debug("ServiceLogic.GeoSupportFunction1EInternal wa1.display = \n" + wa.Print());
    Log.Debug("ServiceLogic.GeoSupportFunction1EInternal wa2f1e.display = \n" + wafe.Print());
    Log.Debug("--------------------------------------------------------------------------------------------------------");
    address.PoliticalInfo.ZipCode = wafe.zip_code;
    address.PoliticalInfo.StreetCodeB10SC = wa.out_b10sc1.ToString();
    address.PoliticalInfo.StandardizedStreetName = wa.out_stname1;
    address.PoliticalInfo.SegmentID = wafe.segment_id;
    address.PoliticalInfo.MunicipalCourtDistrict = wafe.mc;
    address.PoliticalInfo.HouseCode = wafe.true_hns;
    address.PoliticalInfo.CityCouncilDistrict = wafe.co;
    address.PoliticalInfo.CensusTract2010 = wafe.census_tract_2000;
    address.PoliticalInfo.CensusBlock2010 = wafe.census_block_2000;
    address.GeographicInfo.ErrorMessage = new ErrorMessage();
    address.GeographicInfo.ErrorMessage.ErrorCode = wa.out_grc;
    address.GeographicInfo.ErrorMessage.ErrorDescription = wa.out_error_message;
    address.CityInfo.BBL = wafa.bbl.boro+wafa.bbl.block + wafa.bbl.lot;
    address.CityInfo.BIN =  wafa.bin.boro + wafa.bin.binnum;
    address.CityInfo.HurricaneZone = wafex.hurricane_zone;
    address.CityInfo.Latitude = wafa.latitude;
    address.CityInfo.Longitude = wafa.longitude;
    address.CityInfo.FDNY = new CityInfoFDNY();
    address.CityInfo.FDNY.FDNY_ID = wafex.fdny_id;
    address.CityInfo.FDNY.FireBattalion = wafex.fire_bat;
    address.CityInfo.FDNY.FireCompanyNumber = wafex.fire_co_num;
    address.CityInfo.FDNY.FireCompanyType = wafex.fire_co_type;
    address.CityInfo.FDNY.FireDivision = wafex.fire_div;
    address.CityInfo.DeptOfHealth = new CityInfoDeptOfHealth();
    address.CityInfo.DeptOfHealth.HealthArea = wafex.health_area;
    address.CityInfo.DeptOfHealth.HealthCenterDistrict = wafex.health_center_dist;
    address.CityInfo.NeighborhoodTabulationArea = new CityInfoNeighborhoodTabulationArea();
    address.CityInfo.NeighborhoodTabulationArea.NTA = wafex.nta;
    address.CityInfo.NeighborhoodTabulationArea.NTAName = wafex.nta_name;
    address.CityInfo.NYPD = new CityInfoNYPD();
    address.CityInfo.NYPD.NYPD_ID = wafex.nypd_id;
    address.CityInfo.NYPD.PolicePatrolBoroughCommand = wafex.police_patrol_boro;
    address.CityInfo.NYPD.PolicePrecinct = wafex.police_pct;
    address.CityInfo.DeptOfSanitation = new CityInfoDeptOfSanitation();
    address.CityInfo.DeptOfSanitation.SanitationDistrict = wafex.san_dist;
    address.CityInfo.DeptOfSanitation.SanitationSchdSctnAndSubsctn = wafex.san_sched;
    address.CityInfo.USPSCityName = wafex.USPS_city_name;
    return address;
}

 
public static NYCInformationByAddressResultsNYCInformationByAddress[] NYCInformationByAddressResultsNYCInformationByAddress(string HouseNumber, string StreetName, string BoroughOrZipCode)
{
    Log.Debug("ServiceLogic.NYCInformationByAddressResultsNYCInformationByAddressSchoolInfo Called");
    ArrayList list = new ArrayList();
    Log.Debug("ServiceLogic.NYCInformationByAddressResultsNYCInformationByAddressSchoolInfo calling GeographicPoliticalInformationbyAddress");
    NYCInformationByAddressResultsNYCInformationByAddress[] addressArray = GeographicPoliticalInformationbyAddressNY(HouseNumber, StreetName, BoroughOrZipCode);
    Log.Debug("ServiceLogic.NYCInformationByAddressResultsNYCInformationByAddressSchoolInfo successfully called GeographicPoliticalInformationbyAddress");
    for (int i = 0; i < addressArray.Length; i++)
    {
        NYCInformationByAddressResultsNYCInformationByAddressSchoolInfo schoolInfo = new NYCInformationByAddressResultsNYCInformationByAddressSchoolInfo();
        GeographicInformationByAddressResults geographicInfo = new GeographicInformationByAddressResults();
           geographicInfo.AssemblyDistrict = addressArray[i].GeographicInfo.AssemblyDistrict;
            geographicInfo.Borough7DigitStreetCode = addressArray[i].GeographicInfo.Borough7DigitStreetCode;
            geographicInfo.CensusBlock2000 = addressArray[i].GeographicInfo.CensusBlock2000;
            geographicInfo.CensusTract2000 = addressArray[i].GeographicInfo.CensusTract2000;
            geographicInfo.CommunityDistrict = addressArray[i].GeographicInfo.CommunityDistrict;
            geographicInfo.CongressionalDistrict = addressArray[i].GeographicInfo.CongressionalDistrict;
            geographicInfo.DCPLocallyValidStreetNames = addressArray[i].GeographicInfo.DCPLocallyValidStreetNames;
            geographicInfo.DOTStreetLightContractorArea = addressArray[i].GeographicInfo.DOTStreetLightContractorArea;
            geographicInfo.ElectionDistrict = addressArray[i].GeographicInfo.ElectionDistrict;
            geographicInfo.HighCrossStreetCount = addressArray[i].GeographicInfo.HighCrossStreetCount;
            geographicInfo.HighCrossStreets = addressArray[i].GeographicInfo.HighCrossStreets;
            geographicInfo.HighHouseNumber = addressArray[i].GeographicInfo.HighHouseNumber;
            geographicInfo.LionKey = addressArray[i].GeographicInfo.LionKey;
            geographicInfo.LowCrossStreetCount = addressArray[i].GeographicInfo.LowCrossStreetCount;
            geographicInfo.LowCrossStreets = addressArray[i].GeographicInfo.LowCrossStreets;
            geographicInfo.LowHouseNumber = addressArray[i].GeographicInfo.LowHouseNumber;
            geographicInfo.MarbleHillRikersIslandFlag = addressArray[i].GeographicInfo.MarbleHillRikersIslandFlag;
            geographicInfo.ReservedFORGeoSupportUSE = addressArray[i].GeographicInfo.ReservedFORGeoSupportUSE;
            geographicInfo.SchoolDistrict = addressArray[i].GeographicInfo.SchoolDistrict;
            geographicInfo.SenateDistrict = addressArray[i].GeographicInfo.SenateDistrict;
            geographicInfo.SideOfStreetIndicator = addressArray[i].GeographicInfo.SideOfStreetIndicator;
            geographicInfo.SpecialCaseAddressIdentifier = addressArray[i].GeographicInfo.SpecialCaseAddressIdentifier;
            geographicInfo.SplitElectionDistrictFlag = addressArray[i].GeographicInfo.SplitElectionDistrictFlag;
            geographicInfo.SplitSchoolDistrictFlag = addressArray[i].GeographicInfo.SplitSchoolDistrictFlag;
            geographicInfo.TrueHouseNumber = addressArray[i].GeographicInfo.TrueHouseNumber;
            geographicInfo.X = addressArray[i].GeographicInfo.X;
            geographicInfo.Y = addressArray[i].GeographicInfo.Y;
            geographicInfo.ZipCode = addressArray[i].GeographicInfo.ZipCode;
        
        PoliticalInfo politicalInfo = new PoliticalInfo (); 
            politicalInfo.HouseCode = addressArray[i].PoliticalInfo.HouseCode;
            politicalInfo.ZipCode = addressArray[i].PoliticalInfo.ZipCode;
            politicalInfo.CensusTract2010 = addressArray[i].PoliticalInfo.CensusTract2010;
            politicalInfo.CensusBlock2010 = addressArray[i].PoliticalInfo.CensusBlock2010;
            politicalInfo.MunicipalCourtDistrict = addressArray[i].PoliticalInfo.MunicipalCourtDistrict;
            politicalInfo.CityCouncilDistrict = addressArray[i].PoliticalInfo.CityCouncilDistrict;
            politicalInfo.StreetCodeB10SC = addressArray[i].PoliticalInfo.StreetCodeB10SC;
            politicalInfo.StandardizedStreetName = addressArray[i].PoliticalInfo.StandardizedStreetName;
            politicalInfo.SegmentID = addressArray[i].PoliticalInfo.SegmentID;
       
        CityInfo cityInfo = new CityInfo();
            cityInfo.BBL = addressArray[i].CityInfo.BBL;
            cityInfo.BIN = addressArray[i].CityInfo.BIN;
            cityInfo.DeptOfHealth = addressArray[i].CityInfo.DeptOfHealth;
            cityInfo.DeptOfSanitation = addressArray[i].CityInfo.DeptOfSanitation;
            cityInfo.FDNY = addressArray[i].CityInfo.FDNY;
            cityInfo.HurricaneZone = addressArray[i].CityInfo.HurricaneZone;
            cityInfo.Latitude = addressArray[i].CityInfo.Latitude;
            cityInfo.Longitude = addressArray[i].CityInfo.Longitude;
            cityInfo.NeighborhoodTabulationArea = addressArray[i].CityInfo.NeighborhoodTabulationArea;
            cityInfo.NYPD = addressArray[i].CityInfo.NYPD;
            cityInfo.USPSCityName = addressArray[i].CityInfo.USPSCityName;

            #region ZoneCheck

            HttpWebResponse response;
            try
            {
                WebRequest wr = WebRequest.Create(ConfigurationManager.AppSettings["zoneCheckServer"]);
                wr.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings["ZoneCheckTimeout"]);
                Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress Status Check Called");
                response = (HttpWebResponse)wr.GetResponse();
                response.Close();
            }
            catch (WebException ex)
            {
                Log.Error("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress GIS Server unavailable.", ex);
                throw;
            }

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.Found || response.StatusCode == HttpStatusCode.Continue)
            {
                Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress Status Check Success");
                ZoneCheck_MapServer service = new ZoneCheck_MapServer();
                Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress call GetServerInfo");
                MapServerInfo mapinfo = service.GetServerInfo(service.GetDefaultMapName());
                Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress GetServerInfo Successful");
                MapDescription mapdesc = mapinfo.DefaultMapDescription;
                //Set Results to not return Geometry
                int LayerCount = mapdesc.LayerDescriptions.Count;
                for (int j = 0; j < LayerCount; ++j)
                {
                    LayerResultOptions layerResultOptions = new LayerResultOptions();
                    layerResultOptions.IncludeGeometry = false;
                    mapdesc.LayerDescriptions[i].LayerResultOptions = layerResultOptions;
                }

                ImageDisplay impDisp = new ImageDisplay();
                impDisp.ImageWidth = 200;
                impDisp.ImageHeight = 200;
                impDisp.ImageDPI = 96;

                PointN point = new PointN();
                point.X = Convert.ToDouble(geographicInfo.X);
                point.Y = Convert.ToDouble(geographicInfo.Y);

                int[] layers = { CURRENT_ELEMENTARY_ZONE_LAYERID, CURRENT_MIDDLE_ZONE_LAYERID, CURRENT_HIGH_ZONE_LAYERID, NEXT_ELEMENTARY_ZONE_LAYERID, NEXT_MIDDLE_ZONE_LAYERID, NEXT_HIGH_ZONE_LAYERID };
                Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress call Identify");
                MapServerIdentifyResult[] MapServerresults = service.Identify(mapdesc, impDisp, point, 0, esriIdentifyOption.esriIdentifyAllLayers, layers);
                Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress successfully called Identify");
                if (MapServerresults != null)
                {
                    Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress parse Identify");
                    schoolInfo.CurrentYear = new ZonedSchools();
                    schoolInfo.CurrentYear.SchoolYear = SchoolYear.ToString();
                    schoolInfo.NextYear = new ZonedSchools();
                    schoolInfo.NextYear.SchoolYear = (SchoolYear + 1).ToString();
                    for (int k = 0; k < MapServerresults.Length; ++k)
                    {
                        switch (MapServerresults[k].LayerID)
                        {
                            case CURRENT_ELEMENTARY_ZONE_LAYERID:
                                schoolInfo.CurrentYear.ElementarySchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                break;
                            case CURRENT_MIDDLE_ZONE_LAYERID:
                                schoolInfo.CurrentYear.MiddleSchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                break;
                            case CURRENT_HIGH_ZONE_LAYERID:
                                schoolInfo.CurrentYear.HighSchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                break;
                            case NEXT_ELEMENTARY_ZONE_LAYERID:
                                schoolInfo.NextYear.ElementarySchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                break;
                            case NEXT_MIDDLE_ZONE_LAYERID:
                                schoolInfo.NextYear.MiddleSchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                break;
                            case NEXT_HIGH_ZONE_LAYERID:
                                schoolInfo.NextYear.HighSchools = ParseIdentifyResultsForSchoolInfo_v1(MapServerresults[k].Properties);
                                break;
                        }
                    }
                    Log.Debug("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress parse successful");
                }
            }
            else
            {
                Log.Error("ServiceLogic.EnrollmentInformationByAddressResultsEnrollmentInfoByAddress Status Check Failed Response not 200");
            }

            #endregion
       
        NYCInformationByAddressResultsNYCInformationByAddress address = new NYCInformationByAddressResultsNYCInformationByAddress(geographicInfo, politicalInfo, schoolInfo, cityInfo);
        list.Add(address);
    }
    Log.Debug("ServiceLogic.NYCInformationByAddressResults Successful");
    return (NYCInformationByAddressResultsNYCInformationByAddress[]) list.ToArray(typeof(NYCInformationByAddressResultsNYCInformationByAddress));
}

 

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        public static PropertyLevelInformationByAddressResults PropertyLevelInformationbyAddress(string HouseNumber, string StreetName, string Borough)
        {
            geo geosupport = new geo();
            Wa1 wa1 = new Wa1();
            Wa2F1a wa2F1a = new Wa2F1a();
            wa1.Clear();
            wa1.in_func_code = "1A";
            wa1.in_platform_ind = "C";

            try
            {
                string boroCode = ResolveBoroughNumber(Borough);
                if (boroCode.Length > 0)
                {
                    wa1.in_b10sc1.boro = boroCode;
                }
                wa1.in_hnd = HouseNumber;
                wa1.in_stname1 = StreetName;

                geosupport.GeoCall(ref wa1, ref wa2F1a);
            }
            catch (WebException ex)
            {
                Log.Error("ServiceLogic.PropertyLevelInformationbyAddress GeoCall failed", ex);
                throw;
            }

            PropertyLevelInformationByAddressResults results = new PropertyLevelInformationByAddressResults();
            results.LowHouseNumberSorted = wa2F1a.lohns;
            results.BoroughBlockLot = new BoroughBlockLot();
            results.BoroughBlockLot.Borough = wa2F1a.bbl.boro;
            results.BoroughBlockLot.Block = wa2F1a.bbl.block;
            results.BoroughBlockLot.Lot = wa2F1a.bbl.lot;
            results.RPADSelfCheckCode = wa2F1a.rpad_bldg_class;
            results.CornerCode = wa2F1a.corner_code;
            results.NumberOfBuildings = wa2F1a.num_of_bldgs;
            results.NumberOfBlockFaces = wa2F1a.num_of_blockfaces;
            results.InteriorFlag = wa2F1a.interior_flag;
            results.VacantFlag = wa2F1a.vacant_flag;
            results.MarbleHillRikersIslandFlag = wa2F1a.mh_ri_flag;
            results.AddressOverflowFlag = wa2F1a.addr_overflow_flag;
            results.StrollKey = wa2F1a.stroll_key;
            results.BuildingIdentificationNumber = new BuildingIdentificationNumber();
            results.BuildingIdentificationNumber.BinNumber = wa2F1a.bin.binnum;
            results.BuildingIdentificationNumber.Borough = wa2F1a.bin.boro;
            results.CondoFlag = wa2F1a.condo_flag;
            results.CondoNumber = wa2F1a.condo_num;
            results.COOPNumber = wa2F1a.coop_num;
            results.X = wa2F1a.x_coord;
            results.Y = wa2F1a.x_coord;
            results.NumberOfAddresses = wa2F1a.num_of_addrs;
            
            int count = 0;
            results.AlternateStreets = new List<AddressRange>();            
            if (int.TryParse(wa2F1a.num_of_addrs, out count))
            {
                for (int i = 0; i < count; ++i)
                {
                    AddressRange addressrange = new AddressRange();
                    addressrange.LowHouseNumberDisplay = wa2F1a.addr_list[i].lhnd;
                    addressrange.HighHouseNumberDisplay = wa2F1a.addr_list[i].hhnd;

                    addressrange.Borough7DigitStreetCode = new Borough7DigitStreetCode();
                    addressrange.Borough7DigitStreetCode.BoroughCode = wa2F1a.addr_list[i].b7sc.boro;
                    addressrange.Borough7DigitStreetCode.LocalGroupCode = wa2F1a.addr_list[i].b7sc.lgc;
                    addressrange.Borough7DigitStreetCode.StreetCode = wa2F1a.addr_list[i].b7sc.sc5;
                    addressrange.BuildingIdentificationNumber = new BuildingIdentificationNumber();
                    addressrange.BuildingIdentificationNumber.Borough = wa2F1a.addr_list[i].bin.boro;
                    addressrange.BuildingIdentificationNumber.BinNumber = wa2F1a.addr_list[i].bin.binnum;
                    addressrange.SideOFStreetIndicator = wa2F1a.addr_list[i].sos;
                    addressrange.AddressType = wa2F1a.addr_list[i].addr_type;

                    results.AlternateStreets.Add(addressrange);
                }
            }

            return results;
        }

        public static GeographicInformationByIntersectionResults GeographicInformationbyIntersection(string StreetName1, string Borough1, string StreetName2, string Borough2)
        {
            geo geosupport = new geo();
            Wa1 wa1 = new Wa1();
            Wa2F2 wa2F2 = new Wa2F2();
            wa1.Clear();
            wa1.in_func_code = "2";
            wa1.in_platform_ind = "C";

            try
            {
                string borough1 = ResolveBoroughNumber(Borough1);
                string borough2 = ResolveBoroughNumber(Borough2);
                wa1.in_stname1 = StreetName1;
                wa1.in_b10sc1.boro = borough1;
                wa1.in_stname2 = StreetName2;
                if (borough2.Length > 0)
                    wa1.in_b10sc2.boro = borough2;

                geosupport.GeoCall(ref wa1, ref wa2F2);
            }
            catch (WebException ex)
            {
                Log.Error("ServiceLogic.GeographicInformationbyIntersection GeoCall failed", ex);
                throw;
            }

            GeographicInformationByIntersectionResults results = new GeographicInformationByIntersectionResults();

            results.DuplicateIntersectionCount = wa2F2.dup_intersect_cnt;
            results.CrossStreetCount = wa2F2.x_sts_cnt;
            results.CrossStreets = new List<Borough5DigitStreetCode>();            
            for (int i = 0; i < wa2F2.x_sts.Length; ++i)
            {
                if (wa2F2.x_sts[i].boro.Trim().Length == 0 && wa2F2.x_sts[i].sc5.Trim().Length == 0)
                {
                    break;
                }
                else
                {
                    Borough5DigitStreetCode streetcode = new Borough5DigitStreetCode();
                    streetcode.BoroughCode = wa2F2.x_sts[i].boro;
                    streetcode.FiveDigitStreetCode = wa2F2.x_sts[i].sc5;
                    results.CrossStreets.Add(streetcode);
                }
            }

            results.LionNodeNumber = wa2F2.lion_node_num;
            results.X = wa2F2.x_coord;
            results.Y = wa2F2.y_coord;
            results.MarbleHillRikersIslandFlag = wa2F2.mh_ri_flag;
            results.CommunityDistrict = new CommunityDistrict();
            results.CommunityDistrict.BoroughCode = wa2F2.com_dist.boro;
            results.CommunityDistrict.DistrictNumber = wa2F2.com_dist.district_number;
            results.ZipCode = wa2F2.zip_code;
            results.SchoolDistrict = wa2F2.school_dist;
            results.CensusTract2000 = wa2F2.census_tract_2000;
            results.AssemblyDistrict = wa2F2.ad;
            results.CongressionalDistrict = wa2F2.cd;
            results.SenateDistrict = wa2F2.sd;

            return results;
        }

        public static ZoneInfoByAddressResultsZoneInfoByAddress[] ZoneInfobyAddressResultsZoneInfoByAddress(string HouseNumber, string StreetName, string BoroughOrZipCode)
        {
            Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress Called");
            ArrayList list = new ArrayList();
            Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress calling GeographicInformationbyAddress");
            GeographicInformationByAddressResults[] GeoInfoByAddressResult = GeographicInformationbyAddress(HouseNumber, StreetName, BoroughOrZipCode);
            Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress successfully called GeographicInformationbyAddress");
            for (int i = 0; i < GeoInfoByAddressResult.Length; ++i)
            {
                ZoneInfoByAddressResultsZoneInfoByAddressSchoolInfo zoneSchoolInfo = new ZoneInfoByAddressResultsZoneInfoByAddressSchoolInfo();
                
                #region GeographicInformation

                GeographicInformationByAddressResults geographicInfo = new GeographicInformationByAddressResults();
                geographicInfo.AssemblyDistrict = GeoInfoByAddressResult[i].AssemblyDistrict;
                geographicInfo.Borough7DigitStreetCode = GeoInfoByAddressResult[i].Borough7DigitStreetCode;
                geographicInfo.CensusBlock2000 = GeoInfoByAddressResult[i].CensusBlock2000;
                geographicInfo.CensusTract2000 = GeoInfoByAddressResult[i].CensusTract2000;
                geographicInfo.CommunityDistrict = GeoInfoByAddressResult[i].CommunityDistrict;
                geographicInfo.CongressionalDistrict = GeoInfoByAddressResult[i].CongressionalDistrict;
                geographicInfo.DCPLocallyValidStreetNames = GeoInfoByAddressResult[i].DCPLocallyValidStreetNames;
                geographicInfo.DOTStreetLightContractorArea = GeoInfoByAddressResult[i].DOTStreetLightContractorArea;
                geographicInfo.ElectionDistrict = GeoInfoByAddressResult[i].ElectionDistrict;
                geographicInfo.HighCrossStreetCount = GeoInfoByAddressResult[i].HighCrossStreetCount;
                geographicInfo.HighCrossStreets = GeoInfoByAddressResult[i].HighCrossStreets;
                geographicInfo.HighHouseNumber = GeoInfoByAddressResult[i].HighHouseNumber;
                geographicInfo.LionKey = GeoInfoByAddressResult[i].LionKey;
                geographicInfo.LowCrossStreetCount = GeoInfoByAddressResult[i].LowCrossStreetCount;
                geographicInfo.LowCrossStreets = GeoInfoByAddressResult[i].LowCrossStreets;
                geographicInfo.LowHouseNumber = GeoInfoByAddressResult[i].LowHouseNumber;
                geographicInfo.MarbleHillRikersIslandFlag = GeoInfoByAddressResult[i].MarbleHillRikersIslandFlag;
                geographicInfo.ReservedFORGeoSupportUSE = GeoInfoByAddressResult[i].ReservedFORGeoSupportUSE;
                geographicInfo.SchoolDistrict = GeoInfoByAddressResult[i].SchoolDistrict;
                geographicInfo.SenateDistrict = GeoInfoByAddressResult[i].SenateDistrict;
                geographicInfo.SideOfStreetIndicator = GeoInfoByAddressResult[i].SideOfStreetIndicator;
                geographicInfo.SpecialCaseAddressIdentifier = GeoInfoByAddressResult[i].SpecialCaseAddressIdentifier;
                geographicInfo.SplitElectionDistrictFlag = GeoInfoByAddressResult[i].SplitElectionDistrictFlag;
                geographicInfo.SplitSchoolDistrictFlag = GeoInfoByAddressResult[i].SplitSchoolDistrictFlag;
                geographicInfo.TrueHouseNumber = GeoInfoByAddressResult[i].TrueHouseNumber;
                geographicInfo.X = GeoInfoByAddressResult[i].X;
                geographicInfo.Y = GeoInfoByAddressResult[i].Y;
                geographicInfo.ZipCode = GeoInfoByAddressResult[i].ZipCode;

                #endregion

                #region ZoneCheck

                HttpWebResponse response;
                try
                {
                    WebRequest wr = WebRequest.Create(ConfigurationManager.AppSettings["zoneCheckServer"]);
                    wr.Timeout =  Convert.ToInt32(ConfigurationManager.AppSettings["ZoneCheckTimeout"]);
                    Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress Status Check Called");
                    response = (HttpWebResponse)wr.GetResponse();
                    response.Close();
                }
                catch (WebException ex)
                {
                    Log.Error("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress GIS Server unavailable.", ex);
                    throw;
                }

                if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.Found || response.StatusCode == HttpStatusCode.Continue)
                {
                    Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress Status Check Success");
                    ZoneCheck_MapServer service = new ZoneCheck_MapServer();
                    Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress call GetServerInfo");
                    MapServerInfo mapinfo = service.GetServerInfo(service.GetDefaultMapName());
                    Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress GetServerInfo Successful");
                    MapDescription mapdesc = mapinfo.DefaultMapDescription;
                    //Set Results to not return Geometry
                    int LayerCount = mapdesc.LayerDescriptions.Count;
                    for (int j = 0; j < LayerCount; ++j)
                    {
                        LayerResultOptions layerResultOptions = new LayerResultOptions();
                        layerResultOptions.IncludeGeometry = false;
                        mapdesc.LayerDescriptions[i].LayerResultOptions = layerResultOptions;
                    }

                    ImageDisplay impDisp = new ImageDisplay();
                    impDisp.ImageWidth = 200;
                    impDisp.ImageHeight = 200;
                    impDisp.ImageDPI = 96;

                    PointN point = new PointN();
                    point.X = Convert.ToDouble(geographicInfo.X);
                    point.Y = Convert.ToDouble(geographicInfo.Y);

                    int[] layers = { CURRENT_ELEMENTARY_ZONE_LAYERID, CURRENT_MIDDLE_ZONE_LAYERID };
                    Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress call Identify");
                    MapServerIdentifyResult[] MapServerresults = service.Identify(mapdesc, impDisp, point, 0, esriIdentifyOption.esriIdentifyAllLayers, layers);
                    Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress successfully called Identify");
                    if (MapServerresults != null)
                    {
                        Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress parse Identify");
                        for (int k = 0; k < MapServerresults.Length; ++k)
                        {
                            switch (MapServerresults[k].LayerID)
                            {
                                case CURRENT_ELEMENTARY_ZONE_LAYERID:
                                    zoneSchoolInfo.ElementarySchools = ParseIdentifyResultsForSchoolInfo(MapServerresults[k].Properties);
                                    break;
                                case CURRENT_MIDDLE_ZONE_LAYERID:
                                    zoneSchoolInfo.MiddleSchools = ParseIdentifyResultsForSchoolInfo(MapServerresults[k].Properties);
                                    break;
                            }
                        }
                        Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress parse successful");
                    }
                }
                else
                {
                    Log.Error("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress Status Check Failed Response not 200");
                }

                #endregion

                ZoneInfoByAddressResultsZoneInfoByAddress zoneInfoByAddress = new ZoneInfoByAddressResultsZoneInfoByAddress(geographicInfo, zoneSchoolInfo);
                list.Add(zoneInfoByAddress);
            }
            Log.Debug("ServiceLogic.ZoneInfobyAddressResultsZoneInfoByAddress Successful");
            return (ZoneInfoByAddressResultsZoneInfoByAddress[])list.ToArray(typeof(ZoneInfoByAddressResultsZoneInfoByAddress));
        }


        private static ZonedSchoolInfo ParseIdentifyResultsForSchoolInfo_v1(PropertySet properties)
        {
            ZonedSchoolInfo zonedschoolInfo = new ZonedSchoolInfo();
            List<ZonedSchoolInfoSchoolInfo> schoolsInfo = new List<ZonedSchoolInfoSchoolInfo>();

            string dbn = "", remarks = "", zone_dist = "", polygon_id = "";
                    
            for (int i = 0; i < properties.PropertyArray.Count; ++i)
            {
                string key = properties.PropertyArray[i].Key;
                switch (key)
                {
                    case DBN_KEY:
                        dbn = (string)properties.PropertyArray[i].Value;
                        dbn = dbn.Trim();
                        break;
                    case REMARKS_KEY: 
                        remarks = (string)properties.PropertyArray[i].Value; 
                        remarks = remarks.Trim(); 
                        break;
                    case ZONEDDIST_KEY:
                        zone_dist = (string)properties.PropertyArray[i].Value;
                        break;
                    case ELEMENTARY_POLYGON:
                    case MIDDLE_POLYGON:
                    case HIGH_POLYGON:
                        polygon_id = (string)properties.PropertyArray[i].Value;
                        break;
                }
            }

            //use default remarks if none are present
            if (string.IsNullOrEmpty(remarks))
                remarks = ConfigurationManager.AppSettings["DefaultRemarks"];

            //Split the dbn field if there is a comma delimited list.
            if(dbn.IndexOf(',') >= 0)
            {
                string[] dbns = dbn.Split(',');
                for (int i = 0; i < dbns.Length; ++i)
                {
                    ZonedSchoolInfoSchoolInfo school = new ZonedSchoolInfoSchoolInfo();
                    school.LocationCode = dbns[i].Trim();
                    schoolsInfo.Add(school);
                }
            }

            else if (dbn.Length > 0) 
            {
                ZonedSchoolInfoSchoolInfo school = new ZonedSchoolInfoSchoolInfo();
                school.LocationCode = dbn;
                schoolsInfo.Add(school);
            }


            zonedschoolInfo.PolygonId = polygon_id;
            zonedschoolInfo.Remarks = remarks;
            zonedschoolInfo.ZonedDistrict = zone_dist;
            zonedschoolInfo.SchoolInfo = schoolsInfo;


            return zonedschoolInfo;
        }

        private static List<SchoolInfo> ParseIdentifyResultsForSchoolInfo(PropertySet properties)
        {
            List<SchoolInfo> schools = new List<SchoolInfo>();
            string dbn = "", remarks = "";
            for (int i = 0; i < properties.PropertyArray.Count; ++i)
            {
                string key = properties.PropertyArray[i].Key;
                switch (key)
                {
                    case DBN_KEY:
                        dbn = (string)properties.PropertyArray[i].Value;
                        dbn = dbn.Trim();
                        break;
                    case REMARKS_KEY:
                        remarks = (string)properties.PropertyArray[i].Value;
                        remarks = remarks.Trim();
                        break;
                }
            }
            //Split the dbn field if there is a comma delimited list.
            if (dbn.IndexOf(',') >= 0)
            {
                string[] dbns = dbn.Split(',');
                for (int i = 0; i < dbns.Length; ++i)
                {
                    SchoolInfo schoolinfo = new SchoolInfo();
                    schoolinfo.LocationCode = dbns[i].Trim();
                    schoolinfo.Remarks = remarks;
                    schools.Add(schoolinfo);
                }
            }
            else if (dbn.Length > 0 && remarks.Length > 0)
            {
                SchoolInfo schoolinfo = new SchoolInfo();
                schoolinfo.LocationCode = dbn;
                schoolinfo.Remarks = remarks;
                schools.Add(schoolinfo);
            }
            //use default remarks if none are present
            else if (dbn.Length > 0)
            {
                SchoolInfo schoolinfo = new SchoolInfo();
                schoolinfo.LocationCode = dbn;
                schoolinfo.Remarks = ConfigurationManager.AppSettings["DefaultRemarks"];
                schools.Add(schoolinfo);
            }
            else if (remarks.Length > 0)
            {
                SchoolInfo schoolinfo = new SchoolInfo();
                schoolinfo.LocationCode = string.Empty;
                schoolinfo.Remarks = remarks;
                schools.Add(schoolinfo);
            }
            return schools;
        }

        private static string ResolveBoroughNumber(string text)
        {
            string data = "";
            switch (text.ToLower().Trim())
            {
                case "1":
                case "m":
                case "manhattan": data = "1"; break;
                case "2":
                case "x":
                case "bronx": data = "2"; break;
                case "3":
                case "k":
                case "brooklyn": data = "3"; break;
                case "4":
                case "q":
                case "queens": data = "4"; break;
                case "5":
                case "r":
                case "staten island": data = "5"; break;
            }
            return data;
        }

        private static int SchoolYear
        {
            get
            {
                DateTime now = DateTime.Now;
                if (now.Month < 7)
                {
                    return now.Year - 1;
                }
                else
                {
                    return now.Year;
                }
            }
        }

        /// <summary>
        /// This was test code to check the results of the custom security library vs. .NET's implementation of basic Auth.  This is no longer in use.
        /// </summary>
        /// <returns></returns>
        public static bool CheckBasicAuth()
        {   
            if (HttpContext.Current.Request.Headers["Authorization"] != null)
            {
                NYCDOE.Security.DirectoryUtilities.Configuration.ConfigurationData configData = System.Configuration.ConfigurationManager.GetSection("NYCDOE.Security.DirectoryUtilities.Configuration") as NYCDOE.Security.DirectoryUtilities.Configuration.ConfigurationData;
                NYCDOE.Security.DirectoryUtilities.Configuration.ActiveDirectoryConfiguration adConfig = configData.ActiveDirectory;
                
                string encoded = HttpContext.Current.Request.Headers["Authorization"];
                byte[] b = Convert.FromBase64String(encoded.Substring(6));
                String decoded = System.Text.Encoding.UTF8.GetString(b);

                String[] split = decoded.Split(':');
                if (split.Length == 2)
                {
                    bool obj = NYCDOE.Security.DirectoryUtilities.UI.UserControls.LDAPAuthentication.Authenticate(HttpContext.Current, split[0], split[1]);
                    return obj;
                }
                return false;
            }
            else return false;
        }

        #endregion
    }
}
