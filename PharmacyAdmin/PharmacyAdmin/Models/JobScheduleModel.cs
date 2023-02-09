using System;

namespace PharmacyAdmin.Model
{
    public class JobScheduleModel
    {
        // Add these two fields to make this model interoperable with the Entity classes which have these properties in the EntityBase Abstract class. We dont have a model base class
        public int Id { get; set; }  
        public bool IsLoadedFromDB { get; set; }

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

        public JobScheduleModel()
        {
        }

        public static JobScheduleModel Construct(Guid jobID)
        {
            JobScheduleModel model = new JobScheduleModel()
            {
                JobID = jobID
            };

            model.Id = 0;
            model.IsLoadedFromDB = false;
            return model;
        }
    }
}
