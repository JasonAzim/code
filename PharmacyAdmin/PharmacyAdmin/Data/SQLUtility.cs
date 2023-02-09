using System;
using System.Data;
using System.Data.SqlClient;
using Global;

namespace PharmacyAdmin.Data
{
    public static class SQLUtility 
    {
        public static readonly string WHERE = " where ";
        public static readonly string AND = " and ";

        public static readonly string ORDER_BY = " order by ";

        #region Ambrisentan
        public static readonly string AMBRISENTAN_REMS_TABLE_SELECT = @"
            SELECT [Record ID] as RecordID,Activity_GUID,[Record Date and Timestamp] as RecordDateTimeStamp,[Record Type] as RecordType
            ,[SP Unique ID] as SPUniqueID,[SP NABP-NCPDP ID] as SPNABPNCPDPID,[Patient REMS ID] as PatientREMSID
            ,[Patient Status] as PatientStatus,[Patient Status Reason] as PatientStatusReason,[Prescriber REMS ID] as PrescriberREMSID
            ,[Drug NDC Number] as DrugNDCNumber,[Days Supply] as DaysSupply,[Quantity shipped] as QuantityShipped
            ,[Fill Number] as FillNumber,[Refills Remaining] as RefillsRemaining,[Female of Reproductive Potential] as FemaleOfReproductivePotential
            ,[Medication Guide] as MedicationGuide,[Pregnancy Test Response] as PregnancyTestResponse,[REMS Tests Response Provider] as REMSTestsResponseProvider
            ,[REMS Tests Response Capture Timestamp] as REMSTestsResponseCaptureTimestamp
	        ,[REMS Required Tests Physician Authorization Response] as REMSRequiredTestsPhysicianAuthorizationResponse
            ,[REMS Required Tests Physician Authorization Date] as REMSRequiredTestsPhysicianAuthorizationDate,[Patient Counseling Provided] as PatientCounselingProvided
            ,[Patient Year of Birth] as PatientYearOfBirth,[Transaction ID] as TransactionID,[SP Patient ID] as SPPatientID
            ,[Gender],[Dispense Authorization Code] as DispenseAuthorizationCode,[Restatement Flag] as RestatementFlag
            ,[Prescriber NPI] as PrescriberNPI,CONVERT(VARCHAR(10), [Ship Date], 101) as ShipDate,[Ship Date] as ShipDatePart,[Patient Last Name] as PatientLastName
            ,[Patient First Name] as PatientFirstName,[Created Timestamp] as CreatedTimestamp,[delflag]
            ,CASE WHEN [Submit to UBC] IS NULL THEN 'Y' WHEN [Submit to UBC] = '' THEN 'Y' ELSE [Submit to UBC] END  as SubmitToUBC,isnull(FinalStatus,'None') as FinalStatus
			,CASE WHEN FinalStatus IS NULL THEN 'false' ELSE 'true' END as StatusControlDisabled
            ,UpdatedBy
            FROM [Ambrisentan_REMS_Data]";

        public static readonly string AMBRISENTAN_REMS_TABLE_FILTER_PATIENT_LAST_NAME = @" Upper([Patient Last Name]) not like '%TEST%'";
        public static readonly string AMBRISENTAN_REMS_TABLE_FILTER_SUBMIT_TO_UBC = @" ltrim(rtrim([Submit to UBC])) != 'N'";
        public static readonly string AMBRISENTAN_REMS_TABLE_FILTER_SHIP_DATE = @" [Ship Date] >= Convert(datetime, '{0}' )";
        public static readonly string AMBRISENTAN_REMS_TABLE_FILTER_ORDER_BY = @" order by [Ship Date]";
        public static readonly string AMBRISENTAN_REMS_TABLE_FILTER_ORDER_BY_TRANSACTION_ID = @" order by [Transaction ID]";

        public static string AmbrisentanREMSGetAllQuery(string SortColumnName, string SortDirection, string ShipDate, bool FilterSubmitToUBC = false)
        {
            string query = AMBRISENTAN_REMS_TABLE_SELECT + WHERE + AMBRISENTAN_REMS_TABLE_FILTER_PATIENT_LAST_NAME + AND + string.Format(AMBRISENTAN_REMS_TABLE_FILTER_SHIP_DATE, ShipDate);

            if (FilterSubmitToUBC)
            {
                query += AND + AMBRISENTAN_REMS_TABLE_FILTER_SUBMIT_TO_UBC;
            }
            query += ORDER_BY + SortColumnName + " " + SortDirection;

            return query;
        }

        public static string AmbrisentanREMSGetAllQuery(string SortColumnName, string SortDirection, string ShipDate)
        {
            return AMBRISENTAN_REMS_TABLE_SELECT + WHERE + AMBRISENTAN_REMS_TABLE_FILTER_PATIENT_LAST_NAME + AND + string.Format(AMBRISENTAN_REMS_TABLE_FILTER_SHIP_DATE, ShipDate) + AND + AMBRISENTAN_REMS_TABLE_FILTER_SUBMIT_TO_UBC + ORDER_BY + SortColumnName + " " + SortDirection;
        }

        public static string AmbrisentanREMSListQuery(string SortDirection, string ShipDate)
        {
            string query = AMBRISENTAN_REMS_TABLE_SELECT + WHERE + AMBRISENTAN_REMS_TABLE_FILTER_PATIENT_LAST_NAME + AND + string.Format(AMBRISENTAN_REMS_TABLE_FILTER_SHIP_DATE, ShipDate) + AMBRISENTAN_REMS_TABLE_FILTER_ORDER_BY + " " + SortDirection;
            return query;
        }

        public static string AmbrisentanREMSGetSortColumnName(string SortColumnName)
        {
            if (SortColumnName == "TransactionID")
            {
                SortColumnName = "[Transaction ID]";
            }
            else if (SortColumnName == "ShipDate")
            {
                SortColumnName = "[Ship Date]";
            }
            else if (SortColumnName == "MRN")
            {
                SortColumnName = "[SP Patient ID]";
            }
            else if (SortColumnName == "FinalStatus")
            {
                SortColumnName = "FinalStatus";
            }
            else if (SortColumnName == "Restatement")
            {
                SortColumnName = "[Restatement Flag]";
            }
            else if (SortColumnName == "SubmitToUBC")
            {
                SortColumnName = "[Submit To UBC]";
            }
            else if (SortColumnName == "Created")
            {
                SortColumnName = "[Created Timestamp]";
            }
            else if (SortColumnName == "UpdatedBy")
            {
                SortColumnName = "UpdatedBy";
            }

            return SortColumnName;
        }
        #endregion

        public static readonly string ERRORLOG_VIEW_SELECT = "SELECT ErrorLogID,ErrorClass,ErrorType,Code,Object,Source,SourceLineNo,Message,ReportedDate,UserID,LoginID,DisplayException,Trace,Detail,Debug FROM ErrorLog";
        public static readonly string ERRORLOG_FILTER_BY_ID = "ErrorLogID = {0}";

        #region Jobs
        public static readonly string ISHUB_JOB_TABLE_SELECT = @"SELECT[JobID],[Name],[Owner],[Category],[Description],[Enabled],[NotificationType],[NotificationEnabled],[Status],[ScheduleTime],[Schedule],[LastRunTime],[LastStopTime],[RunTimeMinutes],[CreatedDate],[LastUpdateDate] ,[UpdatedBy] FROM [dbo].[Job] ";
        public static readonly string ISHUB_JOB_TABLE_FILTER_CATEGORY = @" Category = @Category ";

        public static string ISHUBGetJobs(string Category)
        {
            string query = string.Empty;

            if (string.IsNullOrEmpty(Category))
            {
                query = ISHUB_JOB_TABLE_SELECT;
            }
            else
            {
                query = ISHUB_JOB_TABLE_SELECT + WHERE + ISHUB_JOB_TABLE_FILTER_CATEGORY;
            }

            return query;
        }

        public static string ISHUBJobInsertQuery()
        {
            string query;

            query = $"IF NOT EXISTS(Select Name From [dbo].[Job] where Name = @Name)";
            query += $"insert into[dbo].[Job]([JobID],[Name],[Owner],[Category],[Description],[Enabled],[NotificationType],[NotificationEnabled],[Status],[CreatedDate], LastUpdateDate, UpdatedBy)";
            query += $"values(NEWID(), @Name,@Owner,@Category,@Description,@Enabled,@NotificationType,@NotificationEnabled,@Status,@CreatedDate,@LastUpdateDate,@UpdatedBy)";

            return query;
        }

        public static readonly string ISHUB_JOB_SCHEDULES_TABLE_SELECT = @"select S.JobScheduleID, S.JobID, S.Enabled, S.ScheduleType, S.Frequency, S.ScheduledDate, S.DailyFrequencyOccursOnceAt, S.DailyFrequencyOccursEvery, S.DailyFrequencyRate, S.StartDate, S.EndDate, S.Status, S.CreatedDate, S.LastUpdateDate, S.UpdatedBy
                                                                           from dbo.JobSchedule S
                                                                           inner join dbo.Job J on J.JobID = S.JobID
                                                                           where S.JobScheduleID is not null ";
        public static readonly string ISHUB_Job_UpdateSchedule = @"
        update dbo.JobSchedule set ScheduledDate = @ScheduledDate, LastUpdateDate = GetDate(), UpdatedBy = @UpdatedBy where JobID = @JobID";

        #endregion

        #region URAC
        public static readonly string ISHUB_JOB_SCHEDULE_TABLE_SELECT = @"
                select JobScheduleID,JobID,Enabled,ScheduleType,Frequency,ScheduledDate,DailyFrequencyOccursOnceAt,DailyFrequencyOccursEvery,DailyFrequencyRate,StartDate,EndDate,Status,CreatedDate,LastUpdateDate,UpdatedBy
                from dbo.JobSchedule";

        public static readonly string ISHUB_JOB_FILTER_RXNUMBER = @" JobID = @JobID";

        public static readonly string ISHUB_JOB_SCHEDULE_VIEW_SELECT = @"
               select S.JobScheduleID, S.JobID, S.Enabled, S.ScheduleType, S.Frequency, S.ScheduledDate, S.DailyFrequencyOccursOnceAt, S.DailyFrequencyOccursEvery, S.DailyFrequencyRate, S.StartDate, S.EndDate, S.Status, S.CreatedDate, S.LastUpdateDate, S.UpdatedBy
               from dbo.JobSchedule S
               inner join dbo.Job J on J.JobID = S.JobID
               where J.Name =  @JobName";

        // Not used but good for listing Jobs and their schedules
        public static readonly string ISHUB_JOB_SCHEDULES_VIEW_SELECT = @"
                select J.JobID,J.Name,J.Enabled,NotificationEnabled,J.Status as JobStatus,ScheduleTime,LastRunTime,LastStopTime,S.JobScheduleID,S.Enabled as JobScheduleEnabled,S.ScheduledDate,S.StartDate,S.EndDate
                from dbo.Job J
                left join dbo.JobSchedule S on J.JobID = S.JobID";

        public static readonly string ISHUB_JOB_RUNNING_VIEW_SELECT = @"
                SELECT max(run_requested_date) as max_run_requested_date
                FROM msdb.dbo.sysjobactivity AS sja
                INNER JOIN msdb.dbo.sysjobs AS sj ON sja.job_id = sj.job_id
                where sj.name = '{0}'
                and sja.start_execution_date IS NOT NULL AND sja.stop_execution_date IS NULL
                group by sj.name, sja.job_id";

        public static readonly string ISHUB_JOB_OUTCOME_VIEW_SELECT = @"
                SELECT msdb.dbo.agent_datetime(run_date, run_time) as run_date_time
                FROM msdb.dbo.sysjobhistory h
                JOIN msdb.dbo.sysjobs j ON j.job_id = h.job_id
                LEFT JOIN ISHUB.dbo.Job i ON j.job_id = i.JobID
                where j.name = '{0}'
                  and step_name = '(Job outcome)'
                order by run_date, run_time";

        public static readonly string ISHUB_JOB_RUN_SUMMARY_VIEW_SELECT = @"
        select 'Total Dispense Records' as KPI,(select count(*) from [URAC].[tb_dispense_tat_step1]) as 'Count'
          union
        select 'Missing End Dates' as KPI,(select count(*) from [URAC].[tb_dispense_tat_step1] where tat_end_date is null) as 'Count'
          union
        select 'Missing Start Dates' as KPI,(select count(*) from [URAC].[tb_dispense_tat_step1] where tat_start_date is null) as 'Count'
          union
        select 'Start Date greater than End Date' as KPI,(select count(*) from [URAC].[tb_dispense_tat_step1] where tat_start_date > tat_end_date) as 'Count'";

        public static readonly string URAC_TURNAROUNDTIME_DISPENSE_STEP1 = @"
        select line_numb,[RX number] as RXNumber,[Fill Num] as FillNum,tat_start_date,tat_end_date,invoice_start_date_of_service,[Ticket Confirmation Date] as TicketConfirmationDate
        from [URAC].[tb_dispense_tat_step1] ";

        public static readonly string URAC_TURNAROUNDTIME_FILTER_DATEZERO = @" tat_start_date > tat_end_date";

        public static readonly string URAC_ISHUB_ScheduleJob = @"
        update dbo.JobSchedule set ScheduledDate = @ScheduledDate, StartDate = @StartDate, EndDate = @EndDate, LastUpdateDate = GetDate(), UpdatedBy = @UpdatedBy where JobID = @JobID";

        public static readonly string URAC_TB_DISPENSE_TAT_STEP1 = @"
        SELECT [line_numb],[dispense_order_sys_id],[mrn],[RX number] as RXNumber,[Drug Name Strength Form] as DrugNameStrengthForm
               ,[invoice_start_date_of_service],[Ticket Confirmation Date] as TicketConfirmationDate,[Fill Num] as FillNum
	           ,[PreviousFillNum],[Patient_Last Name] as PatientLastName,[Patient First Name] as PatientFirstName
	           ,[Patient Street Address] as PatientStreetAddress,[Patient City] as PatientCity,[Patient State] as PatientState
	           ,[Patient Zip Code] as PatientZipCode,[Patient Date of Birth] as PatientDateofBirth,[Patient Gender] as PatientGender
	           ,[Drug NDC] as DrugNDC,[Drug DEA Schedule] as DrugDEASchedule,[Directions for use] as Directionsforuse
	           ,[Quantity Dispensed] as QuantityDispensed,[Days Supply] as DaysSupply,[Date Filled] as DateFilled,[Delivery Date] as DeliveryDate
	           ,[Number of refills authorized] as Numberofrefillsauthorized,[Doctor Name] as DoctorName,[Doctor DEA] as DoctorDEA
	           ,[Doctor NPI] as DoctorNPI,[Doctor Street Address] as DoctorStreetAddress,[Doctor State] as DoctorState,[Doctor Zip] as DoctorZip
               ,[Payment name] as PaymentName,[Name of insurance billed] as Nameofinsurancebilled,[payor_type],[dispense_type],[insurance_identifier]
	           ,[invoice_sys_id],[SortUID]
        FROM[URAC].[tb_dispense_tat_step1]";

        public static readonly string URAC_TB_DISPENSE_TAT_STEP2 = @"
        SELECT [line_numb],[dispense_order_sys_id],[SortUID],[invoice_sys_id],[mrn],[RX number] as RXNumber
               ,[Drug Name Strength Form] as DrugNameStrengthForm,[invoice_start_date_of_service],[Ticket Confirmation Date] as TicketConfirmationDate,[tat_start_date]
               ,[tat_end_date],[New_Refill],[Fill Num] as FillNum,[Patient_Last Name] as PatientLastName,[Patient First Name] as PatientFirstName,[Patient Street Address] as PatientStreetAddress
               ,[Patient City] as PatientCity,[Patient State] as PatientState,[Patient Zip Code] as PatientZipCode,[Patient Date of Birth] as PatientDateofBirth,[Patient Gender] as PatientGender,[Drug NDC] as DrugNDC
               ,[Drug DEA Schedule] as DrugDEASchedule,[Directions for use] as Directionsforuse,[Quantity Dispensed] as QuantityDispensed,[Days Supply] as DaysSupply
               ,[Date Filled] as DateFilled,[Delivery Date] as DeliveryDate,[Number of refills authorized] as Numberofrefillsauthorized,[Doctor Name] as DoctorName
               ,[Doctor DEA] as DoctorDEA,[Doctor NPI] as DoctorNPI,[Doctor Street Address] as DoctorStreetAddress,[Doctor State] as DoctorState
               ,[Doctor Zip] as DoctorZip,[Payment name] as PaymentNname,[Name of insurance billed] as Nameofinsurancebilled,[payor_type]
               ,[dispense_type],[queue_move],[claimsnew_date],[intervention_required_date],[all_queue_moves],[last_events],[Turn Around Time Type] as TurnAroundTimeType
        FROM [URAC].[tb_dispense_tat_step2]";

