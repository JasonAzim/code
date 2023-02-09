using System;
using System.Collections.Generic;
using Global;

namespace PharmacyAdmin.Service
{
    public class ScheduleService
    {
        private static readonly string[] DateRanges = new[]
        {
            "LAST_WEEK", "LAST_BIWEEK", "LAST_MONTH", "CURRENT_WEEK", "CURRENT_MONTH"
        };

        public List<DateTime> GetDateList(DateTime StartDate, DateTime EndDate)
        {
            List<DateTime> dateList = new List<DateTime>();
            while (StartDate <= EndDate)
            {
                dateList.Add(StartDate);
                StartDate = StartDate.AddDays(1);
            }
            return dateList;
        }

        public List<int> GetYearList(DateTime StartDate, DateTime EndDate)
        {
            List<int> Numbers = new List<int>();
            while (StartDate <= EndDate)
            {
                Numbers.Add(StartDate.Year);
                StartDate = StartDate.AddYears(1);
            }
            return Numbers;
        }

        public List<int> GetMonthList(DateTime StartDate, DateTime EndDate)
        {
            List<int> Months = new List<int>();
            while (StartDate <= EndDate)
            {
                Months.Add(StartDate.Month);
                StartDate = StartDate.AddMonths(1);
            }
            return Months;
        }

        public void SetDateRange(ref DateTime StartDate,ref DateTime EndDate, string RelativeDateRange)
        {
            // By Default Relative Date Range is TODAY_DATE
            DateTime RunDate = DateTime.Now;

            if (RelativeDateRange == Constants.LAST_WEEK_FROM_TODAY)
            {
                StartDate = FirstDayOfLastWeek(RunDate, DayOfWeek.Sunday);
                EndDate = LastDayOfLastWeek(RunDate, DayOfWeek.Sunday);
            }
            else if (RelativeDateRange == Constants.LAST_BIWEEK_FROM_TODAY)
            {
                StartDate = FirstDayOfLastWeek(RunDate, DayOfWeek.Sunday);
                EndDate = StartDate.AddDays(-1);
                StartDate = FirstDayOfWeek(EndDate, DayOfWeek.Sunday);
            }
            else if (RelativeDateRange == Constants.LAST_MONTH_FROM_TODAY)
            {
                StartDate = FirstDayOfLastMonth(RunDate);
                EndDate = LastDayOfMonth(StartDate);
            }
            else if (RelativeDateRange == Constants.CURRENT_WEEK_FROM_TODAY)
            {
                StartDate = FirstDayOfWeek(RunDate);
                EndDate = LastDayOfWeek(StartDate);
            }
            else if (RelativeDateRange == Constants.CURRENT_MONTH_FROM_TODAY)
            {
                StartDate = FirstDayOfMonth(RunDate);
                EndDate = LastDayOfMonth(StartDate);
            }
            else if (RelativeDateRange == Constants.CURRENT_YEAR_FROM_TODAY)
            {
                StartDate = FirstDayOfYear(RunDate);
                EndDate = LastDayOfYear(StartDate);
            }
        }

