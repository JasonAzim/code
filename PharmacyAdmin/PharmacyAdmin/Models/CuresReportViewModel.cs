using System;
using System.ComponentModel.DataAnnotations;
using PharmacyAdmin.Data;
using Global;
using System.Collections.Generic;

namespace PharmacyAdmin.Model
{
    public class CuresReportViewModel
    {
        public string BatchNumber { get; set; }

        public string SelectedSearchType { get; set; }

        // Screen Dimensions used to adjust the grid dimensions dynamically
        public bool ColumnVirtualization { get; set; } = false;

        public int WindowHeight { get; set; }
        public int WindowWidth { get; set; }

        public List<string> GridWidths { get; set; } = new List<string> { "1500px", "2000px", "2500px", "3000px", "3500px" };

        public string GridWidth { get; set; } = "2000px";

        public enum SearchDates
        {
            [Display(Name = "None - All Of Time")]
            None,
            [Display(Name = "Ticket Confirmation Date")]
            FillDate
        }

        public string SelectedSearchDate { get; set; } = "None";
        public DateTime SearchStartDate { get; set; } = DateTime.Now;
        public DateTime SearchEndDate { get; set; } = DateTime.Now;

        public bool UseCachedTableChecked { get; set; } = true;

        public int RecordID { get; set; }
        public string SearchValues { get; set; }
        public bool RecordSelected { get; set; }

        public string UpdatedBy { get; set; } = "PharmacyAdmin";

        public string UserID { get; set; }
        
        public string SelectedSearchMode { get; set; }

        public bool ExportAllPages { get; set; } = true;

        public int PageSize { get; set; } = 50;

        public bool IsGridDataLoadComplete { get; set; } = true; // Do not show the loading image when the page first loads
        public string GridDataLoadCompleteStatus { get; set; }

        public CuresReportViewModel()
        {
        }        
    }
}
