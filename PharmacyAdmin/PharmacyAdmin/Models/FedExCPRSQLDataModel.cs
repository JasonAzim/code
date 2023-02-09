using System.Collections.Generic;

namespace PharmacyAdmin.Model
{
    public class FedExCPRSQLDataModel
    {
        public List<FedExCPRSQLDispenseModel> FedExCPRSQLDispenseList { get; set; }

        public List<FedExCPRSQLDispenseModel> FedExCPRSQLTicketList { get; set; }

        public FedExCPRSQLDataModel()
        {
            FedExCPRSQLDispenseList = new List<FedExCPRSQLDispenseModel>();
            FedExCPRSQLTicketList = new List<FedExCPRSQLDispenseModel>();
            //RecordID = dispense_prescription_sys_id.Value;
        }
    }
}
