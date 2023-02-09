using System;

namespace PharmacyAdmin.Model
{
    public class ViewCubeNCPDPModel
    {
        public int LABLOGNO { get; set; }

		public DateTime ncpdp_date_filled_timestamp { get; set; }
	    public DateTime ncpdp_invoice_datebilled_timestamp { get; set; }
	    public DateTime ncpdp_invoice_date_of_service_timestamp { get; set; }
        public string ncpdp_rx_number { get; set; }
	    public string ncpdp_rx_description { get; set; }
	    public string ncpdp_days_supply { get; set; }
	    public string ncpdp_drug_name { get; set; }
	    public string ncpdp_ndc { get; set; }
	    public decimal ncpdp_quantity_dispenses { get; set; }
	    public decimal ncpdp_quantity_intended_to_dispense { get; set; }
	    public decimal ncpdp_quantity_prescribed { get; set; }
	    public string ncpdp_quantity_new_refill_code { get; set; }
	    public string ncpdp_daw_yn { get; set; }
	    public string ncpdp_daw_description { get; set; }
	    public string ncpdp_count_of_refills { get; set; }
	    public string ncpdp_uom { get; set; }
	    public DateTime ncpdp_date_written_timestamp { get; set; }
	    public decimal ncpdp_days_intended { get; set; }
	    public string ncpdp_compound_code { get; set; }
	    public string ncpdp_compound_dosage_form { get; set; }
	    public string ncpdp_compound_dosage_unit_form { get; set; }
	    public string ncpdp_route_of_administration { get; set; }
	    public string ncpdp_product_id_qualifier { get; set; }
	    public string ncpdp_compound_type { get; set; }
	    public decimal ncpdp_pharmacy_service_type { get; set; }
	    public string ncpdp_eligible_clarification_code { get; set; }
	    public string ncpdp_dispensing_status { get; set; }
	    public int ncpdp_invoice_number { get; set; }
	    public string ncpdp_claim_number { get; set; }
   	    public decimal ncpdp_primary_claim_billed { get; set; }
	    public decimal ncpdp_primary_claim_expected { get; set; }
	    public decimal ncpdp_primary_claim_paid { get; set; }
	    //public decimal ncpdp_primary_claim_payor { get; set; }  This field is giving an IDataFormat Exception
	    public decimal ncpdp_secondary_claim_billed { get; set; }
	    public decimal ncpdp_secondary_claim_expected { get; set; }
	    public string ncpdp_secondary_claim_payor { get; set; }
	    public decimal ncpdp_secondary_claim_paid { get; set; }
	    public decimal ncpdp_patient_copay_expected { get; set; }
	    public decimal ncpdp_patient_secondary_copay_expected { get; set; }
	    public decimal ncpdp_other_secondary_copay_expected { get; set; }
	    public decimal ncpdp_patient_paid { get; set; }
	    public decimal ncpdp_claim_adjustment { get; set; }
	    public decimal ncpdp_claim_balance { get; set; }
	    public decimal ncpdp_ncpdp_cost { get; set; }
	    public decimal ncpdp_ticket_cost { get; set; }
		public decimal ncpdp_ticket_cost_new { get; set; }
		public string ncpdp_claim_status { get; set; }
 	    public string patient_mrn { get; set; }
  	    public string primary_payor_binno { get; set; }
	    public string primary_payor_pcn { get; set; }
	    public string primary_payor_name { get; set; }
	    public string primary_payor_type { get; set; }
	    public string secondary_payor_binno { get; set; }
	    public string secondary_payor_pcn { get; set; }
	    public string secondary_payor_name { get; set; }
	    public string secondary_payor_type { get; set; }
	   public decimal ncpdp_invoice_expected { get; set; }
	   public decimal ncpdp_invoice_billed { get; set; }
	   public decimal ncpdp_sec_ncpdp_cost { get; set; }
	   public decimal ncpdp_new_profit { get; set; }
	   public decimal awp { get; set; }

	   // Custom Fields
	   public string UpdatedBy { get; set; } // [UpatedBy]

	   public ViewCubeNCPDPModel()
	   {
	   }
	}
}