        public static readonly string URAC_TB_DISPENSE_TAT = @"
        SELECT [line_numb],[Drug Name Strength Form] as DrugNameStrengthForm,[Start Date of Service] as tat_start_date,[End Date of Service] as tat_end_date
               ,[Fill Num] as FillNum,[NDC],[Drug NDC] as DrugNDC,[Drug DEA Schedule] as DrugDEASchedule,[Directions for use] as Directionsforuse
               ,[Quantity Dispensed] as QuantityDispensed,[Days Supply] as DaysSupply,[Date Filled] as DateFilled,[Delivery Date] as DeliveryDate
               ,[Number of refills authorized] as Numberofrefillsauthorized,[Doctor Name] as DoctorName,[Doctor DEA] as DoctorDEA
               ,[Doctor Street Address] DoctorStreetAddress,[Doctor State] as DoctorState,[Doctor Zip] as DoctorZip,[Payment name] as PaymentName
               ,[Name of insurance billed] as Nameofinsurancebilled,[payor_type],[Turn Around Time Type] as TurnAroundTimeType,[dispense_order_sys_id]
               ,[SortUID],[New_Refill],[invoice_start_date_of_service],[Ticket Confirmation Date] as TicketConfirmationDate
               ,[last_events],[queue_move],[all_queue_moves]
        FROM [URAC].[tb_dispense_tat]";

        public static readonly string URAC_TURNAROUNDTIME_CALCULATE_STEP2 = @"
        select line_numb,[Drug Name Strength Form] as DrugNameStrengthForm,[Start Date of Service] as tat_start_date,[End Date of Service] as tat_end_date,[Ticket Confirmation Date] as TicketConfirmationDate,[turnaround_time], last_events, queue_move, all_queue_moves,[Fill Num] as FillNum,[Drug NDC] as DrugNDC,[Drug DEA Schedule] as DrugDEASchedule,[Directions for use] as Directionsforuse,[Quantity Dispensed] as QuantityDispensed,[Days Supply] as DaysSupply,[Date Filled] as DateFilled,[Delivery Date] as DeliveryDate,[Number of refills authorized] as Numberofrefillsauthorized,[Doctor Name] as DoctorName,[Doctor DEA] as DoctorDEA,[Doctor Street Address] as DoctorStreetAddress,[Doctor State] as DoctorState,[Doctor Zip] as DoctorZip,[Payment name] as PaymentName,[Name of insurance billed] as Nameofinsurancebilled,[payor_type],[type] as DrugType,Clean_vs_Unclean,New_Refill from [URAC].[Calc_Turnaround_Time_Step2] ";

        #endregion

        #region ISHUB Master Search
        public static readonly string ISHUB_MASTER_DISPENSE_ORIGINAL_TABLE_SELECT = @"

                select RxTransactionID,RxNumber,RefillNumber,DateFilled,PatientSerialNumber,DispensedItemNDC,DispensedItemName,TotalPriceSubmitted,TotalPricePaid,PatientPaidAmount,AcquisitionCost,GrossProfit
                ,CASE DatabaseUID WHEN 1 THEN 'CPRSQL' WHEN 2 THEN 'PioneerRX' ELSE 'Unknown' END As DatabaseName
                from [Master].[Dispense]";

        public static readonly string ISHUB_MASTER_DISPENSE_CHANGED_TABLE_SELECT =
                @"select M.RxTransactionID,M.RxNumber,M.RefillNumber,M.DateFilled,M.PatientSerialNumber,M.DispensedItemNDC,M.DispensedItemName
                ,CASE WHEN D.TotalPriceSubmittedOverlay IS NOT NULL THEN D.TotalPriceSubmittedOverlay ELSE M.TotalPriceSubmitted END AS TotalPriceSubmitted
                ,CASE WHEN D.TotalPricePaidOverlay IS NOT NULL THEN D.TotalPricePaidOverlay ELSE M.TotalPricePaid END AS TotalPricePaid
                ,CASE WHEN D.PatientPaidAmountOverlay IS NOT NULL THEN D.PatientPaidAmountOverlay ELSE M.PatientPaidAmount END AS PatientPaidAmount
                ,CASE WHEN D.AcquisitionCostOverlay IS NOT NULL THEN D.AcquisitionCostOverlay ELSE M.AcquisitionCost END AS AcquisitionCost
                ,CASE WHEN D.GrossProfitOverlay IS NOT NULL THEN D.GrossProfitOverlay ELSE M.GrossProfit END AS GrossProfit
                ,CASE M.DatabaseUID WHEN 1 THEN 'CPRSQL' WHEN 2 THEN 'PioneerRX' ELSE 'Unknown' END As DatabaseName
                ,M.ticket_shipping_tracking_number
                from [Master].[Dispense] M LEFT JOIN [dbo].Dispense D ON M.RxTransactionID = D.RxTransactionID
                where M.RxNumber = @RxNumber";

        public static readonly string ISHUB_MASTER_FILTER_RXNUMBER = @" RxNumber = @RxNumber";
        public static readonly string ISHUB_MASTER_FILTER_RXTRANSACTIONID = @" RxTransactionID = @RxTransactionID";

        public static readonly string ISHUB_FN_PROFITABILITY_AUDIT_FUNCTION_SELECT = @"
                select AuditID, AuditUID, RXNO, INVOICENO, TableName, ColumnName, ColumnKey, FieldName, OriginalValue, OverlayValue, TouchDate, Chgbyhost, CreatedBy from[Pharmacy].[fn_profitability_audit] (@RXNO,@INVOICENO)";

        public static readonly string ISHUB_FN_PROFITABILITY_AUDIT_MASTER_FUNCTION_SELECT = @"
                select AuditID, AuditUID, RXNO, INVOICENO, TableName, ColumnName, ColumnKey, FieldName, OriginalValue, OverlayValue, TouchDate, Chgbyhost, CreatedBy from[Pharmacy].[fn_profitability_audit_master] (@RXNO)";

        public static readonly string PioneerRX_POINTOFSALE_SHIPMENT_SELECT = @"
                select shipment.ShipmentID, sale.SaleReceiptNumber,shipment.TrackingNumber,saleDetail.ChangedOn
                from (select SaleTransactionID,ChangedOn from PointOfSale.SaleTransactionDetail WITH(NOLOCK) where ReferenceID = @RxTransactionID) AS saleDetail
                JOIN PointOfSale.SaleTransaction AS sale WITH(NOLOCK) ON sale.SaleTransactionID = saleDetail.SaleTransactionID 
                JOIN PointOfSale.Shipment AS shipment WITH(NOLOCK) ON shipment.SaleReceiptString = CONVERT(VARCHAR(20), sale.SaleReceiptNumber)";

        #endregion

        #region Profitability Edit
        public static readonly string DataWarehouse_NCPDP_Adjustment_UpdatePrimaryTPPaid = @"
         update [Data Warehouse].Pharmacy.tb_cube_ncpdp_adjustment
         set ncpdp_primary_claim_paid = @ncpdp_primary_claim_paid
            ,TPRevenuePlusPatientCopay = @TPRevenuePlusPatientCopay
            ,GrossProfit = @GrossProfit
            ,GrossProfitAdjusted = @GrossProfitAdjusted
            ,CHG_PrimaryTP = @CHG_PrimaryTP
         where ncpdp_rx_number = @RXNO and ncpdp_invoice_number = @BILLNO and ncpdp_primary_claim_paid_response_sys_id = @response_sys_id";

        public static readonly string DataWarehouse_NCPDP_Adjustment_UpdateSecondaryTPPaid = @"
         update [Data Warehouse].Pharmacy.tb_cube_ncpdp_adjustment
         set ncpdp_secondary_claim_paid = @ncpdp_secondary_claim_paid               
            ,TPRevenuePlusPatientCopay = @TPRevenuePlusPatientCopay
            ,GrossProfit = @GrossProfit
            ,GrossProfitAdjusted = @GrossProfitAdjusted
            ,CHG_SecondaryTP = @CHG_SecondaryTP
         where ncpdp_rx_number = @RXNO and ncpdp_invoice_number = @BILLNO and ncpdp_secondary_claim_paid_response_sys_id = @response_sys_id";

        public static readonly string DataWarehouse_NCPDP_Adjustment_UpdateCopay = @"
         update [Data Warehouse].Pharmacy.tb_cube_ncpdp_adjustment
         set Copay = @Copay           
            ,TPRevenuePlusPatientCopay = @TPRevenuePlusPatientCopay
            ,GrossProfit = @GrossProfit
            ,GrossProfitAdjusted = @GrossProfitAdjusted
            ,CHG_Copay = @CHG_Copay
         where ncpdp_rx_number = @RXNO and ncpdp_invoice_number = @BILLNO and ncpdp_patient_primary_copay_expected_response_sys_id = @response_sys_id";

        public static readonly string DataWarehouse_NCPDP_Adjustment_UpdateHistory = @"
         update [Data Warehouse].Pharmacy.tb_cube_ncpdp_adjustment 
           set CHGFLAG = @CHGFLAG,
               CHG = @History,
               CHG_PrimaryTP = @CHG_PrimaryTP,
               CHG_SecondaryTP = @CHG_SecondaryTP,
               CHG_Copay = @CHG_Copay,
               CHG_RecordDeleted = @CHG_RecordDeleted,
               CHG_COGS = @CHG_COGS
         where ncpdp_rx_number = @RXNO and ncpdp_invoice_number = @BILLNO and ticket_number = @TICKNO";

        public static readonly string DataWarehouse_NCPDP_Adjustment_UpdateCost = @"
         update [Data Warehouse].Pharmacy.tb_cube_ncpdp_adjustment 
           set ncpdp_ticket_cost = @COGS, [COGS Adjusted] = @COGSAdjusted
               ,GrossProfit = @GrossProfit,GrossProfitAdjusted = @GrossProfitAdjusted
               ,CHG_COGS = @CHG_COGS
         where ncpdp_rx_number = @RXNO and ncpdp_invoice_number = @BILLNO and ticket_number = @TICKNO";

        public static readonly string DataWarehouse_NCPDP_Adjustment_Delete = @"
         update [Data Warehouse].Pharmacy.tb_cube_ncpdp_adjustment 
           set ticket_item_delflag_overlay = @DELFLAG,
               CHG_RecordDeleted = @CHG_RecordDeleted
         where ncpdp_rx_number = @RXNO and ncpdp_invoice_number = @BILLNO and ticket_number = @TICKNO";

        public static readonly string DataWarehouse_NCPDP_Adjustment_Update = @"
         update CPRSQL.Pharmacy.tb_cube_ncpdp_adjustment 
           set ncpdp_primary_claim_paid = @ncpdp_primary_claim_paid
               ,ncpdp_secondary_claim_paid = @ncpdp_secondary_claim_paid
               ,ncpdp_ticket_cost = @COGS,[COGS Adjusted] = @COGSAdjusted
               ,GrossProfit = @GrossProfit
               ,GrossProfitAdjusted = @GrossProfitAdjusted
               ,CHG = @History
         where ncpdp_rx_number = @RXNO and ncpdp_invoice_number = @BILLNO and ticket_number = @TICKNO";

        // Begin Profitability Functions for CPRSQL
        public static readonly string CPRSQL_TB_CUBE_NCPDP_PARTIALS_SELECT = @"
        select ncpdp.patient_mrn, ncpdp.ncpdp_rx_number, ticket.TICKNO, ticket.CONFDATE as ticket_confirmation_date, ncpdp.ncpdp_invoice_number, ncpdp.ncpdp_rx_description, ncpdp_ndc
               , (select top 1 TICKCI.TICKNO from TICKCI where TICKCI.BILLNO = ticket.BILLNO and TICKCI.MRN = ticket.MRN and TICKCI.CONFDATE<> ticket.CONFDATE) as related_number
              from rpbi.tb_cube_ncpdp ncpdp
              inner join
              (select distinct TICKCI.MRN, TICKCI.TICKNO, TICKCI.BILLNO, TICKCI.CONFDATE from TICKC LEFT JOIN TICKCI ON TICKCI.TICKNO = TICKC.TICKNO AND TICKCI.DELFLAG = 0
              where TICKC.NAME_ IN ('Balance Completion','Balance Fulfillment') and TICKC.DELFLAG = 0 and LTRIM(RTRIM(TICKC.TOBILL)) = 'X') as ticket on ticket.BILLNO = ncpdp.ncpdp_invoice_number and ticket.MRN = ncpdp.patient_mrn";

        public static readonly string CPRSQL_TB_CUBE_NCPDP_PARTIALS_FILTER_RXNUMBER = @" ncpdp.ncpdp_rx_number = '{0}' ";

        public static readonly string CPRSQL_TB_CUBE_NCPDP_PARTIALS_ORDER = @" ncpdp_rx_number,ncpdp_invoice_number,ticket_confirmation_date ";

        public static readonly string CPRSQL_VW_CUBE_NCPDP_PARTIALS_SELECT = @"
               select LABLOGNO, ncpdp_date_filled_timestamp,ncpdp_rx_number, ncpdp_drug_name, ncpdp_ndc,ncpdp_quantity_new_refill_code, ncpdp_invoice_number,
                      ncpdp_primary_claim_sys_id, ncpdp_primary_claim_billed,ncpdp_primary_claim_expected, ncpdp_primary_claim_paid_response_sys_id,ncpdp_primary_claim_paid, ncpdp_primary_claim_payor,primary_payor_type,
                      ncpdp_secondary_claim_sys_id, ncpdp_secondary_claim_billed,ncpdp_secondary_claim_expected, ncpdp_secondary_claim_payor,ncpdp_secondary_claim_paid_response_sys_id, ncpdp_secondary_claim_paid,
                      ncpdp_patient_copay_expected_response_sys_id, ncpdp_patient_copay_expected,ncpdp_patient_secondary_copay_expected_response_sys_id,ncpdp_patient_secondary_copay_expected,
                      ncpdp_other_secondary_copay_expected_response_sys_id,ncpdp_other_secondary_copay_expected, ncpdp_patient_paid,ncpdp_claim_adjustment, ncpdp_ncpdp_cost, ncpdp_ticket_cost,ncpdp_ticket_cost as ticket_total_cost, patient_mrn
                      , ncpdp_invoice_expected, ncpdp_invoice_billed, ticket_confirmation_date,TICKNO, ticket_item_delivins, ticket_item_change, ticket_item_chgbyhost,ticket_item_delflag_overlay, Copay, TPRevenuePlusPatientCopay
                      , COGS, COGSAdjusted, GrossProfit = Round(TPRevenuePlusPatientCopay - COGS, 2),GrossProfitAdjusted = Round(TPRevenuePlusPatientCopay - COGSAdjusted, 2), ticket_quantity, PrimaryTPPaid_chgbyhost,SecondaryTPPaid_chgbyhost,Copay_chgbyhost, ticket_cost_chgbyhost, ticket_item_name,vatext
                      ,ROW_NUMBER() OVER(ORDER BY ncpdp_rx_number,ncpdp_quantity_new_refill_code,ncpdp_invoice_number,ticket_confirmation_date ASC) AS ROWNUM
                      ,PrimaryTPHistory,SecondaryTPHistory,CopayHistory,COGSHistory,RecordDeletedHistory
               FROM Pharmacy.vw_cube_ncpdp_partials";

        public static readonly string CPRSQL_VW_CUBE_NCPDP_PARTIALS_ORDER = @" ncpdp_rx_number,ncpdp_quantity_new_refill_code,ncpdp_invoice_number,ticket_confirmation_date ";

