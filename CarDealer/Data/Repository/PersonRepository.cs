using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using Global.Utility;
using Global.Repository;
using CarDealer;
using CarDealer.Data;
using CarDealer.Repository;

namespace Pharmacy.Repository
{
    public sealed class PersonRepository : RepositoryBase, IRepository<PersonEntity>
    {
        private PersonEntity _Entity = null;

        public PersonEntity Entity
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

        private PersonRepository()
        {
            // used by static instance constructor
            DataManager = new SQLManager();
            this.ObjectName = Constants.TABLE_PERSON;
        }

        // An entity can be read from a database or it can be created by filling in the properties. 
        public PersonRepository(PersonEntity entity)
        {
            Entity = entity;
            DataManager = new SQLManager();
            this.ObjectName = Constants.TABLE_COMPANY;
        }

        public static PersonRepository Instance { get { return Nested.instance; } }

        private class Nested
        {
            // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly PersonRepository instance = new PersonRepository();
        }

        #region Common CRUD Functions
        public List<PersonEntity> GetAll()
        {
            SQLProxy proxy = new SQLProxy();
            //string query = SQLUtility.PERSONS_VIEW_SELECT;
            string query = string.Empty;

            DataSet dsResult = proxy.GetDataSetFromQuery(query);
            //proxy.State.MaskException = true;
            HandleException(proxy.State, (int)ObjectActionType.View);
            if (State.ErrorOccurred)
            {
                int ErrorLogId = 0;
                if (DataManager.State.MaskException)
                {
                    ErrorLog = new ErrorLogEntity(){
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
                return new List<PersonEntity>() { new PersonEntity(){ Id = 0, reaction = "Err" } };
            }

            var aList = dsResult.Tables[0].AsEnumerable().Select(dataRow => new PersonEntity {
                Id = 1,
                PersonID = dataRow.Field<string>("MRN").ToString(),
                FirstName = dataRow.Field <string>("First_Name"),
                LastName = dataRow.Field<string>("Last_Name")
            }).ToList();
            return aList;
        }

        public List<PersonEntity> GetPersons()
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
                return new List<PersonEntity>() { new PersonEntity() { Id = 0, reaction = "Err" } };
            }

            var aList = dsResult.Tables[0].AsEnumerable().Select(dataRow => new PersonEntity
            {
                Id = 1,
                PersonID = dataRow.Field<string>("PersonID").ToString(),
                FirstName = dataRow.Field<string>("FirstName"),
                LastName = dataRow.Field<string>("LastName"),
                DateOfBirth = dataRow.FieldOrDefault<DateTime>("DateOfBirth"),
                SSN = dataRow.Field<string>("SSN")
            }).ToList();
            return aList;
        }

        public PersonEntity GetById(int id)
        {
            PersonEntity aObject = null;
            return aObject;
        }

        public int Create()
        {
            return Create((PersonEntity)Entity);
        }

        public int Create(PersonEntity entity)
        {    
            return 0;
        }

        public void Delete()
        {
            Delete((PersonEntity)Entity);
        }

        public void Delete(PersonEntity entity)
        {
        }

        public void Update()
        {
            Update((PersonEntity)Entity);
        }

        public void Update(PersonEntity entity)
        {
        }
        #endregion

        #region Associations
       

        #endregion
    }

}
