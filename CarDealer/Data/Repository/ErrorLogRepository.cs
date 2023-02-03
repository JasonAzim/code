using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Global.Utility;
using Global.Repository;
using CarDealer.Data;

namespace CarDealer.Repository
{
    public class ErrorLogRepository : RepositoryBase, IRepository<ErrorLogEntity>
    {
        private ErrorLogEntity _Entity = null;

        public ErrorLogEntity Entity
        {
            set
            {
                _Entity = value;
            }
            get
            {
                return _Entity;
            }
        }

        private ErrorLogRepository()
        {
            // used by static instance constructor
            DataManager = new SQLManager();
            this.ObjectName = Constants.TABLE_COMPANY;
        }

        // An entity can be read from a database or it can be created by filling in the properties. 
        public ErrorLogRepository(ErrorLogEntity entity)
        {
            Entity = entity;
            DataManager = new SQLManager();
            this.ObjectName = Constants.TABLE_COMPANY;
        }

        public static ErrorLogRepository Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly ErrorLogRepository instance = new ErrorLogRepository();
        }

        #region Common CRUD Functions
        public List<ErrorLogEntity> GetAll()
        {
            SQLProxy proxy = new SQLProxy();
            string query = SQLUtility.ERRORLOG_VIEW_SELECT;
            DataSet dsResult = proxy.GetDataSetFromQuery(query);
            proxy.State.MaskException = true;
            HandleException(proxy.State, (int)ObjectActionType.View);
            if (State.ErrorOccurred) throw new Exception("Database Log Exception " + DataManager.State.ErrorMessage);

            var aList = dsResult.Tables[0].AsEnumerable().Select(dataRow => new ErrorLogEntity {
                Id = dataRow.Field<int>("ErrorLogID"),
                ErrorClass = dataRow.Field<string>("ErrorClass"),
                ErrorType = dataRow.Field<string>("ErrorType"),
                ErrorCode = dataRow.Field<string>("ErrorCode"),
                ErrorObject = dataRow.Field<string>("ErrorObject"),
                ErrorSource = dataRow.Field<string>("ErrorSource"),
                ErrorSourceLineNo = dataRow.Field<string>("ErrorSourceLineNo"),
                ErrorMessage = dataRow.Field<string>("ErrorMessage"),
                ReportedDate = dataRow.Field<DateTime>("ReportedDate"),
                UserID = dataRow.Field<int>("UserID"),
                LoginID = dataRow.Field<string>("LoginID"),
                DisplayException = dataRow.Field<bool>("DisplayException"),
                StackTrace = dataRow.Field<string>("StackTrace"),
                Detail = dataRow.Field<string>("Detail"),
                Debug = dataRow.Field<string>("Debug")
            }).ToList();

            return aList;
        }

        public ErrorLogEntity GetById(int id)
        {
            SQLProxy proxy = new SQLProxy();
            string query = SQLUtility.ERRORLOG_VIEW_SELECT + SQLUtility.WHERE + string.Format(SQLUtility.ERRORLOG_FILTER_BY_ID, id);
            DataSet dsResult = proxy.GetDataSetFromQuery(query);
            proxy.State.MaskException = true;
            HandleException(proxy.State, (int)ObjectActionType.View);
            if (State.ErrorOccurred) throw new Exception("Database Log Exception " + DataManager.State.ErrorMessage);

            var aObject = dsResult.Tables[0].AsEnumerable().Select(dataRow =>
                new ErrorLogEntity
                {
                    Id = dataRow.Field<int>("ErrorLogID"),
                    ErrorClass = dataRow.Field<string>("ErrorClass"),
                    ErrorType = dataRow.Field<string>("ErrorType"),
                    ErrorCode = dataRow.Field<string>("ErrorCode"),
                    ErrorObject = dataRow.Field<string>("ErrorObject"),
                    ErrorSource = dataRow.Field<string>("ErrorSource"),
                    ErrorSourceLineNo = dataRow.Field<string>("ErrorSourceLineNo"),
                    ErrorMessage = dataRow.Field<string>("ErrorMessage"),
                    ReportedDate = dataRow.Field<DateTime>("ReportedDate"),
                    UserID = dataRow.Field<int>("UserID"),
                    LoginID = dataRow.Field<string>("LoginID"),
                    DisplayException = dataRow.Field<bool>("DisplayException"),
                    StackTrace = dataRow.Field<string>("StackTrace"),
                    Detail = dataRow.Field<string>("Detail"),
                    Debug = dataRow.Field<string>("Debug")
                }).FirstOrDefault();

            return aObject;
        }

        public int Create(ErrorLogEntity entity)
        {
            entity.Id = DataManager.ErrorLogInsert(entity.ErrorClass, entity.ErrorType, entity.ErrorCode, entity.ErrorObject, entity.ErrorSource, entity.ErrorSourceLineNo, entity.ErrorMessage, entity.ReportedDate, entity.UserID, entity.LoginID, entity.DisplayException, entity.StackTrace, entity.Detail, entity.Debug);
            HandleException(DataManager.State, (int)ObjectActionType.Create);
            Entity = entity;
            return entity.Id;
        }

        public void Delete(ErrorLogEntity entity)
        {
        }

        public void Update(ErrorLogEntity entity)
        {
        }

        #endregion
    }
}
