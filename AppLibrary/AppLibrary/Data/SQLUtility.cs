using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using AppLibrary.Model;

namespace AppLibrary.Data
{
    public static class SQLUtility 
    {
        // utility class does not make database connections. It only works on database objects
        #region Data Operations
        public static string GetSPParameter(string ParameterValue)
        {
            string result = string.Empty;
            string pattern = "'{0}'";
            if (ParameterValue == string.Empty)
            {
                result = "NULL";
            }
            else
            {
                result = string.Format(pattern, ParameterValue); 
            }

            return result;
        }

        public static bool? GetBit(object DBObject)
        {
            // Get the value as is, return a nullable type if you have to
            bool? BitValue = null;

            BitValue = (DBObject == System.DBNull.Value) ? BitValue : (bool)DBObject;
            return BitValue;
        }

        public static bool GetBitValue(object DBObject)
        {
            // Set a default value if no value is found. Field does not allow nulls
            bool BitValue = false;

            BitValue = (DBObject == System.DBNull.Value) ? BitValue : (bool)DBObject;
            return BitValue;
        }

        public static bool GetBitValue(object DBObject,bool DefaultValue)
        {
            // Set a user defined default value if no value is found. Field does not allow nulls
            bool BitValue = DefaultValue;

            BitValue = (DBObject == System.DBNull.Value) ? BitValue : (bool)DBObject;
            return BitValue;
        }

        public static int? GetInt(object DBObject)
        {
            // use for nullable int fields.
            int? IntValue = null;

            IntValue = (DBObject == System.DBNull.Value) ? IntValue : (int)DBObject;
            return IntValue;
        }

        public static int GetIntValue(object DBObject)
        {
            // use for non nullable int fields
            int IntValue = 0;
            IntValue = (DBObject == System.DBNull.Value) ? IntValue : (int)DBObject;
            return IntValue;
        }

        public static int GetIntValue(object DBObject,int DefaultValue)
        {
            // use for non nullable int fields, set auser defined default value if null found
            int IntValue = DefaultValue;
            IntValue = (DBObject == System.DBNull.Value) ? IntValue : (int)DBObject;
            return IntValue;
        }

        public static string GetStringValue(object DBObject)
        {
            string StringValue = string.Empty;

            StringValue = (DBObject == System.DBNull.Value) ? StringValue : (string)DBObject;
            return StringValue;
        }

