using System;
using System.Collections.Generic;

namespace PharmacyAdmin.Model
{
    public class ProfitabilityNCPDPModel
    {
        public int LABLOGNO { get; set; }
        public string ncpdp_invoice_company_code { get; set; }
        public string ncpdp_invoice_company_name { get; set; }
        public int ncpdp_invoice_site_number { get; set; }
        public string ncpdp_invoice_site_name { get; set; }
        public DateTime? ncpdp_date_filled_timestamp { get; set; }
        public DateTime? ncpdp_invoice_datebilled_timestamp { get; set; }
        public DateTime? ncpdp_invoice_date_of_service_timestamp { get; set; }
        public string ncpdp_rx_number { get; set; }
        public string ncpdp_rx_description { get; set; }
        public string ncpdp_days_supply { get; set; }
        public string ncpdp_drug_name { get; set; }
        public string drug_name { get; set; }
        public string ncpdp_ndc { get; set; }
        public decimal? ncpdp_quantity_dispenses { get; set; }
        public decimal? ncpdp_quantity_intended_to_dispense { get; set; }
        public decimal? ncpdp_quantity_prescribed { get; set; }
        public string ncpdp_quantity_new_refill_code { get; set; }
        public string ncpdp_daw_yn { get; set; }
        public string ncpdp_daw_description { get; set; }
        public string ncpdp_count_of_refills { get; set; }
        public string ncpdp_uom { get; set; }
        public DateTime? ncpdp_date_written_timestamp { get; set; }
        public decimal? ncpdp_days_intended { get; set; }
        public string ncpdp_compound_code { get; set; }
        public string ncpdp_compound_dosage_form { get; set; }
        public string ncpdp_compound_dosage_unit_form { get; set; }
        public string ncpdp_route_of_administration { get; set; }
        public string ncpdp_product_id_qualifier { get; set; }
        public string ncpdp_compound_type { get; set; }
        public decimal? ncpdp_pharmacy_service_type { get; set; }
        public string ncpdp_eligible_clarification_code { get; set; }
        public string ncpdp_dispensing_status { get; set; }
        public string ncpdp_authorization_id { get; set; }
        public string ncpdp_authorization_type_id { get; set; }
        public string ncpdp_pharmacist_initials { get; set; }
        public string ncpdp_prior_authorization_type_code { get; set; }
        public string ncpdp_pa_mc_code { get; set; }
        public int ncpdp_invoice_number { get; set; }
        public string ncpdp_claim_number { get; set; }
        public int ncpdp_primary_claim_sys_id { get; set; }