        public static readonly string CPRSQL_VW_CUBE_NCPDP_PARTIALS_FILTER_DISPENSE_DATERANGE = @" convert(date,ncpdp_date_filled_timestamp) >= '{0}' and convert(date,ncpdp_date_filled_timestamp) <= '{1}' ";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_FUNCTION_SELECT = @"
                select [ncpdp_date_filled_timestamp],[ncpdp_invoice_datebilled_timestamp],[ncpdp_invoice_date_of_service_timestamp],[ncpdp_rx_number],[ncpdp_rx_description],
	                   [ncpdp_days_supply],[ncpdp_drug_name],[ncpdp_ndc],[ncpdp_quantity_dispenses],[ncpdp_quantity_intended_to_dispense],[ncpdp_quantity_prescribed],
	                   [ncpdp_quantity_new_refill_code],[ncpdp_daw_yn],[ncpdp_daw_description],[ncpdp_count_of_refills],[ncpdp_uom],[ncpdp_date_written_timestamp],
	                   [ncpdp_days_intended],[ncpdp_compound_code],[ncpdp_compound_dosage_form],[ncpdp_compound_dosage_unit_form],[ncpdp_route_of_administration],
	                   [ncpdp_product_id_qualifier],[ncpdp_compound_type],[ncpdp_pharmacy_service_type],[ncpdp_eligible_clarification_code],[ncpdp_dispensing_status],
	                   [ncpdp_invoice_number],[ncpdp_claim_number],[ncpdp_primary_claim_billed],[ncpdp_primary_claim_expected],[ncpdp_primary_claim_paid],
	                   [ncpdp_primary_claim_payor],[ncpdp_secondary_claim_billed],[ncpdp_secondary_claim_expected],[ncpdp_secondary_claim_payor],
	                   [ncpdp_secondary_claim_paid],[ncpdp_patient_copay_expected],[ncpdp_patient_secondary_copay_expected],[ncpdp_other_secondary_copay_expected],
	                   [ncpdp_patient_paid],[ncpdp_claim_adjustment],[ncpdp_claim_balance],[ncpdp_ncpdp_cost],[ncpdp_ticket_cost],[ncpdp_claim_status],
	                   [patient_mrn],[primary_payor_binno],[primary_payor_pcn],[primary_payor_name],[primary_payor_type],[secondary_payor_binno],
	                   [secondary_payor_pcn],[secondary_payor_name],[secondary_payor_type],[ncpdp_invoice_expected],[ncpdp_invoice_billed],[ncpdp_sec_ncpdp_cost],[ncpdp_new_profit],[awp]
                  from [Pharmacy].[fn_cube_ncpdp](@LABLOGNO)";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_AUDIT_RXNO_FUNCTION_SELECT = @"
            	select  [LABLOGNO],[ncpdp_invoice_company_code] ,[ncpdp_invoice_company_name] ,[ncpdp_invoice_site_number] ,[ncpdp_invoice_site_name] ,[ncpdp_date_filled_timestamp] ,[ncpdp_invoice_datebilled_timestamp] 
                       ,[ncpdp_invoice_date_of_service_timestamp] ,[ncpdp_rx_number] ,[ncpdp_rx_description] ,[ncpdp_days_supply] ,[ncpdp_drug_name] ,[ncpdp_ndc] ,[ncpdp_quantity_dispenses] 
                       ,[ncpdp_quantity_intended_to_dispense] ,[ncpdp_quantity_prescribed] ,[ncpdp_quantity_new_refill_code] ,[ncpdp_daw_yn] ,[ncpdp_daw_description] ,[ncpdp_count_of_refills] ,[ncpdp_uom] 
                       ,[ncpdp_date_written_timestamp] ,[ncpdp_days_intended] ,[ncpdp_compound_code] ,[ncpdp_compound_dosage_form] ,[ncpdp_compound_dosage_unit_form] ,[ncpdp_route_of_administration] 
                       ,[ncpdp_product_id_qualifier] ,[ncpdp_compound_type] ,[ncpdp_pharmacy_service_type] ,[ncpdp_eligible_clarification_code] ,[ncpdp_dispensing_status] ,[ncpdp_authorization_id] 
                       ,[ncpdp_authorization_type_id] ,[ncpdp_pharmacist_initials] ,[ncpdp_prior_authorization_type_code] ,[ncpdp_pa_mc_code] ,[ncpdp_invoice_number] ,[ncpdp_claim_number] ,[ncpdp_primary_claim_sys_id], [ncpdp_primary_claim_billed] 
                       ,[ncpdp_primary_claim_expected] ,[ncpdp_primary_claim_paid_response_sys_id], [ncpdp_primary_claim_paid] ,[ncpdp_primary_claim_payor] ,[ncpdp_secondary_claim_sys_id], [ncpdp_secondary_claim_billed] ,[ncpdp_secondary_claim_expected] ,[ncpdp_secondary_claim_payor]
                       ,[ncpdp_secondary_claim_paid_response_sys_id], [ncpdp_secondary_claim_paid] ,[ncpdp_patient_copay_expected_response_sys_id], [ncpdp_patient_copay_expected] ,[ncpdp_patient_secondary_copay_expected_response_sys_id], [ncpdp_patient_secondary_copay_expected], [ncpdp_other_secondary_copay_expected_response_sys_id], [ncpdp_other_secondary_copay_expected] ,[ncpdp_patient_paid] ,[ncpdp_claim_adjustment] 
                       ,[ncpdp_claim_balance] ,[ncpdp_ncpdp_cost] ,[ncpdp_ticket_cost] ,[ncpdp_claim_status] ,[patient_mrn] ,[patient_full_name] ,[patient_first_name] ,[patient_last_name] 
                       ,[patient_date_of_birth_timestamp] ,[patient_gender] ,[patient_state] ,[patient_city] ,[patient_address] ,[patient_zip] ,[physician_full_name] ,[physician_last_name] ,[physician_first_name] 
                       ,[physician_npi_number] ,[physician_dea_number] ,[physician_license_number] ,[physician_address] ,[physician_city] ,[physician_state] ,[physician_zip_code] ,[primary_payor_binno] 
                       ,[primary_payor_pcn] ,[primary_payor_name] ,[primary_payor_type] ,[secondary_payor_binno] ,[secondary_payor_pcn] ,[secondary_payor_name] ,[secondary_payor_type] 
                       ,[ncpdp_primary_payor_relationship_code] ,[patient_primary_payor_policy_number] ,[patient_primary_payor_group_number] ,[ncpdp_invoice_expected] ,[ncpdp_invoice_billed] ,[ncpdp_sec_ncpdp_cost] 
                       ,[ncpdp_new_profit] ,[primary_ancillary_provider] ,[ncpdp_revenue_status] ,[invoice_therapy] ,[ncpdp_hcpcs_code] ,[patient_icd9_1] ,[patient_icd9_2] ,[patient_icd9_3] ,[patient_icd9_4] 
                       ,[patient_icd9_1_diagnosis] ,[patient_icd9_2_diagnosis] ,[patient_icd9_3_diagnosis] ,[patient_icd9_4_diagnosis] ,[patient_icd10_1] ,[patient_icd10_2] ,[patient_icd10_3] ,[patient_icd10_4] 
                       ,[patient_icd10_1_diagnosis] ,[patient_icd10_2_diagnosis] ,[patient_icd10_3_diagnosis] ,[patient_icd10_4_diagnosis] ,[invoice_icd9_1] ,[invoice_icd9_2] ,[invoice_icd9_3] ,[invoice_icd9_4] 
                       ,[invoice_icd9_1_diagnosis] ,[invoice_icd9_2_diagnosis] ,[invoice_icd9_3_diagnosis] ,[invoice_icd9_4_diagnosis] ,[invoice_icd10_1] ,[invoice_icd10_2] ,[invoice_icd10_3] ,[invoice_icd10_4] 
                       ,[invoice_icd10_1_diagnosis] ,[invoice_icd10_2_diagnosis] ,[invoice_icd10_3_diagnosis] ,[invoice_icd10_4_diagnosis] ,[ncpdp_invoice_company_sys_id] ,[pri_optional_organization] 
                       ,[sec_optional_organization] ,[awp] ,[patient_team] ,[patient_code_status] ,[patient_referral_organization] ,[ncpdp_rx_origin_description], [ticket_confirmation_date], [ticket_unit_cost], [inventory_item_name] 
                       ,[TPRevenuePlusPatientCopay], [Copay], COGS, [COGSAdjusted], GrossProfit = Round(TPRevenuePlusPatientCopay - COGS, 2), [TPRevenue], [TICKNO],ticket_quantity
                       ,[ticket_cost_original],[ticket_cost_overlay],[Copay_original],[Copay_overlay]
                       ,[revenue_total_expected_original],[revenue_total_expected_overlay],[primary_claim_billed_original],[primary_claim_billed_overlay]
                       ,[secondary_claim_billed_original],[secondary_claim_billed_overlay]
                       from [Pharmacy].[fn_cube_ncpdp_search_audit_rxno] (@RXNO)";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_FUNCTION_SELECT = @"
            	select  [LABLOGNO],[ncpdp_invoice_company_code] ,[ncpdp_invoice_company_name] ,[ncpdp_invoice_site_number] ,[ncpdp_invoice_site_name] ,[ncpdp_date_filled_timestamp] ,[ncpdp_invoice_datebilled_timestamp] 
                       ,[ncpdp_invoice_date_of_service_timestamp] ,[ncpdp_rx_number] ,[ncpdp_rx_description] ,[ncpdp_days_supply] ,[ncpdp_drug_name] ,[ncpdp_ndc] ,[ncpdp_quantity_dispenses] 
                       ,[ncpdp_quantity_intended_to_dispense] ,[ncpdp_quantity_prescribed] ,[ncpdp_quantity_new_refill_code] ,[ncpdp_daw_yn] ,[ncpdp_daw_description] ,[ncpdp_count_of_refills] ,[ncpdp_uom] 
                       ,[ncpdp_date_written_timestamp] ,[ncpdp_days_intended] ,[ncpdp_compound_code] ,[ncpdp_compound_dosage_form] ,[ncpdp_compound_dosage_unit_form] ,[ncpdp_route_of_administration] 
                       ,[ncpdp_product_id_qualifier] ,[ncpdp_compound_type] ,[ncpdp_pharmacy_service_type] ,[ncpdp_eligible_clarification_code] ,[ncpdp_dispensing_status] ,[ncpdp_authorization_id] 
                       ,[ncpdp_authorization_type_id] ,[ncpdp_pharmacist_initials] ,[ncpdp_prior_authorization_type_code] ,[ncpdp_pa_mc_code] ,[ncpdp_invoice_number] ,[ncpdp_claim_number] ,[ncpdp_primary_claim_sys_id], [ncpdp_primary_claim_billed] 
                       ,[ncpdp_primary_claim_expected] ,[ncpdp_primary_claim_paid_response_sys_id], [ncpdp_primary_claim_paid] ,[ncpdp_primary_claim_payor] ,[ncpdp_secondary_claim_sys_id], [ncpdp_secondary_claim_billed] ,[ncpdp_secondary_claim_expected] ,[ncpdp_secondary_claim_payor]
                       ,[ncpdp_secondary_claim_paid_response_sys_id], [ncpdp_secondary_claim_paid] ,[ncpdp_patient_copay_expected_response_sys_id], [ncpdp_patient_copay_expected] ,[ncpdp_patient_secondary_copay_expected_response_sys_id], [ncpdp_patient_secondary_copay_expected], [ncpdp_other_secondary_copay_expected_response_sys_id], [ncpdp_other_secondary_copay_expected] ,[ncpdp_patient_paid] ,[ncpdp_claim_adjustment] 
                       ,[ncpdp_claim_balance] ,[ncpdp_ncpdp_cost] ,[ncpdp_ticket_cost] ,[ncpdp_claim_status] ,[patient_mrn] ,[patient_full_name] ,[patient_first_name] ,[patient_last_name] 
                       ,[patient_date_of_birth_timestamp] ,[patient_gender] ,[patient_state] ,[patient_city] ,[patient_address] ,[patient_zip] ,[physician_full_name] ,[physician_last_name] ,[physician_first_name] 
                       ,[physician_npi_number] ,[physician_dea_number] ,[physician_license_number] ,[physician_address] ,[physician_city] ,[physician_state] ,[physician_zip_code] ,[primary_payor_binno] 
                       ,[primary_payor_pcn] ,[primary_payor_name] ,[primary_payor_type] ,[secondary_payor_binno] ,[secondary_payor_pcn] ,[secondary_payor_name] ,[secondary_payor_type] 
                       ,[ncpdp_primary_payor_relationship_code] ,[patient_primary_payor_policy_number] ,[patient_primary_payor_group_number] ,[ncpdp_invoice_expected] ,[ncpdp_invoice_billed] ,[ncpdp_sec_ncpdp_cost] 
                       ,[ncpdp_new_profit] ,[primary_ancillary_provider] ,[ncpdp_revenue_status] ,[invoice_therapy] ,[ncpdp_hcpcs_code] ,[patient_icd9_1] ,[patient_icd9_2] ,[patient_icd9_3] ,[patient_icd9_4] 
                       ,[patient_icd9_1_diagnosis] ,[patient_icd9_2_diagnosis] ,[patient_icd9_3_diagnosis] ,[patient_icd9_4_diagnosis] ,[patient_icd10_1] ,[patient_icd10_2] ,[patient_icd10_3] ,[patient_icd10_4] 
                       ,[patient_icd10_1_diagnosis] ,[patient_icd10_2_diagnosis] ,[patient_icd10_3_diagnosis] ,[patient_icd10_4_diagnosis] ,[invoice_icd9_1] ,[invoice_icd9_2] ,[invoice_icd9_3] ,[invoice_icd9_4] 
                       ,[invoice_icd9_1_diagnosis] ,[invoice_icd9_2_diagnosis] ,[invoice_icd9_3_diagnosis] ,[invoice_icd9_4_diagnosis] ,[invoice_icd10_1] ,[invoice_icd10_2] ,[invoice_icd10_3] ,[invoice_icd10_4] 
                       ,[invoice_icd10_1_diagnosis] ,[invoice_icd10_2_diagnosis] ,[invoice_icd10_3_diagnosis] ,[invoice_icd10_4_diagnosis] ,[ncpdp_invoice_company_sys_id] ,[pri_optional_organization] 
                       ,[sec_optional_organization] ,[awp] ,[patient_team] ,[patient_code_status] ,[patient_referral_organization] ,[ncpdp_rx_origin_description], [ticket_confirmation_date], [inventory_item_name] 
                       ,[TPRevenuePlusPatientCopay], [Copay], COGS, [COGSAdjusted], GrossProfit = Round(TPRevenuePlusPatientCopay - COGS, 2), [TPRevenue], [TICKNO],ticket_quantity
                       ,PrimaryTPPaid_chgbyhost, SecondaryTPPaid_chgbyhost, Copay_chgbyhost, ticket_cost_chgbyhost from [Pharmacy].[fn_cube_ncpdp_search_rxno] (@RXNO)";

        // Faster version of the query above, This contains only the columns that are needed
        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_FUNCTION_GRID = @"
               SELECT LABLOGNO, ncpdp_date_filled_timestamp, ncpdp_rx_number, ncpdp_drug_name, ncpdp_ndc, ncpdp_quantity_new_refill_code, ncpdp_invoice_number, ncpdp_primary_claim_sys_id, ncpdp_primary_claim_billed
               , ncpdp_primary_claim_expected, ncpdp_primary_claim_paid_response_sys_id, ncpdp_primary_claim_paid, ncpdp_primary_claim_payor,primary_payor_type, ncpdp_secondary_claim_sys_id, ncpdp_secondary_claim_billed,
               ncpdp_secondary_claim_expected, ncpdp_secondary_claim_payor,ncpdp_secondary_claim_paid_response_sys_id, ncpdp_secondary_claim_paid, ncpdp_patient_copay_expected_response_sys_id, ncpdp_patient_copay_expected
               , ncpdp_patient_secondary_copay_expected_response_sys_id, ncpdp_patient_secondary_copay_expected, ncpdp_other_secondary_copay_expected_response_sys_id, ncpdp_other_secondary_copay_expected,
               ncpdp_patient_paid, ncpdp_claim_adjustment, ncpdp_ncpdp_cost, ncpdp_ticket_cost, patient_mrn, ncpdp_invoice_expected, ncpdp_invoice_billed, ticket_confirmation_date, TICKNO, COGS as ticket_total_cost, ticket_item_delivins, ticket_item_change
               , ticket_item_chgbyhost, ticket_name AS ticket_item_name, ticket_each_cost, ticket_quantity, ticket_cpk_tickc, ticket_item_delflag_overlay, Copay, TPRevenuePlusPatientCopay, COGS, COGSAdjusted, GrossProfit = Round(TPRevenuePlusPatientCopay - COGSAdjusted, 2), ticket_quantity
               , PrimaryTPPaid_chgbyhost, SecondaryTPPaid_chgbyhost, Copay_chgbyhost, ticket_cost_chgbyhost, ticket_partials_name, ticket_partials_ticket_number,vatext
                FROM [Pharmacy].[fn_cube_ncpdp_search_rxno] (@RXNO)";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_ITEM_FUNCTION_GRID = @"
               SELECT LABLOGNO,ncpdp_date_filled_timestamp	ncpdp_rx_number,ncpdp_drug_name,ncpdp_ndc,ncpdp_quantity_new_refill_code,ncpdp_invoice_number,ncpdp_primary_claim_sys_id,ncpdp_primary_claim_billed
               ,ncpdp_primary_claim_expected,ncpdp_primary_claim_paid_response_sys_id,ncpdp_primary_claim_paid,ncpdp_primary_claim_payor,ncpdp_secondary_claim_sys_id,ncpdp_secondary_claim_billed
               ,ncpdp_secondary_claim_expected,ncpdp_secondary_claim_payor,ncpdp_secondary_claim_paid_response_sys_id,ncpdp_secondary_claim_paid,ncpdp_patient_copay_expected_response_sys_id
               ,ncpdp_patient_copay_expected,ncpdp_patient_secondary_copay_expected_response_sys_id,ncpdp_patient_secondary_copay_expected,ncpdp_other_secondary_copay_expected_response_sys_id
               ,ncpdp_other_secondary_copay_expected,ncpdp_patient_paid,ncpdp_claim_adjustment,ncpdp_ncpdp_cost,ncpdp_ticket_cost,patient_mrn,ncpdp_invoice_expected,ncpdp_invoice_billed,ticket_confirmation_date
               ,TICKNO,ticket_delivins,ticket_change,ticket_chgbyhost,ticket_item_name,ticket_item_each_cost,ticket_item_quantity,ticket_item_sys_id,ticket_delflag_overlay,ticket_item_delflag_overlay
               ,Copay,TPRevenuePlusPatientCopay,COGS,COGSAdjusted,GrossProfit = Round(TPRevenuePlusPatientCopay - COGS, 2),ticket_quantity,PrimaryTPPaid_chgbyhost,SecondaryTPPaid_chgbyhost,Copay_chgbyhost,ticket_cost_chgbyhost FROM [Pharmacy].[fn_cube_ncpdp_search_rxno_item](@RXNO)";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_ORDER = @" ncpdp_rx_number,ncpdp_quantity_new_refill_code,ncpdp_invoice_number,ticket_confirmation_date ";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_FILTER_DISPENSE_DATERANGE = @" convert(date,ncpdp_date_filled_timestamp) >= '{0}' and convert(date,ncpdp_date_filled_timestamp) <= '{1}' ";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_DATEFILLED_FUNCTION = @"
                  select LABLOGNO,ncpdp_date_filled_timestamp,ncpdp_rx_number,ncpdp_drug_name,ncpdp_ndc,ncpdp_quantity_new_refill_code,ncpdp_invoice_number,ncpdp_primary_claim_sys_id
                         ,ncpdp_primary_claim_billed,ncpdp_primary_claim_expected,ncpdp_primary_claim_paid_response_sys_id,ncpdp_primary_claim_paid,ncpdp_primary_claim_payor
                         ,ncpdp_secondary_claim_sys_id,ncpdp_secondary_claim_billed,ncpdp_secondary_claim_expected,ncpdp_secondary_claim_payor,ncpdp_secondary_claim_paid_response_sys_id
                         ,ncpdp_secondary_claim_paid,ncpdp_patient_copay_expected_response_sys_id,ncpdp_patient_copay_expected,ncpdp_patient_secondary_copay_expected_response_sys_id
                         ,ncpdp_patient_secondary_copay_expected,ncpdp_other_secondary_copay_expected_response_sys_id,ncpdp_other_secondary_copay_expected,ncpdp_patient_paid,ncpdp_claim_adjustment
                         ,ncpdp_ncpdp_cost,ncpdp_ticket_cost,patient_mrn,ncpdp_invoice_expected,ncpdp_invoice_billed,ticket_confirmation_date,TICKNO,Copay,TPRevenuePlusPatientCopay,COGS
                         ,COGSAdjusted,GrossProfit = Round(TPRevenuePlusPatientCopay - COGS, 2),ticket_quantity,PrimaryTPPaid_chgbyhost,SecondaryTPPaid_chgbyhost,Copay_chgbyhost,ticket_cost_chgbyhost from Pharmacy.fn_cube_ncpdp_search_datefilled('{0}','{1}')";

