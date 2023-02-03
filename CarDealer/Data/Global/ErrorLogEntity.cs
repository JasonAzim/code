using System;
using System.Diagnostics;


namespace Global.Repository
{
    public class ErrorLogEntity : EntityBase
    {
        public string Code { get; set; }
        public string ErrorClass { get; set; }
        public string ErrorType { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorObject { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorSourceLineNo { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime ReportedDate { get; set; }
        public int UserID { get; set; }
        public string LoginID { get; set; }
        public bool DisplayException { get; set; }
        public string StackTrace { get; set; }
        public string Detail { get; set; }
        public string Debug { get; set; }

        public static ErrorLogEntity Construct(string code, string errorClass, string errorType, string errorCode, string errorObject, string errorSource, string errorSourceLineNo, string errorMessage, DateTime reportedDate, int userID, string loginID, bool displayException, string stackTrace, string detail, string debug)
        {
            ErrorLogEntity entity = new ErrorLogEntity()
            {
                Code = code,
                ErrorClass = errorClass,
                ErrorType = errorType,
                ErrorCode = errorCode,
                ErrorObject = errorObject,
                ErrorSource = errorSource,
                ErrorSourceLineNo = errorSourceLineNo,
                ErrorMessage = errorMessage,
                ReportedDate = reportedDate,
                UserID = userID,
                LoginID = loginID,
                DisplayException = displayException,
                StackTrace = stackTrace,
                Detail = detail,
                Debug = debug
            };

            entity.Id = 0;
            entity.IsLoadedFromDB = false;
            return entity;
        }
    }
}