        public decimal? ncpdp_primary_claim_billed { get; set; }
        public decimal? ncpdp_primary_claim_expected { get; set; }
        public int ncpdp_primary_claim_paid_response_sys_id { get; set; }
        public decimal? ncpdp_primary_claim_paid { get; set; }
        public string ncpdp_primary_claim_payor { get; set; }
        public int ncpdp_secondary_claim_sys_id { get; set; }
        public decimal? ncpdp_secondary_claim_billed { get; set; }
        public decimal? ncpdp_secondary_claim_expected { get; set; }
        public string ncpdp_secondary_claim_payor { get; set; }
        public int ncpdp_secondary_claim_paid_response_sys_id { get; set; }
        public decimal? ncpdp_secondary_claim_paid { get; set; }
        public int ncpdp_patient_copay_expected_response_sys_id { get; set; }
        public decimal? ncpdp_patient_copay_expected { get; set; }
        public int ncpdp_patient_secondary_copay_expected_response_sys_id { get; set; }
        public decimal? ncpdp_patient_secondary_copay_expected { get; set; }
        public int ncpdp_other_secondary_copay_expected_response_sys_id { get; set; }
        public decimal? ncpdp_other_secondary_copay_expected { get; set; }
        public decimal? ncpdp_patient_paid { get; set; }
        public decimal? ncpdp_claim_adjustment { get; set; }
        public decimal? ncpdp_claim_balance { get; set; }
        public decimal? ncpdp_ncpdp_cost { get; set; }
        public decimal? ncpdp_ticket_cost { get; set; }
        public string ncpdp_claim_status { get; set; }
        public string patient_mrn { get; set; }
        public string patient_full_name { get; set; }
        public string patient_first_name { get; set; }
        public string patient_last_name { get; set; }
        public DateTime? patient_date_of_birth_timestamp { get; set; }
        public string patient_gender { get; set; }
        public string patient_state { get; set; }
        public string patient_city { get; set; }
        public string patient_address { get; set; }
        public string patient_zip { get; set; }
        public string physician_full_name { get; set; }
        public string physician_last_name { get; set; }
        public string physician_first_name { get; set; }
        public string physician_npi_number { get; set; }
        public string physician_dea_number { get; set; }
        public string physician_license_number { get; set; }
        public string physician_address { get; set; }
        public string physician_city { get; set; }
        public string physician_state { get; set; }
        public string physician_zip_code { get; set; }
        public string primary_payor_binno { get; set; }
        public string primary_payor_pcn { get; set; }
        public string primary_payor_name { get; set; }
        public string primary_payor_type { get; set; }
        public string secondary_payor_binno { get; set; }
        public string secondary_payor_pcn { get; set; }
        public string secondary_payor_name { get; set; }
        public string secondary_payor_type { get; set; }
        public string ncpdp_primary_payor_relationship_code { get; set; }
        public string patient_primary_payor_policy_number { get; set; }
        public string patient_primary_payor_group_number { get; set; }
        public decimal? ncpdp_invoice_expected { get; set; }
        public decimal? ncpdp_invoice_billed { get; set; }
        public decimal? ncpdp_sec_ncpdp_cost { get; set; }
        public decimal? ncpdp_new_profit { get; set; }
        public string primary_ancillary_provider { get; set; }
        public string ncpdp_revenue_status { get; set; }
        public string invoice_therapy { get; set; }
        public string ncpdp_hcpcs_code { get; set; }
        public string patient_icd9_1 { get; set; }
        public string patient_icd9_2 { get; set; }
        public string patient_icd9_3 { get; set; }
        public string patient_icd9_4 { get; set; }
        public string patient_icd9_1_diagnosis { get; set; }
        public string patient_icd9_2_diagnosis { get; set; }
        public string patient_icd9_3_diagnosis { get; set; }
        public string patient_icd9_4_diagnosis { get; set; }
        public string patient_icd10_1 { get; set; }
        public string patient_icd10_2 { get; set; }
        public string patient_icd10_3 { get; set; }
        public string patient_icd10_4 { get; set; }
        public string patient_icd10_1_diagnosis { get; set; }
        public string patient_icd10_2_diagnosis { get; set; }
        public string patient_icd10_3_diagnosis { get; set; }
        public string patient_icd10_4_diagnosis { get; set; }
        public string invoice_icd9_1 { get; set; }
        public string invoice_icd9_2 { get; set; }
        public string invoice_icd9_3 { get; set; }
        public string invoice_icd9_4 { get; set; }
        public string invoice_icd9_1_diagnosis { get; set; }
        public string invoice_icd9_2_diagnosis { get; set; }
        public string invoice_icd9_3_diagnosis { get; set; }
        public string invoice_icd9_4_diagnosis { get; set; }
        public string invoice_icd10_1 { get; set; }
        public string invoice_icd10_2 { get; set; }
        public string invoice_icd10_3 { get; set; }
        public string invoice_icd10_4 { get; set; }
        public string invoice_icd10_1_diagnosis { get; set; }
        public string invoice_icd10_2_diagnosis { get; set; }
        public string invoice_icd10_3_diagnosis { get; set; }
        public string invoice_icd10_4_diagnosis { get; set; }
        public int ncpdp_invoice_company_sys_id { get; set; }
        public string pri_optional_organization { get; set; }
        public string sec_optional_organization { get; set; }
        public decimal? awp { get; set; }
        public string patient_team { get; set; }
        public string patient_code_status { get; set; }
        public string patient_referral_organization { get; set; }
        public string ncpdp_rx_origin_description { get; set; }

