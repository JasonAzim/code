using System;
using PharmacyAdmin.Data;
using Global;

namespace PharmacyAdmin.Model
{
    public class UracTurnAroundTimeDispenseModel
    {
		public int? dispense_order_sys_id { get; set; }
		public string mrn { get; set; }
	    public string RXNumber { get; set; }
        public string DrugNameStrengthForm { get; set; }
        public DateTime? invoice_start_date_of_service { get; set; }
		public DateTime? TicketConfirmationDate { get; set; }
		public string FillNum { get; set; }
	    public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientStreetAddress { get; set; }
        public string PatientCity { get; set; }
        public string PatientState { get; set; }
        public string PatientZipCode { get; set; }
        public DateTime? PatientDateofBirth { get; set; }
        public string PatientGender { get; set; }
        public string NDC { get; set; }

        public string DrugNDC { get; set; }
        public string DrugType { get; set; }
        public string DrugDEASchedule { get; set; }
        public string Directionsforuse { get; set; }
        public string QuantityDispensed { get; set; }
        public string DaysSupply { get; set; }
        public DateTime? DateFilled { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int? Numberofrefillsauthorized { get; set; }
        public string DoctorName { get; set; }
        public string DoctorDEA { get; set; }
        public string DoctorNPI { get; set; }
        public string DoctorStreetAddress { get; set; }
        public string DoctorState { get; set; }
	    public string DoctorZip { get; set; }
        public string PaymentName { get; set; }
        public string Nameofinsurancebilled { get; set; }
        public string payor_type { get; set; }
        public string dispense_type { get; set; }
        public string insurance_identifier { get; set; }
        public int invoice_sys_id { get; set; }
	    public long SortUID { get; set; }
        public int line_numb { get; set; }
        public DateTime? tat_start_date { get; set; }
        public string tat_start_date_type { get; set; }
	    public DateTime? tat_end_date { get; set; }
        public string tat_end_date_type { get; set; }
        
        public DateTime date_1 { get; set; } // generic date field used for CLAIMSNEW Date
        public string field_1 { get; set; } // generic field used for New_Refill determination
        public string New_Refill { get; set; } 

        public DateTime? date_2 { get; set; } // gerneric field used for logging info for New_Refill determination
        public string field_2 { get; set; }
        public DateTime? date_3 { get; set; }
        public string field_3 { get; set; }
        public string queue_move { get; set; }
        public string all_queue_moves { get; set; }

        public int turnaround_time { get; set; }
        public string Clean_vs_Unclean { get; set; }
        public string TurnAroundTimeType { get; set; }
        public string last_events { get; set; }

        public string UpdatedBy { get; set; } = "PharmacyAdmin";

        public string UserID { get; set; }
       
        public UracTurnAroundTimeDispenseModel()
        {
        }
        
        public ObjectState State()
        {
            ObjectState ViewModelNotification = new ObjectState(false, string.Empty);

            return ViewModelNotification;
        }
    }
}
