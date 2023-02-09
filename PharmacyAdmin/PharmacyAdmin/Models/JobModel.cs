using System;

namespace PharmacyAdmin.Model
{
    public class JobModel
    {
        // Add these two fields to make this model interoperable with the Entity classes which have these properties in the EntityBase Abstract class. We dont have a model base class
        public int Id { get; set; }  
        public bool IsLoadedFromDB { get; set; }

        public Guid JobID { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool? Enabled { get; set; }
        public string NotificationType { get; set; }
        public bool? NotificationEnabled { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public string UpdatedBy { get; set; } = "PharmacyAdmin"; // [UpatedBy]

        public JobModel()
        {
        }

        public static JobModel Construct(Guid jobID)
        {
            JobModel model = new JobModel()
            {
                JobID = jobID
            };

            model.Id = 0;
            model.IsLoadedFromDB = false;
            return model;
        }
    }
}
