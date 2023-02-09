using System;

namespace PharmacyAdmin.Model
{
    public class CPRSQLPatientExportViewModel
    {
        public bool FilterByMRNChecked { get; set; }
        public string MRNs { get; set; }
        public string FilterByMRNSql { get; set; }
        public bool FilterByDrugNamesChecked { get; set; }
        public string DrugNames { get; set; }
        public string FilterByDrugNamesSql { get; set; }

        public bool FilterByICDCodesChecked { get; set; }
        public string ICDCodes { get; set; }
        public string FilterByICDCodesSql { get; set; }

        public string OutputCSV { get; set; }
        public string OutputQuery { get; set; }

        public CPRSQLPatientExportViewModel()
        {
        }
    }
}