        public static readonly string CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_TABLE_SELECT = @"
        SELECT [ncpdp_invoice_site_name],[Date Billed],[Date of Service],[Inventory_unit],[Package_size]
               ,[Compound],[Patient First Name],[Patient Last Name],[Prescriber Last Name],[Prescriber First Name],[Referral Organization]
               ,[ncpdp_primary_inventory_item_id],[ncpdp_revenue_status],[ncpdp_claim_status_description],[ncpdp_days_supply],[Payor Bin],[refillnum]
               ,[inventory_item_category],[ncpdp_rx_origin_description],[rxorigin],[gname],[Therapy Type],[DAW],[Specialty],[NIOSH],[EPA],[DEA_SCHEDULE]
               ,[LABLOGNO],[ncpdp_date_filled_timestamp],[ncpdp_rx_number],[ncpdp_drug_name],[ncpdp_ndc],[ncpdp_quantity_new_refill_code],[ncpdp_invoice_number],[ncpdp_primary_claim_sys_id]
               ,[ncpdp_primary_claim_billed],[ncpdp_primary_claim_expected],[ncpdp_primary_claim_paid_response_sys_id],[ncpdp_primary_claim_paid],[ncpdp_primary_claim_payor],[primary_payor_type]
               ,[ncpdp_secondary_claim_sys_id],[ncpdp_secondary_claim_billed],[ncpdp_secondary_claim_expected],[ncpdp_secondary_claim_payor],[ncpdp_secondary_claim_paid_response_sys_id]
               ,[ncpdp_secondary_claim_paid],[ncpdp_patient_copay_expected_response_sys_id],[ncpdp_patient_copay_expected],[ncpdp_patient_secondary_copay_expected_response_sys_id]
               ,[ncpdp_patient_secondary_copay_expected],[ncpdp_other_secondary_copay_expected_response_sys_id],[ncpdp_other_secondary_copay_expected],[ncpdp_patient_paid]
               ,[ncpdp_claim_adjustment],[ncpdp_ncpdp_cost],[ncpdp_ticket_cost],[patient_mrn],[ncpdp_invoice_expected],[ncpdp_invoice_billed],[ticket_confirmation_date],[TICKNO]
               ,[Copay],[TPRevenuePlusPatientCopay],[COGS],[COGSAdjusted],[GrossProfit],[GrossProfitAdjusted],[ticket_quantity],[PrimaryTPPaid_chgbyhost],[SecondaryTPPaid_chgbyhost],[Copay_chgbyhost],[ticket_cost_chgbyhost]
               ,ticket_item_delivins,ticket_item_change,ticket_item_chgbyhost,ticket_item_delflag_overlay, ticket_partials_name,ticket_total_cost,vatext
               FROM [Pharmacy].[tb_cube_ncpdp_profitability] ";

        public static readonly string CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_VIEW_SELECT = @"
        SELECT [ncpdp_invoice_site_name],[Date Billed],[Date of Service],[Inventory_unit],[Package_size]
               ,[Compound],[Patient First Name],[Patient Last Name],[Prescriber Last Name],[Prescriber First Name],[Referral Organization]
               ,[ncpdp_primary_inventory_item_id],[ncpdp_revenue_status],[ncpdp_claim_status_description],[ncpdp_days_supply],[Payor Bin],[refillnum]
               ,[inventory_item_category],[ncpdp_rx_origin_description],[rxorigin],[gname],[Therapy Type],[DAW],[Specialty],[NIOSH],[EPA],[DEA_SCHEDULE]
               ,[LABLOGNO],[ncpdp_date_filled_timestamp],[ncpdp_rx_number],[ncpdp_drug_name],[ncpdp_ndc],[ncpdp_quantity_new_refill_code],[ncpdp_invoice_number],[ncpdp_primary_claim_sys_id]
               ,[ncpdp_primary_claim_billed],[ncpdp_primary_claim_expected],[ncpdp_primary_claim_paid_response_sys_id],[ncpdp_primary_claim_paid],[ncpdp_primary_claim_payor],[primary_payor_type]
               ,[ncpdp_secondary_claim_sys_id],[ncpdp_secondary_claim_billed],[ncpdp_secondary_claim_expected],[ncpdp_secondary_claim_payor],[ncpdp_secondary_claim_paid_response_sys_id]
               ,[ncpdp_secondary_claim_paid],[ncpdp_patient_copay_expected_response_sys_id],[ncpdp_patient_copay_expected],[ncpdp_patient_secondary_copay_expected_response_sys_id]
               ,[ncpdp_patient_secondary_copay_expected],[ncpdp_other_secondary_copay_expected_response_sys_id],[ncpdp_other_secondary_copay_expected],[ncpdp_patient_paid]
               ,[ncpdp_claim_adjustment],[ncpdp_ncpdp_cost],[ncpdp_ticket_cost],[patient_mrn],[ncpdp_invoice_expected],[ncpdp_invoice_billed],[ticket_confirmation_date],[TICKNO]
               ,[Copay],[TPRevenuePlusPatientCopay],[COGS],[COGSAdjusted],[GrossProfit],[GrossProfitAdjusted],[ticket_quantity],[PrimaryTPPaid_chgbyhost],[SecondaryTPPaid_chgbyhost],[Copay_chgbyhost],[ticket_cost_chgbyhost]
               ,ticket_item_delivins,ticket_item_change,ticket_item_chgbyhost,ticket_item_delflag_overlay, ticket_partials_name,ticket_total_cost,vatext
               ,ROW_NUMBER() OVER(ORDER BY ncpdp_rx_number,ncpdp_quantity_new_refill_code,ncpdp_invoice_number,ticket_confirmation_date ASC) AS ROWNUM
	           ,PrimaryTPHistory = replace(
                        replace(
				               (SELECT '[PrimaryTP' + convert(varchar,AuditID) + '] ' + convert(varchar(50),format(COALESCE(SHORT_VAL_ORIGINAL,0), '$#,##0.00'), 0) + ' to ' + convert(varchar(50),format(COALESCE(SHORT_VAL_OVERLAY,0), '$#,##0.00'), 0) + ' on ' + convert(varchar,TOUCHDATE,101) + ' by ' + Chgbyhost as Hist
                                FROM ISHUB.dbo.NCPDPREPS b 
                                WHERE BILLNO = ncpdp_invoice_number
								and FIELDNAME = 'F9P'
				                and DELFLAG = 0
	                            order by AuditID
                                FOR XML PATH('')
	                     ),'<Hist>','')
	                ,'</Hist>','::')
               , (select count(*) from ISHUB.dbo.NCPDPREPS where RXNO = ncpdp_rx_number and BILLNO = ncpdp_invoice_number and TICKNO = TICKNO and FIELDNAME='F9P') AS PrimaryTPUpdated
               ,SecondaryTPHistory = replace(
                        replace(
				               (SELECT '[SecondaryTP' + convert(varchar,AuditID) + '] ' + convert(varchar(50),format(COALESCE(SHORT_VAL_ORIGINAL,0), '$#,##0.00'), 0) + ' to ' + convert(varchar(50),format(COALESCE(SHORT_VAL_OVERLAY,0), '$#,##0.00'), 0) + ' on ' + convert(varchar,TOUCHDATE,101) + ' by ' + Chgbyhost as Hist
                                FROM ISHUB.dbo.NCPDPREPS b 
                                WHERE BILLNO = ncpdp_invoice_number
								and FIELDNAME = 'F9S'
				                and DELFLAG = 0
	                            order by AuditID
                                FOR XML PATH('')
	                     ),'<Hist>','')
	                ,'</Hist>','::')
               , (select count(*) from ISHUB.dbo.NCPDPREPS where RXNO = ncpdp_rx_number and BILLNO = ncpdp_invoice_number and TICKNO = TICKNO and FIELDNAME='F9S') AS SecondaryTPUpdated
               ,CopayHistory = replace(
                        replace(
				               (SELECT '[Copay' + convert(varchar,AuditID) + '] ' + convert(varchar(50),format(COALESCE(SHORT_VAL_ORIGINAL,0), '$#,##0.00'), 0) + ' to ' + convert(varchar(50),format(COALESCE(SHORT_VAL_OVERLAY,0), '$#,##0.00'), 0) + ' on ' + convert(varchar,TOUCHDATE,101) + ' by ' + Chgbyhost as Hist
                                FROM ISHUB.dbo.NCPDPREPS b 
                                WHERE BILLNO = ncpdp_invoice_number
								and FIELDNAME = 'F5'
				                and DELFLAG = 0
	                            order by AuditID
                                FOR XML PATH('')
	                     ),'<Hist>','')
	                ,'</Hist>','::')
               , (select count(*) from ISHUB.dbo.NCPDPREPS where RXNO = ncpdp_rx_number and BILLNO = ncpdp_invoice_number and TICKNO = TICKNO and FIELDNAME='F5') AS CopayUpdated
               ,COGSHistory = replace(
                        replace(
				               (SELECT '[COGS' + convert(varchar,AuditID) + '] ' + convert(varchar(50),format(COALESCE(COST,0), '$#,##0.00'), 0) + ' to ' + convert(varchar(50),format(COALESCE(COST_OVERLAY,0), '$#,##0.00')) + ' on ' + convert(varchar,TOUCHDATE,101) + ' by ' + Chgbyhost as Hist
                                FROM [ISHUB].[dbo].[TICKC] b 
                                WHERE BILLNO = ncpdp_invoice_number
				                and DELFLAG = 0
	                            order by AuditID
                                FOR XML PATH('')
	                     ),'<Hist>','')
	                ,'</Hist>','::')
               , (select count(*) from ISHUB.dbo.TICKC where RXNO = ncpdp_rx_number and BILLNO = ncpdp_invoice_number and TICKNO = TICKNO) AS COGSUpdated
               ,RecordDeletedHistory = replace(
                        replace(
				               (SELECT '[TicketDelete' +  convert(varchar,AuditID) + '] ' + convert(varchar(50),DELFLAG) + ' to ' + convert(varchar(50),DELFLAG_OVERLAY) + ' on ' + convert(varchar,TOUCHDATE,101) + ' by ' + Chgbyhost as Hist
                                FROM ISHUB.dbo.TICKCI b 
                                WHERE BILLNO = ncpdp_invoice_number
				                and DELFLAG = 0
	                            order by AuditID
                                FOR XML PATH('')
	                     ),'<Hist>','')
	                ,'</Hist>','::')
	           , (select count(*) from ISHUB.dbo.TICKCI where RXNUM = ncpdp_rx_number and BILLNO = ncpdp_invoice_number and TICKNO = TICKNO) AS RecordDeleted
               FROM CPRSQL.Pharmacy.tb_cube_ncpdp_profitability ";

        public static readonly string CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_FILTER_CONF_DATE = @" ticket_confirmation_date >= '{0}' and ticket_confirmation_date <= '{1}'";
        public static readonly string CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_RX_NUMBER_IN_FILTER = @" ncpdp_rx_number in ({0}) ";
        public static readonly string CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_RX_NUMBER_FILTER = @" ncpdp_rx_number = '{0}' ";
        public static readonly string CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_INVOICE_NUMBER_IN_FILTER = @" ncpdp_invoice_number in ({0}) ";
        public static readonly string CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_INVOICE_NUMBER_FILTER = @" ncpdp_invoice_number = '{0}' ";

