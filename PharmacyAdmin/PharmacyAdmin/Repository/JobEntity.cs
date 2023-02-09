using System;

namespace Pharmacy.Data.Repository
{
    public class JobEntity : EntityBase
    {
        public Guid JobID { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
        public string NotificationType { get; set; }
        public bool NotificationEnabled { get; set; }
        public int Status { get; set; } // Custom Job Status that we control
        public DateTime ScheduleTime { get; set; }
        public string Schedule { get; set; }
        public DateTime LastRunTime { get; set; }
        public DateTime LastStopTime { get; set; }
        public int RunTimeMinutes { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }

        // Job History columns from system table

        public string run_date { get; set; }
        public string run_time { get; set; }
        public DateTime run_date_time { get; set; }
        public int run_status { get; set; }
        public string step_id { get; set; }
        public string step_name { get; set; }
        public string message { get; set; }

        public bool RecordSelected { get; set; }

        public static JobEntity Construct(Guid jobID)
        {
            JobEntity entity = new JobEntity()
            {
                JobID = jobID
            };

            entity.Id = 0;
            entity.IsLoadedFromDB = false;
            return entity;
        }
    }
}