        // Ticket
        public int? dispense_sys_id { get; set; }
        public int? rx_sys_id { get; set; }
        public string ticket_item_name { get; set; }
        public string ticket_item_delivins { get; set; }
        public string ticket_item_change { get; set; }
        public string ticket_name { get; set; }
        public string ticket_chgbyhost { get; set; }
        public string ticket_item_chgbyhost { get; set; }
        public decimal? ticket_each_cost { get; set; }
        public decimal? ticket_total_cost { get; set; }
        public int? ticket_item_sys_id { get; set; }
        public int ticket_cpk_tickc { get; set; }
        public int? ticket_delflag_overlay { get; set; }
        public int? ticket_item_delflag_overlay { get; set; }

        public string ticket_delflag_chgbyhost_class
        {
            get
            {
                return GetElementClass(RecordDeleted);
            }
        }

        public string ticket_item_delflag_chgbyhost_class
        {
            get
            {
                return GetElementClass(RecordDeleted);
            }
        }

        public DateTime? ticket_confirmation_date { get; set; }
        public decimal? ticket_unit_cost { get; set; }

        public string ticket_partials_name { get; set; }
        public int ticket_partials_ticket_number { get; set; }

        public string inventory_item_name { get; set; }

        // custom fields
        public string vatext { get; set; }
        public decimal? TPRevenuePlusPatientCopay { get; set;}
        public decimal? Copay { get; set; }
        public decimal? COGS { get; set; }
        public decimal? COGSAdjusted { get; set; }
        public decimal? GrossProfit { get; set; }

        public decimal? TPRevenue
        {
            get
            {
                return ncpdp_primary_claim_paid + ncpdp_secondary_claim_paid;
            }
        }

        public decimal? ncpdp_ticket_item_each_cost { get; set; }
        public decimal? GrossProfitAdjusted { get; set; }
         

        public string History { get; set; }
        public string PrimaryTPHistory { get; set; }
        public int PrimaryTPUpdated { get; set; }
        public string SecondaryTPHistory { get; set; }
        public int SecondaryTPUpdated { get; set; }
        public string CopayHistory { get; set; }
        public int CopayUpdated { get; set; }
        public string COGSHistory { get; set; }
        public int COGSUpdated { get; set; }
        public string RecordDeletedHistory { get; set; }
        public int RecordDeleted { get; set; }
        public int CHGFLAG
        {
            get
            {
                if (PrimaryTPUpdated > 0) return 1;
                else if (SecondaryTPUpdated > 0) return 1;
                else if (CopayUpdated > 0) return 1;
                else if (COGSUpdated > 0) return 1;
                else if (RecordDeleted > 0) return 1;
                else return 0;
            }
        }

        public int TICKNO { get; set; }
        public decimal? ticket_quantity { get; set; }
        public int ticket_item_quantity { get; set; }

        // Audit Tracking Overlay fields and original values
        public decimal? ticket_cost_original { get; set; }

        public decimal? ticket_cost_overlay { get; set; }
        public decimal? Copay_original { get; set; }
        public decimal? Copay_overlay { get; set; }
        public decimal? revenue_total_expected_original { get; set; }
        public decimal? revenue_total_expected_overlay { get; set; }
        public decimal? primary_claim_billed_original { get; set; }
        public decimal? primary_claim_billed_overlay { get; set; }
        public decimal? secondary_claim_billed_original { get; set; }
        public decimal? secondary_claim_billed_overlay { get; set; }

        public string GetElementClass(int count)
        {
            return (count == 0)
                      ? ""
                      : "highlightCell";
        }

        public string PrimaryTPPaid_chgbyhost { get; set; }
       
        public string PrimaryTPPaid_class
        {
            get
            {
                return GetElementClass(PrimaryTPUpdated);
            }
        }

        public string SecondaryTPPaid_chgbyhost { get; set; }

        public string SecondaryTPPaid_class
        {
            get
            {
                return GetElementClass(SecondaryTPUpdated);
            }
        }

        public string Copay_chgbyhost { get; set; }

        public string Copay_class
        {
            get
            {
                return GetElementClass(CopayUpdated);
            }
        }