        public static string GetStringValue(object DBObject,Type DBDataType)
        {
            // Make sure object is not null before calling this function
            // Null data is not handle by this function
            string Result = string.Empty;

            if (DBDataType == System.Type.GetType("System.String"))
            {
                string stringValue = (string)DBObject;
                Result = stringValue;
            }
            else if (DBDataType == System.Type.GetType("System.Int32"))
            {
                int intValue = (int)DBObject;
                Result = intValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.Boolean"))
            {
                bool boolValue = (bool)DBObject;
                Result = boolValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.DateTime"))
            {
                DateTime datetimeValue = (DateTime)DBObject;
                Result = datetimeValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.Decimal"))
            {
                Decimal decimalValue = (decimal)DBObject;
                Result = decimalValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.Byte"))
            {
                Byte byteValue = (byte)DBObject;
                Result = byteValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.Char"))
            {
                char charValue = (char)DBObject;
                Result = charValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.Double"))
            {
                double doubleValue = (double)DBObject;
                Result = doubleValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.Int16"))
            {
                Int16 IntSixteenValue = (Int16)DBObject;
                Result = IntSixteenValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.SByte"))
            {
                sbyte sbyteValue = (sbyte)DBObject;
                Result = sbyteValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.Single"))
            {
                Single singleValue = (Single)DBObject;
                Result = singleValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.TimeSpan"))
            {
                TimeSpan timespanValue = (TimeSpan)DBObject;
                Result = timespanValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.UInt16"))
            {
                UInt16 UIntSixteenValue = (UInt16)DBObject;
                Result = UIntSixteenValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.UInt32"))
            {
                UInt32 UInt32Value = (UInt32)DBObject;
                Result = UInt32Value.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.UInt64"))
            {
                UInt64 UInt64Value = (ulong)DBObject;
                Result = UInt64Value.ToString(); 
            }
            return Result;
        }

        public static string GetGUIDValue(object DBObject)
        {
            Guid GuidResult = new Guid();
            string Result = string.Empty;

            if (DBObject == System.DBNull.Value)
            {
                Result = string.Empty;
            }
            else
            {
                GuidResult = (Guid)DBObject;
                Result = GuidResult.ToString(); 
            }

            return Result;
        }

        public static DateTime GetDateValue(object DBObject)
        {
            DateTime DateValue;
            DateValue = (DBObject == System.DBNull.Value) ? DateTime.Now : (DateTime)DBObject;
            return DateValue;
        }

        public static DateTime GetDateValue(object DBObject,DateTime InitialDateTime)
        {
            // if a datetime is not found then intialize it to the value passed in to this function
            DateTime DateValue;
            DateValue = (DBObject == System.DBNull.Value) ? InitialDateTime : (DateTime)DBObject;
            return DateValue;
        }

        public static float GetFloatValue(object DBObject)
        {
            // use for non nullable int fields
            float Result = 0;
            Result = (DBObject == System.DBNull.Value) ? Result : Convert.ToSingle(DBObject);
            return Result;
        }

        public static object GetDBValue(object DBObject)
        {
            int IntegerObject = 0;
            string StringObject = string.Empty;
            object Result = null;
            if (DBObject.GetType() == IntegerObject.GetType())
            {
                IntegerObject = (DBObject == System.DBNull.Value) ? IntegerObject : (int)DBObject;
                Result = IntegerObject;
            }
            else if (DBObject.GetType() == StringObject.GetType())
            {
                StringObject = (DBObject == System.DBNull.Value) ? string.Empty : (string)DBObject;
                Result = StringObject;
            }

            return Result;
        }

        public static bool Compare(string ObjectValue,string DefaultValue)
        {
            bool Result = (ObjectValue == DefaultValue) ? true : false;
            return Result;
        }

        public static bool HasData(DataSet dsData)
        {
            bool HasData = false;

            if (dsData.Tables == null)
            {
                HasData = false;
            }
            else if (dsData.Tables.Count == 0)
            {
                HasData = false;
            }
            else
            {
                if (dsData.Tables[0].Rows.Count == 0)
                {
                    HasData = false;
                }
                else
                {
                    HasData = true;
                }
            }

            return HasData;
        }

        public static bool HasData(DataTable tblData)
        {
            bool HasData = false;

            if (tblData == null)
            {
                HasData = false;
            }
            else
            {
                if (tblData.Rows.Count == 0)
                {
                    HasData = false;
                }
                else
                {
                    HasData = true;
                }
            }

            return HasData;
        }
        #endregion

        #region Get Business Objects
        public static UserInfo GetUserInfo(DataRow row)
        {
            UserInfo data = new UserInfo();


            int UserID = GetIntValue(row["UserID"]);
            data.UserID = UserID.ToString();
            int PermissionReferenceNo = GetIntValue(row["PermissionReferenceNo"]);
            data.Permission = GetStringValue(row["Permission"]);
            data.Title = GetStringValue(row["ContactTitle"]);
            data.LoginID = GetStringValue(row["LoginID"]);
            data.Password = GetStringValue(row["Password"]);
            data.Domain = GetStringValue(row["Domain"]);
            data.FirstName = GetStringValue(row["FirstName"]);
            data.MiddleName = GetStringValue(row["MiddleName"]);
            data.LastName = GetStringValue(row["LastName"]);

            // This is the personal email in tblUser
            data.Email = GetStringValue(row["Email"]);

            return data;
        }

        #endregion

        #region Add Parameters
        public static SqlParameter AddSQLParameterBit(string ParameterName, bool ParameterValue)
        {
            SqlParameter SqlParameterBit = new SqlParameter(ParameterName, SqlDbType.Bit);
            SqlParameterBit.Value = ParameterValue;
            return SqlParameterBit;
        }

        public static SqlParameter AddSQLParameterInt(string ParameterName, int ParameterValue)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Int);
            SqlParameterInt.Value = ParameterValue;
            return SqlParameterInt;
        }

        public static SqlParameter AddSQLParameterInt(string ParameterName, ParameterDirection Direction)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Int);
            SqlParameterInt.Direction = Direction;
            return SqlParameterInt;
        }

        public static SqlParameter AddSQLParameterVarChar(string ParameterName, int ParameterSize, string ParameterValue)
        {
            SqlParameter SqlParameterVarChar = new SqlParameter(ParameterName, SqlDbType.VarChar, ParameterSize);

            if (ParameterValue == string.Empty)
            {
                SqlParameterVarChar.Value = System.DBNull.Value;
            }
            else
            {
                SqlParameterVarChar.Value = ParameterValue;
            }

            return SqlParameterVarChar;
        }

        public static SqlParameter AddSQLParameterVarChar(string ParameterName, int ParameterSize, ParameterDirection Direction)
        {
            SqlParameter SqlParameterVarChar = new SqlParameter(ParameterName, SqlDbType.VarChar, ParameterSize);
            SqlParameterVarChar.Direction = Direction;
            return SqlParameterVarChar;
        }

        public static SqlParameter AddSQLParameterDateTime(string ParameterName, DateTime ParameterValue)
        {
            SqlParameter SqlParameterDateTime = new SqlParameter(ParameterName, SqlDbType.DateTime);
            SqlParameterDateTime.Value = ParameterValue;
            return SqlParameterDateTime;
        }

        public static SqlParameter AddSQLParameterUniqueIdentifier(string ParameterName, string ParameterValue)
        {
            SqlParameter SqlParameterUniqueIdentifier = new SqlParameter(ParameterName, SqlDbType.UniqueIdentifier);
            Guid paramObjectGUID = new Guid(ParameterValue);
            SqlParameterUniqueIdentifier.Value = paramObjectGUID;
            return SqlParameterUniqueIdentifier;
        }

        public static SqlParameter AddSQLParameterFloat(string ParameterName, float ParameterValue)
        {
            SqlParameter SqlParameterFloat = new SqlParameter(ParameterName, SqlDbType.Float);
            SqlParameterFloat.Value = ParameterValue;
            return SqlParameterFloat;
        }
        #endregion

        #region Stored procedure Calls

        public static SqlParameter[] GetFacilityStatusAll(int ClientID)
        {
            SqlParameter[] arParms = new SqlParameter[1];

            arParms[0] = AddSQLParameterInt("@ClientID",ClientID);

            return arParms;
        }

        public static SqlParameter[] GetFacilityAlertCount(int FacilityID)
        {
            SqlParameter[] arParms = new SqlParameter[2];

            arParms[0] = AddSQLParameterInt("@FacilityID",FacilityID);
            arParms[1] = AddSQLParameterInt("@AlertCount",ParameterDirection.Output);

            return arParms;
        }

        public static SqlParameter[] InsertAlert(string AlertCategory, int AlertObjectID, int AlertLevel, string CreatedBy, DateTime CreatedDate)
        {
            SqlParameter[] arParms = new SqlParameter[6];

            arParms[0] = AddSQLParameterInt("@AlertID",ParameterDirection.Output);
            arParms[1] = AddSQLParameterVarChar("@AlertType",100,AlertCategory);
            arParms[2] = AddSQLParameterInt("@AlertObjectID",AlertObjectID);
            arParms[3] = AddSQLParameterInt("@AlertLevel",AlertLevel);

            return arParms;
        }

        public static SqlParameter[] InsertAlertEmail(int AlertID, string ToAddress, string CCAddress, string ApplicationName, string CreatedBy, DateTime CreatedDate)
        {
            SqlParameter[] arParms = new SqlParameter[7];

            arParms[0] = AddSQLParameterInt("@AlertEmailID",ParameterDirection.Output);
            arParms[1] = AddSQLParameterInt("@AlertID",AlertID);
            arParms[2] = AddSQLParameterVarChar("@ToAddress",150,ToAddress);

            return arParms;
        }

        public static SqlParameter[] InsertLocation(int FacilityID, string Name, string Description, bool IsActive, string CreateBy, DateTime CreateDate, string LastUpdateBy, DateTime LastUpdateDate)
        {
            SqlParameter[] arParms = new SqlParameter[9];

            arParms[0] = AddSQLParameterInt("@LocationID",ParameterDirection.Output);
            arParms[1] = AddSQLParameterInt("@FacilityID",FacilityID);
            arParms[2] = AddSQLParameterVarChar("@Name",100,Name);
            arParms[3] = AddSQLParameterVarChar("@Description",500,Description);
            arParms[4] = AddSQLParameterBit("@IsActive",IsActive);
            arParms[5] = AddSQLParameterVarChar("@CreateBy",50,CreateBy);
            arParms[6] = AddSQLParameterDateTime("@CreateDate",CreateDate);
            arParms[7] = AddSQLParameterVarChar("@LastUpdateBy",50,CreateBy);
            arParms[8] = AddSQLParameterDateTime("@LastUpdateDate",CreateDate);

            return arParms;
        }

        public static SqlParameter[] InsertOrganization(string Code, string Name, string DisplayName, string Description, int Version, bool IsActive, string CreateBy, DateTime CreateDate, string LastUpdateBy, DateTime LastUpdateDate)
        {
            SqlParameter[] arParms = new SqlParameter[11];

            arParms[0] = AddSQLParameterInt("@OrganizationID",ParameterDirection.Output);
            arParms[1] = AddSQLParameterVarChar("@Code",50,Code);
            arParms[2] = AddSQLParameterVarChar("@Name",100,Name);
            arParms[3] = AddSQLParameterVarChar("@DisplayName",100,DisplayName);
            arParms[4] = AddSQLParameterVarChar("@Description",250,Description);
            arParms[5] = AddSQLParameterInt("@Version",Version);
            arParms[6] = AddSQLParameterBit("@IsActive",IsActive);
            arParms[7] = AddSQLParameterVarChar("@CreateBy",50,CreateBy);
            arParms[8] = AddSQLParameterDateTime("@CreateDate",CreateDate);
            arParms[9] = AddSQLParameterVarChar("@LastUpdateBy",50,CreateBy);
            arParms[10] = AddSQLParameterDateTime("@LastUpdateDate",CreateDate);

            return arParms;
        }

        public static SqlParameter[] InsertUser(int ClientID, int PermissionReferenceNo, string Domain, string Password, string Email, int FacilityID, int ContactReferenceNo, string FirstName, string MiddleName, string LastName, string SurName, int TitleReferenceNo, int OrganizationReferenceNo, string AddressLine1, string City, int StateReferenceNo, string PostalCode, string WorkPhone, string HomePhone, string CellPhone, string ContactEmail)
        {
            SqlParameter[] arParms = new SqlParameter[22];

            arParms[0] = AddSQLParameterVarChar("@LoginID",75,ParameterDirection.Output);
            arParms[1] = AddSQLParameterInt("@ClientID",ClientID);
            arParms[2] = AddSQLParameterInt("@PermissionReferenceNo",PermissionReferenceNo);
            arParms[3] = AddSQLParameterVarChar("@Domain",50,Domain);
            arParms[4] = AddSQLParameterVarChar("@Password",100,Password);
            arParms[5] = AddSQLParameterVarChar("@Email",100,Email);
            arParms[6] = AddSQLParameterInt("@FacilityID",FacilityID);
            arParms[7] = AddSQLParameterInt("@ContactReferenceNo",ContactReferenceNo);
            arParms[8] = AddSQLParameterVarChar("@FirstName",75,FirstName);
            arParms[10] = AddSQLParameterVarChar("@LastName",75,LastName);
            arParms[12] = AddSQLParameterInt("TitleReferenceNo",TitleReferenceNo);
            arParms[13] = AddSQLParameterInt("@OrganizationReferenceNo",OrganizationReferenceNo);
            arParms[14] = AddSQLParameterVarChar("@AddressLine1",100,AddressLine1);
            arParms[15] = AddSQLParameterVarChar("@City",35,City);
            arParms[18] = AddSQLParameterVarChar("@WorkPhone",50,WorkPhone);

            return arParms;
        }

        public static SqlParameter[] InsertContact(int ClientID, int ContactReferenceNo, string FirstName, string MiddleName, string LastName, string SurName, int TitleReferenceNo, int OrganizationReferenceNo, string AddressLine1, string City, int StateReferenceNo, string PostalCode, string WorkPhone, string HomePhone, string CellPhone, string ContactEmail)
        {
            SqlParameter[] arParms = new SqlParameter[17];

            arParms[0] = AddSQLParameterInt("@ContactID",ParameterDirection.Output);
            arParms[1] = AddSQLParameterInt("@ClientID",ClientID);
            arParms[2] = AddSQLParameterInt("@ContactReferenceNo",ContactReferenceNo);
            arParms[3] = AddSQLParameterVarChar("@FirstName",75,FirstName);
            arParms[4] = AddSQLParameterVarChar("@MiddleName", 75, MiddleName);
            arParms[5] = AddSQLParameterVarChar("@LastName", 75, LastName);
            arParms[6] = AddSQLParameterVarChar("@SurName", 50, SurName);
            arParms[7] = AddSQLParameterInt("TitleReferenceNo",TitleReferenceNo);
            arParms[8] = AddSQLParameterInt("@OrganizationReferenceNo",OrganizationReferenceNo);
            arParms[9] = AddSQLParameterVarChar("@AddressLine1", 100, AddressLine1);
            arParms[10] = AddSQLParameterVarChar("@City", 35, City);
            arParms[11] = AddSQLParameterInt("@StateReferenceNo",StateReferenceNo);
            arParms[12] = AddSQLParameterVarChar("@PostalCode",16,PostalCode);
            arParms[13] = AddSQLParameterVarChar("@WorkPhone",50,WorkPhone);
            arParms[14] = AddSQLParameterVarChar("@HomePhone",50,HomePhone);
            arParms[15] = AddSQLParameterVarChar("@CellPhone",50,CellPhone);
            arParms[16] = AddSQLParameterVarChar("@Email",100,ContactEmail);

            return arParms;
        }

        public static SqlParameter[] InsertAnnouncement(int StatusReferenceID, string Title, string Author, string Description, DateTime DatePublished, int PublisherUserID)
        {
            SqlParameter[] arParms = new SqlParameter[7];

            arParms[0] = AddSQLParameterInt("@AnnouncementID",ParameterDirection.Output);
            arParms[1] = AddSQLParameterInt("@StatusReferenceID",StatusReferenceID);
            arParms[2] = AddSQLParameterVarChar("@Title",150,Title);
            arParms[3] = AddSQLParameterVarChar("@Author",150,Author);
            arParms[4] = AddSQLParameterVarChar("@Description",2000,Description);
            arParms[5] = AddSQLParameterDateTime("@DatePublished",DatePublished);

            // The PublisherUserID can be the AutherUserID or it can be the UserID of the person who
            // sent out the announcement. Usage is based on scenario.
            arParms[6] = AddSQLParameterInt("@PublisherUserID",PublisherUserID);

            return arParms;
        }

        public static SqlParameter[] InsertUserTrack(int UserID, string BrowserType, string UserIPAddress, string RemoteHostIPAddress,string DomainName,string RefererURL,string CurrentPage,string ServerPortNo,string ServerSoftware,string Activity,DateTime ActivityDate)
        {
            SqlParameter[] arParms = new SqlParameter[12];

            arParms[0] = AddSQLParameterInt("@UserTrackID",ParameterDirection.Output);
            arParms[1] = AddSQLParameterInt("@UserID", UserID);
            arParms[2] = AddSQLParameterVarChar("@BrowserType", 250, BrowserType);
            arParms[3] = AddSQLParameterVarChar("@UserIPAddress", 100, UserIPAddress);
            arParms[4] = AddSQLParameterVarChar("@RemoteHostIPAddress", 100, RemoteHostIPAddress);
            arParms[5] = AddSQLParameterVarChar("@DomainName", 200, DomainName);
            arParms[6] = AddSQLParameterVarChar("@RefererURL", 400, RefererURL);
            arParms[7] = AddSQLParameterVarChar("@CurrentPage", 200, CurrentPage);
            arParms[8] = AddSQLParameterVarChar("@ServerPortNo", 50, ServerPortNo);
            arParms[9] = AddSQLParameterVarChar("@ServerSoftware", 150, ServerSoftware);
            arParms[10] = AddSQLParameterVarChar("@Activity", 100, Activity);
            arParms[11] = AddSQLParameterDateTime("@ActivityDate", ActivityDate);

            return arParms;
        }

        public static SqlParameter[] InsertErrorLog(string ErrorClass,string @ErrorType, string ErrorCode, string ErrorObject, string ErrorSource, string ErrorSourceLineNo, string ErrorMessage, DateTime ReportedDate, int UserID, string LoginID, bool DisplayException, string StackTrace, string Detail, string Debug)
        {
            SqlParameter[] arParms = new SqlParameter[15];

            arParms[0] = AddSQLParameterInt("@ErrorLogID", ParameterDirection.Output);
            arParms[1] = AddSQLParameterVarChar("@ErrorClass", 100, ErrorClass);
            arParms[2] = AddSQLParameterVarChar("@ErrorType", 100, ErrorType);
            arParms[3] = AddSQLParameterVarChar("@Code", 100, ErrorCode);
            arParms[4] = AddSQLParameterVarChar("@Object", 100, ErrorObject);
            arParms[5] = AddSQLParameterVarChar("@Source", 200, ErrorSource);
            arParms[6] = AddSQLParameterVarChar("@SourceLineNo", 50, ErrorSourceLineNo);
            arParms[7] = AddSQLParameterVarChar("@Message", 250, ErrorMessage);
            arParms[8] = AddSQLParameterDateTime("@ReportedDate", ReportedDate);
            arParms[9] = AddSQLParameterInt("@UserID",UserID);
            arParms[10] = AddSQLParameterVarChar("@LoginID", 100, LoginID);
            arParms[11] = AddSQLParameterBit("@DisplayException",DisplayException);
            arParms[12] = AddSQLParameterVarChar("@Trace", 700, StackTrace);
            arParms[13] = AddSQLParameterVarChar("@Detail", 700, Detail);
            arParms[14] = AddSQLParameterVarChar("@Debug", 700, Debug);

            return arParms;
        }

        public static SqlParameter[] UpdateUserFacility(int UserID, int FacilityID,bool IsDefault,int Option)
        {
            SqlParameter[] arParms = new SqlParameter[4];

            arParms[0] = AddSQLParameterInt("@UserID", UserID);
            arParms[1] = AddSQLParameterInt("@FacilityID", FacilityID);
            arParms[2] = AddSQLParameterBit("@IsDefault", IsDefault);
            arParms[3] = AddSQLParameterInt("@Option", Option);

            return arParms;
        }

        public static SqlParameter[] UpdateAnnouncement(int AnnouncementID,string AnnouncementStatus,string Title,string Description,DateTime DatePublished)
        {
            SqlParameter[] arParms = new SqlParameter[5];

            arParms[0] = AddSQLParameterInt("@AnnouncementID", AnnouncementID);
            arParms[1] = AddSQLParameterVarChar("@AnnouncementStatus", 100, AnnouncementStatus);
            arParms[2] = AddSQLParameterVarChar("@Title", 150, Title);
            arParms[3] = AddSQLParameterVarChar("@Description", 2000, Description);
            arParms[4] = AddSQLParameterDateTime("@DatePublished", DatePublished);

            return arParms;
        }

        public static SqlParameter[] UpdateReportQuestion(int QuestionID, int FacilityID, string Code, string Heading, string QuestionText)
        {
            SqlParameter[] arParms = new SqlParameter[5];

            arParms[0] = AddSQLParameterInt("@QuestionID", QuestionID);
            arParms[1] = AddSQLParameterInt("@FacilityID", FacilityID);
            arParms[2] = AddSQLParameterVarChar("@Code", 100, Code);
            arParms[3] = AddSQLParameterVarChar("@Heading", 100, Heading);
            arParms[4] = AddSQLParameterVarChar("@QuestionText", 1500, QuestionText);

            return arParms;
        }
        public static SqlParameter[] DeleteUser(int UserID)
        {
            SqlParameter[] arParms = new SqlParameter[1];

            arParms[0] = AddSQLParameterInt("@UserID", UserID);

            return arParms;
        }

        public static SqlParameter[] DeleteFacilityAlertSetting(int FacilityAlertSettingID)
        {
            SqlParameter[] arParms = new SqlParameter[1];

            arParms[0] = AddSQLParameterInt("@FacilityAlertSettingID", FacilityAlertSettingID);

            return arParms;
        }


        public static SqlParameter[] DeleteObject(string ObjectType, string ObjectName)
        {
            SqlParameter[] arParms = new SqlParameter[2];

            arParms[0] = AddSQLParameterVarChar("@ObjectType",100,ObjectType);
            arParms[1] = AddSQLParameterVarChar("@ObjectName",100,ObjectName);

            return arParms;
        }

        public static SqlParameter[] DeleteObject(string ObjectGUID)
        {
            SqlParameter[] arParms = new SqlParameter[1];

            arParms[0] = AddSQLParameterUniqueIdentifier("@SelectedObjectGUID",ObjectGUID);

            return arParms;
        }


        public static SqlParameter[] DeletePersonByNameUsage(string PersonName, string PersonType)
        {
            SqlParameter[] arParms = new SqlParameter[2];

            arParms[0] = AddSQLParameterVarChar("@PersonName", 100,PersonName);
            arParms[1] = AddSQLParameterVarChar("@PersonType", 100,PersonType);

            return arParms;
        }

        public static SqlParameter[] DeletePersonByUID(string PersonUID, string PersonType)
        {
            SqlParameter[] arParms = new SqlParameter[2];

            arParms[0] = AddSQLParameterVarChar("@PersonUID",100,PersonUID);
            arParms[1] = AddSQLParameterVarChar("@PersonType", 100,PersonType);

            return arParms;
        }

        #endregion

    }
    
}
