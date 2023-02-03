using System;
using System.Collections.Generic;
using System.Linq;
using Global.Repository;
using CarDealer.Repository;

namespace CarDealer.Shape
{
    public class ErrorLogShape
    {
        public int ErrorLogID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string ErrorClass { get; set; } = string.Empty;
        public string ErrorType { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string ErrorObject { get; set; } = string.Empty;
        public string ErrorSource { get; set; } = string.Empty;
        public string ErrorSourceLineNo { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public DateTime ReportedDate { get; set; }
        public int UserID { get; set; }
        public string LoginID { get; set; } = string.Empty;
        public bool DisplayException { get; set; }
        public string StackTrace { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public string Debug { get; set; } = string.Empty;

        public ErrorLogShape()
        {

        }

        public ErrorLogShape Map(ErrorLogEntity source)
        {
            ErrorLogShape shape = new ErrorLogShape() {
                ErrorLogID = source.Id,
                Code = source.Code,
                ErrorClass = source.ErrorClass,
                ErrorType = source.ErrorType,
                ErrorCode = source.ErrorCode,
                ErrorObject = source.ErrorObject,
                ErrorSource = source.ErrorSource,
                ErrorSourceLineNo = source.ErrorSourceLineNo,
                ErrorMessage = source.ErrorMessage,
                ReportedDate = source.ReportedDate,
                UserID = source.UserID,
                LoginID = source.LoginID,
                DisplayException = source.DisplayException,
                StackTrace = source.StackTrace,
                Detail = source.Detail,
                Debug = source.Debug
            };
            return shape;
        }
    }
}