        public string ticket_cost_chgbyhost { get; set; }

        public string ticket_cost_class
        {
            get
            {
                return GetElementClass(COGSUpdated);
            }
        }

        public string RowClass
        {
            get
            {
                if (TicketSelected)
                {
                    return "RowFormatStrike";
                }
                else
                {
                    return "";
                }
            }
        }

        public bool IsDuplicate { get; set; } = false; // Is a duplicate invoice record
        public string related_number { get; set; }
        public int DELFLAG { get; set; }
        public bool RecordSelected { get; set; }
        public bool TicketSelected { get; set; }

        public string UpdatedBy { get; set; } = "";

        public string MessageDelete
        {
            get
            {
                string message = (TicketSelected == true) ? " deleted" : " restored";
                return "Ticket #" + TICKNO + message;
            }
        }

        public int? ROWNUM { get; set; }

        public string Events { get; set; } 

        public ProfitabilityNCPDPModel ShallowCopy()
        {
            return (ProfitabilityNCPDPModel)this.MemberwiseClone();
        }

        public ProfitabilityNCPDPModel Calculate()
        {
            ProfitabilityNCPDPModel entity = (ProfitabilityNCPDPModel)this.MemberwiseClone(); // changed entity with the new recalculated values
            entity.COGS = ticket_total_cost;

            //decimal zero = 0M;

            List<string> ContractList = new List<string>() { "No Contract", "BLANKT", "Other Vendor" };
            if (string.IsNullOrEmpty(vatext))
            {
                entity.COGSAdjusted = ticket_total_cost;
            }
            else if (ContractList.Contains(vatext))
            {
                entity.COGSAdjusted = ticket_total_cost;
            }
            else
            {
                decimal adjustment = 0.8M;
                entity.COGSAdjusted = decimal.Multiply(ticket_total_cost.Value, adjustment);
            }

            if (ncpdp_primary_claim_payor == "GRATIS")
            {
                entity.TPRevenuePlusPatientCopay = ncpdp_primary_claim_paid;
            }
            else if (ncpdp_primary_claim_payor == "REPLACEMENT FILL-**LOSS**")
            {
                entity.TPRevenuePlusPatientCopay = ncpdp_primary_claim_paid;
            }
            else if (ncpdp_primary_claim_payor == "REPLACEMENT FILL-**NO LOSS**")
            {
                entity.TPRevenuePlusPatientCopay = ncpdp_primary_claim_paid;
            }
            else if (ncpdp_primary_claim_payor == "CASH")
            {
                entity.TPRevenuePlusPatientCopay = ncpdp_primary_claim_paid + Copay;
            }
            else if (ncpdp_secondary_claim_paid == null)
            {
                entity.TPRevenuePlusPatientCopay = ncpdp_primary_claim_paid + Copay;
            }
            else if (ncpdp_secondary_claim_paid == 0)
            {
                entity.TPRevenuePlusPatientCopay = ncpdp_primary_claim_paid + Copay;
            }
            else
            {
                entity.TPRevenuePlusPatientCopay = ncpdp_primary_claim_paid + ncpdp_secondary_claim_paid;
            }

            entity.GrossProfit = entity.TPRevenuePlusPatientCopay - entity.COGS;

            System.Diagnostics.Debug.WriteLine("COGS old={0} and new={1}", COGS, entity.COGS);
            System.Diagnostics.Debug.WriteLine("COGSAdjusted old={0} and new={1}", COGSAdjusted, entity.COGSAdjusted);

            System.Diagnostics.Debug.WriteLine("TPRevenuePlusPatientCopay old={0} and new={1}", TPRevenuePlusPatientCopay, entity.TPRevenuePlusPatientCopay);
            System.Diagnostics.Debug.WriteLine("GrossProfit old={0} and new={1}", GrossProfit, entity.GrossProfit);
            System.Diagnostics.Debug.WriteLine("GrossProfitAdjusted old={0} and new={1}", GrossProfitAdjusted, entity.GrossProfitAdjusted);

            return entity;
        }

    }
}
