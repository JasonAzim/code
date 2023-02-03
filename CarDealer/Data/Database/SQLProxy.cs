using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Global.Utility;

namespace CarDealer.Data
{
    public class SQLProxy : DataObject 
    {
        public const string NONE = "NONE";

        private string _connectionString = string.Empty;

        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }

        public SQLProxy()
        {
            _connectionString = SettingsHelper.SQLServerDB();
            State = new ObjectState();
        }

        public SQLProxy(string DatabaseConnectionString)
        {
            _connectionString = DatabaseConnectionString;
            State = new ObjectState();
        }

        public string SQLQuery = string.Empty;

        private string SetQuery(string query)
        {
            State.Query = query;
            SQLQuery = query;
            return query;
        }

        public DataTable GetDataSchema(string dataSetName, string TableName,string SQL)
        {
            DataTable table = new DataTable(dataSetName);

            SqlConnection DBConnection = null;
            string Result = string.Empty;

            try
            {
                DBConnection = new SqlConnection(_connectionString);

                SqlDataAdapter adapter = new SqlDataAdapter(SQL, DBConnection);

                DataTableMapping mapping = adapter.TableMappings.Add("Table", TableName);
                // mapping.ColumnMappings.Add("CompanyName", "Name");

                DBConnection.Open();

                adapter.FillSchema(table, SchemaType.Mapped);
                adapter.Fill(table);
                return table;

            }
            catch (Exception ex)
            {
                HandleException(ex, "Get failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

            return table;

        }

        public object[] ExecuteSingleRowCommand(SqlCommand cmd)
        {
            cmd.Connection = new SqlConnection(_connectionString);
            object[] result = null;
            using (cmd.Connection)
            {
                try
                {
                    cmd.Connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    if (rdr.Read())
                    {
                        result = new object[rdr.FieldCount];
                        rdr.GetValues(result);
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Get failed");
                }

            }
            return result;
        }

        public List<object[]> ExecuteMultipleRowCommand(SqlCommand cmd)
        {
            List<object[]> result = new List<object[]>();
            cmd.Connection = new SqlConnection(_connectionString);
            using (cmd.Connection)
            {
                try
                {
                    cmd.Connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        object[] oneRow = new object[rdr.FieldCount];
                        rdr.GetValues(oneRow);
                        result.Add(oneRow);
                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Get failed");
                }
            }
            return result;
        }

        public void ExecuteNonQueryCommand(SqlCommand cmd, int? expectedRecordsAffected)
        {
            cmd.Connection = new SqlConnection(_connectionString);
            int recordsAffected = 0;
            using (cmd.Connection)
            {
                try
                {
                    cmd.Connection.Open();
                    recordsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Get failed");
                }

            }
            if (expectedRecordsAffected != null && recordsAffected != expectedRecordsAffected)
            {
                throw new Exception("testing");
            }
        }

        public object ExecuteScalarCommand(SqlCommand cmd)
        {
            cmd.Connection = new SqlConnection(_connectionString);
            object result = null;
            using (cmd.Connection)
            {
                try
                {
                    cmd.Connection.Open();
                    result = cmd.ExecuteScalar();
                    cmd.Connection.Close();
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Get failed");
                }

            }
            return result;
        }

        public void ExecuteSingleRowCommand(Dictionary<string, object> resultContainer, SqlCommand cmd)
        {
            cmd.Connection = new SqlConnection(_connectionString);
            //object[] result = null;
            using (cmd.Connection)
            {
                try
                {
                    cmd.Connection.Open();
                    SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    if (rdr.Read())
                    {
                        for (int i = 0; i < rdr.FieldCount; i++)
                            resultContainer.Add(rdr.GetName(i), rdr[i]);

                    }
                }
                catch (Exception ex)
                {
                    HandleException(ex, "Get failed");
                }
            }
        }

        public string GetStringColumn(string Query)
        {
            SqlConnection DBConnection = null;
            string Result = string.Empty;

            try
            {
                DBConnection = new SqlConnection(_connectionString);
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(Query); ;

                SqlDataReader reader = DBCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    Result = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                }
                else
                {
                    // No rows were returned.
                    State.ErrorType = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

            return Result;
        }

        public void Insert(string SequenceSelectQuery,string InsertQuery, ref string returnID, string Field1, string Field2)
        {
            SqlConnection DBConnection = null;

            // string SelectQuery = "select to_char(seq_activity.nextval) from dual";
            // string InsertQuery = "insert into table (id, field1, field2) values  ({0}, 'Field1','Field2')";

            string Query = SequenceSelectQuery;

            try
            {
                DBConnection = new SqlConnection(_connectionString);
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(SequenceSelectQuery);

                DBCommand.CommandText = SetQuery(InsertQuery);

                SqlDataReader reader = DBCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    returnID = reader.GetString(0);

                    Query = string.Format(InsertQuery, returnID);

                    DBCommand.CommandText = SetQuery(Query);
                    DBCommand.ExecuteNonQuery();
                }
                else
                {
                    // No rows were returned.
                    State.ErrorType = 1;
                }

            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

        }

        public string GetQueryReferenceID(string TableName,string Name)
        {
            string SQL = string.Empty;
            SQL = "select ID as Code,DisplayName as Name from {0} where Name = '{1}'";

            string Query = string.Format(SQL, TableName, Name);
            return Query;
        }

        public string GetQueryReferenceIDOnly(string TableName, string Name)
        {
            string SQL = string.Empty;
            SQL = "select ID from {0} where Name = '{1}'";

            string Query = string.Format(SQL, TableName, Name);
            return Query;
        }

        public string GetQueryReferenceNameFromCode(string TableName, string Code)
        {
            string SQL = string.Empty;
            SQL = "select Name from {0} where Code = '{1}'";

            string Query = string.Format(SQL, TableName, Code);
            return Query;
        }

        public void ReadUserInfo(string userID, ref string firstName, ref string middleName, ref string lastName, ref string email, ref string phoneNumber)
        {
            SqlConnection DBConnection = null;
            string Query = "select first_name, middle_name, last_name, email_address, phone_number from users where id = '" + userID + "'";

            try
            {
                DBConnection = new SqlConnection(_connectionString);
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(Query);

                SqlDataReader reader = DBCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();

                    firstName = reader.IsDBNull(0) ? NONE : reader.GetString(0);
                    middleName = reader.IsDBNull(1) ? NONE : reader.GetString(1);
                    lastName = reader.IsDBNull(2) ? NONE : reader.GetString(2);
                    email = reader.IsDBNull(3) ? NONE : reader.GetString(3);
                    phoneNumber = reader.IsDBNull(4) ? NONE : reader.GetString(4);
                }
                else
                {
                    // No rows were returned.
                    State.ErrorType = 1; 
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

        }

        public string ExecuteScalar(string Query)
        {
            SqlConnection DBConnection = new SqlConnection(_connectionString);
            string Results = string.Empty;

            try
            {
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(Query);
                SqlDataReader reader = DBCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Results = reader.IsDBNull(0) ? NONE : reader.GetString(0);
                }

            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

            return Results;
        }

        public int ExecuteScalarInt(string Query)
        {
            SqlConnection DBConnection = new SqlConnection(_connectionString);
            int Result = 0;

            try
            {
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(Query);
                SqlDataReader reader = DBCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    Result = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
                }

            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

            return Result;
        }

        public decimal ExecuteScalarDecimal(string Query)
        {
            SqlConnection DBConnection = new SqlConnection(_connectionString);
            decimal Results = decimal.Zero;

            try
            {
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(Query);
                SqlDataReader reader = DBCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        Results = reader.GetDecimal(0);
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

            return Results; 
        }

        public DataSet GetDataSetFromQuery(string SelectQuery)
        {
            DataSet DS = new DataSet();

            try
            {
                string Query = SetQuery(SelectQuery);
                DS = SQLHelper.ExecuteDataset(_connectionString, CommandType.Text, Query);
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }

            return DS;
        }

        public void ExecuteQueries(List<string> QueryList)
        {
            SqlConnection DBConnection = new SqlConnection(_connectionString);

            try
            {
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
                HandleException(ex, "update data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

        }

        public void ExecuteQuery(string Query)
        {
            SqlConnection DBConnection = new SqlConnection(_connectionString);

            try
            {
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(Query);
                DBCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }
        }

        public Hashtable GetColumnNames(string Query)
        {
            SqlConnection DBConnection = new SqlConnection(_connectionString);
            Hashtable Results = new Hashtable();

            string dataType = string.Empty;
            string columnName = string.Empty;

            try
            {
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(Query);

                SqlDataReader reader = DBCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        columnName = reader.IsDBNull(0) ? NONE : reader.GetString(0);
                        dataType = reader.IsDBNull(4) ? NONE : reader.GetString(4);

                        Results.Add(columnName, dataType);
                    }
                }
                else
                {
                    State.ErrorType = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

            return Results;
        }

        public ArrayList GetList(string Query)
        {
            SqlConnection DBConnection = new SqlConnection(_connectionString);
            ArrayList Results = new ArrayList();
            string Item = string.Empty;

            try
            {
                DBConnection.Open();

                SqlCommand DBCommand = DBConnection.CreateCommand();

                DBCommand.CommandText = SetQuery(Query);

                SqlDataReader reader = DBCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Item = reader.IsDBNull(0) ? NONE : reader.GetString(0);
                        Results.Add(Item);
                    }
                }
                else
                {
                    State.ErrorType = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "Get data failed");
            }
            finally
            {
                if (DBConnection != null)
                {
                    DBConnection.Close();
                }
            }

            return Results;
        }

    }
}