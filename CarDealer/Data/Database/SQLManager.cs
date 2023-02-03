using System;
using System.Collections; 
using System.Collections.Generic;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Global.Utility;

namespace CarDealer.Data
{
    public class SQLManager : DataObject
    {
        private string _ConnectionString = string.Empty;

        public string ConnectionString
        {
            get
            {
                return _ConnectionString;
            }
            set
            {
                _ConnectionString = value;
            }
        }

        private bool _PersistConnection = false;

        public bool PersistConnection
        {
            get
            {
                return _PersistConnection;
            }
            set
            {
                _PersistConnection = value;
            }
        }

        private SqlConnection _SharedConnection = null;

        public SQLManager()
        {
            _ConnectionString = SettingsHelper.SQLServerDB();
            State = new ObjectState();
        }

        public SQLManager(string connectionString)
        {
            _ConnectionString = connectionString;
            State = new ObjectState();
        }

        ~SQLManager()
        {
            State = null;
            DisposeConnection();
        }

        public void DisposeConnection()
        {
            if (_PersistConnection == true)
            {
                if (_SharedConnection != null)
                {
                    _SharedConnection.Close();
                    _SharedConnection.Dispose();
                    _SharedConnection = null;
                }
            }
            else
            {
            }
        }

        private void DisposeConnection(SqlConnection DBConnection)
        {
            if (_PersistConnection == false)
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                    DBConnection.Dispose();
                }
            }
            else
            {
                // Close the connection but do not dispose it since we will use it for next transaction
                if (DBConnection != null)
                {
                    if (DBConnection.State == ConnectionState.Closed)
                    {
                        // Connection is already closed so do nothing
                    }
                    else
                    {
                        // Close the connection
                        DBConnection.Close();
                    }
                }
            }
        }

        private void DisposeReader(SqlDataReader reader)
        {
            if (reader != null)
            {
                reader.Close();
                ((IDisposable)reader).Dispose();
            }
        }

        public string SQLQuery = string.Empty;

        private string SetQuery(string query)
        {
            State.Query = query;
            SQLQuery = query;
            return query;
        }

        public string ServerName = string.Empty;

        #region Worker Functions

        private SqlConnection GetConnection(string connectionString)
        {
            SqlConnection DBConnection = null;

            if (_PersistConnection == true)
            {
                // Use in case of a transaction to execute multiple commands sequentially
                if (_SharedConnection == null)
                {
                    // initialize the shared connection
                    _SharedConnection = new SqlConnection(connectionString);
                    DBConnection = _SharedConnection;
                }
                else
                {
                    DBConnection = _SharedConnection;
                }
            }
            else
            {
                DBConnection = new SqlConnection(connectionString);
            }
            return DBConnection;
        }

        public void ExecuteSP(string SPName, ref SqlParameter[] arParms)
        {
            SqlConnection DBConnection = null;

            try
            {
                DBConnection = GetConnection(_ConnectionString);

                string Query = SetQuery("EXECUTE @Result = " + SPName);

                SQLHelper.ExecuteNonQuery(DBConnection, CommandType.StoredProcedure, SPName, arParms);
            }
            catch (Exception exp)
            {
                HandleException(exp, "Execute Store Procedure Failed:" + SPName);
            }
            finally
            {
                DisposeConnection(DBConnection);
            }
        }

        public DataSet ExecuteSPDataSet(string SPName, ref SqlParameter[] arParms)
        {
            SqlConnection DBConnection = null;
            DataSet result = null;

            try
            {
                DBConnection = GetConnection(_ConnectionString);

                string Query = SetQuery("EXECUTE @Result = " + SPName);

                result = SQLHelper.ExecuteDataset(DBConnection, SPName, arParms);
            }
            catch (Exception ex)
            {
                HandleException(ex, "Execute Store Procedure Failed:" + SPName);
            }
            finally
            {
                DisposeConnection(DBConnection);
            }

            return result;
        }

        public void ExecuteQuery(string SQL)
        {
            // SqlConnection that will be used to execute the sql commands
            SqlConnection DBConnection = null;

            try
            {
                string Query = SetQuery(SQL);
                DBConnection = GetConnection(_ConnectionString);
                SQLHelper.ExecuteNonQuery(DBConnection, CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                HandleException(ex, "ExecuteQuery Error");
            }
            finally
            {
                DisposeConnection(DBConnection);
            }
        }

        public void ExecuteQueries(List<string> QueryList)
        {
            // SqlConnection that will be used to execute the sql commands
            SqlConnection DBConnection = null;

            try
            {
                DBConnection = GetConnection(_ConnectionString);
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                foreach (string query in QueryList)
                {
                    DBCommand.CommandText = SetQuery(query);
                    DBCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "ExecuteQueries Error");
            }
            finally
            {
                DisposeConnection(DBConnection);
            }
        }

        public DataSet GetData(string SQL)
        {
            SqlConnection DBConnection = null;
            DataSet DS = new DataSet();

            try
            {
                DBConnection = GetConnection(_ConnectionString);
                string Query = SetQuery(SQL);
                DS = SQLHelper.ExecuteDataset(DBConnection, CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                DisposeConnection(DBConnection);
            }

            return DS;
        }

        public static IDictionary<string, object> ReadRow(DbDataReader reader)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            for (int i = 0; i < reader.FieldCount; ++i)
            {
                object value = reader.GetValue(i);
                string name = reader.GetName(i);
                if ((value != null))
                {
                    if (value is System.DBNull)
                    {
                        value = null;
                    }
                }
                if (value != null || !ret.ContainsKey(name))
                {
                    ret[name] = value;
                }
            }

            return ret;
        }

        public DataSet GetPagedData(string SQL,string TableName, int Start, int Max)
        {
            SqlConnection DBConnection = null;
            DataSet result = null;

            try
            {
                DBConnection = GetConnection(_ConnectionString);
                string Query = SetQuery(SQL);

                DBConnection.Open();

                SqlDataAdapter DA = new SqlDataAdapter(Query, DBConnection);

                DA.Fill(result, Start, Max, TableName);
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get paged data failed");
            }
            finally
            {
                DisposeConnection(DBConnection);
            }

            return result;
        }

        public int ExecuteScalar(string SQL)
        {
            SqlConnection DBConnection = null;
            int result = 0;

            try
            {
                DBConnection = GetConnection(_ConnectionString);
                string Query = SetQuery(SQL);
                Object ReturnedObject = SQLHelper.ExecuteScalar(DBConnection, CommandType.Text, Query);

                if (ReturnedObject == System.DBNull.Value)
                {
                    result = 0;
                }
                else
                {
                    result = (int)ReturnedObject;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex,"Get Count failed");
            }
            finally
            {
                DisposeConnection(DBConnection);
            }

            return result;
        }

        public string ExecuteScalarReturnString(string SQL)
        {
            SqlConnection DBConnection = null;
            SqlDataReader reader = null;
            string result = string.Empty;

            try
            {
                DBConnection =  GetConnection(_ConnectionString);
                string Query = SetQuery(SQL);
                Object ReturnedObject = SQLHelper.ExecuteScalar(DBConnection, CommandType.Text, Query);

                if (ReturnedObject == System.DBNull.Value)
                {
                    result = "None";
                }
                else
                {
                    result = (string)ReturnedObject;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex,"ExecuteSQL Failed");
            }
            finally
            {
                DisposeReader(reader); 
                DisposeConnection(DBConnection);
            }

            return result;
        }

        public DataSet GetSchemaFromTableName(string TableName)
        {
            string SQL = "select top 1 * from " + TableName;

            return GetData(SQL);
        }

        public DataSet GetSchema(string SQL)
        {
            return GetData(SQL);
        }

        public DataSet convertDataReaderToDataSet(SqlDataReader reader)
        {
            DataSet dataSet = new DataSet();
            do
            {
                // Create new data table

                DataTable schemaTable = reader.GetSchemaTable();
                DataTable dataTable = new DataTable();

                if (schemaTable != null)
                {
                    // A query returning records was executed

                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        DataRow dataRow = schemaTable.Rows[i];
                        // Create a column name that is unique in the data table
                        string columnName = (string)dataRow["ColumnName"]; //+ "<C" + i + "/>";
                        // Add the column definition to the data table
                        DataColumn column = new DataColumn(columnName, (Type)dataRow["DataType"]);
                        dataTable.Columns.Add(column);
                    }

                    dataSet.Tables.Add(dataTable);

                    // Fill the data table we just created

                    while (reader.Read())
                    {
                        DataRow dataRow = dataTable.NewRow();

                        for (int i = 0; i < reader.FieldCount; i++)
                            dataRow[i] = reader.GetValue(i);

                        dataTable.Rows.Add(dataRow);
                    }
                }
                else
                {
                    // No records were returned

                    DataColumn column = new DataColumn("RowsAffected");
                    dataTable.Columns.Add(column);
                    dataSet.Tables.Add(dataTable);
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = reader.RecordsAffected;
                    dataTable.Rows.Add(dataRow);
                }
            }
            while (reader.NextResult());
            return dataSet;
        }
        #endregion

        #region List Functions

        public List<string> ExecuteSQLReturnList(string SQL, string FieldName)
        {
            SqlConnection DBConnection = null;
            SqlDataReader reader = null;
            List<string> ResultList = new List<string>();

            try
            {
                DBConnection = GetConnection(_ConnectionString);
                string Query = SetQuery(SQL);
                reader = SQLHelper.ExecuteReader(DBConnection, CommandType.Text, Query);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ResultList.Add(reader[FieldName].ToString());
                    }
                }
                else
                {
                    ResultList = null;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "failed");
            }
            finally
            {
                DisposeReader(reader); 
                DisposeConnection(DBConnection);
            }

            return ResultList;
        }
        #endregion

        #region Stored procedure Calls

        public int ErrorLogInsert(string ErrorClass,string ErrorType,string ErrorCode,string ErrorObject,string ErrorSource,string ErrorSourceLineNo,string ErrorMessage,DateTime ReportedDate,int UserID,string LoginID,bool DisplayException,string StackTrace,string Detail,string Debug)
        {
            SqlParameter[] arParms = SQLUtility.ErrorLogInsert(ErrorClass, ErrorType, ErrorCode, ErrorObject, ErrorSource, ErrorSourceLineNo, ErrorMessage, ReportedDate, UserID, LoginID, DisplayException, StackTrace, Detail, Debug);

            ExecuteSP("ErrorLogInsert", ref arParms);

            return (int)arParms[0].Value;
        }


        #endregion

    }
}