        public static readonly string CPRSQL_VW_TICKET_ITEM_VIEW_SELECT = @"
             SELECT ticket_item_dispense_sys_id AS dispense_sys_id,ticket_item_rx_sys_id AS rx_sys_id,ticket_item_invoice_sys_id AS ncpdp_invoice_number,ticket_item_ticket_number AS TICKNO,ticket_item_delflag_overlay,ticket_item_quantity,ticket_item_each_cost AS ticket_each_cost,ticket_item_extended_cost AS ticket_total_cost,ticket_item_chgbyhost
             FROM  Pharmacy.vw_ticket_item where ticket_item_invoice_sys_id = @INVOICENO and ticket_item_ticket_number = @TICKNO and ticket_item_billable_yn='Y' ";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_INVOICENO_FUNCTION_SELECT = @"
                select  [LABLOGNO],[ncpdp_invoice_company_code] ,[ncpdp_invoice_company_name] ,[ncpdp_invoice_site_number] ,[ncpdp_invoice_site_name] ,[ncpdp_date_filled_timestamp] ,[ncpdp_invoice_datebilled_timestamp] 
                       ,[ncpdp_invoice_date_of_service_timestamp] ,[ncpdp_rx_number] ,[ncpdp_rx_description] ,[ncpdp_days_supply] ,[ncpdp_drug_name] ,[ncpdp_ndc] ,[ncpdp_quantity_dispenses] 
                       ,[ncpdp_quantity_intended_to_dispense] ,[ncpdp_quantity_prescribed] ,[ncpdp_quantity_new_refill_code] ,[ncpdp_daw_yn] ,[ncpdp_daw_description] ,[ncpdp_count_of_refills] ,[ncpdp_uom] 
                       ,[ncpdp_date_written_timestamp] ,[ncpdp_days_intended] ,[ncpdp_compound_code] ,[ncpdp_compound_dosage_form] ,[ncpdp_compound_dosage_unit_form] ,[ncpdp_route_of_administration] 
                       ,[ncpdp_product_id_qualifier] ,[ncpdp_compound_type] ,[ncpdp_pharmacy_service_type] ,[ncpdp_eligible_clarification_code] ,[ncpdp_dispensing_status] ,[ncpdp_authorization_id] 
                       ,[ncpdp_authorization_type_id] ,[ncpdp_pharmacist_initials] ,[ncpdp_prior_authorization_type_code] ,[ncpdp_pa_mc_code] ,[ncpdp_invoice_number] ,[ncpdp_claim_number] ,[ncpdp_primary_claim_billed] 
                       ,[ncpdp_primary_claim_expected] ,[ncpdp_primary_claim_paid] ,[ncpdp_primary_claim_payor] ,[ncpdp_secondary_claim_billed] ,[ncpdp_secondary_claim_expected] ,[ncpdp_secondary_claim_payor]
                       ,[ncpdp_secondary_claim_paid] ,[ncpdp_patient_copay_expected] ,[ncpdp_patient_secondary_copay_expected] ,[ncpdp_other_secondary_copay_expected] ,[ncpdp_patient_paid] ,[ncpdp_claim_adjustment] 
                       ,[ncpdp_claim_balance] ,[ncpdp_ncpdp_cost] ,[ncpdp_ticket_cost] ,[ncpdp_claim_status] ,[patient_mrn] ,[patient_full_name] ,[patient_first_name] ,[patient_last_name] 
                       ,[patient_date_of_birth_timestamp] ,[patient_gender] ,[patient_state] ,[patient_city] ,[patient_address] ,[patient_zip] ,[physician_full_name] ,[physician_last_name] ,[physician_first_name] 
                       ,[physician_npi_number] ,[physician_dea_number] ,[physician_license_number] ,[physician_address] ,[physician_city] ,[physician_state] ,[physician_zip_code] ,[primary_payor_binno] 
                       ,[primary_payor_pcn] ,[primary_payor_name] ,[primary_payor_type] ,[secondary_payor_binno] ,[secondary_payor_pcn] ,[secondary_payor_name] ,[secondary_payor_type] 
                       ,[ncpdp_primary_payor_relationship_code] ,[patient_primary_payor_policy_number] ,[patient_primary_payor_group_number] ,[ncpdp_invoice_expected] ,[ncpdp_invoice_billed] ,[ncpdp_sec_ncpdp_cost] 
                       ,[ncpdp_new_profit] ,[primary_ancillary_provider] ,[ncpdp_revenue_status] ,[invoice_therapy] ,[ncpdp_hcpcs_code] ,[patient_icd9_1] ,[patient_icd9_2] ,[patient_icd9_3] ,[patient_icd9_4] 
                       ,[patient_icd9_1_diagnosis] ,[patient_icd9_2_diagnosis] ,[patient_icd9_3_diagnosis] ,[patient_icd9_4_diagnosis] ,[patient_icd10_1] ,[patient_icd10_2] ,[patient_icd10_3] ,[patient_icd10_4] 
                       ,[patient_icd10_1_diagnosis] ,[patient_icd10_2_diagnosis] ,[patient_icd10_3_diagnosis] ,[patient_icd10_4_diagnosis] ,[invoice_icd9_1] ,[invoice_icd9_2] ,[invoice_icd9_3] ,[invoice_icd9_4] 
                       ,[invoice_icd9_1_diagnosis] ,[invoice_icd9_2_diagnosis] ,[invoice_icd9_3_diagnosis] ,[invoice_icd9_4_diagnosis] ,[invoice_icd10_1] ,[invoice_icd10_2] ,[invoice_icd10_3] ,[invoice_icd10_4] 
                       ,[invoice_icd10_1_diagnosis] ,[invoice_icd10_2_diagnosis] ,[invoice_icd10_3_diagnosis] ,[invoice_icd10_4_diagnosis] ,[ncpdp_invoice_company_sys_id] ,[pri_optional_organization] 
                       ,[sec_optional_organization] ,[awp] ,[patient_team] ,[patient_code_status] ,[patient_referral_organization] ,[ncpdp_rx_origin_description], [ticket_confirmation_date], [inventory_item_name] 
                       from [Pharmacy].[fn_cube_ncpdp_search_invoiceno] (@INVOICENO)";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_TICKNO_FUNCTION_SELECT = @"
            	select  [LABLOGNO],[ncpdp_invoice_company_code] ,[ncpdp_invoice_company_name] ,[ncpdp_invoice_site_number] ,[ncpdp_invoice_site_name] ,[ncpdp_date_filled_timestamp] ,[ncpdp_invoice_datebilled_timestamp] 
                       ,[ncpdp_invoice_date_of_service_timestamp] ,[ncpdp_rx_number] ,[ncpdp_rx_description] ,[ncpdp_days_supply] ,[ncpdp_drug_name] ,[ncpdp_ndc] ,[ncpdp_quantity_dispenses] 
                       ,[ncpdp_quantity_intended_to_dispense] ,[ncpdp_quantity_prescribed] ,[ncpdp_quantity_new_refill_code] ,[ncpdp_daw_yn] ,[ncpdp_daw_description] ,[ncpdp_count_of_refills] ,[ncpdp_uom] 
                       ,[ncpdp_date_written_timestamp] ,[ncpdp_days_intended] ,[ncpdp_compound_code] ,[ncpdp_compound_dosage_form] ,[ncpdp_compound_dosage_unit_form] ,[ncpdp_route_of_administration] 
                       ,[ncpdp_product_id_qualifier] ,[ncpdp_compound_type] ,[ncpdp_pharmacy_service_type] ,[ncpdp_eligible_clarification_code] ,[ncpdp_dispensing_status] ,[ncpdp_authorization_id] 
                       ,[ncpdp_authorization_type_id] ,[ncpdp_pharmacist_initials] ,[ncpdp_prior_authorization_type_code] ,[ncpdp_pa_mc_code] ,[ncpdp_invoice_number] ,[ncpdp_claim_number] ,[ncpdp_primary_claim_billed] 
                       ,[ncpdp_primary_claim_expected] ,[ncpdp_primary_claim_paid] ,[ncpdp_primary_claim_payor] ,[ncpdp_secondary_claim_billed] ,[ncpdp_secondary_claim_expected] ,[ncpdp_secondary_claim_payor]
                       ,[ncpdp_secondary_claim_paid] ,[ncpdp_patient_copay_expected] ,[ncpdp_patient_secondary_copay_expected] ,[ncpdp_other_secondary_copay_expected] ,[ncpdp_patient_paid] ,[ncpdp_claim_adjustment] 
                       ,[ncpdp_claim_balance] ,[ncpdp_ncpdp_cost] ,[ncpdp_ticket_cost] ,[ncpdp_claim_status] ,[patient_mrn] ,[patient_full_name] ,[patient_first_name] ,[patient_last_name] 
                       ,[patient_date_of_birth_timestamp] ,[patient_gender] ,[patient_state] ,[patient_city] ,[patient_address] ,[patient_zip] ,[physician_full_name] ,[physician_last_name] ,[physician_first_name] 
                       ,[physician_npi_number] ,[physician_dea_number] ,[physician_license_number] ,[physician_address] ,[physician_city] ,[physician_state] ,[physician_zip_code] ,[primary_payor_binno] 
                       ,[primary_payor_pcn] ,[primary_payor_name] ,[primary_payor_type] ,[secondary_payor_binno] ,[secondary_payor_pcn] ,[secondary_payor_name] ,[secondary_payor_type] 
                       ,[ncpdp_primary_payor_relationship_code] ,[patient_primary_payor_policy_number] ,[patient_primary_payor_group_number] ,[ncpdp_invoice_expected] ,[ncpdp_invoice_billed] ,[ncpdp_sec_ncpdp_cost] 
                       ,[ncpdp_new_profit] ,[primary_ancillary_provider] ,[ncpdp_revenue_status] ,[invoice_therapy] ,[ncpdp_hcpcs_code] ,[patient_icd9_1] ,[patient_icd9_2] ,[patient_icd9_3] ,[patient_icd9_4] 
                       ,[patient_icd9_1_diagnosis] ,[patient_icd9_2_diagnosis] ,[patient_icd9_3_diagnosis] ,[patient_icd9_4_diagnosis] ,[patient_icd10_1] ,[patient_icd10_2] ,[patient_icd10_3] ,[patient_icd10_4] 
                       ,[patient_icd10_1_diagnosis] ,[patient_icd10_2_diagnosis] ,[patient_icd10_3_diagnosis] ,[patient_icd10_4_diagnosis] ,[invoice_icd9_1] ,[invoice_icd9_2] ,[invoice_icd9_3] ,[invoice_icd9_4] 
                       ,[invoice_icd9_1_diagnosis] ,[invoice_icd9_2_diagnosis] ,[invoice_icd9_3_diagnosis] ,[invoice_icd9_4_diagnosis] ,[invoice_icd10_1] ,[invoice_icd10_2] ,[invoice_icd10_3] ,[invoice_icd10_4] 
                       ,[invoice_icd10_1_diagnosis] ,[invoice_icd10_2_diagnosis] ,[invoice_icd10_3_diagnosis] ,[invoice_icd10_4_diagnosis] ,[ncpdp_invoice_company_sys_id] ,[pri_optional_organization] 
                       ,[sec_optional_organization] ,[awp] ,[patient_team] ,[patient_code_status] ,[patient_referral_organization] ,[ncpdp_rx_origin_description], [ticket_confirmation_date], [inventory_item_name] 
                       from [Pharmacy].[fn_cube_ncpdp_search_tickno] (@TICKNO)";

        public static readonly string CPRSQL_FN_CUBE_NCPDP_SEARCH_MRN_FUNCTION_SELECT = @"
            	select  [LABLOGNO],[ncpdp_invoice_company_code] ,[ncpdp_invoice_company_name] ,[ncpdp_invoice_site_number] ,[ncpdp_invoice_site_name] ,[ncpdp_date_filled_timestamp] ,[ncpdp_invoice_datebilled_timestamp] 
                       ,[ncpdp_invoice_date_of_service_timestamp] ,[ncpdp_rx_number] ,[ncpdp_rx_description] ,[ncpdp_days_supply] ,[ncpdp_drug_name] ,[ncpdp_ndc] ,[ncpdp_quantity_dispenses] 
                       ,[ncpdp_quantity_intended_to_dispense] ,[ncpdp_quantity_prescribed] ,[ncpdp_quantity_new_refill_code] ,[ncpdp_daw_yn] ,[ncpdp_daw_description] ,[ncpdp_count_of_refills] ,[ncpdp_uom] 
                       ,[ncpdp_date_written_timestamp] ,[ncpdp_days_intended] ,[ncpdp_compound_code] ,[ncpdp_compound_dosage_form] ,[ncpdp_compound_dosage_unit_form] ,[ncpdp_route_of_administration] 
                       ,[ncpdp_product_id_qualifier] ,[ncpdp_compound_type] ,[ncpdp_pharmacy_service_type] ,[ncpdp_eligible_clarification_code] ,[ncpdp_dispensing_status] ,[ncpdp_authorization_id] 
                       ,[ncpdp_authorization_type_id] ,[ncpdp_pharmacist_initials] ,[ncpdp_prior_authorization_type_code] ,[ncpdp_pa_mc_code] ,[ncpdp_invoice_number] ,[ncpdp_claim_number] ,[ncpdp_primary_claim_billed] 
                       ,[ncpdp_primary_claim_expected] ,[ncpdp_primary_claim_paid] ,[ncpdp_primary_claim_payor] ,[ncpdp_secondary_claim_billed] ,[ncpdp_secondary_claim_expected] ,[ncpdp_secondary_claim_payor]
                       ,[ncpdp_secondary_claim_paid] ,[ncpdp_patient_copay_expected] ,[ncpdp_patient_secondary_copay_expected] ,[ncpdp_other_secondary_copay_expected] ,[ncpdp_patient_paid] ,[ncpdp_claim_adjustment] 
                       ,[ncpdp_claim_balance] ,[ncpdp_ncpdp_cost] ,[ncpdp_ticket_cost] ,[ncpdp_claim_status] ,[patient_mrn] ,[patient_full_name] ,[patient_first_name] ,[patient_last_name] 
                       ,[patient_date_of_birth_timestamp] ,[patient_gender] ,[patient_state] ,[patient_city] ,[patient_address] ,[patient_zip] ,[physician_full_name] ,[physician_last_name] ,[physician_first_name] 
                       ,[physician_npi_number] ,[physician_dea_number] ,[physician_license_number] ,[physician_address] ,[physician_city] ,[physician_state] ,[physician_zip_code] ,[primary_payor_binno] 
                       ,[primary_payor_pcn] ,[primary_payor_name] ,[primary_payor_type] ,[secondary_payor_binno] ,[secondary_payor_pcn] ,[secondary_payor_name]  
                       ,[ncpdp_primary_payor_relationship_code] ,[patient_primary_payor_policy_number] ,[patient_primary_payor_group_number] ,[ncpdp_invoice_expected] ,[ncpdp_invoice_billed] ,[ncpdp_sec_ncpdp_cost] 
                       ,[ncpdp_new_profit] ,[primary_ancillary_provider] ,[ncpdp_revenue_status] ,[invoice_therapy] ,[ncpdp_hcpcs_code] ,[patient_icd9_1] ,[patient_icd9_2] ,[patient_icd9_3] ,[patient_icd9_4] 
                       ,[patient_icd9_1_diagnosis] ,[patient_icd9_2_diagnosis] ,[patient_icd9_3_diagnosis] ,[patient_icd9_4_diagnosis] ,[patient_icd10_1] ,[patient_icd10_2] ,[patient_icd10_3] ,[patient_icd10_4] 
                       ,[patient_icd10_1_diagnosis] ,[patient_icd10_2_diagnosis] ,[patient_icd10_3_diagnosis] ,[patient_icd10_4_diagnosis] ,[invoice_icd9_1] ,[invoice_icd9_2] ,[invoice_icd9_3] ,[invoice_icd9_4] 
                       ,[invoice_icd9_1_diagnosis] ,[invoice_icd9_2_diagnosis] ,[invoice_icd9_3_diagnosis] ,[invoice_icd9_4_diagnosis] ,[invoice_icd10_1] ,[invoice_icd10_2] ,[invoice_icd10_3] ,[invoice_icd10_4] 
                       ,[invoice_icd10_1_diagnosis] ,[invoice_icd10_2_diagnosis] ,[invoice_icd10_3_diagnosis] ,[invoice_icd10_4_diagnosis] ,[ncpdp_invoice_company_sys_id] ,[pri_optional_organization] 
                       ,[sec_optional_organization] ,[awp] ,[patient_team] ,[patient_code_status] ,[patient_referral_organization] ,[ncpdp_rx_origin_description], [ticket_confirmation_date], [inventory_item_name] 
                       from [Pharmacy].[fn_cube_ncpdp_search_mrn] (@MRN)";

        public static string CPRSQLProfitabilityReportDuplicateQuery(string SearchType, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            string Query;

            //Query = SQLUtility.CPRSQL_TB_CUBE_NCPDP_PARTIALS_SELECT + string.Format(SQLUtility.CPRSQL_TB_CUBE_NCPDP_PARTIALS_FILTER_RXNUMBER, "593769") + SQLUtility.ORDER_BY + SQLUtility.CPRSQL_TB_CUBE_NCPDP_PARTIALS_ORDER;
            //Query = SQLUtility.CPRSQL_TB_CUBE_NCPDP_PARTIALS_SELECT + SQLUtility.ORDER_BY + SQLUtility.CPRSQL_TB_CUBE_NCPDP_PARTIALS_ORDER;
            Query = SQLUtility.CPRSQL_VW_CUBE_NCPDP_PARTIALS_SELECT;

            if (SelectedDateSearch.ToUpper() != Constants.NONE)
            {
                Query += WHERE + string.Format(CPRSQL_VW_CUBE_NCPDP_PARTIALS_FILTER_DISPENSE_DATERANGE, SearchStartDate.ToShortDateString(), SearchEndDate.ToShortDateString());
            }

            Query += ORDER_BY + CPRSQL_VW_CUBE_NCPDP_PARTIALS_ORDER;
            System.Diagnostics.Debug.WriteLine(Query);
            return Query;
        }

        public static string CPRSQLProfitabilityReportSearchByRXNOQuery(string RXNO, string SearchType, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            string Query;

            //Query = SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_AUDIT_RXNO_FUNCTION_SELECT;
            //Query = SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_FUNCTION_SELECT;
            Query = SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_FUNCTION_GRID;

            if (SelectedDateSearch.ToUpper() != Constants.NONE)
            {
                Query += WHERE + string.Format(CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_FILTER_DISPENSE_DATERANGE, SearchStartDate.ToShortDateString(), SearchEndDate.ToShortDateString());
            }

            Query += ORDER_BY + CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_ORDER;
            System.Diagnostics.Debug.WriteLine(Query.Replace("@RXNO", RXNO));
            return Query;
        }

        public static string GetProfitabilityQuery(string SearchType, string SearchMode, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            string Query = string.Empty;
            //Query = CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_TABLE_SELECT + WHERE;
            Query = CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_VIEW_SELECT + WHERE;

            if (!string.IsNullOrEmpty(SearchValues))
            {
                if (IsCSVNumbers)
                {
                    if (SearchMode == "INVOICENO")
                    {
                        Query += string.Format(CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_INVOICE_NUMBER_IN_FILTER, DataUtility.INFilterVarchar(SearchValues));
                    }
                    else
                    {
                        Query += string.Format(CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_RX_NUMBER_IN_FILTER, DataUtility.INFilterVarchar(SearchValues));
                    }
                }
                else
                {
                    if (SearchMode == "INVOICENO")
                    {
                        Query += string.Format(CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_INVOICE_NUMBER_FILTER, SearchValues);
                    }
                    else
                    {
                        Query += string.Format(CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_RX_NUMBER_FILTER, SearchValues);
                    }
                }
            }

            if (SelectedDateSearch.ToUpper() != Constants.NONE)
            {
                Query += string.Format(CPRSQL_TB_CUBE_NDPDP_PROFITABILITY_FILTER_CONF_DATE, SearchStartDate.ToShortDateString(), SearchEndDate.ToShortDateString());
            }

            Query += SQLUtility.ORDER_BY + SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_RXNO_ORDER;
            System.Diagnostics.Debug.WriteLine(Query);
            return Query;
        }

        #endregion

        #region CPRSQL Patient Export
        public static readonly string CPRSQL_Patient_Export_Update = @"
UPDATE dbo.HR
SET istherigypatient = 1
WHERE MRN IN ({0})
";

