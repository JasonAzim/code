namespace PharmacyAdmin.Model
{
    public class TableSortModel
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public bool IsAscending  { get; set; }
    
    
        public string SortColumnName { get; set; } // Holds the name of the column that has been sorted

        public string SortDirection
        {
            get
            {
                return (IsAscending == true)
                     ? "ASC"
                     : "DESC";
            }
        }

        public string ActiveSortColumn { get; set; } // Icon Sort Column Name

        public TableSortModel()
        {
        }

        public static TableSortModel ConstructFedExCPRSQLTable() 
        {
            return new TableSortModel() { IsAscending = true, SortColumnName = "" };
        }

        public static TableSortModel ConstructFedExPioneerRXTable()
        {
            return new TableSortModel() { IsAscending = true, SortColumnName = "" };
        }

        public static TableSortModel ConstructProfitabilityReportDefaultTable()
        {
            return new TableSortModel() { IsAscending = true, SortColumnName = "" };
        }

        public static TableSortModel ConstructProfitabilityReportPartialsTable()
        {
            return new TableSortModel() { IsAscending = true, SortColumnName = "" };
        }
    }
}
