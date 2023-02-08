using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace CarDealer.Data
{
    public static class SQLUtility 
    {
        public static readonly string VIEW_PREFIX = "v";
        public static readonly string PERSON_TABLE_NAME = "HR";

        public static readonly string WHERE = " where ";
        public static readonly string ORDER_BY = " order by ";

        public static readonly string CATEGORY_TABLE_SELECT = "select CategoryNo,[Name] from dbo.Category";

        public static readonly string ERRORLOG_VIEW_SELECT = "SELECT ErrorLogID,ErrorClass,ErrorType,Code,Object,Source,SourceLineNo,Message,ReportedDate,UserID,LoginID,DisplayException,Trace,Detail,Debug FROM ErrorLog";
        public static readonly string ERRORLOG_FILTER_BY_ID = "ErrorLogID = {0}";

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

        public static SqlParameter AddSQLParameterDecimal(string ParameterName, ParameterDirection Direction)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Decimal);
            SqlParameterInt.Direction = Direction;
            return SqlParameterInt;
        }

        public static SqlParameter AddSQLParameterDecimal(string ParameterName, Decimal ParameterValue)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Decimal);
            SqlParameterInt.Value = ParameterValue;
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

        public static SqlParameter AddSQLParameterVarCharMax(string ParameterName, string ParameterValue)
        {
            SqlParameter SqlParameterVarChar = new SqlParameter(ParameterName, SqlDbType.VarChar, -1);

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

        public static SqlParameter AddSQLParameterDateTime(string ParameterName, DateTime ParameterValue)
        {
            SqlParameter SqlParameterDateTime = new SqlParameter(ParameterName, SqlDbType.DateTime);
            SqlParameterDateTime.Value = ParameterValue;
            return SqlParameterDateTime;
        }

        public static SqlParameter AddSQLParameterUniqueIdentifier(string ParameterName, string ParameterValue)
        {
            SqlParameter SqlParameterUniqueIdentifier = new SqlParameter(ParameterName, SqlDbType.UniqueIdentifier);

            if (ParameterValue == string.Empty)
            {
                SqlParameterUniqueIdentifier.Value = System.DBNull.Value;
            }
            else
            {
                SqlParameterUniqueIdentifier.Value = new Guid(ParameterValue);
            }
            
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
        public static SqlParameter[] TableDelete(int ID, string FieldName)
        {
            SqlParameter[] arParms = new SqlParameter[1];
            string ParameterName = "@" + FieldName;
            arParms[0] = AddSQLParameterInt(ParameterName, ID);

            return arParms;
        }

        public static SqlParameter[] ErrorLogInsert(string ErrorClass, string ErrorType, string ErrorCode, string ErrorObject, string ErrorSource, string ErrorSourceLineNo, string ErrorMessage, DateTime ReportedDate, int UserID, string LoginID, bool DisplayException, string StackTrace, string Detail, string Debug)
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
            arParms[9] = AddSQLParameterInt("@UserID", UserID);
            arParms[10] = AddSQLParameterVarChar("@LoginID", 100, LoginID);
            arParms[11] = AddSQLParameterBit("@DisplayException", DisplayException);
            arParms[12] = AddSQLParameterVarChar("@Trace", 700, StackTrace);
            arParms[13] = AddSQLParameterVarChar("@Detail", 700, Detail);
            arParms[14] = AddSQLParameterVarChar("@Debug", 700, Debug);

            return arParms;
        }

        #endregion

    }

}