        public List<DateTime> GetDateList(DateTime RunDate, string RelativeDateRange)
        {
            // By Default Relative Date Range is TODAY_DATE
            DateTime StartDate = DateTime.Now;
            DateTime EndDate = DateTime.Now;

            if (RelativeDateRange == Constants.LAST_WEEK_FROM_TODAY)
            {
                StartDate = FirstDayOfLastWeek(RunDate, DayOfWeek.Sunday);
                EndDate = LastDayOfLastWeek(RunDate, DayOfWeek.Sunday);
            }
            else if (RelativeDateRange == Constants.LAST_BIWEEK_FROM_TODAY)
            {
                StartDate = FirstDayOfLastWeek(RunDate, DayOfWeek.Sunday);
                EndDate = StartDate.AddDays(-1);
                StartDate = FirstDayOfWeek(EndDate, DayOfWeek.Sunday);
            }
            else if (RelativeDateRange == Constants.LAST_MONTH_FROM_TODAY)
            {
                StartDate = FirstDayOfLastMonth(RunDate);
                EndDate = LastDayOfMonth(StartDate);
            }
            else if (RelativeDateRange == Constants.CURRENT_WEEK_FROM_TODAY)
            {
                StartDate = FirstDayOfWeek(RunDate);
                EndDate = LastDayOfWeek(StartDate);
            }
            else if (RelativeDateRange == Constants.CURRENT_MONTH_FROM_TODAY)
            {
                StartDate = FirstDayOfMonth(RunDate);
                EndDate = LastDayOfMonth(StartDate);
            }
            else if (RelativeDateRange == Constants.CURRENT_YEAR_FROM_TODAY)
            {
                StartDate = FirstDayOfYear(RunDate);
                EndDate = LastDayOfYear(StartDate);
            }

            System.Diagnostics.Debug.WriteLine(string.Format("Processing Date Range is {0} to {1}", StartDate.ToShortDateString(), EndDate.ToShortDateString()));

            return GetDateList(StartDate, EndDate);
        }

        // Simple
        public DateTime FirstDayOfWeek(DateTime InputDate, DayOfWeek startOfWeek)
        {
            int diff = (7 + (InputDate.DayOfWeek - startOfWeek)) % 7; // 1 = Mon, 2 = Tue, 3 = Wed, 4 = Thu, 5 = Fri, 6 = Sat, 7 = Sun
            return InputDate.AddDays(-1 * diff).Date;
        }

        // Advance
        public DateTime FirstDayOfWeek(DateTime InputDate)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = InputDate.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;

            if (diff < 0)
            {
                diff += 7;
            }

            return InputDate.AddDays(-diff).Date;
        }

        public DateTime LastDayOfWeek(DateTime InputDate)
        {
            return FirstDayOfWeek(InputDate).AddDays(6);
        }

        public DateTime FirstDayOfLastWeek(DateTime InputDate, DayOfWeek startOfWeek)
        {
            return FirstDayOfWeek(InputDate.AddDays(-7), startOfWeek);
        }

        public DateTime LastDayOfLastWeek(DateTime InputDate, DayOfWeek startOfWeek)
        {
            return FirstDayOfLastWeek(InputDate, startOfWeek).AddDays(6);
        }

        public DateTime FirstDayOfYear(DateTime InputDate)
        {
            return new DateTime(InputDate.Year, 1, 1);
        }

        public DateTime LastDayOfYear(DateTime InputDate)
        {
            return new DateTime(InputDate.Year, 12, 31);
        }

        public DateTime FirstDayOfMonth(DateTime InputDate)
        {
            return new DateTime(InputDate.Year, InputDate.Month, 1);
        }

        public DateTime LastDayOfMonth(DateTime InputDate)
        {
            return FirstDayOfMonth(InputDate).AddMonths(1).AddDays(-1);
        }

        public DateTime FirstDayOfNextMonth(DateTime InputDate)
        {
            return FirstDayOfMonth(InputDate).AddMonths(1);
        }

        public DateTime FirstDayOfLastMonth(DateTime InputDate)
        {
            return FirstDayOfMonth(InputDate).AddMonths(-1);
        }

        public string GetRelativeDateRange(RelativeDateType relativeDateType, RelativeDateSubType relativeDateSubType)
        {
            return relativeDateType.ToString() + "_" + relativeDateSubType.ToString();
        }

        public RelativeDateType GetRelativeDateType(string RelativeDateTypeValue)
        {
            return (RelativeDateType)Enum.Parse(typeof(RelativeDateType), RelativeDateTypeValue.ToUpper());
        }

        public RelativeDateSubType GetRelativeDateSubType(string RelativeDateTypeValue)
        {
            return (RelativeDateSubType)Enum.Parse(typeof(RelativeDateSubType), RelativeDateTypeValue.ToUpper());
        }

        public int DateDifferenceInMonths(DateTime date1, DateTime date2)
        {
            return ((date1.Year - date2.Year) * 12) + date1.Month - date2.Month;
        }

    }
}
