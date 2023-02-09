using System;

namespace PharmacyAdmin.Model
{
    public class StatisticModel
    {
        // Add these two fields to make this model interoperable with the Entity classes which have these properties in the EntityBase Abstract class. We dont have a model base class
        public int Id { get; set; }  
        public bool IsLoadedFromDB { get; set; }

        public Guid StatisticID { get; set; }
        public DateTime ReportDate { get; set; }

        public string KPI { get; set; }
        public int Count { get; set; }

        public StatisticModel()
        {
        }
    }
}
