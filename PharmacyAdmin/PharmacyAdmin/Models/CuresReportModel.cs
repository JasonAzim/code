using System;
using System.Collections.Generic;

namespace PharmacyAdmin.Model
{
    public class CuresReportModel
    {
        public Guid? ReportUID { get; set; }
        public string StateCode { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? DateSubmitted { get; set; }
        public int RecordCount { get; set; }
        public string ReportType { get; set; }
        public int ErrorCode { get; set; }
        public string ReportStatus { get; set; }
        public string FileName { get; set; }
        public string BatchNumber { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string UpdatedBy { get; set; } = "";
    }
}
