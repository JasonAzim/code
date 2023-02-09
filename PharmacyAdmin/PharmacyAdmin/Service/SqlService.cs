using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PharmacyAdmin.Data;
using Pharmacy.Data.Repository;
using PharmacyAdmin.Model;
using System.Text;
using Global;

namespace PharmacyAdmin.Service
{
    public static class DbCommandExtensionMethods
    {
        public static void AddParameter(this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }

    public interface ISqlService : IDisposable
    {
        ObjectState DBState { get; }

        Task<List<DispenseModel>> ISHUBGetByRxNumber(string RxNumber);
        Task<List<AuditModel>> ISHUBGetProfitabilityAuditMaster(string RXNO);
        Task<int> ISHUBDispenseUpdate(string DatabaseName, string FieldName, string RXNO, string RxTransactionID, decimal? Original, decimal? OVERLAY, string CHGBYHOST, AuditModel AuditChange);
        Task<int> ISHUBGetRecordCount(string TableName, string ColumnName, string ColumnValue);

        Task<List<FedExCPRSQLDispenseModel>> CPRSQLFedExSearchDispense(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch,DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx);
        Task<FedExCPRSQLDataModel> CPRSQLFedExSearch(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx, bool Merge = true);

        Task<List<DocumentModel>> PioneerRXGetFedexShipmentTrackingNumber(string DatabaseName, string RXNO, string RxTransactionID);

        Task<List<FedExPioneerRXDispenseModel>> PioneerRXFedExSearchDispense(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx);
        Task<List<FedExPioneerRXDispenseModel>> PioneerRXFedExSearchDispenseDoc(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx);

        Task<List<DocumentModel>> DocumentSearch(string TrackingNumberCSV, int DatabaseUID);
        Task<List<DocumentModel>> DocumentSearch(string csv);
        Task<List<DocumentModel>> DocumentRead(string TrackingNumber, int DatabaseUID);
        Task<List<DocumentModel>> DocumentRead(Guid DocumentID, int DatabaseUID);

        Task<ViewCubeNCPDPModel> GetByLABLOGNO(string LABLOGNO);
        Task<List<ProfitabilityNCPDPModel>> GetByRXNO(string RXNO, string SearchType, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate);
        Task<List<ProfitabilityNCPDPModel>> GetByDateFilled(string SearchType, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate);
        Task<List<ProfitabilityNCPDPModel>> GetProfitability(string SearchType, string SearchMode, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate);
        Task<List<ProfitabilityNCPDPModel>> ProfitabilityReportEditor(string SearchType, string SearchMode, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate);
        Task<List<ProfitabilityNCPDPModel>> GetProfitabilityReport(string SearchType, string SearchMode, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate);

        Task<List<ProfitabilityNCPDPModel>> GetByINVOICENO(string INVOICENO);
        Task<List<ProfitabilityNCPDPModel>> GetByTICKNO(string TICKNO);
        Task<List<ProfitabilityNCPDPModel>> GetByMRN(string MRN);
        Task<List<ProfitabilityNCPDPModel>> GetProfitabilityTicketItem(int INVOICENO, int TICKNO);
        Task<List<AuditModel>> GetProfitabilityAudit(string RXNO, int INVOICENO);

        Task<int> DataWarehouse_NCPDP_Adjustment_Update(string RXNO, int BILLNO, int TICKNO, decimal? ncpdp_primary_claim_paid, int ncpdp_primary_claim_paid_response_sys_id, decimal? ncpdp_secondary_claim_paid, int ncpdp_secondary_claim_paid_response_sys_id, decimal? Copay, int ncpdp_patient_primary_copay_expected_response_sys_id, decimal? COGS, decimal? COGSAdjusted, decimal? TPRevenuePlusPatientCopay, decimal? GrossProfit, decimal? GrossProfitAdjusted, int CHGFLAG, string History, string PrimaryTPHistory, string SecondaryTPHistory, string CopayHistory, string COGSHistory, string RecordDeletedHistory, string Events, int ticket_number_partials, int? ticket_item_delflag_overlay);

        Task<int> TICKC_Update(string RXNO, int BILLNO, int TICKNO, decimal? COST, decimal? COST_OVERLAY, decimal? COGS, decimal? ncpdp_primary_claim_paid, decimal? ncpdp_secondary_claim_paid, decimal? Copay, string ncpdp_primary_claim_payor, string vatext, string CHGBYHOST);

        Task<int> INVOICES_UpdateRenenueTotalExpected(string RXNO, int CPK_INVOICES, int TICKNO, decimal? EXPECTED, decimal? EXPECTED_OVERLAY, string CHGBYHOST);

        Task<int> LINEITEMS_Update(ProfitabilityNCPDPModel entity);
        Task<int> CLAIMS_PrimaryClaimUpdate(ProfitabilityNCPDPModel entity);
        Task<int> CLAIMS_SecondaryClaimUpdate(ProfitabilityNCPDPModel entity);

        Task<int> NCPDPREP_Update(string RXNO, int BILLNO, int TICKNO, int CPK_NCPDPREPS, decimal? SHORT_VAL, decimal? SHORT_VAL_OVERLAY, string CHG, string CHGBYHOST);

        Task<int> Update(ProfitabilityNCPDPModel entity);

        Task<List<ProfitabilityNCPDPModel>> GetProfitabilityReportDuplicate(string SearchType, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate);

        Task<int> TICKCI_UpdateDelFlag(ProfitabilityNCPDPModel entity);

        Task<string> GetPatientExportData(CPRSQLPatientExportViewModel model);
        Task<string> GetPatientExportDataCSV(CPRSQLPatientExportViewModel model);

        Task<string> GetPatientExportQuery(CPRSQLPatientExportViewModel model);
        Task<string> GetPatientExportQuery2(CPRSQLPatientExportViewModel model);

        Task<List<JobScheduleModel>> ISHUBGetJobSchedules(string JobName);
        Task<List<StatisticModel>> ISHUBGetJobSummary(string StatisticName);
        Task<List<UracTurnAroundTimeDispenseModel>> URACTurnAroundTimeDiscrepancies(string JobName);
        Task<List<UracTurnAroundTimeDispenseModel>> URACTurnAroundTimeReport(string JobName, string TableName);
        Task<int> URAC_ISHUB_ScheduleJob(JobScheduleModel entity);
        Task<string> ISHUBJobStatus(string JobName);
        Task<List<JobEntity>> ISHUBGetJobs(string Category = null);
        Task<int> ISHUBAddJob(string JobName, string Owner, string Category, string Description, int Enabled, string NotificationType, int NotificationEnabled, int Status, string UpdatedBy);
        Task<int> ISHUB_Job_UpdateSchedule(JobScheduleModel entity);

        Task<List<CuresReportModel>> GetCuresReport(DateTime DateFrom, DateTime DateTo);

    }

    public class SqlService : DataObject, ISqlService
    {
        private readonly IConfiguration _config;
        private Settings _appSettings;

        private Settings GetAppSettings()
        {
            Settings AppSettings = new Settings();
            AppSettings.Mode = _config.GetSection("AppSettings:Mode").Value;
            AppSettings.DocumentStorageMode = _config.GetSection("AppSettings:DocumentStorageMode").Value;
            AppSettings.DocumentDownloadPath = _config.GetSection("AppSettings:DocumentDownloadPath").Value;
            AppSettings.DocumentDownloadFolderCPRPlus = _config.GetSection("AppSettings:DocumentDownloadFolderCPRPlus").Value;
            AppSettings.DocumentDownloadFolderPioneerRX = _config.GetSection("AppSettings:DocumentDownloadFolderPioneerRX").Value;
            AppSettings.CPRPlusDocumentDownloadStartDate = _config.GetSection("AppSettings:CPRPlusDocumentDownloadStartDate").Value;
            AppSettings.PioneerRXDocumentDownloadStartDate = _config.GetSection("AppSettings:PioneerRXDocumentDownloadStartDate").Value;

            return AppSettings;
        }

        public SqlService(IConfiguration config)
        {
            _config = config;
            _appSettings = GetAppSettings();
            State = new ObjectState();
        }

        public SqlService()
        {
            State = new ObjectState();
        }

        public SqlConnection CreateSqlConnection(string DatabaseName = "CPRSQL")
        {
            if (DatabaseName == "CRPSQL")
                return new SqlConnection(_config.GetConnectionString("CPRSQLConnection"));
            else if (DatabaseName == "ALPHAScript")
                return new SqlConnection(_config.GetConnectionString("ALPHAScriptSqlConnection"));
            else
                return new SqlConnection(_config.GetConnectionString("ISHUBConnection"));
        }

