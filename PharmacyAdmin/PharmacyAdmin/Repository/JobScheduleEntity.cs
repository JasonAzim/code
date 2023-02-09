using System;

namespace Pharmacy.Data.Repository
{
    public class JobScheduleEntity : EntityBase
    {
        public int JobScheduleID { get; set; }
        public Guid JobID { get; set; }
        public bool? Enabled { get; set; }
        public string ScheduleType { get; set; }
        public string Frequency { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string DailyFrequencyOccursOnceAt { get; set; }
        public int DailyFrequencyOccursEvery { get; set; }
        public string DailyFrequencyRate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public string UpdatedBy { get; set; } = "PharmacyAdmin"; // [UpatedBy]

        public static JobScheduleEntity Construct(Guid jobID)
        {
            JobScheduleEntity entity = new JobScheduleEntity()
            {
                JobID = jobID
            };

            entity.Id = 0;
            entity.IsLoadedFromDB = false;
            return entity;
        }
    }
}
