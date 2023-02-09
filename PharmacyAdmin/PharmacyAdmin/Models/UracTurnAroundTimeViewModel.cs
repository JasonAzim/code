using System;
using System.ComponentModel.DataAnnotations;
using PharmacyAdmin.Data;
using Global;

namespace PharmacyAdmin.Model
{
    public class UracTurnAroundTimeViewModel
    {
        public string JobName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        public string JobScheduleDate { get; set; }

        public string QueueMoveStartDate { get; set; }
        public string QueueMoveEndDate { get; set; }

        public int TotalDispenseRecordCount { get; set; }
        public string TotalDispenseRecordCountDisplay
        {
            get
            {
                if (TotalDispenseRecordCount == 0)
                {
                    return TotalDispenseRecordCount.ToString() + " Records";
                }
                else
                {
                    return TotalDispenseRecordCount.ToString() + " Records";
                }
            }
        }

        public string UpdatedBy { get; set; } = "PharmacyAdmin";

        public string UserID { get; set; }
        public string JobStatusMessage { get; set; }

        public enum UracTables
        {
            [Display(Name = "Step1 - Raw Data")]
            tb_dispense_tat_step1,
            [Display(Name = "Step2 - Date Determination")]
            tb_dispense_tat_step2,
            [Display(Name = "Step3 - Dispense NO PHI")]
            tb_dispense_tat,
            [Display(Name = "Step4 - Calculations")]
            Calc_Turnaround_Time_Step2
        }

        public string SelectedUracTable { get; set; } = "Calc_Turnaround_Time_Step2";

        public bool ExportAllPages { get; set; }

        public UracTurnAroundTimeViewModel()
        {
            JobName = "CPRSQL_URAC_TurnAroundTime";
            //DateTime.Now.Date
            JobScheduleDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 7, 0, 0, 0).ToString("yyyy-MM-dd HH tt");
            //JobScheduleDate = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH");
            StartDate = DateTime.Parse("2022-01-01").ToString("yyyy-MM-dd");
            EndDate = DateTime.Parse("2022-12-31").ToString("yyyy-MM-dd");

            QueueMoveStartDate = DateTime.Parse("2021-12-01").ToString("yyyy-MM-dd");
            QueueMoveEndDate = DateTime.Parse("2023-01-31").ToString("yyyy-MM-dd");
        }

        public ObjectState State()
        {
            ObjectState ViewModelNotification = new ObjectState(false, string.Empty);

            return ViewModelNotification;
        }
    }
}
