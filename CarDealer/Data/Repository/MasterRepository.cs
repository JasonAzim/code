using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Global.Utility;
using Global.Repository;
using CarDealer;
using CarDealer.Data;
using CarDealer.Repository;

namespace CarDealer.Repository
{
    public sealed class MasterRepository : RepositoryBase, IRepository<CategoryEntity>
    {
        private CategoryEntity _Entity = null;

        public CategoryEntity Entity
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

        private MasterRepository()
        {
            // used by static instance constructor
            DataManager = new SQLManager();
            this.ObjectName = Constants.TABLE_PERSON;
        }

        // An entity can be read from a database or it can be created by filling in the properties. 
        public MasterRepository(CategoryEntity entity)
        {
            Entity = entity;
            DataManager = new SQLManager();
            this.ObjectName = Constants.TABLE_COMPANY;
        }

        public static MasterRepository Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly MasterRepository instance = new MasterRepository();
        }

        #region Common CRUD Functions
        public List<CategoryEntity> GetAll()
        {
            SQLProxy proxy = new SQLProxy();
            string query = SQLUtility.CATEGORY_TABLE_SELECT;
            //string query = string.Empty;

            DataSet dsResult = proxy.GetDataSetFromQuery(query);
            //proxy.State.MaskException = true;
            HandleException(proxy.State, (int)ObjectActionType.View);
            if (State.ErrorOccurred)
            {
                int ErrorLogId = 0;
                if (DataManager.State.MaskException)
                {
                    ErrorLog = new ErrorLogEntity()
                    {
                        ErrorClass = "Database",
                        ErrorType = "Get",
                        ErrorCode = "1",
                        ErrorObject = this.ObjectName,
                        ErrorSource = "GetAll()",
                        ErrorSourceLineNo = "63",
                        ErrorMessage = DataManager.State.ErrorMessage,
                        ReportedDate = DateTime.Now,
                        UserID = 1,
                        LoginID = SettingsHelper.EmailFromAddress(),
                        DisplayException = false,
                        StackTrace = "",
                        Detail = DataManager.SQLQuery,
                        Debug = "Check Query"
                    };

                    ErrorLogId = ErrorLogRepository.Instance.Create(ErrorLog);
                }
                return new List<CategoryEntity>() { new CategoryEntity() { Id = 0, reaction = "Err" } };
            }

            var aList = dsResult.Tables[0].AsEnumerable().Select(dataRow => new CategoryEntity
            {
                CategoryNo = dataRow.Field<int>("CategoryNo"),
                Name = dataRow.Field<string>("Name")
            }).ToList();
            return aList;
        }


        public List<CategoryEntity> GetCategories()
        {
            //SQLProxy proxy = new SQLProxy(SettingsHelper.SQLServerDB(SettingsHelper.DBNamePharmacy));
            SQLProxy proxy = new SQLProxy();
            //string query = SQLUtility.HR_PERSON_VIEW_SELECT;
            string query = string.Empty;

            DataSet dsResult = proxy.GetDataSetFromQuery(query);
            //proxy.State.MaskException = true;
            HandleException(proxy.State, (int)ObjectActionType.View);
            if (State.ErrorOccurred)
            {
                int ErrorLogId = 0;
                if (DataManager.State.MaskException)
                {
                    ErrorLog = new ErrorLogEntity()
                    {
                        ErrorClass = "Database",
                        ErrorType = "Get",
                        ErrorCode = "1",
                        ErrorObject = this.ObjectName,
                        ErrorSource = "GetAll()",
                        ErrorSourceLineNo = "63",
                        ErrorMessage = DataManager.State.ErrorMessage,
                        ReportedDate = DateTime.Now,
                        UserID = 1,
                        LoginID = SettingsHelper.EmailFromAddress(),
                        DisplayException = false,
                        StackTrace = "",
                        Detail = DataManager.SQLQuery,
                        Debug = "Check Query"
                    };

                    ErrorLogId = ErrorLogRepository.Instance.Create(ErrorLog);
                }
                return new List<CategoryEntity>() { new CategoryEntity() { Id = 0, reaction = "Err" } };
            }

            var aList = dsResult.Tables[0].AsEnumerable().Select(dataRow => new CategoryEntity
            {
                CategoryNo = dataRow.Field<int>("CategoryNo"),
                Name = dataRow.Field<string>("Name")
            }).ToList();
            return aList;
        }

        public CategoryEntity GetById(int CategoryNo)
        {
            CategoryEntity aObject = null;
            return aObject;
        }

        public int Create()
        {
            return Create((CategoryEntity)Entity);
        }

        public int Create(CategoryEntity entity)
        {    
            return 0;
        }

        public void Delete()
        {
            Delete((CategoryEntity)Entity);
        }

        public void Delete(CategoryEntity entity)
        {
        }

        public void Update()
        {
            Update((CategoryEntity)Entity);
        }

        public void Update(CategoryEntity entity)
        {
        }
        #endregion

        #region Associations
       

        #endregion
    }

}
