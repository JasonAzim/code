using System;

namespace PharmacyAdmin.Model
{
    public class AuditModel
    {
        public int AuditID { get; set; }
        public int AuditUID { get; set; }

        public string RXNO { get; set; }

        public string INVOICENO { get; set; }

        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnKey { get; set; }

        public string FieldName { get; set; }
        public string OriginalValue { get; set; }
        public string OverlayValue { get; set; }

        public DateTime TouchDate { get; set; }
        public string Chgbyhost { get; set; }

        public string CreatedBy { get; set; }

        public AuditModel()
        {
        }
    }
}
