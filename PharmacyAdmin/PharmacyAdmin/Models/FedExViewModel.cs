using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using PharmacyAdmin.Helpers;
using PharmacyAdmin.Data;
using Global;

namespace PharmacyAdmin.Model
{
    public class FedExViewModel
    {
        public enum Databases { Alphascript = 1, Hoover = 2 };
        public string SelectedDatabase { get; set; }
        public int SelectedDatabaseInt
        {
            get
            {
                return (SelectedDatabase == Databases.Alphascript.ToString()) ? 1 : 2;
            }
        }

        public string SearchMessage
        {
            get
            {
                return (SelectedDatabase == "Alphascript")
                     ? "Enter Comma Separated Values:"
                     : "Enter Comma Separated Values:";
            }
        }

        public enum SearchTypes { RxNumber, TrackingNumber};
        public string SelectedSearchType { get; set; }

        public enum SearchDates
        {
            [Display(Name = "None - All Of Time")]
            None,
            [Display(Name = "Fill Date")]
            FillDate
        }

        public string SelectedSearchDate { get; set; } = "None";
        public DateTime SearchStartDate { get; set; } = DateTime.Now;
        public DateTime SearchEndDate { get; set; } = DateTime.Now;

        public int RecordID { get; set; }
        public string SearchValues { get; set; }
        
        public int TicketNO { get; set; }

        public string Test { get; set; }

        public bool ShowSearch { get; set; } = true;

        public bool RecordSelected { get; set; }

        public string UpdatedBy { get; set; } = "PharmacyAdmin";

        public string UserID { get; set; }

        public bool IsCSVNumbers { get; set; }

        public FedExViewModel()
        {
        }
        
        // Document Access Mode is combination of DocumentAccessType + DocumentNavigation function. Gets the location of the document i.e. where it is stored
        public string GetDocumentUri(string DocumentAccessMode,string DocumentGuid, string RxNumber,string TrackingNumber, string ReferenceNumber, string DispenseDate, string ShipDate)
        {
            Databases DatabaseUID = (Databases)Enum.Parse(typeof(Databases), SelectedDatabase);
            int Database = (int)DatabaseUID;
            
            string uri = string.Empty;

            if (DocumentAccessMode == DocumentAccessType.VIEW.ToString())
            {
                uri = Constants.ViewDocumentRoute + string.Format("?UserID={0}&Database={1}&DOCID={2}&RXNO={3}&TrackingNO={4}&REFNO={5}&DownloadDate={6}&ShipDate={7}", WebUtility.UrlEncode(UserID), Database.ToString(), DocumentGuid, RxNumber, TrackingNumber, ReferenceNumber, DispenseDate, ShipDate);
            }
            else
            {
                uri = Constants.DownloadDocumentRoute + string.Format("?UserID={0}&Database={1}&DOCID={2}&RXNO={3}&TrackingNO={4}&REFNO={5}&DownloadDate={6}&ShipDate={7}", WebUtility.UrlEncode(UserID), Database.ToString(), DocumentGuid, RxNumber, TrackingNumber, ReferenceNumber, DispenseDate, ShipDate);
            }

            return uri;
        }

        public string GetDocumentUri(string DocumentAccessMode, string DocumentGuid, string URI)
        {
            Databases DatabaseUID = (Databases)Enum.Parse(typeof(Databases), SelectedDatabase);
            int Database = (int)DatabaseUID;

            string uri = string.Empty;

            if (DocumentAccessMode == DocumentAccessType.VIEW.ToString())
            {
                uri = Constants.ViewDocumentRoute + string.Format("?UserID={0}&Database={1}&URI={2}&DOCID={3}", WebUtility.UrlEncode(UserID), Database.ToString(), WebUtility.UrlEncode(URI), DocumentGuid);
            }
            else
            {
                uri = Constants.DownloadDocumentRoute + string.Format("?UserID={0}&Database={1}&URI={2}&DOCID={3}", WebUtility.UrlEncode(UserID), Database.ToString(), WebUtility.UrlEncode(URI), DocumentGuid);
            }

            return uri;
        }

        public ObjectState State()
        {
            ObjectState ViewModelNotification = new ObjectState(false, string.Empty);
            
            if (string.IsNullOrEmpty(SearchValues))
            {
                return new ObjectState(true, "Please enter a search value(s)");
            }

            IsCSVNumbers = SearchValues.Contains(",");
            //IsCSVNumbers = StateHelper.IsCSV(SearchValues);
            //IsCSVNumbers = StateHelper.IsNumberCSV(SearchValues);

            bool ContainAlphabets = StateHelper.ContainsAlphabets(SearchValues);

            if (SelectedDatabase == FedExViewModel.Databases.Alphascript.ToString())
            {
                if (IsCSVNumbers)
                {
                    if (SelectedSearchType == FedExViewModel.SearchTypes.RxNumber.ToString())
                    {
                        if (!StateHelper.IsRxNumberCSV(SearchValues, (int)Databases.Alphascript))
                        {
                            return new ObjectState(true, "Invalid RxNumber, enter 6 digit number(s)");
                        }
                    }
                    else
                    {
                        if (!StateHelper.IsTrackingNumberCSV(SearchValues))
                        {
                            return new ObjectState(true, "Invalid Tracking Number, enter 12 digit number(s)");
                        }
                    }
                }
                else
                {
                    // single value was entered
                    if (SelectedSearchType == FedExViewModel.SearchTypes.RxNumber.ToString())
                    {
                        if (!StateHelper.IsRxNumber(SearchValues.Trim(), (int)Databases.Alphascript))
                        {
                            return new ObjectState(true, "Invalid RxNumber, enter 6 digit number(s)");
                        }
                    }
                    else
                    {
                        if (!StateHelper.IsTrackingNumber(SearchValues.Trim()))
                        {
                            return new ObjectState(true, "Invalid Tracking Number, enter 12 digit number(s)");
                        }
                    }
                }
            }
            else if (SelectedDatabase == FedExViewModel.Databases.Hoover.ToString())
            {
                if (IsCSVNumbers)
                {
                    if (SelectedSearchType == FedExViewModel.SearchTypes.RxNumber.ToString())
                    {
                        if (!StateHelper.IsRxNumberCSV(SearchValues, (int)Databases.Hoover))
                        {
                            return new ObjectState(true, "Invalid RxNumber, enter 7 digit number(s)");
                        }
                    }
                    else
                    {
                        if (!StateHelper.IsTrackingNumberCSV(SearchValues))
                        {
                            return new ObjectState(true, "Invalid Tracking Number, enter 12 digit number(s)");
                        }
                    }
                }
                else
                {
                    // single value was entered
                    if (SelectedSearchType == FedExViewModel.SearchTypes.RxNumber.ToString())
                    {
                        if (!StateHelper.IsRxNumber(SearchValues.Trim(), (int)Databases.Hoover))
                        {
                            return new ObjectState(true, "Invalid RxNumber, enter 7 digit number(s)");
                        }
                    }
                    else
                    {
                        if (!StateHelper.IsTrackingNumber(SearchValues))
                        {
                            return new ObjectState(true, "Invalid Tracking Number, enter 12 digit number(s)");
                        }
                    }

                    //if (SelectedSearchType == FedExViewModel.SearchTypes.RxNumber.ToString())
                    //{
                    //    if (SearchValues.Length != 7)
                    //    {
                    //        return new ObjectState(true, "RxNumber has to be 6 characters long");
                    //    }
                    //}
                }
            }

            return ViewModelNotification;
        }

    }
}