        public static readonly string CRPSQL_Patient_Export = @"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED

;WITH MYNCPDP AS (
                  SELECT CPK_NCPDP, BILLNO, MRN, COPAY,DIAGNOSIS, ROW_NUMBER() OVER (PARTITION BY BILLNO, MRN ORDER BY CPK_NCPDP DESC) AS ROWNUM
                  FROM NCPDP 
                  WHERE NCPDP.DELFLAG = 0 AND NCPDP.CLAIMSTAT NOT IN ('T', ''))

,EMAILKEYS AS (
                SELECT LABELS.CPK_LABELS, CONVERT(INTEGER, LABELS.LINK) AS CFK_HR, LABELS.ORDERNO AS CFK_OT, LABELS.REFILLNUM AS VALUE
                FROM LABELS
                          INNER JOIN OT ON OT.DELFLAG = 0 AND OT.LISTID = 'SRX-CONTREFILLS' AND OT.NO = LABELS.ORDERNO AND OT.DRUG = 1 AND OT.THERAPY = 1 AND OT.SPECIALTY = 1
                WHERE LABELS.DELFLAG = 0 AND OT.STATUS != 'DC''D')
, TEMPTBL AS (
			 SELECT 
                OT.MRN AS 'external_id',
				HR.FIRST_NAME AS 'FIRST_NAME',
				HR.LAST_NAME AS 'LAST_NAME', 
				CASE WHEN HR.SEX='F' THEN 'Female' 
				     WHEN HR.SEX='M' THEN 'Male' END AS 'gender',
                convert(varchar(10), cast(HR.DOB AS date), 101) AS 'birth_date',
				HR.HEIGHT AS 'height',
				HR.WEIGHT AS 'weight',
				/*RIGHT(HR.SSN,4) AS 'ssn_last_four',*/
				'' AS ssn_last_four,
				CASE
					WHEN HR.EMAIL NOT LIKE '%@%' THEN ''
					ELSE HR.EMAIL
				END AS 'email',
                REPLACE(HR.PHONE,'-','') AS 'home_phone',
				REPLACE(HR.CELLPHONE,'-','') AS 'mobile_phone',
				REPLACE(HR.ADDRESS,',','||') AS 'address_line_1',
				HR.ADDRESS2 AS 'address_line_2',
				HR.CITY AS 'city',
				HR.STATE AS 'state',
				HR.ZIP AS 'zip',
				'USA' AS 'country',
				REPLACE(HR.ALLERGIES,',','||') AS 'allergy',
				REPLACE(HR.OTHALLERGY,',','||') AS 'other_allergy',
				'Alphascript' AS 'pharmacy',
				'' AS 'medical_plan',
				'' AS 'remote_medical_plan_id',
				'' AS 'pbm',
				'' AS 'remote_pbm_id',
				'' AS 'pcc_identifier',
				'' AS 'ec_first_name',
				'' AS 'ec_last_name',
				'' AS 'ec_telephone',
				'' AS 'ec_relationship',
				'Oncology' AS 'therapeutic_category',															
				'' AS 'diagnosis 2',
				SUBSTRING(OT.DESCRIP ,1,(CHARINDEX(' ',OT.DESCRIP  + ' ')-1)) AS 'medications', 
                LABELS.SCRIPTEXT AS 'rx_numbers',	  								   
				'' AS 'rx_number_start',
				convert(varchar(10), cast(labels.nextcomp AS date), 101) AS 'due_date'

FROM OT WITH (NOLOCK) 
                INNER JOIN HR WITH (NOLOCK) ON HR.DELFLAG = 0 AND HR.MRN = OT.MRN AND HR.PAT_STAT != 'INACTIVE'
				  {0}
																						  
                LEFT JOIN LABELS WITH (NOLOCK) ON LABELS.DELFLAG = 0 AND LABELS.ORDERNO = OT.NO AND LABELS.DISCYN <> '*'
                LEFT JOIN PTSHIP WITH (NOLOCK) ON PTSHIP.DELFLAG = 0 AND PTSHIP.MRN = OT.MRN
                LEFT JOIN INSCOMP WITH (NOLOCK) ON INSCOMP.DELFLAG = 0 AND INSCOMP.NO = OT.INSNO
                LEFT JOIN MyNCPDP NCPDP ON NCPDP.BILLNO = OT.BILLNO AND NCPDP.MRN = OT.MRN AND NCPDP.ROWNUM = 1
                LEFT JOIN INSVERI ON INSVERI.DELFLAG = 0 AND Ot.Cfk_Insveri = Insveri.refnum
                LEFT JOIN POPUPDATA POP_ENTSTATUS ON POP_ENTSTATUS.DELFLAG = 0 AND POP_ENTSTATUS.CPK_POPUPDATA = OT.CFK_POPUPDATA_ENTSTATUS
                LEFT JOIN PNNAMES P_ENTSTATUS ON P_ENTSTATUS.DELFLAG = 0 AND P_ENTSTATUS.NO = OT.CFK_PNNAMES_ENTSTATUS
                LEFT JOIN POPUPDATA POP_LASTEVENT ON POP_LASTEVENT.DELFLAG = 0 AND POP_LASTEVENT.CPK_POPUPDATA = OT.CFK_POPUPDATA_ENTSTATUS_LASTEVENT
                left join WEB_RX_REFILLS on rtrim(WEB_RX_REFILLS.SCRIPTEXT) = rtrim(LABELS.SCRIPTEXT) and WEB_RX_REFILLS.DELFLAG=0 
                     and WEB_RX_REFILLS.FILLED=0 and WEB_RX_REFILLS.REJECTED=0 and WEB_RX_REFILLS.IMPORTED=1
                Left Join (Select Count(*) As InboundEmailCount, PatientEmailLog.Cfk_hr
				           From PatientEmailLog 
						   Where PatientEmailLog.Delflag = 0 And PatientEmailLog.Outbound = 0  And PatientEmailLog.Processed = 0
						   Group By PatientEmailLog.Cfk_Hr) PatientEmailLogInbound On PatientEmailLogInbound.Cfk_Hr = Convert(Integer, Ot.Mrn)
                Left Join (Select Count(*) As OutboundEmailCount, PatientEmailLog.Cfk_Hr, PatientEmailLog.Cfk_Ot, PatientEmailLog.Cfk_Labels
                           From PatientEmailLog
                           Inner Join EmailKeys On EmailKeys.Cfk_Hr = PatientEmailLog.Cfk_Hr And EmailKeys.Cfk_Ot = PatientEmailLog.Cfk_Ot And EmailKeys.Cpk_Labels = PatientEmailLog.Cfk_Labels And EmailKeys.Value = PatientEmailLog.ControlValue
                           Where PatientEmailLog.Delflag = 0 And PatientEmailLog.Sent = 1
                           Group By PatientEmailLog.Cfk_Hr, PatientEmailLog.Cfk_Ot, PatientEmailLog.Cfk_Labels) PatientEmailLogOutbound 
						      On PatientEmailLogOutbound.Cfk_Hr = Convert(Integer, Ot.Mrn) And PatientEmailLogOutbound.Cfk_Ot = Ot.No  
							  And PatientEmailLogOutbound.Cfk_Labels = Labels.Cpk_Labels
                              AND PATIENTEMAILLOGOUTBOUND.CFK_OT = OT.NO
                              AND PATIENTEMAILLOGOUTBOUND.CFK_LABELS = LABELS.CPK_LABELS
                left join tickinfo on tickinfo.delflag = 0 and tickinfo.mrn = ot.mrn and tickinfo.no = ot.tickinfono
                left join (
                           select distinct x.ZIPCODE, x.TIMEZONE
                           from zipcodes x
                          ) z on z.zipcode = left(coalesce(NULLIF(tickinfo.sh_zip,''),hr.zip), 5)

				Left Join IcdOrder IcdOrder On IcdOrder.CFK_OT = OT.NO And IcdOrder.Delflag = 0 And IcdOrder.RANK = 1
		        Left Join IcdMasterList Icd10 On Icd10.Cpk_IcdMasterList = IcdOrder.Cfk_IcdMasterList_Icd10 And Icd10.Cfk_IcdType = 0
		        Left Join IcdMasterList Icd9 On Icd9.Cpk_IcdMasterList = IcdOrder.Cfk_IcdMasterList_Icd9 And Icd9.Cfk_IcdType = 2
		
WHERE OT.DELFLAG = 0 
   AND HR.istherigypatient = 0 
   AND HR.PAT_STAT != 'INACTIVE' 
   AND hr.cfk_POPUPDATA_ENTTEAMS <> 710 
   AND OT.DRUG = 1 
   AND OT.THERAPY = 1 
   AND OT.SPECIALTY = 1 
   AND labels.nextcomp IS NOT NULL
   And Coalesce(Icd10.Cpk_IcdMasterList, Icd9.Cpk_IcdMasterList) Is Not Null	
   {1}	
   {2}
) 

SELECT 
               temp.external_id,
				temp.FIRST_NAME AS 'first_name',
				temp.LAST_NAME AS 'last_name',
				temp.gender,
                temp.birth_date,
				temp.height,
				temp.weight,
				temp.ssn_last_four,
				temp.email,
                temp.home_phone,
				temp.mobile_phone,
				temp.address_line_1,
				temp.address_line_2,
				temp.city,
				temp.state,
				temp.zip,
				temp.country,
				temp.allergy,
				temp.other_allergy,
				temp.pharmacy,
				temp.medical_plan,
				temp.remote_medical_plan_id,
				temp.pbm,
				temp.remote_pbm_id,
				temp.pcc_identifier,
				temp.ec_first_name,
				temp.ec_last_name,
				temp.ec_telephone,
				temp.ec_relationship,
				STUFF((
					SELECT '::' + t.[therapeutic_category] from TEMPTBL t WHERE (t.external_id=temp.external_id)
					for XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)'),1,2,'') AS therapeutic_category,
				temp.[diagnosis 2] AS 'diagnosis',
				STUFF((
					SELECT '::' + 
					CASE 
						WHEN t.[medications] = 'Efavirenz/Emtricitab/Tenofovir' THEN 'efavirenz/emtricitabine/tenofovir disoproxil fumarate'
						WHEN t.[medications] = 'abacavir/lamivudine' THEN 'abacavir/lamivudine'
						WHEN t.[medications] = 'Emtricitab/tenofovir' THEN 'emtricitabine/tenofovir disoproxil fumarate'
						WHEN t.[medications] = 'ritonavir' THEN 'ritonavir'
						WHEN t.[medications] = 'atazanavir' THEN 'atazanavir'
						WHEN t.[medications] = 'lamivudine' THEN 'lamivudine'
						ELSE UPPER(LEFT(t.[medications],1))+LOWER(SUBSTRING(t.[medications],2,LEN(t.[medications])))
					END
					/*SUBSTRING(t.[medications] ,1,(CHARINDEX(' ',t.[medications]  + ' ')-1))*/
					from TEMPTBL t WHERE (t.external_id=temp.external_id)
					for XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)'),1,2,'') AS medications,
				STUFF((
					SELECT '::' + t.[rx_numbers] from TEMPTBL t WHERE (t.external_id=temp.external_id)
					for XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)'),1,2,'') AS rx_numbers,
				temp.rx_number_start,
				STUFF((
					SELECT '::' + t.[due_date] from TEMPTBL t WHERE (t.external_id=temp.external_id)
					for XML PATH(''),TYPE).value('(./text())[1]','VARCHAR(MAX)'),1,2,'') AS due_date 
				from TEMPTBL temp
				group by external_id, FIRST_NAME, LAST_NAME, gender, birth_date, height, weight, ssn_last_four,email,home_phone,mobile_phone,address_line_1,address_line_2,city,state,zip,country,allergy,other_allergy,pharmacy,medical_plan,remote_medical_plan_id,pbm,remote_pbm_id,pcc_identifier,ec_first_name,ec_last_name,ec_telephone,ec_relationship,therapeutic_category,[diagnosis 2],rx_number_start
            ";
        #endregion

        #region Document Management CPRSQL
        public static readonly string CPRSQL_TB_CUBE_TICKET_VIEW_SELECT = @"
                 select ticket_number,ticket_item_name,ticket_type,ticket_stage,ticket_invoice_number,ticket_confirmation_date_timestamp as ticket_confirmation_date,patient_mrn,patient_full_name,itemname as drug_name,  
                        ticket_payor,ticket_payor_type,ticket_payor_pcn,ticket_payor_binno,inventory_ndc,ticket_shipping_method,patient_team,ticket_creator,ticket_shipping_service_type
                        ,ticket_shipping_package_cost,ticket_shipping_tracking_number,ticket_shipping_method,ticket_ship_to_address,ticket_ship_to_city,ticket_ship_to_state,ticket_ship_to_zip,ticket_ship_to_phone
                 from [Pharmacy].[tb_cube_ticket] ";

        public static readonly string CPRSQL_TB_CUBE_TICKET_DOCUMENT_VIEW_SELECT = @"
                 select t.ticket_number,t.ticket_item_name,t.inventory_ndc,t.ticket_type,t.ticket_stage,t.ticket_invoice_number,t.ticket_confirmation_date_timestamp as ticket_confirmation_date,t.patient_mrn,t.patient_full_name,itemname as drug_name,  
                        t.ticket_payor,t.ticket_payor_type,t.ticket_payor_pcn,t.ticket_payor_binno,t.inventory_ndc,t.ticket_shipping_method,t.patient_team,t.ticket_creator,t.ticket_shipping_service_type
                        ,t.ticket_shipping_package_cost,t.ticket_shipping_tracking_number,t.ticket_shipping_method,t.ticket_ship_to_address,t.ticket_ship_to_city,t.ticket_ship_to_state,t.ticket_ship_to_zip,t.ticket_ship_to_phone,
                        Case When doc.DownloadDate is null Then 0 When doc.DownloadDate < '{0}' Then 0 Else 1 END as DocumentDownloaded,
                        doc.DocumentID,doc.RXNumber,doc.TOUCHDATE,doc.CREATEDON,doc.Code,doc.TrackCode,
                        Case When doc.TrackStatus is null Then '' When doc.TrackStatus = 'DL' Then 'Delivered' Else '' END as TrackStatus,
                        doc.ServiceInfo,doc.OtherIdentifier,doc.OtherIdentifierType,doc.DownloadDate,doc.ImageAvailability
                 from [Pharmacy].[tb_cube_ticket] t left join ISHUB.Pharmacy.Document doc ON t.ticket_shipping_tracking_number = doc.TrackingNumber ";

        public static readonly string CPRSQL_TB_CUBE_TICKET_FILTER_FEDEX = @" ticket_shipping_method = 'FedEx' and ticket_shipping_tracking_number is not null ";

        public static readonly string CPRSQL_TB_CUBE_TICKET_FILTER_FEDEX_TRACKING_NOT_NULL = @" ticket_shipping_method = 'FedEx' and ticket_shipping_tracking_number is not null ";

        public static readonly string CPRSQL_TB_CUBE_TICKET_FILTER_TICKET_CONFIRMATION_DATERANGE = @" convert(date,ticket_confirmation_date_timestamp) >= @StartDate and convert(date,ticket_confirmation_date_timestamp) <= @EndDate ";

        public static readonly string CPRSQL_TB_CUBE_TICKET_FILTER_TICKET_CONFIRMATION_DATE = @" convert(date,ticket_confirmation_date_timestamp) = @DispenseDate ";

        public static readonly string CPRSQL_TB_CUBE_TICKET_FILTER_TICKET_NUMBER = @" ticket_number = {0} ";

        public static readonly string CPRSQL_TB_CUBE_TICKET_IN_FILTER_TICKET_NUMBER = @" ticket_number in ({0}) ";

        public static readonly string ISHUB_DOCUMENT_TABLE_SELECT = @"
        select DocumentID as DocumentGUID,Code,TrackingNumber,DownloadDate,DownloadPath,DownloadMethod,DownloadCode, DatabaseUID,DocumentCount,
        Case When TrackStatus is null Then '' When TrackStatus = 'DL' Then 'Delivered' Else '' END as TrackStatus,
        TrackCode,OtherIdentifierType,OtherIdentifier,ImageAvailability,TrackEvents,RxID,RxTransactionID,RXNumber,TICKNO,TOUCHDATE from Pharmacy.Document";

        public static readonly string ISHUB_DOCUMENT_FILTER_CODE = @" Code = @ErrorCode";
        public static readonly string ISHUB_DOCUMENT_FILTER_DATABASE = @" DatabaseUID = @DatabaseUID ";
        public static readonly string ISHUB_DOCUMENT_FILTER_TRACKING_NUMBER = @" TrackingNumber = @TrackingNumber ";
        public static readonly string ISHUB_DOCUMENT_IN_FILTER_TRACKING_NUMBER = @" TrackingNumber IN ({0}) ";

        public static readonly string ISHUB_DOCUMENT_FILTER_DOCUMENTID = @" DocumentID = @DocumentID ";

        public static readonly string CPRSQL_TB_CUBE_DISPENSE_TABLE_SELECT = @"
                   select ncpdp_dispense_sys_id,dispense_prescription_sys_id,rx_number,dispense_date_timestamp,refillnum,mrn,
                   patient_first_name,patient_last_name,patient_full_name,ndc,line9 as drug_name,pharmacist,
                   ticket_original_ticket_sys_id,ticket_sys_id,ticket_created_user_sys_id,ticket_service_type,ticket_shipping_method,ticket_type,ticket_shipping_tracking_number,ticket_confirmation_date,
                   Case When dispense_date_timestamp is null Then 0 When dispense_date_timestamp < '{0}' Then 0 Else 1 END as DocumentDownloaded
                   from Pharmacy.tb_cube_dispense";

        public static readonly string CPRSQL_TB_CUBE_DISPENSE_DOCUMENT_SELECT = @"
                   select d.ncpdp_dispense_sys_id,d.dispense_prescription_sys_id,d.rx_number,d.dispense_date_timestamp,d.refillnum,d.mrn,
                   d.patient_first_name,d.patient_last_name,d.patient_full_name,d.ndc,d.line9 as drug_name,d.pharmacist,
                   d.ticket_original_ticket_sys_id,d.ticket_sys_id,d.ticket_created_user_sys_id,d.ticket_service_type,d.ticket_shipping_method,d.ticket_type,d.ticket_shipping_tracking_number,d.ticket_confirmation_date,
                   Case When d.dispense_date_timestamp is null Then 0 When d.dispense_date_timestamp < '{0}' Then 0 Else 1 END as DocumentDownloaded,
                   doc.DocumentID,doc.RXNumber,doc.TOUCHDATE,doc.CREATEDON,doc.Code,doc.TrackCode,
                   Case When doc.TrackStatus is null Then '' When doc.TrackStatus = 'DL' Then 'Delivered' Else '' END as TrackStatus,
                   doc.ServiceInfo,doc.OtherIdentifier,doc.OtherIdentifierType,doc.DownloadDate,doc.ImageAvailability
                   from Pharmacy.tb_cube_dispense d left join ISHUB.Pharmacy.Document doc ON d.ticket_shipping_tracking_number = doc.TrackingNumber";

        public static readonly string CPRSQL_TB_CUBE_DISPENSE_DOCUMENT_DELIVER_SELECT = @"
                   select d.ncpdp_dispense_sys_id,d.dispense_prescription_sys_id,d.rx_number,d.dispense_date_timestamp,d.refillnum,d.mrn,
                   d.patient_first_name,d.patient_last_name,d.patient_full_name,d.ndc,d.line9 as drug_name,d.pharmacist,
                   d.ticket_original_ticket_sys_id,d.ticket_sys_id,d.ticket_created_user_sys_id,d.ticket_service_type,d.ticket_shipping_method,d.ticket_type,d.ticket_shipping_tracking_number,d.ticket_confirmation_date,
                   Case When d.dispense_date_timestamp is null Then 0 When d.dispense_date_timestamp < '{0}' Then 0 Else 1 END as DocumentDownloaded,
                   d.DELIVINS,d.DELIVINS_TICKNO,
                   doc.DocumentID,doc.RXNumber,doc.TOUCHDATE,doc.CREATEDON,doc.Code,doc.TrackCode,
                   Case When doc.TrackStatus is null Then '' When doc.TrackStatus = 'DL' Then 'Delivered' Else '' END as TrackStatus,
                   doc.ServiceInfo,doc.OtherIdentifier,doc.OtherIdentifierType,doc.DownloadDate,doc.ImageAvailability
                   from Pharmacy.tb_cube_dispense d left join ISHUB.Pharmacy.Document doc ON d.ticket_shipping_tracking_number = doc.TrackingNumber";

        public static readonly string CPRSQL_TB_CUBE_DIPENSE_TICKET_SHIPPING_METHOD_FILTER = @" ticket_shipping_method = '{0}' ";
        //ticket_shipping_method = 'FedEx'
        public static readonly string CPRSQL_TB_CUBE_DISPENSE_RX_NUMBER_IN_FILTER = @" rx_number in ({0}) ";
        public static readonly string CPRSQL_TB_CUBE_DISPENSE_RX_NUMBER_FILTER = @" rx_number = '{0}' ";

        public static readonly string CPRSQL_TB_CUBE_DISPENSE_TRACKING_NUMBER_IN_FILTER = @" ticket_shipping_tracking_number in ({0}) ";
        public static readonly string CPRSQL_TB_CUBE_DISPENSE_TRACKING_NUMBER_FILTER = @" ticket_shipping_tracking_number = '{0}' ";
        public static readonly string CPRSQL_TB_CUBE_DISPENSE_ORDER = @" ticket_shipping_method,rx_number,refillnum";

        public static readonly string CPRSQL_TB_CUBE_FILTER_DISPENSE_DATERANGE = @" convert(date,dispense_date_timestamp) >= '{0}' and convert(date,dispense_date_timestamp) <= '{1}' ";

        public static readonly string CPRSQL_TB_CUBE_FILTER_DISPENSE_DATE = @" convert(date,dispense_date_timestamp) = @DispenseDate ";

        public static readonly string CPRSQL_TB_CUBE_FILTER_DISCONTINUED = @" ncpdp_dispense_sys_id is not null ";
        public static readonly string CPRSQL_TB_CUBE_FILTER_DISCONTINUE = @" dispense_type != 'Discontinued' ";

        public static string CPRSQLFedExSearchDispenseQuery(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate,DateTime DocumentDownloadStartDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx)
        {
            string Query;

            if (ShippingMethodType == ShippingMethod.FedEx)
            {
                Query = string.Format(CPRSQL_TB_CUBE_DISPENSE_DOCUMENT_DELIVER_SELECT, DocumentDownloadStartDate.ToShortDateString()) + WHERE + CPRSQL_TB_CUBE_FILTER_DISCONTINUED + AND + string.Format(CPRSQL_TB_CUBE_DIPENSE_TICKET_SHIPPING_METHOD_FILTER, ShippingMethod.FedEx.ToString());
                // Query = string.Format(CPRSQL_TB_CUBE_DISPENSE_DOCUMENT_SELECT, DocumentDownloadStartDate.ToShortDateString()) + WHERE + CPRSQL_TB_CUBE_FILTER_DISCONTINUED + AND + string.Format(CPRSQL_TB_CUBE_DIPENSE_TICKET_SHIPPING_METHOD_FILTER, ShippingMethod.FedEx.ToString());
                // Query = string.Format(CPRSQL_TB_CUBE_DISPENSE_TABLE_SELECT, DocumentDownloadStartDate.ToShortDateString()) + WHERE + CPRSQL_TB_CUBE_FILTER_DISCONTINUED + AND + string.Format(CPRSQL_TB_CUBE_DIPENSE_TICKET_SHIPPING_METHOD_FILTER, ShippingMethod.FedEx.ToString());
            }
            else
            {
                Query = string.Format(CPRSQL_TB_CUBE_DISPENSE_DOCUMENT_DELIVER_SELECT, DocumentDownloadStartDate.ToShortDateString()) + WHERE + CPRSQL_TB_CUBE_FILTER_DISCONTINUED;
                // Query = string.Format(CPRSQL_TB_CUBE_DISPENSE_DOCUMENT_SELECT, DocumentDownloadStartDate.ToShortDateString()) + WHERE + CPRSQL_TB_CUBE_FILTER_DISCONTINUED;
                // Query = string.Format(CPRSQL_TB_CUBE_DISPENSE_TABLE_SELECT, DocumentDownloadStartDate.ToShortDateString()) + WHERE + CPRSQL_TB_CUBE_FILTER_DISCONTINUED;
            }

            if (IsCSVNumbers)
            {
                Query += AND;

                if (SelectedSearchType == "RxNumber")
                {
                    Query += string.Format(CPRSQL_TB_CUBE_DISPENSE_RX_NUMBER_IN_FILTER, DataUtility.INFilterVarchar(SearchValues));
                }
                else
                {
                    Query += string.Format(CPRSQL_TB_CUBE_DISPENSE_TRACKING_NUMBER_IN_FILTER, DataUtility.INFilterVarchar(SearchValues));
                }
            }
            else
            {
                Query += AND;
                if (SelectedSearchType == "RxNumber")
                {
                    Query += string.Format(CPRSQL_TB_CUBE_DISPENSE_RX_NUMBER_FILTER, SearchValues);
                }
                else
                {
                    Query += string.Format(CPRSQL_TB_CUBE_DISPENSE_TRACKING_NUMBER_FILTER, SearchValues);
                }
            }

            if (SelectedDateSearch.ToUpper() != Constants.NONE)
            {
                Query += AND + string.Format(CPRSQL_TB_CUBE_FILTER_DISPENSE_DATERANGE, SearchStartDate.ToShortDateString(), SearchEndDate.ToShortDateString());
            }

            Query += ORDER_BY + CPRSQL_TB_CUBE_DISPENSE_ORDER;
            System.Diagnostics.Debug.WriteLine(Query);
            return Query;
        }

        public static readonly string ISHUB_DOCUMENT_AUDIT_INSERT_SP = @"[Pharmacy].[DocumentAuditInsertSP]";

        public static int GetISHUBDataSourceID(string DataSourceName)
        {
            int DataSourceID = 0;
            if ((DataSourceName == Global.DataSource.CPRSQL.ToString()) || (DataSourceName.ToUpper() == "ALPHASCRIPT"))
            {
                DataSourceID = 1;
            }
            else if ((DataSourceName == Global.DataSource.PioneerRX.ToString()) || (DataSourceName.ToUpper() == "HOOVER"))
            {
                DataSourceID = 2;
            }
            return DataSourceID;
        }
        #endregion

        #region Document Management PioneerRX
        public static readonly string PIONEERRX_DISPENSE_VIEW_SELECT = @"
               select rx.RxID,rx.RxNumber,rx.PharmacistID,rx.PatientID,rx.PrescriberID,rx.PrescribedItemID,rx.DateWritten,rx.ExpirationDate,rx.Quantity,rx.RxStatusChangedOn,rx.LegacyNumber,
                      rxt.RxTransactionID,rxt.DateFilled,rxt.PrimaryPatientPayMethodID,rxt.DispensedItemID,rxt.DispensedQuantity,rxt.DaysSupply,rxt.PricingMethodID,rxt.RefillNumber,rxt.RxTransactionStatusTypeID,rxt.CompletedDate
                      ,shipment.ShipmentID,shipment.CreatedOn,shipment.SaleReceiptString,shipment.TrackingNumber,shipment.ShipperName
                      ,sale.PostingDate,sale.TransactionStatusID,sale.SignatureRawID,sale.CardTicketNumber,sale.SignatureTypeID,sale.IsDelivery,sale.DeliveryStatusTypeID,sale.GrossProfitAmount
                      ,personPt.LastName AS 'PatientLastName',personPt.FirstName AS 'PatientFirstName',personPt.SerialNumberPerson AS 'PatientSerialNumber',
                      ISNULL(personPt.LastName+ ', ', '') + ISNULL(personPt.Salutation+ ' ','') +  ISNULL(personPt.FirstName, '') + ISNULL(' ' + personPt.MiddleName, '') + ISNULL(', ' + personPt.Suffix, '') AS PatientFullName,
                      dItem.NDC AS 'DispensedItemNDC',
                      CASE WHEN ( dItem.ItemName IS NOT NULL ) THEN dItem.itemName
		                   WHEN ( rx.prescribedivid IS NOT NULL ) THEN 'IV'
		              ELSE NULL
                      END AS 'DispensedItemName',
                      General.GetFormattedNDC(dItem.NDC) AS 'DispensedItemNdcFormatted',
                      Case When rxt.DateFilled is null Then 0 When rxt.DateFilled < '{0}' Then 0 Else 1 END as DocumentDownloaded
                from Prescription.Rx rx WITH (NOLOCK) 
                JOIN Prescription.RxTransaction rxt WITH (NOLOCK) ON rx.RxID = rxt.RxID
                LEFT JOIN Person.Person personPt WITH (NOLOCK) ON rx.PatientID = personPt.PersonID
                LEFT JOIN Item.Item dItem WITH (NOLOCK)ON rxt.DispensedItemID = dItem.ItemID
                JOIN PointOfSale.SaleTransactionDetail AS saleDetail WITH(NOLOCK) ON saleDetail.ReferenceID = rxt.RxTransactionID
                JOIN PointOfSale.SaleTransaction AS sale WITH(NOLOCK) ON saleDetail.SaleTransactionID = sale.SaleTransactionID 
                JOIN PointOfSale.Shipment AS shipment WITH(NOLOCK) ON shipment.SaleReceiptString = CONVERT(VARCHAR(20), sale.SaleReceiptNumber)";

        public static readonly string PIONEERRX_DISPENSE_SHIPPER_FILTER_FEDEX = @" ShipperName = '{0}' ";
        //public static readonly string PIONEERRX_DISPENSE_SHIPPER_FILTER_FEDEX = @" ShipperName = '{0}' and Shipment.TrackingNumber is not null ";

        public static readonly string PIONEERRX_DISPENSE_SHIPMENT_TRACKING_NULL_FILTER_FEDEX = @" Shipment.TrackingNumber is not null ";

        public static readonly string PIONEERRX_DISPENSE_RX_NUMBER_IN_FILTER = @" RxNumber in ({0}) ";
        public static readonly string PIONEERRX_DISPENSE_RX_NUMBER_FILTER = @" RxNumber = '{0}' ";

        public static readonly string PIONEERRX_DISPENSE_TRACKING_NUMBER_IN_FILTER = @" TrackingNumber in ({0}) ";
        public static readonly string PIONEERRX_DISPENSE_TRACKING_NUMBER_FILTER = @" TrackingNumber = '{0}' ";
        public static readonly string PIONEERRX_DISPENSE_ORDER = @" ShipperName,RxNumber,RefillNumber";

        public static readonly string PIONEERRX_DISPENSE_FILTER_DISPENSE_DATERANGE = @" convert(date,DateFilled) >= '{0}' and convert(date,DateFilled) <= '{1}' ";

        public static readonly string PIONEERRX_DISPENSE_FILTER_DISPENSE_DATE = @" convert(date,DateFilled) = @DispenseDate ";

        public static string PioneerRXFedExSearchDispenseQuery(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, DateTime DocumentDownloadStartDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx)
        {
            string Query;

            if (ShippingMethodType == ShippingMethod.FedEx)
            {
                Query = string.Format(PIONEERRX_DISPENSE_VIEW_SELECT, DocumentDownloadStartDate.ToShortDateString()) + WHERE + string.Format(PIONEERRX_DISPENSE_SHIPPER_FILTER_FEDEX, ShippingMethod.FedEx.ToString());
            }
            else
            {
                Query = string.Format(PIONEERRX_DISPENSE_VIEW_SELECT, DocumentDownloadStartDate.ToShortDateString());
            }

            if (IsCSVNumbers)
            {
                Query += (ShippingMethodType == ShippingMethod.FedEx) ? AND : WHERE;

                if (SelectedSearchType == "RxNumber")
                {
                    Query += string.Format(PIONEERRX_DISPENSE_RX_NUMBER_IN_FILTER, DataUtility.INFilterVarchar(SearchValues));
                }
                else
                {
                    Query += string.Format(PIONEERRX_DISPENSE_TRACKING_NUMBER_IN_FILTER, DataUtility.INFilterVarchar(SearchValues));
                }
            }
            else
            {
                Query += (ShippingMethodType == ShippingMethod.FedEx) ? AND : WHERE;

                if (SelectedSearchType == "RxNumber")
                {
                    Query += string.Format(PIONEERRX_DISPENSE_RX_NUMBER_FILTER, SearchValues);
                }
                else
                {
                    Query += string.Format(PIONEERRX_DISPENSE_TRACKING_NUMBER_FILTER, SearchValues);
                }
            }

            if (SelectedDateSearch.ToUpper() != Constants.NONE)
            {
                Query += AND + string.Format(PIONEERRX_DISPENSE_FILTER_DISPENSE_DATERANGE, SearchStartDate.ToShortDateString(), SearchEndDate.ToShortDateString());
            }

            Query += ORDER_BY + PIONEERRX_DISPENSE_ORDER;
            System.Diagnostics.Debug.WriteLine(Query);

            return Query;
        }

        #endregion

        #region Cures Report
        public static readonly string ISHUB_REPORT_CURES_VIEW_SELECT = @"
        select ReportUID,StateCode,DateFrom,DateTo,DateSubmitted,RecordCount,ReportType,ErrorCode,ReportStatus,[FileName],BatchNumber,LastUpdateDate from [Report].[CuresView] order by StateCode,BatchNumber";

        public static string GetCuresReportQuery(DateTime SearchStartDate, DateTime SearchEndDate)
        {
            string Query = string.Empty;
            Query = ISHUB_REPORT_CURES_VIEW_SELECT;

            System.Diagnostics.Debug.WriteLine(Query);
            return Query;
        }
        #endregion

        public static string GetISHUBDispenseInsertQuery(string FieldName)
        {
            string query;

            query  =  $"IF NOT EXISTS(Select RxTransactionID From [dbo].[Dispense] where RxTransactionID = @RxTransactionID and {FieldName} is not null)";
            query +=  $"insert into dbo.Dispense(RxNumber,RefillNumber,DateFilled,RxTransactionID,{FieldName},{FieldName}Overlay,{FieldName}UpdateDate,{FieldName}UpdatedBy)";
            query += @"select RxNumber,RefillNumber,DateFilled,RxTransactionID,@Original,@Overlay,@UpdateDate,@CHGBYHOST from [Master].[Dispense] 
                       where RxTransactionID = @RxTransactionID";

            return query;
        }

        public static string GetISHUBDispenseUpdateQuery(string FieldName)
        {
            string query;
            query  = $"IF EXISTS(Select RxTransactionID From [dbo].[Dispense] where RxTransactionID = @RxTransactionID)";
            query += $"update dbo.Dispense set {FieldName} = @Original, {FieldName}Overlay = @Overlay, {FieldName}UpdateDate = @UpdateDate, {FieldName}UpdatedBy = @CHGBYHOST where RxTransactionID = @RxTransactionID";
            return query;
        }

        public static string GetISHUBDispenseUpdateQuery(string FieldName, string TableName = "[dbo].[Dispense]")
        {
            string query;
            query = $"IF EXISTS(Select RxTransactionID From {TableName} where RxTransactionID = @RxTransactionID)";
            query += $"update {TableName} set {FieldName} = @Overlay where RxTransactionID = @RxTransactionID";
            return query;
        }

        public static string GetRecordCountQuery(string TableName, string ColumnName)
        {
            string query;
            query = $"select count(*) from {TableName} where {ColumnName} = @ColumnValue";
            return query;
        }

        // utility class does not make database connections. It only works on database objects
        #region Data Operations
        public static string GetParameterizedQuery(string Query, string ParameterName, string ParameterValue)
        {
            string result = string.Empty;

            using (SqlCommand command = new SqlCommand(Query))
            {
                command.Parameters.AddWithValue(ParameterName, ParameterValue);
                result = command.CommandText;
            }
            return result;
        }

        public static string GetSPParameter(string ParameterValue)
        {
            string result = string.Empty;
            string pattern = "'{0}'";
            if (ParameterValue == string.Empty)
            {
                result = "NULL";
            }
            else
            {
                result = string.Format(pattern, ParameterValue); 
            }

            return result;
        }

        public static bool? GetBit(object DBObject)
        {
            // Get the value as is, return a nullable type if you have to
            bool? BitValue = null;

            BitValue = (DBObject == System.DBNull.Value) ? BitValue : (bool)DBObject;
            return BitValue;
        }

        public static bool GetBitValue(object DBObject)
        {
            // Set a default value if no value is found. Field does not allow nulls
            bool BitValue = false;

            BitValue = (DBObject == System.DBNull.Value) ? BitValue : (bool)DBObject;
            return BitValue;
        }

        public static bool GetBitValue(object DBObject,bool DefaultValue)
        {
            // Set a user defined default value if no value is found. Field does not allow nulls
            bool BitValue = DefaultValue;

            BitValue = (DBObject == System.DBNull.Value) ? BitValue : (bool)DBObject;
            return BitValue;
        }

        public static int? GetInt(object DBObject)
        {
            // use for nullable int fields.
            int? IntValue = null;

            IntValue = (DBObject == System.DBNull.Value) ? IntValue : (int)DBObject;
            return IntValue;
        }

        public static int GetIntValue(object DBObject)
        {
            // use for non nullable int fields
            int IntValue = 0;
            IntValue = (DBObject == System.DBNull.Value) ? IntValue : (int)DBObject;
            return IntValue;
        }

        public static int GetIntValue(object DBObject,int DefaultValue)
        {
            // use for non nullable int fields, set auser defined default value if null found
            int IntValue = DefaultValue;
            IntValue = (DBObject == System.DBNull.Value) ? IntValue : (int)DBObject;
            return IntValue;
        }

        public static string GetStringValue(object DBObject)
        {
            string StringValue = string.Empty;

            StringValue = (DBObject == System.DBNull.Value) ? StringValue : (string)DBObject;
            return StringValue;
        }

        public static string GetStringValue(object DBObject,Type DBDataType)
        {
            // Make sure object is not null before calling this function
            // Null data is not handle by this function
            string Result = string.Empty;

            if (DBDataType == System.Type.GetType("System.String"))
            {
                string stringValue = (string)DBObject;
                Result = stringValue;
            }
            else if (DBDataType == System.Type.GetType("System.Int32"))
            {
                int intValue = (int)DBObject;
                Result = intValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.Boolean"))
            {
                bool boolValue = (bool)DBObject;
                Result = boolValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.DateTime"))
            {
                DateTime datetimeValue = (DateTime)DBObject;
                Result = datetimeValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.Decimal"))
            {
                Decimal decimalValue = (decimal)DBObject;
                Result = decimalValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.Byte"))
            {
                Byte byteValue = (byte)DBObject;
                Result = byteValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.Char"))
            {
                char charValue = (char)DBObject;
                Result = charValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.Double"))
            {
                double doubleValue = (double)DBObject;
                Result = doubleValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.Int16"))
            {
                Int16 IntSixteenValue = (Int16)DBObject;
                Result = IntSixteenValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.SByte"))
            {
                sbyte sbyteValue = (sbyte)DBObject;
                Result = sbyteValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.Single"))
            {
                Single singleValue = (Single)DBObject;
                Result = singleValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.TimeSpan"))
            {
                TimeSpan timespanValue = (TimeSpan)DBObject;
                Result = timespanValue.ToString();
            }
            else if (DBDataType == System.Type.GetType("System.UInt16"))
            {
                UInt16 UIntSixteenValue = (UInt16)DBObject;
                Result = UIntSixteenValue.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.UInt32"))
            {
                UInt32 UInt32Value = (UInt32)DBObject;
                Result = UInt32Value.ToString(); 
            }
            else if (DBDataType == System.Type.GetType("System.UInt64"))
            {
                UInt64 UInt64Value = (ulong)DBObject;
                Result = UInt64Value.ToString(); 
            }
            return Result;
        }

        public static string GetGUIDValue(object DBObject)
        {
            Guid GuidResult = new Guid();
            string Result = string.Empty;

            if (DBObject == System.DBNull.Value)
            {
                Result = string.Empty;
            }
            else
            {
                GuidResult = (Guid)DBObject;
                Result = GuidResult.ToString(); 
            }

            return Result;
        }

        public static DateTime GetDateValue(object DBObject)
        {
            DateTime DateValue;
            DateValue = (DBObject == System.DBNull.Value) ? DateTime.Now : (DateTime)DBObject;
            return DateValue;
        }

        public static DateTime GetDateValue(object DBObject,DateTime InitialDateTime)
        {
            // if a datetime is not found then intialize it to the value passed in to this function
            DateTime DateValue;
            DateValue = (DBObject == System.DBNull.Value) ? InitialDateTime : (DateTime)DBObject;
            return DateValue;
        }

        public static float GetFloatValue(object DBObject)
        {
            // use for non nullable int fields
            float Result = 0;
            Result = (DBObject == System.DBNull.Value) ? Result : Convert.ToSingle(DBObject);
            return Result;
        }

        public static object GetDBValue(object DBObject)
        {
            int IntegerObject = 0;
            string StringObject = string.Empty;
            object Result = null;
            if (DBObject.GetType() == IntegerObject.GetType())
            {
                IntegerObject = (DBObject == System.DBNull.Value) ? IntegerObject : (int)DBObject;
                Result = IntegerObject;
            }
            else if (DBObject.GetType() == StringObject.GetType())
            {
                StringObject = (DBObject == System.DBNull.Value) ? string.Empty : (string)DBObject;
                Result = StringObject;
            }

            return Result;
        }

        public static bool Compare(string ObjectValue,string DefaultValue)
        {
            bool Result = (ObjectValue == DefaultValue) ? true : false;
            return Result;
        }

        public static bool HasData(DataSet dsData)
        {
            bool HasData = false;

            if (dsData.Tables == null)
            {
                HasData = false;
            }
            else if (dsData.Tables.Count == 0)
            {
                HasData = false;
            }
            else
            {
                if (dsData.Tables[0].Rows.Count == 0)
                {
                    HasData = false;
                }
                else
                {
                    HasData = true;
                }
            }

            return HasData;
        }

        public static bool HasData(DataTable tblData)
        {
            bool HasData = false;

            if (tblData == null)
            {
                HasData = false;
            }
            else
            {
                if (tblData.Rows.Count == 0)
                {
                    HasData = false;
                }
                else
                {
                    HasData = true;
                }
            }

            return HasData;
        }
        #endregion

        #region Add Parameters
        public static SqlParameter AddSQLParameterBit(string ParameterName, bool ParameterValue)
        {
            SqlParameter SqlParameterBit = new SqlParameter(ParameterName, SqlDbType.Bit);
            SqlParameterBit.Value = ParameterValue;
            return SqlParameterBit;
        }

        public static SqlParameter AddSQLParameterInt(string ParameterName, int ParameterValue)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Int);
            SqlParameterInt.Value = ParameterValue;
            return SqlParameterInt;
        }

        public static SqlParameter AddSQLParameterInt(string ParameterName, int? ParameterValue)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Int);
            if (ParameterValue == null)
            {
                SqlParameterInt.Value = System.DBNull.Value;
            }
            else
            {
                SqlParameterInt.Value = ParameterValue;
            }
            
            return SqlParameterInt;
        }

        public static SqlParameter AddSQLParameterInt(string ParameterName, ParameterDirection Direction)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Int);
            SqlParameterInt.Direction = Direction;
            return SqlParameterInt;
        }

        public static SqlParameter AddSQLParameterDecimal(string ParameterName, ParameterDirection Direction)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Decimal);
            SqlParameterInt.Direction = Direction;
            return SqlParameterInt;
        }

        public static SqlParameter AddSQLParameterDecimal(string ParameterName, Decimal ParameterValue)
        {
            SqlParameter SqlParameterInt = new SqlParameter(ParameterName, SqlDbType.Decimal);
            SqlParameterInt.Value = ParameterValue;
            return SqlParameterInt;
        }

        // For VarChar Max pass in -1 for the ParameterSize
        public static SqlParameter AddSQLParameterVarChar(string ParameterName, int ParameterSize, string ParameterValue)
        {
            SqlParameter SqlParameterVarChar = new SqlParameter(ParameterName, SqlDbType.VarChar, ParameterSize);

            if (ParameterValue == string.Empty)
            {
                SqlParameterVarChar.Value = System.DBNull.Value;
            }
            else
            {
                SqlParameterVarChar.Value = ParameterValue;
            }

            return SqlParameterVarChar;
        }

        public static SqlParameter AddSQLParameterVarChar(string ParameterName, int ParameterSize, ParameterDirection Direction)
        {
            SqlParameter SqlParameterVarChar = new SqlParameter(ParameterName, SqlDbType.VarChar, ParameterSize);
            SqlParameterVarChar.Direction = Direction;
            return SqlParameterVarChar;
        }

        public static SqlParameter AddSQLParameterVarCharMax(string ParameterName, string ParameterValue)
        {
            SqlParameter SqlParameterVarChar = new SqlParameter(ParameterName, SqlDbType.VarChar, -1);

            if (ParameterValue == string.Empty)
            {
                SqlParameterVarChar.Value = System.DBNull.Value;
            }
            else
            {
                SqlParameterVarChar.Value = ParameterValue;
            }

            return SqlParameterVarChar;
        }

        public static SqlParameter AddSQLParameterDateTime(string ParameterName, DateTime ParameterValue)
        {
            SqlParameter SqlParameterDateTime = new SqlParameter(ParameterName, SqlDbType.DateTime);
            SqlParameterDateTime.Value = ParameterValue;
            return SqlParameterDateTime;
        }

        public static SqlParameter AddSQLParameterUniqueIdentifier(string ParameterName, string ParameterValue)
        {
            SqlParameter SqlParameterUniqueIdentifier = new SqlParameter(ParameterName, SqlDbType.UniqueIdentifier);

            if (ParameterValue == string.Empty)
            {
                SqlParameterUniqueIdentifier.Value = System.DBNull.Value;
            }
            else
            {
                SqlParameterUniqueIdentifier.Value = new Guid(ParameterValue);
            }
            
            return SqlParameterUniqueIdentifier;
        }

        public static SqlParameter AddSQLParameterUniqueIdentifier(string ParameterName, ParameterDirection Direction)
        {
            SqlParameter SqlParameterUniqueIdentifier = new SqlParameter(ParameterName, SqlDbType.UniqueIdentifier);
            SqlParameterUniqueIdentifier.Direction = Direction;
            return SqlParameterUniqueIdentifier;
        }
        public static SqlParameter AddSQLParameterFloat(string ParameterName, float ParameterValue)
        {
            SqlParameter SqlParameterFloat = new SqlParameter(ParameterName, SqlDbType.Float);
            SqlParameterFloat.Value = ParameterValue;
            return SqlParameterFloat;
        }
        #endregion

        #region Parameterized Queries
        public static SqlParameter[] CPRSQLDispenseRepositoryGetAll(DateTime DispenseDate)
        {
            SqlParameter[] arParms = new SqlParameter[1];
            arParms[0] = AddSQLParameterDateTime("@DispenseDate", DispenseDate);

            return arParms;
        }

        public static SqlParameter[] CPRSQLDispenseRepositoryGetAll(DateTime StartDate, DateTime EndDate)
        {
            SqlParameter[] arParms = new SqlParameter[2];

            arParms[0] = AddSQLParameterDateTime("@StartDate", StartDate);
            arParms[1] = AddSQLParameterDateTime("@EndDate", EndDate);

            return arParms;
        }

        public static SqlParameter[] GetJobStatus(DateTime ScheduleDate)
        {
            SqlParameter[] arParms = new SqlParameter[1];
            arParms[0] = AddSQLParameterDateTime("@ScheduleDate", ScheduleDate);

            return arParms;
        }

        public static SqlParameter[] UpdateJobCalendar(int JobScheduleID, DateTime ScheduleDate)
        {
            SqlParameter[] arParms = new SqlParameter[2];
            arParms[0] = AddSQLParameterInt("@JobScheduleID", JobScheduleID);
            arParms[1] = AddSQLParameterDateTime("@ScheduleDate", ScheduleDate);

            return arParms;
        }

        public static SqlParameter[] DocumentRead(string ShipmentTrackingNumber)
        {
            SqlParameter[] arParms = new SqlParameter[1];
            arParms[0] = AddSQLParameterVarChar("@TrackingNumber", 25, ShipmentTrackingNumber);

            return arParms;
        }

        public static SqlParameter[] DocumentRead(int DatabaseUID, string ErrorCode)
        {
            SqlParameter[] arParms = new SqlParameter[1];
            arParms[0] = AddSQLParameterInt("@DatabaseUID", DatabaseUID);
            arParms[1] = AddSQLParameterVarChar("@Code", 25, ErrorCode);

            return arParms;
        }


        #endregion

        #region Stored Procedures
        public static SqlParameter[] TableDelete(int ID, string FieldName)
        {
            SqlParameter[] arParms = new SqlParameter[1];
            string ParameterName = "@" + FieldName;
            arParms[0] = AddSQLParameterInt(ParameterName, ID);

            return arParms;
        }

        public static SqlParameter[] ErrorLogInsert(string ErrorClass, string ErrorType, string ErrorCode, string ErrorObject, string ErrorSource, string ErrorSourceLineNo, string ErrorMessage, DateTime ReportedDate, int UserID, string LoginID, bool DisplayException, string StackTrace, string Detail, string Debug)
        {
            SqlParameter[] arParms = new SqlParameter[15];

            arParms[0] = AddSQLParameterInt("@ErrorLogID", ParameterDirection.Output);
            arParms[1] = AddSQLParameterVarChar("@ErrorClass", 100, ErrorClass);
            arParms[2] = AddSQLParameterVarChar("@ErrorType", 100, ErrorType);
            arParms[3] = AddSQLParameterVarChar("@Code", 100, ErrorCode);
            arParms[4] = AddSQLParameterVarChar("@Object", 100, ErrorObject);
            arParms[5] = AddSQLParameterVarChar("@Source", 200, ErrorSource);
            arParms[6] = AddSQLParameterVarChar("@SourceLineNo", 50, ErrorSourceLineNo);
            arParms[7] = AddSQLParameterVarChar("@Message", 250, ErrorMessage);
            arParms[8] = AddSQLParameterDateTime("@ReportedDate", ReportedDate);
            arParms[9] = AddSQLParameterInt("@UserID", UserID);
            arParms[10] = AddSQLParameterVarChar("@LoginID", 100, LoginID);
            arParms[11] = AddSQLParameterBit("@DisplayException", DisplayException);
            arParms[12] = AddSQLParameterVarChar("@Trace", 700, StackTrace);
            arParms[13] = AddSQLParameterVarChar("@Detail", 700, Detail);
            arParms[14] = AddSQLParameterVarChar("@Debug", 700, Debug);

            return arParms;
        }

        public static SqlParameter[] DocumentInsert(string TrackingNumber,string Code, DateTime DownloadDate, string DownloadPath,int? DownloadMethod,string DownloadCode, int DatabaseUID)
        {
            SqlParameter[] arParms = new SqlParameter[6];

            arParms[0] = AddSQLParameterUniqueIdentifier("@DocumentID", ParameterDirection.Output);
            arParms[1] = AddSQLParameterVarChar("@Code", 25, Code);
            arParms[2] = AddSQLParameterVarChar("@TrackingNumber", 25, TrackingNumber);
            arParms[3] = AddSQLParameterDateTime("@DownloadDate", DownloadDate);
            arParms[4] = AddSQLParameterVarChar("@DownloadPath", 200, DownloadPath);
            arParms[5] = AddSQLParameterInt("@DownloadMethod", DownloadMethod);
            arParms[6] = AddSQLParameterVarChar("@DownloadCode", 25, DownloadCode);
            arParms[7] = AddSQLParameterInt("@DatabaseUID", DatabaseUID);

            return arParms;
        }

        #endregion

    }

}