        // Todo using IDbConnection and IDataReader - change to concrete types SqlDataReader and SqlConnection because we are only dealing with sql databases and not oracle
        public IDbConnection CPRSQLConnection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("CPRSQLConnection"));
            }
        }

        public IDbConnection ISHUBSqlConnection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("ISHUBConnection"));
            }
        }

        public IDbConnection ALPHAScriptSqlConnection
        {
            get
            {
                if (_appSettings.Mode == "Prod")
                {
                    return new SqlConnection(_config.GetConnectionString("ALPHAScriptSqlConnection"));
                }
                return new SqlConnection(_config.GetConnectionString("ISHUBConnection"));
            }
        }

        public IDbConnection PioneerRXSqlConnection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("PioneerRXSqlConnection"));
            }
        }
        
        public ObjectState DBState
        {
            get
            {
                return State;
            }
        }

        public void Dispose()
        {
        }

        public async Task<List<JobModel>> ISUB_GetJobs()
        {
            List<JobModel> JobList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = string.Empty;
                    
                    Query = SQLUtility.ISHUB_JOB_SCHEDULE_TABLE_SELECT;
                    

                    conn.Open();

                    return await Task.FromResult(conn.Query<JobModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "ISHUB_GetJobs failed");
            }
            return await Task.FromResult(JobList);
        }

        #region Job Change Schema or Data

        public async Task<List<JobEntity>> ISHUBGetJobs(string Category = null)
        {
            List<JobEntity> EntityList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = SQLUtility.ISHUBGetJobs(Category);
                    conn.Open();

                    EntityList = await Task.FromResult(conn.Query<JobEntity>(Query, new { Category = Category }, commandType: CommandType.Text).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "ISHUBGetJobs failed");
            }
            return await Task.FromResult(EntityList);
        }

        public Task<int> ISHUBAddJob(string JobName, string Owner, string Category, string Description, int Enabled,string NotificationType,int NotificationEnabled,int Status,string UpdatedBy)
        {
            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string query;

                    query = SQLUtility.ISHUBJobInsertQuery();

                        var results = conn.Execute(query, new
                        {
                            Name = JobName,
                            Owner = Owner,
                            Category = Category,
                            Description = Description,
                            Enabled = Enabled,
                            NotificationType = NotificationType,
                            NotificationEnabled = NotificationEnabled,
                            Status = Status,
                            CreatedDate = DateTime.Now,
                            LastUpdateDate = DateTime.Now,
                            UpdatedBy = UpdatedBy
                        });

                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "ISHUBAddJob failed");
            }

            return Task.FromResult(result);
        }

        public Task<int> ISHUB_Job_UpdateSchedule(JobScheduleModel entity)
        {
            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string query;

                    query = SQLUtility.ISHUB_Job_UpdateSchedule;

                    var results = conn.Execute(query, new
                    {
                        ScheduledDate = entity.ScheduledDate,
                        UpdatedBy = entity.UpdatedBy,
                        JobID = entity.JobID
                    });

                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "ISHUB_Job_UpdateSchedule failed");
            }

            return Task.FromResult(result);
        }

        #endregion

        #region URAC
        public async Task<List<JobScheduleModel>> ISHUBGetJobSchedules(string JobName)
        {
            List<JobScheduleModel> JobScheduleList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = string.Empty;
                    Query = SQLUtility.ISHUB_JOB_SCHEDULE_VIEW_SELECT;
                    
                    conn.Open();

                    return await Task.FromResult(conn.Query<JobScheduleModel>(Query, new { JobName = JobName }, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "ISHUBGetJobSchedule failed");
            }
            return await Task.FromResult(JobScheduleList);
        }

        public Task<int> URAC_ISHUB_ScheduleJob(JobScheduleModel entity)
        {
            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string query;

                    query = SQLUtility.URAC_ISHUB_ScheduleJob;

                    var results = conn.Execute(query, new
                    {
                        ScheduledDate = entity.ScheduledDate,
                        StartDate = entity.StartDate,
                        EndDate = entity.EndDate,
                        UpdatedBy = entity.UpdatedBy,
                        JobID = entity.JobID
                    });

                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "URAC_ISHUB_ScheduleJob failed");
            }

            return Task.FromResult(result);
        }

        public async Task<List<StatisticModel>> ISHUBGetJobSummary(string StatisticName)
        {
            List<StatisticModel> StatisticList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = string.Empty;
                    if (StatisticName == "CPRSQL_URAC_TurnAroundTime")
                    {
                        Query = @"IF OBJECT_ID('[URAC].[tb_dispense_tat_step1]', 'u') IS NOT NULL " + SQLUtility.ISHUB_JOB_RUN_SUMMARY_VIEW_SELECT;
                    }

                    conn.Open();

                    return await Task.FromResult(conn.Query<StatisticModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "ISHUBGetJobSummary failed");
            }
            return await Task.FromResult(StatisticList);
        }

        public async Task<List<UracTurnAroundTimeDispenseModel>> URACTurnAroundTimeDiscrepancies(string JobName)
        {
            List<UracTurnAroundTimeDispenseModel> URACDispenseList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = string.Empty;
                    Query = SQLUtility.URAC_TURNAROUNDTIME_DISPENSE_STEP1 + SQLUtility.WHERE + SQLUtility.URAC_TURNAROUNDTIME_FILTER_DATEZERO;

                    conn.Open();

                    return await Task.FromResult(conn.Query<UracTurnAroundTimeDispenseModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "URACTurnAroundTimeDiscrepancies failed");
            }
            return await Task.FromResult(URACDispenseList);
        }

        public async Task<string> ISHUBJobStatus(string JobName)
        {
            string status = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = string.Empty;
                    Query = string.Format(SQLUtility.ISHUB_JOB_RUNNING_VIEW_SELECT, JobName);

                    conn.Open();

                    IEnumerable<DateTime?> job_max_run_requested_dates = conn.Query<DateTime?>(Query, null, commandType: CommandType.Text, commandTimeout: 180);

                    if ((job_max_run_requested_dates != null) && (job_max_run_requested_dates.Count() > 0))
                    {
                        status = "Job is running. Started at " + job_max_run_requested_dates.FirstOrDefault().Value.ToString();

                    }
                    else
                    {
                        Query = string.Format(SQLUtility.ISHUB_JOB_OUTCOME_VIEW_SELECT, JobName);
                        IEnumerable<DateTime?> job_outcome_dates = conn.Query<DateTime?>(Query, null, commandType: CommandType.Text, commandTimeout: 180);
                        if ((job_outcome_dates != null) && (job_outcome_dates.Count() > 0))
                        {
                            status = "Job completed at " + job_outcome_dates.LastOrDefault().Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "URACTurnAroundTimeStatus failed");
            }
            return await Task.FromResult(status);
        }

        public async Task<List<UracTurnAroundTimeDispenseModel>> URACTurnAroundTimeReport(string JobName, string TableName)
        {
            List<UracTurnAroundTimeDispenseModel> URACDispenseList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = string.Empty;
                    if (TableName == UracTurnAroundTimeViewModel.UracTables.tb_dispense_tat_step1.ToString())
                    {
                        Query = SQLUtility.URAC_TB_DISPENSE_TAT_STEP1;
                    }
                    else if (TableName == UracTurnAroundTimeViewModel.UracTables.tb_dispense_tat_step2.ToString())
                    {
                        Query = SQLUtility.URAC_TB_DISPENSE_TAT_STEP2;
                    }
                    else if (TableName == UracTurnAroundTimeViewModel.UracTables.tb_dispense_tat.ToString())
                    {
                        Query = SQLUtility.URAC_TB_DISPENSE_TAT;
                    }
                    else
                    {
                        Query = SQLUtility.URAC_TURNAROUNDTIME_CALCULATE_STEP2;
                    }

                    conn.Open();

                    return await Task.FromResult(conn.Query<UracTurnAroundTimeDispenseModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "URACTurnAroundTimeReport failed");
            }
            return await Task.FromResult(URACDispenseList);
        }

        #endregion

        #region ISHUB Master Search
        public async Task<List<DispenseModel>> ISHUBGetByRxNumber(string RxNumber)
        {
            List<DispenseModel> DispenseList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = string.Empty;
                    //Query = SQLUtility.ISHUB_MASTER_DISPENSE_ORIGINAL_TABLE_SELECT + SQLUtility.WHERE + SQLUtility.ISHUB_MASTER_FILTER_RXNUMBER;
                    Query = SQLUtility.ISHUB_MASTER_DISPENSE_CHANGED_TABLE_SELECT;
                    Query += " order by DateFilled";
                    conn.Open();

                    return await Task.FromResult(conn.Query<DispenseModel>(Query, new { RxNumber = RxNumber }, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "ISHUBGetByRxNumber failed");
            }
            return await Task.FromResult(DispenseList);
        }

        public Task<List<AuditModel>> ISHUBGetProfitabilityAuditMaster(string RXNO)
        {
            List<AuditModel> AuditList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = SQLUtility.ISHUB_FN_PROFITABILITY_AUDIT_MASTER_FUNCTION_SELECT + " order by INVOICENO";

                    conn.Open();

                    return Task.FromResult(conn.Query<AuditModel>(Query, new { RXNO = RXNO}, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetProfitabilityAudit failed");
            }
            return Task.FromResult(AuditList);
        }

        public Task<int> ISHUBDispenseUpdate(string DatabaseName, string FieldName, string RXNO, string RxTransactionID, decimal? Original, decimal? Overlay, string CHGBYHOST, AuditModel AuditChange)
        {
            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string query;

                    if (AuditChange == null)
                    {
                        // The audit change class is built from the dbo.Dispense audit table so if there is no data in this class then it is a new record and there is no audit history so we will insert a new record
                        query = SQLUtility.GetISHUBDispenseInsertQuery(FieldName);
                    }
                    else
                    {
                        // It is not a new record so we will just update the related columns
                        query = SQLUtility.GetISHUBDispenseUpdateQuery(FieldName);
                    }

                    if (DatabaseName == "PioneerRX")
                    {
                        if (FieldName == "TotalPriceSubmitted")
                        {//Field specific logic - TBD
                        }

                        var results = conn.Execute(query, new
                        {
                            RxTransactionID = RxTransactionID,
                            Original = Original,
                            Overlay = Overlay,
                            UpdateDate = DateTime.Now,
                            CHGBYHOST = CHGBYHOST
                        });

                        /* cannot update Master.Dispense directly because we dont have UpdateBy and UpdateDate columns in there for each specific column.
                        query = SQLUtility.GetISHUBDispenseUpdateQuery(FieldName, "[Master].[Dispense]");
                        var results2 = conn.Execute(query, new {RxTransactionID = RxTransactionID,Overlay = Overlay});
                        */
                    }
                    else if (DatabaseName == "CPRSQL")
                    {
                        var results = conn.Execute(query, new
                        {
                            RxTransactionID = RxTransactionID,
                            Original = Original,
                            Overlay = Overlay,
                            UpdateDate = DateTime.Now,
                            CHGBYHOST = CHGBYHOST
                        });
                    }
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "PioneerRxDispenseUpdate failed");
            }

            return Task.FromResult(result);
        }

        public Task<int> ISHUBGetRecordCount(string TableName, string ColumnName, string ColumnValue)
        {
            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                        
                    string query = SQLUtility.GetRecordCountQuery(TableName,ColumnName);
                        
                    var results = conn.Execute(query, new
                    {
                        TableName = TableName,
                        ColumnValue = ColumnValue,
                    });

                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "PioneerRxDispenseUpdate failed");
            }

            return Task.FromResult(result);
        }

        public Task<List<DocumentModel>> PioneerRXGetFedexShipmentTrackingNumber(string DatabaseName, string RXNO, string RxTransactionID)
        {
            List<DocumentModel> DocumentList = null;
            try
            {
                using (IDbConnection conn = PioneerRXSqlConnection)
                {
                    string Query = SQLUtility.PioneerRX_POINTOFSALE_SHIPMENT_SELECT;

                    conn.Open();

                    return Task.FromResult(conn.Query<DocumentModel>(Query, new { RxTransactionID = RxTransactionID }, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetProfitabilityAudit failed");
            }
            return Task.FromResult(DocumentList);
        }
        #endregion

        #region CPRSQL Profitability Editor Search
        public async Task<ViewCubeNCPDPModel> GetByLABLOGNO(string LABLOGNO)
        {
            ViewCubeNCPDPModel EmptyResult = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQL_FN_CUBE_NCPDP_FUNCTION_SELECT;
                    conn.Open();
                    var result = await conn.QueryAsync<ViewCubeNCPDPModel>(Query, new { LABLOGNO = LABLOGNO });
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetByLABLOGNO failed");
            }

            return await Task.FromResult(EmptyResult);
            //return await Task.Factory.StartNew(() => EmptyResult);
        }

        public async Task<List<ProfitabilityNCPDPModel>> GetByRXNO(string RXNO, string SearchType, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            List<ProfitabilityNCPDPModel> ModelList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQLProfitabilityReportSearchByRXNOQuery(RXNO, SearchType, SelectedDateSearch, SearchStartDate, SearchEndDate);
                    conn.Open();
                    ModelList = await Task.FromResult(conn.Query<ProfitabilityNCPDPModel>(Query, new { RXNO = RXNO }, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }

                ModelList = ProfitabilityReportApplyCSS(ModelList, SearchType);

                return await Task.FromResult(ModelList);
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetByRXNO failed");
            }
            return await Task.FromResult(ModelList);
        }

        public async Task<List<ProfitabilityNCPDPModel>> GetByDateFilled(string SearchType, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            List<ProfitabilityNCPDPModel> EmptyResult = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = string.Format(SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_DATEFILLED_FUNCTION, SearchStartDate.ToShortDateString(), SearchEndDate.ToShortDateString());

                    conn.Open();

                    return await Task.FromResult(conn.Query<ProfitabilityNCPDPModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetByRXNO failed");
            }
            return await Task.FromResult(EmptyResult);
        }

        public async Task<List<ProfitabilityNCPDPModel>> ProfitabilityReportEditor(string SearchType, string SearchMode, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            List<ProfitabilityNCPDPModel> ModelList = null;

            string Query = SQLUtility.GetProfitabilityQuery(SearchType, SearchMode, IsCSVNumbers, SearchValues, SelectedDateSearch, SearchStartDate, SearchEndDate);

            DataTable ResultTable = SaveToDataTable(Query);

            foreach (DataRow row in ResultTable.Rows)
            {
                ProfitabilityNCPDPModel model = new ProfitabilityNCPDPModel();
                model.ncpdp_rx_number = row["ncpdp_rx_number"].ToString();
                model.ncpdp_invoice_number = row.Field<int>("ncpdp_invoice_number");
                model.TICKNO = row.Field<int>("TICKNO");
                model.ncpdp_date_filled_timestamp = row.Field<DateTime?>("ncpdp_date_filled_timestamp");
                model.ticket_confirmation_date = row.Field<DateTime?>("ticket_confirmation_date");
                model.ncpdp_quantity_new_refill_code = row["ncpdp_quantity_new_refill_code"].ToString();
                model.ticket_quantity = row.Field<int>("ticket_quantity");
                model.drug_name = row["drug_name"].ToString();
                model.ncpdp_primary_claim_payor = row["ncpdp_primary_claim_payor"].ToString();
                model.primary_payor_type = row["primary_payor_type"].ToString();
                model.Copay = row.Field<Decimal?>("Copay");
                model.TPRevenuePlusPatientCopay = row.Field<Decimal?>("TPRevenuePlusPatientCopay");
                model.COGS = row.Field<Decimal?>("COGS");
                model.COGSAdjusted = row.Field<Decimal?>("COGSAdjusted");
                model.GrossProfit = row.Field<Decimal?>("GrossProfit");
                model.PrimaryTPHistory = row["PrimaryTPHistory"].ToString();
                model.SecondaryTPHistory = row["SecondaryTPHistory"].ToString();
                model.CopayHistory = row["CopayHistory"].ToString();
                model.COGSHistory = row["COGSHistory"].ToString();
                model.RecordDeletedHistory = row["RecordDeletedHistory"].ToString();
                model.ticket_item_delivins = row["RecordDeletedHistory"].ToString();
            }

            ModelList = ProfitabilityReportApplyCSS(ModelList, SearchType);

            return await Task.FromResult(ModelList);
        }

        public async Task<List<ProfitabilityNCPDPModel>> GetProfitabilityReport(string SearchType, string SearchMode, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            List<ProfitabilityNCPDPModel> ModelList = new List<ProfitabilityNCPDPModel>();

            string Query = SQLUtility.GetProfitabilityQuery(SearchType, SearchMode, IsCSVNumbers, SearchValues, SelectedDateSearch, SearchStartDate, SearchEndDate);
            
            try
            {
                using (SqlConnection conn = CreateSqlConnection())
                {

                    SqlCommand cmd = new SqlCommand(Query, conn);
                    conn.Open();
                    
                    cmd.CommandText = Query;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 1800; // 30 minutes

                    SqlDataReader reader = cmd.ExecuteReader();

                    // Call Read before accessing data.
                    while (reader.Read())
                    {
                        ProfitabilityNCPDPModel model = new ProfitabilityNCPDPModel();

                        /*
                        model.ncpdp_invoice_site_name = (reader["ncpdp_invoice_site_name"] as string) ?? "";
                        model.ncpdp_invoice_datebilled_timestamp = reader["Date Billed"] as DateTime?;
                        model.ncpdp_invoice_date_of_service_timestamp = reader["Date of Service"] as DateTime?;

                        //Inventory_unit
                        //Package_size
                        //Compound
                        model.patient_first_name = (reader["Patient First Name"] as string) ?? "";
                        model.patient_last_name = (reader["Patient Last Name"] as string) ?? "";

                        model.physician_last_name = (reader["Prescriber Last Name"] as string) ?? "";
                        model.physician_first_name = (reader["Prescriber First Name"] as string) ?? "";
                        model.patient_referral_organization = (reader["Referral Organization"] as string) ?? "";
                        //ncpdp_primary_inventory_item_id
                        model.ncpdp_revenue_status = (reader["ncpdp_revenue_status"] as string) ?? "";
                        model.ncpdp_claim_status = (reader["ncpdp_claim_status_description"] as string) ?? "";
                        model.ncpdp_days_supply = (reader["ncpdp_days_supply"] as string) ?? "";
                        model.primary_payor_binno = (reader["Payor Bin"] as string) ?? "";

                        //inventory_item_category
                        model.ncpdp_rx_origin_description = (reader["ncpdp_rx_origin_description"] as string) ?? "";
                        //rxorigin 
                        //gname
                        model.invoice_therapy = (reader["Therapy Type"] as string) ?? "";
                        model.ncpdp_daw_yn = (reader["DAW  Specialty"] as string) ?? "";
                        //NIOSH
                        //EPA
                        //DEA_SCHEDULE
                        */

                        // Main Grid Columns
                        model.ncpdp_rx_number = (reader["ncpdp_rx_number"] as string) ?? "";
                        model.ncpdp_invoice_number = (reader["ncpdp_invoice_number"] as int?) ?? 0;
                        model.TICKNO = (reader["TICKNO"] as int?) ?? 0;
                        model.ncpdp_date_filled_timestamp = reader["ncpdp_date_filled_timestamp"] as DateTime?;
                        model.ticket_confirmation_date = reader["ticket_confirmation_date"] as DateTime? ;
                        model.ncpdp_quantity_new_refill_code = (reader["ncpdp_quantity_new_refill_code"] as string) ?? "";
                        model.ticket_quantity = (reader["ticket_quantity"] as decimal?) ?? Decimal.Zero;
                        model.drug_name = (reader["ncpdp_drug_name"] as string) ?? "";
                        model.ncpdp_primary_claim_payor = (reader["ncpdp_primary_claim_payor"] as string) ?? "";
                        model.primary_payor_type = (reader["primary_payor_type"] as string) ?? "";
                        model.Copay = (reader["Copay"] as decimal?) ?? Decimal.Zero;
                        model.TPRevenuePlusPatientCopay = (reader["TPRevenuePlusPatientCopay"] as decimal?) ?? Decimal.Zero;
                        model.COGS = (reader["COGS"] as decimal?) ?? Decimal.Zero;
                        model.COGSAdjusted = (reader["COGSAdjusted"] as decimal?) ?? Decimal.Zero;
                        model.GrossProfit = (reader["GrossProfit"] as decimal?) ?? Decimal.Zero;
                        model.GrossProfitAdjusted = (reader["GrossProfitAdjusted"] as decimal?) ?? Decimal.Zero;
                        model.PrimaryTPHistory = (reader["PrimaryTPHistory"] as string) ?? "";
                        model.SecondaryTPHistory = (reader["SecondaryTPHistory"] as string) ?? "";
                        model.CopayHistory = (reader["CopayHistory"] as string) ?? "";
                        model.COGSHistory = (reader["COGSHistory"] as string) ?? "";
                        model.RecordDeletedHistory = (reader["RecordDeletedHistory"] as string) ?? "";
                        model.ticket_item_delivins = (reader["ticket_item_delivins"] as string) ?? "";

                        model.LABLOGNO = (reader["LABLOGNO"] as int?) ?? 0;
                        model.ncpdp_ndc = (reader["ncpdp_ndc"] as string) ?? "";
                        model.ncpdp_primary_claim_sys_id = (reader["ncpdp_primary_claim_sys_id"] as int?) ?? 0;
                        model.ncpdp_primary_claim_billed = (reader["ncpdp_primary_claim_billed"] as decimal?) ?? Decimal.Zero;
                        model.ncpdp_primary_claim_expected = (reader["ncpdp_primary_claim_expected"] as decimal?) ?? Decimal.Zero;
                        model.ncpdp_primary_claim_paid_response_sys_id = (reader["ncpdp_primary_claim_paid_response_sys_id"] as int?) ?? 0;
                        model.ncpdp_primary_claim_paid = (reader["ncpdp_primary_claim_paid"] as decimal?) ?? Decimal.Zero;

                        model.ncpdp_secondary_claim_sys_id = (reader["ncpdp_secondary_claim_sys_id"] as int?) ?? 0;
                        model.ncpdp_secondary_claim_billed = (reader["ncpdp_secondary_claim_billed"] as decimal?) ?? Decimal.Zero;
                        model.ncpdp_secondary_claim_expected = (reader["ncpdp_secondary_claim_expected"] as decimal?) ?? Decimal.Zero;
                        model.ncpdp_secondary_claim_paid_response_sys_id = (reader["ncpdp_secondary_claim_paid_response_sys_id"] as int?) ?? 0;
                        model.ncpdp_secondary_claim_paid = (reader["ncpdp_secondary_claim_paid"] as decimal?) ?? Decimal.Zero;

                        model.ncpdp_patient_copay_expected_response_sys_id = (reader["ncpdp_patient_copay_expected_response_sys_id"] as int?) ?? 0;
                        model.ncpdp_patient_copay_expected = (reader["ncpdp_patient_copay_expected"] as decimal?) ?? Decimal.Zero;
                        //model.ncpdp_patient_secondary_copay_expected_response_sys_id = (reader["ncpdp_patient_secondary_copay_expected_response_sys_id"] as int?) ?? 0;
                        //model.ncpdp_patient_secondary_copay_expected = (reader["ncpdp_patient_secondary_copay_expected"] as decimal?) ?? Decimal.Zero;
                        //model.ncpdp_other_secondary_copay_expected_response_sys_id = (reader["ncpdp_other_secondary_copay_expected_response_sys_id"] as int?) ?? 0;
                        //model.ncpdp_other_secondary_copay_expected = (reader["ncpdp_other_secondary_copay_expected"] as decimal?) ?? Decimal.Zero;

                        //model.ncpdp_patient_paid = (reader["ncpdp_patient_paid"] as decimal?) ?? Decimal.Zero;
                        //model.ncpdp_claim_adjustment = (reader["ncpdp_claim_adjustment"] as decimal?) ?? Decimal.Zero;

                        //model.ncpdp_ncpdp_cost = (reader["ncpdp_ncpdp_cost"] as decimal?) ?? Decimal.Zero;
                        //model.ncpdp_ticket_cost = (reader["ncpdp_ticket_cost"] as decimal?) ?? Decimal.Zero;

                        model.patient_mrn = (reader["patient_mrn"] as string) ?? "";
                        //model.ncpdp_invoice_expected = (reader["ncpdp_invoice_expected"] as decimal?) ?? Decimal.Zero;
                        //model.ncpdp_invoice_billed = (reader["ncpdp_invoice_billed"] as decimal?) ?? Decimal.Zero;

                        model.PrimaryTPPaid_chgbyhost = (reader["PrimaryTPPaid_chgbyhost"] as string) ?? "";
                        model.SecondaryTPPaid_chgbyhost = (reader["SecondaryTPPaid_chgbyhost"] as string) ?? "";
                        model.Copay_chgbyhost = (reader["Copay_chgbyhost"] as string) ?? "";
                        model.ticket_cost_chgbyhost = (reader["ticket_cost_chgbyhost"] as string) ?? "";

                        model.ticket_item_change = (reader["ticket_item_change"] as string) ?? "";
                        model.ticket_item_chgbyhost = (reader["ticket_item_chgbyhost"] as string) ?? "";
                        model.ticket_item_delflag_overlay = (reader["ticket_item_delflag_overlay"] as int?) ?? 0;
                        model.ticket_partials_name = (reader["ticket_partials_name"] as string) ?? "";
                        model.ticket_total_cost = (reader["ticket_total_cost"] as decimal?) ?? Decimal.Zero;
                        model.vatext = (reader["vatext"] as string) ?? "";

                        model.ROWNUM = int.Parse(reader["ROWNUM"].ToString());
                        model.PrimaryTPUpdated = (reader["PrimaryTPUpdated"] as int?) ?? 0;
                        model.SecondaryTPUpdated = (reader["SecondaryTPUpdated"] as int?) ?? 0;
                        model.CopayUpdated = (reader["CopayUpdated"] as int?) ?? 0;
                        model.COGSUpdated = (reader["COGSUpdated"] as int?) ?? 0;
                        model.RecordDeleted = (reader["RecordDeleted"] as int?) ?? 0;

                        ModelList.Add(model);
                    }

                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetProfitabilityReport failed");
            }

            double SearchDays = 0;
            if (SelectedDateSearch.ToUpper() != Constants.NONE)
            {
                SearchDays = (SearchEndDate - SearchStartDate).TotalDays;
            }

            if (SearchDays <= 60)
            ModelList = ProfitabilityReportApplyCSS(ModelList, SearchType);

            return await Task.FromResult(ModelList);
        }

        public async Task<List<ProfitabilityNCPDPModel>> GetProfitability(string SearchType,string SearchMode, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            List<ProfitabilityNCPDPModel> ModelList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.GetProfitabilityQuery(SearchType, SearchMode, IsCSVNumbers, SearchValues, SelectedDateSearch, SearchStartDate, SearchEndDate);

                    conn.Open();
                    ModelList = await Task.FromResult(conn.Query<ProfitabilityNCPDPModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }

                ModelList = ProfitabilityReportApplyCSS(ModelList, SearchType);

                return await Task.FromResult(ModelList);
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetByRXNO failed");
            }
            return await Task.FromResult(ModelList);
        }
        
        public async Task<List<ProfitabilityNCPDPModel>> GetByINVOICENO(string INVOICENO)
        {
            List<ProfitabilityNCPDPModel> EmptyResult = null;

            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_INVOICENO_FUNCTION_SELECT;
                    conn.Open();

                    return await Task.FromResult(conn.Query<ProfitabilityNCPDPModel>(Query, new { INVOICENO = INVOICENO }, commandType: CommandType.Text).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetByINVOICENO failed");
            }
            return await Task.FromResult(EmptyResult);
        }

        public async Task<List<ProfitabilityNCPDPModel>> GetByTICKNO(string TICKNO)
        {
            List<ProfitabilityNCPDPModel> EmptyResult = null;

            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_TICKNO_FUNCTION_SELECT;
                    conn.Open();

                    return await Task.FromResult(conn.Query<ProfitabilityNCPDPModel>(Query, new { TICKNO = TICKNO }, commandType: CommandType.Text).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetByTICKNO failed");
            }
            return await Task.FromResult(EmptyResult);
        }

        public async Task<List<ProfitabilityNCPDPModel>> GetByMRN(string MRN)
        {
            List<ProfitabilityNCPDPModel> EmptyResult = null;

            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_MRN_FUNCTION_SELECT;
                    conn.Open();

                    return await Task.FromResult(conn.Query<ProfitabilityNCPDPModel>(Query, new { MRN = MRN }, commandType: CommandType.Text).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetByMRN failed");
            }
            return await Task.FromResult(EmptyResult);
        }

        public Task<List<AuditModel>> GetProfitabilityAudit(string RXNO, int INVOICENO)
        {
            List<AuditModel> AuditList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = SQLUtility.ISHUB_FN_PROFITABILITY_AUDIT_FUNCTION_SELECT;

                    conn.Open();

                    return Task.FromResult(conn.Query<AuditModel>(Query, new { RXNO = RXNO, INVOICENO = INVOICENO }, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetProfitabilityAudit failed");
            }
            return Task.FromResult(AuditList);
        }

        public async Task<List<ProfitabilityNCPDPModel>> GetProfitabilityTicketItem(int INVOICENO, int TICKNO)
        {
            List<ProfitabilityNCPDPModel> ModelList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQL_VW_TICKET_ITEM_VIEW_SELECT;

                    conn.Open();

                    ModelList = await Task.FromResult(conn.Query<ProfitabilityNCPDPModel>(Query, new { INVOICENO = INVOICENO, TICKNO = TICKNO }, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetProfitabilityTicketItem failed");
            }
            return await Task.FromResult(ModelList);
        }

        #endregion

        #region CPRSQL Profitability Editor Update

        public Task<int> DataWarehouse_NCPDP_Adjustment_Update(string RXNO, int BILLNO, int TICKNO, decimal? ncpdp_primary_claim_paid, int ncpdp_primary_claim_paid_response_sys_id, decimal? ncpdp_secondary_claim_paid, int ncpdp_secondary_claim_paid_response_sys_id, decimal? Copay, int ncpdp_patient_primary_copay_expected_response_sys_id, decimal? COGS, decimal? COGSAdjusted, decimal? TPRevenuePlusPatientCopay, decimal? GrossProfit, decimal?GrossProfitAdjusted, int CHGFLAG, string History,string PrimaryTPHistory,string SecondaryTPHistory,string CopayHistory,string COGSHistory,string RecordDeletedHistory, string Events, int ticket_number_partials, int? ticket_item_delflag_overlay)
        {
            int result = 0;

            try
            {
                //using (IDbConnection conn = ISHUBSqlConnection)
                using (IDbConnection conn = ALPHAScriptSqlConnection)
                {
                    // If there are no events just write out the change history
                    var updateHistory = conn.Execute(SQLUtility.DataWarehouse_NCPDP_Adjustment_UpdateHistory, new
                    {
                        RXNO = RXNO,
                        BILLNO = BILLNO,
                        TICKNO = TICKNO,
                        CHGFLAG = CHGFLAG,
                        history = History,
                        CHG_PrimaryTP = PrimaryTPHistory,
                        CHG_SecondaryTP = SecondaryTPHistory,
                        CHG_Copay = CopayHistory,
                        CHG_RecordDeleted = RecordDeletedHistory,
                        CHG_COGS = COGSHistory
                    });
                    result = 1;

                    if (Events.IndexOf("PrimaryTPPaid") != -1)
                    {
                        result = -2;
                        var updatePrimaryTPPaid = conn.Execute(SQLUtility.DataWarehouse_NCPDP_Adjustment_UpdatePrimaryTPPaid, new
                        {
                            RXNO = RXNO,
                            BILLNO = BILLNO,
                            response_sys_id = ncpdp_primary_claim_paid_response_sys_id,
                            ncpdp_primary_claim_paid = ncpdp_primary_claim_paid,
                            TPRevenuePlusPatientCopay = TPRevenuePlusPatientCopay,
                            GrossProfit = GrossProfit,
                            GrossProfitAdjusted = GrossProfitAdjusted,  //calculated
                            CHG_PrimaryTP = PrimaryTPHistory
                        });
                    }
                    result = 2;

                    if (Events.IndexOf("SecondaryTPPaid") != -1)
                    {
                        result = -3;
                        var updateSecondaryTPPaid = conn.Execute(SQLUtility.DataWarehouse_NCPDP_Adjustment_UpdateSecondaryTPPaid, new
                        {
                            RXNO = RXNO,
                            BILLNO = BILLNO,
                            response_sys_id = ncpdp_secondary_claim_paid_response_sys_id,
                            ncpdp_secondary_claim_paid = ncpdp_secondary_claim_paid,
                            TPRevenuePlusPatientCopay = TPRevenuePlusPatientCopay,
                            GrossProfit = GrossProfit,
                            GrossProfitAdjusted = GrossProfitAdjusted,
                            CHG_SecondaryTP = SecondaryTPHistory
                        });
                    }
                    result = 3;

                    if (Events.IndexOf("Copay") != -1)
                    {
                        result = -4;
                        var updateCopay = conn.Execute(SQLUtility.DataWarehouse_NCPDP_Adjustment_UpdateCopay, new
                        {
                            RXNO = RXNO,
                            BILLNO = BILLNO,
                            response_sys_id = ncpdp_patient_primary_copay_expected_response_sys_id,
                            Copay = Copay,
                            TPRevenuePlusPatientCopay = TPRevenuePlusPatientCopay,
                            GrossProfit = GrossProfit,
                            GrossProfitAdjusted = GrossProfitAdjusted,
                            CHG_Copay = CopayHistory
                        });
                    }
                    
                    result = 4;

                    if (Events.IndexOf("Delete") != -1)
                    {
                        result = -5;

                        var deleteTicketRow = conn.Execute(SQLUtility.DataWarehouse_NCPDP_Adjustment_Delete, new
                        {
                            RXNO = RXNO,
                            BILLNO = BILLNO,
                            TICKNO = TICKNO,
                            DELFLAG = ticket_item_delflag_overlay,
                            CHG_RecordDeleted = RecordDeletedHistory
                        });
                    }
                    result = 5;

                    if (Events.IndexOf("Cost") != -1)
                    {
                        result = -6;
                        var updateCost = conn.Execute(SQLUtility.DataWarehouse_NCPDP_Adjustment_UpdateCost, new
                        {
                            RXNO = RXNO,
                            BILLNO = BILLNO,
                            TICKNO = TICKNO,
                            COGS = COGS,
                            COGSAdjusted = COGSAdjusted,
                            GrossProfit = GrossProfit,
                            GrossProfitAdjusted = GrossProfitAdjusted,
                            CHG_COGS = COGSHistory
                        });
                    }
                    result = 6;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "DataWarehouse_NCPDP_Adjustment_Update failed");
            }

            return Task.FromResult(result);
        }

        // This call is obsolete. It can be used to update the ticket unit cost.
        public Task<int> TICKC_UnitCost(ProfitabilityNCPDPModel entity)
        {
            var Params = new DynamicParameters();
            Params.Add("RXNO", entity.ncpdp_rx_number);
            Params.Add("BILLNO", entity.ncpdp_invoice_number);
            Params.Add("COST", entity.ticket_unit_cost, DbType.Decimal);
            Params.Add("CHGBYHOST", entity.UpdatedBy, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[TICKC_UpdateUnitCost]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[TICKC_UpdateCosts] failed");
            }

            return Task.FromResult(result);
        }

        public Task<int> TICKC_Update(string RXNO, int BILLNO, int TICKNO, decimal? COST, decimal? COST_OVERLAY,decimal? COGS, decimal? ncpdp_primary_claim_paid, decimal? ncpdp_secondary_claim_paid, decimal? Copay, string ncpdp_primary_claim_payor, string vatext, string CHGBYHOST)
        {
            var Params = new DynamicParameters();
            Params.Add("RXNO", RXNO);
            Params.Add("BILLNO", BILLNO);
            Params.Add("TICKNO", TICKNO);
            Params.Add("COST", COST, DbType.Decimal);
            Params.Add("COST_OVERLAY", COST_OVERLAY, DbType.Decimal);
            Params.Add("COGS", COGS, DbType.Decimal);
            Params.Add("f9_total_paid_pri", ncpdp_primary_claim_paid, DbType.Decimal);
            Params.Add("f9_total_paid_sec", ncpdp_secondary_claim_paid, DbType.Decimal);
            Params.Add("f5_patient_pay_pri", Copay, DbType.Decimal);
            Params.Add("insurance_name", ncpdp_primary_claim_payor, DbType.String);
            Params.Add("vatext", vatext, DbType.String);
            Params.Add("CHGBYHOST", CHGBYHOST, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[TICKC_UpdateCosts]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[TICKC_UpdateCosts] failed");
            }

            return Task.FromResult(result);
        }

        public Task<int> INVOICES_UpdateRenenueTotalExpected(string RXNO, int CPK_INVOICES,int TICKNO, decimal? EXPECTED, decimal? EXPECTED_OVERLAY, string CHGBYHOST)
        {
            var Params = new DynamicParameters();
            Params.Add("RXNO", RXNO);
            Params.Add("CPK_INVOICES", CPK_INVOICES);
            Params.Add("TICKNO", TICKNO);
            Params.Add("EXPECTED", EXPECTED, DbType.Decimal);
            Params.Add("EXPECTED_OVERLAY", EXPECTED_OVERLAY, DbType.Decimal);
            Params.Add("CHGBYHOST",CHGBYHOST, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[INVOICES_UpdateRevenueTotalExpected]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[INVOICES_UpdateRevenueTotalExpected failed");
            }

            return Task.FromResult(result);
        }

        public Task<int> LINEITEMS_Update(ProfitabilityNCPDPModel entity)
        {
            var Params = new DynamicParameters();
            Params.Add("RXNO", entity.ncpdp_rx_number);
            Params.Add("BILLNO", entity.ncpdp_invoice_number);
            Params.Add("REVENUE_TOTAL_EXPECTED", entity.ncpdp_invoice_expected, DbType.Decimal);
            Params.Add("CHGBYHOST", entity.UpdatedBy, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[LINEITEMS_UpdateRevenueTotalExpected]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[LINEITEMS_UpdateRevenueTotalExpected failed");
            }

            return Task.FromResult(result);
        }

        // SHORT_VAL contains an encypted value which we cannot decrypt so we need to save the decrypted value from UI
        public Task<int> NCPDPREP_Update(string RXNO,int BILLNO, int TICKNO, int CPK_NCPDPREPS, decimal? SHORT_VAL, decimal? SHORT_VAL_OVERLAY,string CHG, string CHGBYHOST)
        {
            var Params = new DynamicParameters();
            Params.Add("RXNO", RXNO);
            Params.Add("BILLNO", BILLNO);
            Params.Add("TICKNO", TICKNO);
            Params.Add("CPK_NCPDPREPS", CPK_NCPDPREPS);
            Params.Add("SHORT_VAL", SHORT_VAL);
            Params.Add("SHORT_VAL_OVERLAY", SHORT_VAL_OVERLAY);
            Params.Add("CHG", CHG, DbType.String);  // FieldName that is changing 
            Params.Add("CHGBYHOST", CHGBYHOST, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[NCPDPREPS_UpdateCosts]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[NCPDPREPS_UpdateCosts failed");
            }

            return Task.FromResult(result);
        }

        public Task<int> CLAIMS_PrimaryClaimUpdate(ProfitabilityNCPDPModel entity)
        {
            var Params = new DynamicParameters();
            Params.Add("RXNO", entity.ncpdp_rx_number);
            Params.Add("CPK_CLAIMS", entity.ncpdp_primary_claim_sys_id);
            Params.Add("claim_billed", entity.ncpdp_primary_claim_billed, DbType.Decimal);
            Params.Add("CHG", "PrimaryClaim", DbType.String);
            Params.Add("CHGBYHOST", entity.UpdatedBy, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[CLAIMS_UpdateCosts]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[CLAIMS_UpdateCosts failed");
            }

            return Task.FromResult(result);
        }

        public Task<int> CLAIMS_SecondaryClaimUpdate(ProfitabilityNCPDPModel entity)
        {
            var Params = new DynamicParameters();
            Params.Add("RXNO", entity.ncpdp_rx_number);
            Params.Add("CPK_CLAIMS", entity.ncpdp_secondary_claim_sys_id);
            Params.Add("claim_billed", entity.ncpdp_secondary_claim_billed, DbType.Decimal);
            Params.Add("CHG", "SecondaryClaim", DbType.String);
            Params.Add("CHGBYHOST", entity.UpdatedBy, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[CLAIMS_UpdateCosts]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[CLAIMS_UpdateCosts Secondary failed");
            }

            return Task.FromResult(result);
        }

        // Not used
        public Task<int> Update(ProfitabilityNCPDPModel entity)
        {
            var Params = new DynamicParameters();
            Params.Add("LABLOGNO", entity.LABLOGNO);
            Params.Add("BILLNO", entity.ncpdp_invoice_number);
            Params.Add("ticket_cost", entity.ncpdp_ticket_cost, DbType.Decimal);
            Params.Add("revenue_total_expected", entity.ncpdp_invoice_expected, DbType.Decimal);
            Params.Add("patient_copay_expected", entity.ncpdp_invoice_expected, DbType.Decimal);
            Params.Add("patient_copay_expected_response_sys_id", entity.ncpdp_invoice_expected, DbType.Int32);
            Params.Add("revenue_total_expected", entity.ncpdp_invoice_expected, DbType.Decimal);
            Params.Add("primary_claim_paid", entity.ncpdp_invoice_expected, DbType.Decimal);
            Params.Add("primary_claim_paid_response_sys_id", entity.ncpdp_invoice_expected, DbType.Int32);
            Params.Add("CHGBYHOST", entity.UpdatedBy, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[NCPDPSearch_UpdateCosts]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[spNCPDPSearch_UpdateCosts failed");
            }

            return Task.FromResult(result);
        }
        #endregion

        #region Profitability Report Partials
        private List<ProfitabilityNCPDPModel> ProfitabilityReportApplyCSS(List<ProfitabilityNCPDPModel> ModelList, string SearchType)
        {
            /*

            for (int i = 0; i < ModelList.Count; i++)
            {
            if (ModelList[i].TICKNO == 281005) System.Diagnostics.Debug.WriteLine("DELFLAG_OVERLAY = " + ModelList[i].ticket_item_delflag_overlay + " TicketSelected = " + ModelList[i].TicketSelected.ToString());
            }
            */

            bool InvoiceGroupingFlag = false;
            var duplicates_invoices = ModelList.GroupBy(x => x.ncpdp_invoice_number).Where(g => g.Count() > 1).Select(y => y.Key);

            if ((ModelList != null) && (ModelList.Count > 0))
            {
                IDictionary<int, string> Invoices = new Dictionary<int, string>();

                for (int i = 0; i < ModelList.Count; i++)
                {
                    if (SearchType == "Partials")
                    {
                        ModelList[i].drug_name = ModelList[i].ticket_item_name;
                    }
                    else
                    {
                        //ModelList[i].drug_name = ModelList[i].ncpdp_drug_name;
                        ModelList[i].drug_name = ModelList[i].ticket_partials_name;
                    }

                    if (!Invoices.ContainsKey(ModelList[i].ncpdp_invoice_number))
                    {
                        // Invoices.Add(ModelList[i].ncpdp_invoice_number, ModelList[i].TICKNO.ToString() + ":" + ModelList[i].ticket_confirmation_date.Value.Date.ToString()); ticket confirmation date can be null so no need to use it
                        Invoices.Add(ModelList[i].ncpdp_invoice_number, ModelList[i].TICKNO.ToString());
                        InvoiceGroupingFlag = !InvoiceGroupingFlag;
                    }

                    if (ModelList[i].ticket_item_delflag_overlay == 1)
                    {
                        ModelList[i].TicketSelected = true;
                        //ModelList[i].CssClassName = (InvoiceGroupingFlag == true) ? "table-active strikeout" : "strikeout";
                    }
                    else
                    {
                        //ModelList[i].CssClassName = (InvoiceGroupingFlag == true) ? "table-active" : "";
                    }

                    if (duplicates_invoices.Contains(ModelList[i].ncpdp_invoice_number))
                    {
                        ModelList[i].IsDuplicate = true;
                    }
                }
            }

            /*
            var duplicates_invoices = ModelList.GroupBy(x => x.ncpdp_invoice_number).Where(g => g.Count() > 1).Select(y => y.Key);
            foreach (var invoice_number in duplicates_invoices)
            {
            }
            */

            return ModelList;
        }

        public async Task<List<ProfitabilityNCPDPModel>> GetProfitabilityReportDuplicate(string SearchType, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate)
        {
            List<ProfitabilityNCPDPModel> ModelList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQLProfitabilityReportDuplicateQuery(SearchType, SelectedDateSearch, SearchStartDate, SearchEndDate);
                    conn.Open();
                    ModelList = await Task.FromResult(conn.Query<ProfitabilityNCPDPModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }

                ModelList = ProfitabilityReportApplyCSS(ModelList, SearchType);

                return await Task.FromResult(ModelList);
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetByRXNO failed");
            }
            return await Task.FromResult(ModelList);
        }

        public Task<int> TICKCI_UpdateDelFlag(ProfitabilityNCPDPModel entity)
        {
            var Params = new DynamicParameters();
            Params.Add("RXNO", entity.ncpdp_rx_number);
            Params.Add("MRN", entity.patient_mrn);
            Params.Add("BILLNO", entity.ncpdp_invoice_number);
            Params.Add("TICKNO", entity.TICKNO, DbType.Int32);
            Params.Add("DELFLAG", entity.ticket_item_delflag_overlay, DbType.Int32);
            Params.Add("CHGBYHOST", entity.UpdatedBy, DbType.String);

            int result = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    IEnumerable<int> results = conn.Query<int>("[dbo].[TICKCI_UpdateDelFlag]", Params, commandType: CommandType.StoredProcedure);
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "[dbo].[TICKC_UpdateCosts] failed");
            }

            return Task.FromResult(result);
        }

        #endregion

        #region Document Management CPRSQL
        public async Task<List<FedExCPRSQLDispenseModel>> CPRSQLFedExSearchDispense(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx)
        {
            //string[] numbers = SearchValues.Split(",");
            List<FedExCPRSQLDispenseModel> FedExCPRSQLDispenseList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQLFedExSearchDispenseQuery(SelectedSearchType, IsCSVNumbers, SearchValues, SelectedDateSearch, SearchStartDate, SearchEndDate, this._appSettings.CPRPlusDocumentDownloadStartDatePart, ShippingMethodType);

                    conn.Open();

                    return await Task.FromResult(conn.Query<FedExCPRSQLDispenseModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetFedExSearchDispenseQuery failed");
            }
            return await Task.FromResult(FedExCPRSQLDispenseList);
        }

        public async Task<List<FedExCPRSQLDispenseModel>> CPRSQLFedExSearchTicket(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx)
        {
            List<FedExCPRSQLDispenseModel> FedExCPRSQLDispenseList = null;
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = string.Empty;

                    Query = SQLUtility.CPRSQL_TB_CUBE_TICKET_VIEW_SELECT + SQLUtility.WHERE + SQLUtility.CPRSQL_TB_CUBE_TICKET_FILTER_FEDEX;
                    Query += SQLUtility.AND + SQLUtility.CPRSQL_TB_CUBE_TICKET_FILTER_TICKET_CONFIRMATION_DATE;

                    conn.Open();

                    return await Task.FromResult(conn.Query<FedExCPRSQLDispenseModel>(Query, new { DispenseDate = DateTime.Parse(SelectedDateSearch) }, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetFedExSearchDispenseQuery failed");
            }
            return await Task.FromResult(FedExCPRSQLDispenseList);
        }

        public async Task<FedExCPRSQLDataModel> CPRSQLFedExSearch(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx, bool Merge = true)
        {
            //string[] numbers = SearchValues.Split(",");
            FedExCPRSQLDataModel FedExCPRSQLData = new FedExCPRSQLDataModel();
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQLFedExSearchDispenseQuery(SelectedSearchType, IsCSVNumbers, SearchValues, SelectedDateSearch, SearchStartDate, SearchEndDate, this._appSettings.CPRPlusDocumentDownloadStartDatePart, ShippingMethodType);

                    conn.Open();

                    FedExCPRSQLData.FedExCPRSQLDispenseList = conn.Query<FedExCPRSQLDispenseModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList();

                    IDictionary<int, string> Tickets = new Dictionary<int, string>();
                   
                    for (int i = 0; i < FedExCPRSQLData.FedExCPRSQLDispenseList.Count; i++)
                    {
                        if (FedExCPRSQLData.FedExCPRSQLDispenseList[i].DELIVINS_TICKNO.HasValue)
                        {
                            if (!Tickets.ContainsKey(FedExCPRSQLData.FedExCPRSQLDispenseList[i].DELIVINS_TICKNO.Value)) Tickets.Add(FedExCPRSQLData.FedExCPRSQLDispenseList[i].DELIVINS_TICKNO.Value, FedExCPRSQLData.FedExCPRSQLDispenseList[i].ticket_sys_id.Value.ToString() + ":" + FedExCPRSQLData.FedExCPRSQLDispenseList[i].ndc + ":" +FedExCPRSQLData.FedExCPRSQLDispenseList[i].DELIVINS);
                        }
                    }

                    if ((Tickets == null) || (Tickets.Count == 0))
                    {
                        FedExCPRSQLData.FedExCPRSQLTicketList = new List<FedExCPRSQLDispenseModel>(); // do nothing set to an empty list and there is nothing to merge into our master list
                    }
                    else
                    {
                        string TicketNumberCSV = string.Join(",", Tickets.Select(x => x.Key).ToArray());

                        //Query = SQLUtility.CPRSQL_TB_CUBE_TICKET_VIEW_SELECT + SQLUtility.WHERE + string.Format(SQLUtility.CPRSQL_TB_CUBE_TICKET_IN_FILTER_TICKET_NUMBER, TicketNumberCSV);
                        Query = string.Format(SQLUtility.CPRSQL_TB_CUBE_TICKET_DOCUMENT_VIEW_SELECT, this._appSettings.CPRPlusDocumentDownloadStartDatePart) + SQLUtility.WHERE + string.Format(SQLUtility.CPRSQL_TB_CUBE_TICKET_IN_FILTER_TICKET_NUMBER, TicketNumberCSV);

                        FedExCPRSQLData.FedExCPRSQLTicketList = conn.Query<FedExCPRSQLDispenseModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList();

                        if ((Merge == true) && (FedExCPRSQLData.FedExCPRSQLTicketList != null) && (FedExCPRSQLData.FedExCPRSQLTicketList.Count > 0))
                        {
                            FedExCPRSQLDispenseModel Model = new FedExCPRSQLDispenseModel();

                            for (int i = 0; i < FedExCPRSQLData.FedExCPRSQLTicketList.Count; i++)
                            {
                                if ((FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_item_name == "Balance Completion") || (FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_item_name == "Balance Fulfillment"))
                                {
                                    Model = new FedExCPRSQLDispenseModel() { rx_number = FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_item_name };
                                    continue; // Do not show Balance Completion records for Redelivery
                                }
                                else if ((FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_item_name == "Redelivery"))
                                {
                                    Model = new FedExCPRSQLDispenseModel() { rx_number = FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_item_name };
                                }

                                Model.RecordType = 1; // highlight
                                Model.CssClassName = "table-warning";  // table-danger

                                string delivery_instructions;
                                if (Tickets.TryGetValue(FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_number.Value, out delivery_instructions))
                                {
                                    string[] instructions = delivery_instructions.Split(':');
                                    //Model.ticket_original_ticket_sys_id = int.Parse(instructions[0]);
                                    Model.ndc = instructions[1];
                                    Model.Note = instructions[2];
                                }
                                else
                                {
                                    Model.ndc = FedExCPRSQLData.FedExCPRSQLTicketList[i].inventory_ndc;
                                }

                                //Model.refillnum = "";
                                Model.ticket_invoice_number = FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_invoice_number;
                                Model.ticket_shipping_tracking_number = FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_shipping_tracking_number;
                                Model.ticket_sys_id = FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_number;
                                Model.ticket_service_type = FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_shipping_service_type;
                                Model.ticket_shipping_method = FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_shipping_method;
                                Model.patient_full_name = FedExCPRSQLData.FedExCPRSQLTicketList[i].patient_full_name; // todo not mapped in sql
                                Model.mrn = FedExCPRSQLData.FedExCPRSQLTicketList[i].patient_mrn;
                                Model.ticket_confirmation_date = FedExCPRSQLData.FedExCPRSQLTicketList[i].ticket_confirmation_date;
                                Model.dispense_date_timestamp = FedExCPRSQLData.FedExCPRSQLTicketList[i].DownloadDate;
                                Model.drug_name = FedExCPRSQLData.FedExCPRSQLTicketList[i].drug_name;
                                Model.ImageAvailability = FedExCPRSQLData.FedExCPRSQLTicketList[i].ImageAvailability;
                                Model.TrackStatus = FedExCPRSQLData.FedExCPRSQLTicketList[i].TrackStatus;
                                Model.DocumentDownloaded = FedExCPRSQLData.FedExCPRSQLTicketList[i].DocumentDownloaded;
                                Model.DownloadCode = FedExCPRSQLData.FedExCPRSQLTicketList[i].DownloadCode;
                                Model.Code = FedExCPRSQLData.FedExCPRSQLTicketList[i].Code;

                                FedExCPRSQLData.FedExCPRSQLDispenseList.Add(Model);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetFedExSearchDispenseQuery failed");
            }
            return await Task.FromResult(FedExCPRSQLData);
        }

        public List<string> ExtractNumberList(string csv)
        {
            string[] numbers = csv.Split(",");
            List<string> Numbers = new List<string>();
            foreach (string number in numbers)
            {
                Numbers.Add(number.Trim());
            }
            return Numbers;                
        }

        public async Task<List<DocumentModel>> DocumentSearch(string csv)
        {
            List<DocumentModel> DocumentList = null;

            List<string> RxNumbers = ExtractNumberList(csv);
            string SqlInFilter = string.Join(",", RxNumbers);

            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    string Query = SQLUtility.CPRSQL_FN_CUBE_NCPDP_SEARCH_TICKNO_FUNCTION_SELECT;
                    conn.Open();

                    return await Task.FromResult(conn.Query<DocumentModel>(Query, new { SqlInFilter = SqlInFilter }, commandType: CommandType.Text).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "DocumentSearch failed");
            }
            return await Task.FromResult(DocumentList);
        }

        public async Task<List<DocumentModel>> DocumentSearch(string TrackingNumberCSV, int DatabaseUID)
        {
            List<DocumentModel> DocumentList = null;

            List<string> TrackingNumbers = ExtractNumberList(TrackingNumberCSV);
            string SqlInFilter = string.Join(",", TrackingNumbers);

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = SQLUtility.ISHUB_DOCUMENT_TABLE_SELECT + SQLUtility.WHERE + string.Format(SQLUtility.ISHUB_DOCUMENT_IN_FILTER_TRACKING_NUMBER, SqlInFilter) + SQLUtility.AND + SQLUtility.ISHUB_DOCUMENT_FILTER_DATABASE;

                    conn.Open();

                    return await Task.FromResult(conn.Query<DocumentModel>(Query, new { DatabaseUID = DatabaseUID }, commandType: CommandType.Text).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "DocumentSearch failed");
            }
            return await Task.FromResult(DocumentList);
        }

        public async Task<List<DocumentModel>> DocumentRead(string TrackingNumber, int DatabaseUID)
        {
            List<DocumentModel> DocumentList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = string.Empty;
                    Query = SQLUtility.ISHUB_DOCUMENT_TABLE_SELECT + SQLUtility.WHERE + SQLUtility.ISHUB_DOCUMENT_FILTER_TRACKING_NUMBER + SQLUtility.AND + SQLUtility.ISHUB_DOCUMENT_FILTER_DATABASE;

                    conn.Open();

                    return await Task.FromResult(conn.Query<DocumentModel>(Query, new { TrackingNumber = TrackingNumber , DatabaseUID = DatabaseUID}, commandType: CommandType.Text).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "DocumentRead failed");
            }
            return await Task.FromResult(DocumentList);
        }

        public async Task<List<DocumentModel>> DocumentRead(Guid DocumentID, int DatabaseUID)
        {
            List<DocumentModel> DocumentList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = string.Empty;
                    Query = SQLUtility.ISHUB_DOCUMENT_TABLE_SELECT + SQLUtility.WHERE + SQLUtility.ISHUB_DOCUMENT_FILTER_DOCUMENTID + SQLUtility.AND + SQLUtility.ISHUB_DOCUMENT_FILTER_DATABASE;

                    conn.Open();

                    return await Task.FromResult(conn.Query<DocumentModel>(Query, new { DocumentID = DocumentID, DatabaseUID = DatabaseUID }, commandType: CommandType.Text).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "DocumentRead failed");
            }
            return await Task.FromResult(DocumentList);
        }

        public async Task<int> DocumentAuditInsertOld(Guid DocumentID, DateTime AccessDate,int AccessSource,int AccessType,string UserID,int DatabaseUID)
        {
            int EmptyResult = 0;

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = SQLUtility.ISHUB_DOCUMENT_AUDIT_INSERT_SP;
                    conn.Open();

                    var affectedRows = conn.Execute(Query,new { DocumentID = DocumentID, AccessDate = AccessDate, AccessSource = AccessSource, AccessType = AccessType, UserID = UserID, DatabaseUID = DatabaseUID }, commandType: CommandType.StoredProcedure);

                    return await Task.FromResult(affectedRows);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "DocumentAuditInsert failed");
            }
            return await Task.FromResult(EmptyResult);
        }

        public async Task<int> DocumentAuditInsert(Guid DocumentUID, DateTime AccessDate, int AccessSource, int AccessType, string UserID, int DatabaseUID)
        {
            int DocumentID = 0;
            var Params = new DynamicParameters();
            Params.Add("DocumentID", DocumentID, DbType.Int32, ParameterDirection.Output);
            Params.Add("DocumentUID", DocumentUID, DbType.Guid);
            Params.Add("AccessDate", AccessDate, DbType.DateTime);
            Params.Add("AccessSource", AccessSource, DbType.Int32);
            Params.Add("AccessType", AccessType, DbType.Int32);
            Params.Add("UserID", UserID, DbType.String);
            Params.Add("DatabaseUID", AccessType, DbType.Int32);

            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = SQLUtility.ISHUB_DOCUMENT_AUDIT_INSERT_SP;
                    conn.Open();

                    var affectedRows = conn.Execute(Query, Params, commandType: CommandType.StoredProcedure);
                    var ID = Params.Get<int>("DocumentID");
                    return await Task.FromResult(ID);
                    //return await Task.FromResult(affectedRows);
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "DocumentAuditInsert failed");
            }
            return await Task.FromResult(0);
        }

        #endregion

        #region Document Management PioneerRX
        public async Task<List<FedExPioneerRXDispenseModel>> PioneerRXFedExSearchDispense(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx)
        {
            //string[] numbers = SearchValues.Split(",");
            List<FedExPioneerRXDispenseModel> FedExPioneerRXDispenseList = null;
            try
            {
                using (IDbConnection conn = PioneerRXSqlConnection)
                {
                    string Query = SQLUtility.PioneerRXFedExSearchDispenseQuery(SelectedSearchType, IsCSVNumbers, SearchValues, SelectedDateSearch, SearchStartDate, SearchEndDate, this._appSettings.PioneerRXDocumentDownloadStartDatePart, ShippingMethodType);

                    conn.Open();

                    return await Task.FromResult(conn.Query<FedExPioneerRXDispenseModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList());
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "PioneerRXFedExSearchDispense failed");
            }
            return await Task.FromResult(FedExPioneerRXDispenseList);
        }

        public async Task<List<FedExPioneerRXDispenseModel>> PioneerRXFedExSearchDispenseDoc(string SelectedSearchType, bool IsCSVNumbers, string SearchValues, string SelectedDateSearch, DateTime SearchStartDate, DateTime SearchEndDate, ShippingMethod ShippingMethodType = ShippingMethod.FedEx)
        {
            //string[] numbers = SearchValues.Split(",");
            List<FedExPioneerRXDispenseModel> EmptyResult = null;
            try
            {
                string TrackingNumberCSV;
               
                List<FedExPioneerRXDispenseModel> FedExPioneerRXDispenseList = new List<FedExPioneerRXDispenseModel>();

                using (IDbConnection conn = PioneerRXSqlConnection)
                {
                    string Query = SQLUtility.PioneerRXFedExSearchDispenseQuery(SelectedSearchType, IsCSVNumbers, SearchValues, SelectedDateSearch, SearchStartDate, SearchEndDate, this._appSettings.PioneerRXDocumentDownloadStartDatePart, ShippingMethodType);

                    conn.Open();

                    FedExPioneerRXDispenseList =  conn.Query<FedExPioneerRXDispenseModel>(Query, null, commandType: CommandType.Text, commandTimeout: 180).ToList();
                    var UniqueTrackingNumbers = FedExPioneerRXDispenseList.Select(x => x.TrackingNumber).Where(y => y.Length == 12).Distinct();
                    TrackingNumberCSV = string.Join(",", UniqueTrackingNumbers);
                }

                Task<List<DocumentModel>> Documents = DocumentSearch(TrackingNumberCSV, 2);

                if ((Documents.Result != null) && (Documents.Result.Count > 0))
                {
                    for (int i = 0; i < FedExPioneerRXDispenseList.Count; i++)
                    {
                        DocumentModel DocumentRecord = Documents.Result.FirstOrDefault(x => x.TrackingNumber == FedExPioneerRXDispenseList[i].TrackingNumber);
                        if (DocumentRecord != null)
                        {
                            FedExPioneerRXDispenseList[i].DocumentID = DocumentRecord.DocumentGUID;
                            FedExPioneerRXDispenseList[i].Code = DocumentRecord.Code;
                            FedExPioneerRXDispenseList[i].DownloadDate = DocumentRecord.DownloadDate;
                            FedExPioneerRXDispenseList[i].DownloadPath = DocumentRecord.DownloadPath;
                            FedExPioneerRXDispenseList[i].DownloadMethod = DocumentRecord.DownloadMethod;
                            FedExPioneerRXDispenseList[i].DownloadCode = DocumentRecord.DownloadCode;
                            FedExPioneerRXDispenseList[i].DocumentCount = DocumentRecord.DocumentCount;
                            FedExPioneerRXDispenseList[i].TrackStatus = DocumentRecord.TrackStatus;
                            FedExPioneerRXDispenseList[i].TrackCode = DocumentRecord.TrackCode;
                            FedExPioneerRXDispenseList[i].OtherIdentifierType = DocumentRecord.OtherIdentifierType;
                            FedExPioneerRXDispenseList[i].OtherIdentifier = DocumentRecord.OtherIdentifier;
                            FedExPioneerRXDispenseList[i].ImageAvailability = DocumentRecord.ImageAvailability;
                            FedExPioneerRXDispenseList[i].TrackEvents = DocumentRecord.TrackEvents;
                            FedExPioneerRXDispenseList[i].RxID = DocumentRecord.RxID;
                            FedExPioneerRXDispenseList[i].RxTransactionID = DocumentRecord.RxTransactionID;
                            FedExPioneerRXDispenseList[i].RXNumber = DocumentRecord.RXNumber;
                            FedExPioneerRXDispenseList[i].TOUCHDATE = DocumentRecord.TOUCHDATE;
                        }
                    }
                }

                return await Task.FromResult(FedExPioneerRXDispenseList);

            }
            catch (Exception ex)
            {
                HandleException(ex, "PioneerRXFedExSearchDispense failed");
            }
            return await Task.FromResult(EmptyResult);
        }

        #endregion

        #region Patient Export
        public IDbCommand GetPatientExportDataCSV(IDbConnection conn, string Query, string MRNs, string DrugNames, string ICDCodes)
        {                    
            IDbCommand cmd = conn.CreateCommand();           
            cmd.CommandText = Query;
            cmd.CommandType = CommandType.Text;
            if (!string.IsNullOrEmpty(MRNs)) cmd.AddParameter("@MRNs", MRNs);
            if (!string.IsNullOrEmpty(DrugNames)) cmd.AddParameter("@DrugNames", DrugNames);
            if (!string.IsNullOrEmpty(ICDCodes)) cmd.AddParameter("@ICDCodes", ICDCodes);

            return cmd;
        }

        private DataTable GetPatientExportDataCSV(string Query, string MRNs,string DrugNames,string ICDCodes)
        {
            DataTable dtResult = new DataTable();
            try
            {
                using (IDbConnection conn = ALPHAScriptSqlConnection)
                {
                    conn.Open();

                    IDbCommand cmd = GetPatientExportDataCSV(conn, Query, MRNs, DrugNames, ICDCodes);
                    cmd.CommandTimeout = 180;

                    IDataReader dr = cmd.ExecuteReader();

                    dtResult.Load(dr);

                    return dtResult;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetPatientExportDataCSV failed");
            }
            return dtResult;
        }

        public Task<string> GetPatientExportDataCSV(CPRSQLPatientExportViewModel model)
        {
            model.FilterByMRNSql = model.FilterByMRNChecked == false ? @" " : @" AND HR.MRN IN(@MRNs) ";
            model.FilterByDrugNamesSql = model.FilterByDrugNamesChecked == false ? @" " : @" AND UPPER(SUBSTRING(ot.DESCRIP, 1, CHARINDEX(' ', ot.DESCRIP))) IN (@DrugNames) ";
            model.FilterByICDCodesSql = model.FilterByICDCodesChecked == false ? @" " : @" AND Icd10.CODE in (@ICDCodes) ";

            string Query = string.Format(SQLUtility.CRPSQL_Patient_Export, model.FilterByMRNSql, model.FilterByICDCodesSql, model.FilterByDrugNamesSql);
            DataTable dtResult = GetPatientExportDataCSV(Query, model.MRNs, model.DrugNames, model.ICDCodes);

            StringBuilder Output = ConvertDataTableToCSV(dtResult);
            System.Diagnostics.Debug.WriteLine(Output.ToString());

            return Task.FromResult(Output.ToString());
        }

        private DataTable SaveToDataTable(string Query, string DB = "CPRSQL")
        {
            DataTable dtResult = new DataTable();
            try
            {
                using (IDbConnection conn = CPRSQLConnection)
                {
                    conn.Open();

                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = Query;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 180;

                    IDataReader dr = cmd.ExecuteReader();

                    dtResult.Load(dr);

                    return dtResult;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "SaveToDataTableCPRSQL failed");
            }
            return dtResult;
        }

        private DataTable SaveToDataTableCPRSQL(string Query)
        {
            DataTable dtResult = new DataTable();
            try
            {
                using (IDbConnection conn = ALPHAScriptSqlConnection)
                {
                    conn.Open();

                    IDbCommand cmd = conn.CreateCommand();
                    cmd.CommandText = Query;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 180;

                    IDataReader dr = cmd.ExecuteReader();

                    dtResult.Load(dr);

                    return dtResult;
                }
            }
            catch (Exception ex)
            {
                HandleException(ex, "SaveToDataTableCPRSQL failed");
            }
            return dtResult;
        }

        private StringBuilder ConvertDataTableToCSV(DataTable dtResult)
        {
            StringBuilder Output = new StringBuilder();

            // write the header row
            for (int j = 0; j < dtResult.Columns.Count; j++)
            {
                Output.Append(dtResult.Columns[j].ColumnName + ",");
            }
            Output.AppendLine();

            // write the content
            for (int i = 0; i < dtResult.Rows.Count; i++)
            {
                for (int j = 0; j < dtResult.Columns.Count; j++)
                {
                    Output.Append(dtResult.Rows[i][j].ToString() + ",");
                }
                Output.AppendLine();
            }
            return Output;
        }

        public Task<string> GetPatientExportData(CPRSQLPatientExportViewModel model)
        {
            model.FilterByMRNSql = model.FilterByMRNChecked == false ? @" " : @" AND HR.MRN IN(" + model.MRNs + ") ";
            model.FilterByDrugNamesSql = model.FilterByDrugNamesChecked == false ? @" " : @" AND UPPER(SUBSTRING(ot.DESCRIP, 1, CHARINDEX(' ', ot.DESCRIP))) IN (" + model.DrugNames + @") ";
            model.FilterByICDCodesSql = model.FilterByICDCodesChecked == false ? @" " : @" AND Icd10.CODE in (" + model.ICDCodes + @") ";

            string Query = string.Format(SQLUtility.CRPSQL_Patient_Export, model.FilterByMRNSql, model.FilterByICDCodesSql, model.FilterByDrugNamesSql);
            DataTable dtResult = SaveToDataTableCPRSQL(Query);

            StringBuilder Output = ConvertDataTableToCSV(dtResult);
            System.Diagnostics.Debug.WriteLine(Output.ToString());

            return Task.FromResult(Output.ToString());
        }

        public Task<string> GetPatientExportQuery(CPRSQLPatientExportViewModel model)
        {
            model.FilterByMRNSql = model.FilterByMRNChecked == false ? @" " : @" AND HR.MRN IN(" + model.MRNs + ") ";
            model.FilterByDrugNamesSql = model.FilterByDrugNamesChecked == false ? @" " : @" AND UPPER(SUBSTRING(ot.DESCRIP, 1, CHARINDEX(' ', ot.DESCRIP))) IN (" + model.DrugNames + @") ";
            model.FilterByICDCodesSql = model.FilterByICDCodesChecked == false ? @" " : @" AND Icd10.CODE in (" + model.ICDCodes + @") ";

            string Query = string.Format(SQLUtility.CRPSQL_Patient_Export, model.FilterByMRNSql, model.FilterByICDCodesSql, model.FilterByDrugNamesSql);
            DataTable dtResult = SaveToDataTableCPRSQL(Query);

            StringBuilder Output = ConvertDataTableToCSV(dtResult);
            System.Diagnostics.Debug.WriteLine(Output.ToString());

            DataView view = new DataView(dtResult);
            DataTable distinctMRNs = view.ToTable(true, "external_id");

            string MRNList = "";
            for (int i = 0; i < distinctMRNs.Rows.Count; i++)
            {
                if (i == 0)
                {
                    MRNList = "'" + distinctMRNs.Rows[i][0].ToString() + "'";
                }
                else
                {
                    MRNList += ",'" + distinctMRNs.Rows[i][0].ToString() + "'";
                }
            }

            string UpdateQuery = string.Format(SQLUtility.CPRSQL_Patient_Export_Update, MRNList);
            return Task.FromResult(UpdateQuery);
        }

        public Task<string> GetPatientExportQuery2(CPRSQLPatientExportViewModel model)
        {
            model.FilterByMRNSql = model.FilterByMRNChecked == false ? @" " : @" AND HR.MRN IN(@MRNs) ";
            model.FilterByDrugNamesSql = model.FilterByDrugNamesChecked == false ? @" " : @" AND UPPER(SUBSTRING(ot.DESCRIP, 1, CHARINDEX(' ', ot.DESCRIP))) IN (@DrugNames) ";
            model.FilterByICDCodesSql = model.FilterByICDCodesChecked == false ? @" " : @" AND Icd10.CODE in (@ICDCodes) ";

            string Query = string.Format(SQLUtility.CRPSQL_Patient_Export, model.FilterByMRNSql, model.FilterByICDCodesSql, model.FilterByDrugNamesSql);
            DataTable dtResult = GetPatientExportDataCSV(Query, model.MRNs, model.DrugNames, model.ICDCodes);

            StringBuilder Output = ConvertDataTableToCSV(dtResult);
            System.Diagnostics.Debug.WriteLine(Output.ToString());

            DataView view = new DataView(dtResult);
            DataTable distinctMRNs = view.ToTable(true, "external_id");

            string MRNList = "";
            for (int i = 0; i < distinctMRNs.Rows.Count; i++)
            {
                if (i == 0)
                {
                    MRNList = "'" + distinctMRNs.Rows[i][0].ToString() + "'";
                }
                else
                {
                    MRNList += ",'" + distinctMRNs.Rows[i][0].ToString() + "'";
                }
            }

            string UpdateQuery = string.Format(SQLUtility.CPRSQL_Patient_Export_Update, MRNList);
            return Task.FromResult(UpdateQuery);
        }

        #endregion

        #region Cures Report
        public async Task<List<CuresReportModel>> GetCuresReport(DateTime DateFrom, DateTime DateTo)
        {
            List<CuresReportModel> ModelList = null;
            try
            {
                using (IDbConnection conn = ISHUBSqlConnection)
                {
                    string Query = SQLUtility.GetCuresReportQuery(DateFrom, DateTo);

                    conn.Open();
                    ModelList = await Task.FromResult(conn.Query<CuresReportModel>(Query, null, commandType: CommandType.Text, commandTimeout: 480).ToList());
                }

                return await Task.FromResult(ModelList);
            }
            catch (Exception ex)
            {
                HandleException(ex, "GetCuresReport failed");
            }
            return await Task.FromResult(ModelList);
        }
        #endregion
    }
}
