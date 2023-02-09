using System;

namespace PharmacyAdmin.Model
{
    public class AmbrisentanREMSModel
    {
        public int RecordID { get; set; }
        public string Activity_GUID { get; set; }
        public string RecordDateTimeStamp { get; set; } // [Record Date and Timestamp]
        public string RecordType { get; set; } //  [Record Type]
        public string SPUniqueID { get; set; } // [SP Unique ID]
        public string SPNABPNCPDPID { get; set; } // [SP NABP-NCPDP ID]
        public string PatientREMSID { get; set; } // [Patient REMS ID]
        public string PatientStatus { get; set; } // [Patient Status]
        public string PatientStatusReason { get; set; } // [Patient Status Reason]
        public string PrescriberREMSID { get; set; } // [Prescriber REMS ID]
        public string DrugNDCNumber { get; set; } // [Drug NDC Number] 
        public string DaysSupply { get; set; } // [Days Supply]
        public string QuantityShipped { get; set; } // [Quantity shipped]
        public string FillNumber { get; set; } // [Fill Number]
        public string RefillsRemaining { get; set; } // [Refills Remaining]
        public string FemaleOfReproductivePotential { get; set; } // [Female of Reproductive Potential]
        public string MedicationGuide { get; set; } // [Medication Guide]
        public string PregnancyTestResponse { get; set; }  // [Pregnancy Test Response]
        public string REMSTestsResponseProvider { get; set; } // [REMS Tests Response Provider] 
        public string REMSTestsResponseCaptureTimestamp { get; set; } // [REMS Tests Response Capture Timestamp] 
        public string REMSRequiredTestsPhysicianAuthorizationResponse { get; set; } // [REMS Required Tests Physician Authorization Response] 
        public string REMSRequiredTestsPhysicianAuthorizationDate { get; set; } // [REMS Required Tests Physician Authorization Date] 
        public string PatientCounselingProvided { get; set; } // [Patient Counseling Provided] 
        public string PatientYearOfBirth { get; set; } // [Patient Year of Birth] 
        public string TransactionID { get; set; } //       [Transaction ID] 
        public string SPPatientID { get; set; } // [SP Patient ID] 
        public string Gender { get; set; }
        public string DispenseAuthorizationCode { get; set; } // [Dispense Authorization Code] 
        public string RestatementFlag { get; set; } // [Restatement Flag]
        public string PrescriberNPI { get; set; } // [Prescriber NPI]
        public string ShipDate { get; set; } // [Ship Date]
        public DateTime ShipDatePart { get; set; } // [Ship Date]

        public string PatientLastName { get; set; } // [Patient Last Name] 
        public string PatientFirstName { get; set; } // [Patient First Name]
        public DateTime CreatedTimestamp { get; set; } // [Created Timestamp] 
        public int delflag { get; set; }
        public string SubmitToUBC { get; set; } // [Submit to UBC] as
        public string FinalStatus { get; set; } // [FinalStatus]
        public string UpdatedBy { get; set; } // [UpatedBy]

        public bool StatusControlDisabled { get; set; } // Status Control Disabled for not None enteries

        public AmbrisentanREMSModel()
        {
        }
    }
}
