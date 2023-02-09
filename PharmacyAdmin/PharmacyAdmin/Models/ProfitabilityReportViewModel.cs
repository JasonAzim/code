using System;
using System.ComponentModel.DataAnnotations;
using PharmacyAdmin.Data;
using Global;
using System.Collections.Generic;

namespace PharmacyAdmin.Model
{
    public class ProfitabilityReportViewModel
    {
        public string RXNO { get; set; }
        public string INVOICENO { get; set; }

        public enum SearchTypes
        {
            [Display(Name = "Date Range")]
            All,
            [Display(Name = "Rx Number or Invoice Number")]
            One,
            [Display(Name = "Partials")]
            Partials
        }

        public enum SearchModes
        {
            [Display(Name = "Search by RX Number")]
            RXNO,
            [Display(Name = "Search by Invoice Number")]
            INVOICENO
        }

        public string SelectedSearchType { get; set; }

        public string SearchMessage
        {
            get
            {
                if (SelectedSearchType == SearchTypes.Partials.ToString())
                {
                    return "Searching Partial Records";
                }
                else if (SelectedSearchType == SearchTypes.All.ToString())
                {
                    if (SelectedSearchDate == "None")
                    {
                        return "Search All Records for Today";
                    }
                    else
                    {
                        return "Searching All Records";
                    }
                }
                else
                {
                    return "Searching";
                }
            }
        }

        // Column Widths
        public string ROWNUM_Width { get; set; } = "90px";
        public string ncpdp_rx_number_Width { get; set; } = "120px";
        public string ncpdp_invoice_number_Width { get; set; } = "135px";
        public string TICKNO_Width { get; set; } = "130px";
        public string ncpdp_date_filled_timestamp_Width { get; set; } = "145px";
        public string ticket_confirmation_date_Width { get; set; } = "150px";
        public string ncpdp_quantity_new_refill_code_Width { get; set; } = "125px";
        public string ticket_quantity_Width { get; set; } = "110px";
        public string drug_name_Width { get; set; } = "200px";
        public string CHGFLAG_Width { get; set; } = "125px";

        public string ncpdp_primary_claim_payor_Width { get; set; } = "200px";
        public string primary_payor_type_Width { get; set; } = "150px";
        public string ncpdp_primary_claim_paid_Width { get; set; } = "150px";
        public string ncpdp_secondary_claim_paid_Width { get; set; } = "150px";
        public string Copay_Width { get; set; } = "150px";
        public string TPRevenuePlusPatientCopay_Width { get; set; } = "200px";
        public string COGS_Width { get; set; } = "140px";
        public string COGSAdjusted_Width { get; set; } = "140px";
        public string GrossProfit_Width { get; set; } = "180px";
        public string GrossProfitAdjusted_Width { get; set; } = "180px";
        public string PrimaryTPHistory_Width { get; set; } = "180px";
        public string SecondaryTPHistory_Width { get; set; } = "175px";
        public string CopayHistory_Width { get; set; } = "175px";
        public string COGSHistory_Width { get; set; } = "175px";
        public string RecordDeletedHistory_Width { get; set; } = "175px";
        public string ticket_item_delivins_Width { get; set; } = "175px";
        public string TicketSelected_Width { get; set; } = "100px";
        public string RecordDeleted_Width { get; set; } = "100px";

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
        public bool IsCSVNumbers { get; set; }
        public string SelectedSearchMode { get; set; }

        public bool ExportAllPages { get; set; } = true;

        public int PageSize { get; set; } = 50;

        public bool IsGridDataLoadComplete { get; set; } = true; // Do not show the loading image when the page first loads
        public string GridDataLoadCompleteStatus { get; set; }

        public string HelpTop { get; set; } = "0%";
        public string HelpLeft { get; set; } = "45%";
        public bool IsHelpVisible { get; set; } = true;

        public ProfitabilityReportViewModel()
        {
        }
        
        public ObjectState State()
        {
            ObjectState ViewModelNotification = new ObjectState(false, string.Empty);

            if (SelectedSearchType == SearchTypes.Partials.ToString())
            {
                SelectedSearchMode = "Billable:" + SearchTypes.Partials.ToString();
            }
            else if (SelectedSearchType == SearchTypes.All.ToString())
            {
                SelectedSearchMode = "" + SearchTypes.All.ToString();

                /*
                if (string.IsNullOrEmpty(SearchValues))
                {
                    return new ObjectState(true, "Please enter a search value(s)");
                }

                IsCSVNumbers = SearchValues.Contains(",");
                //IsCSVNumbers = StateHelper.IsCSV(SearchValues);
                //IsCSVNumbers = StateHelper.IsNumberCSV(SearchValues);

                bool ContainAlphabets = StateHelper.ContainsAlphabets(SearchValues);

                if (IsCSVNumbers)
                {    
                    if (!StateHelper.IsRxNumberCSV(SearchValues, 1))       
                    {
                        return new ObjectState(true, "Invalid RxNumber, enter 6 digit number(s)");   
                    }
                }
                else
                {
                    // single value was entered
                    if (!StateHelper.IsRxNumber(SearchValues.Trim(), 1))   
                    {
                        return new ObjectState(true, "Invalid RxNumber, enter 6 digit number(s)");    
                    }
                }
                */
            }
            else
            {
                if (string.IsNullOrEmpty(RXNO))
                {
                    if (string.IsNullOrEmpty(INVOICENO))
                    {
                        return new ObjectState(true, "Please enter either RX No or Invoice No");
                    }
                    else
                    {
                        SelectedSearchMode = "INVOICENO";
                        // User wants to search by INVOICE NO only. NO RX NO was entered
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(INVOICENO))
                    {
                        // RXNO was entered and no INVOICENO was entered which is correct since we would like only one value to be entered
                        SelectedSearchMode = "RXNO";
                    }
                    else
                    {
                        return new ObjectState(true, "Please enter either RX No or Invoice No");
                    }
                }
            }

            return ViewModelNotification;
        }
    }
}
