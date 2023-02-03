using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public interface IObjectState
{

    int RecordID { get;  set; }
    DateTime DateCreated { get; set; }
    string CreatedBy { get; set;  }
    DateTime LastUpdated { get; set; }
    string LastUpdateBy { get; set; }
    DateTime ReportDate { get; set; }
    bool ISValid { get; set; }
    bool MaskException { get; set; }
    bool ErrorOccurred { get; set; }
    int ErrorType { get; set;  }
    string ErrorMessage { get; set; }
    string DisplayMessage { get; set; }
    string Query { get; set; }
}