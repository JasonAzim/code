using System;
using CarDealer.Data;

namespace Global.Repository
{
   public abstract class RepositoryBase
   {
        private SQLManager _DataManager = null;

        public SQLManager DataManager
        {
            set
            {
                _DataManager = value;
            }
            get
            {
                return _DataManager;
            }
        }

        private string _ObjectName = null;

        public string ObjectName
        {
            get
            {
                return _ObjectName;
            }
            set
            {
                _ObjectName = value;
            }
        }

        private ObjectState _State = new ObjectState();

        public ObjectState State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
            }
        }

        // By default we assume there are no errors
        private ErrorLogEntity _ErrorLog = new ErrorLogEntity() { Id = 0, Code = "Success", ErrorCode = "0" };

        public ErrorLogEntity ErrorLog
        {
            get
            {
                return _ErrorLog;
            }
            set
            {
                _ErrorLog = value;
            }
        }

        protected void HandleException(ObjectState state, int errorType)
        {
            // Save the exeption into the base class so it can be handled
            State = state;

            if (State.ErrorOccurred)
            {
                State.ErrorOccurred = true;
                State.DisplayMessage = "Error modifying data for " + ObjectName;
                State.ErrorType = errorType;
            }
            else
            {
                State.DisplayMessage = ObjectName + " modified successfully";
            }
        }
    }
}
