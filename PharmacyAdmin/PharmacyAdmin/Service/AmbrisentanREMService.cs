using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using PharmacyAdmin.Data;
using PharmacyAdmin.Model;

namespace PharmacyAdmin.Service
{
    public interface IAmbrisentanREMService
    {
        public Task<List<AmbrisentanREMSModel>> GetAll(string SortDirection = "DESC", string SearchTerm = "05/28/2020");
        Task<List<AmbrisentanREMSModel>> GetAll(string SortColumnName, string SortDirection, string SearchTerm);
        Task<List<AmbrisentanREMSModel>> ListAll(int skip, int take, string orderBy, string direction, string search);
        Task<AmbrisentanREMSModel> GetByRecordID(int RecordID);

        Task<int> Update(AmbrisentanREMSModel entity);
    }

    public class AmbrisentanREMService : IAmbrisentanREMService
    {
        private readonly IConfiguration _config;
        private readonly IDapperService _dapperManager;

        public AmbrisentanREMService(IConfiguration config, IDapperService dapperManager)
        {
            _config = config;
            this._dapperManager = dapperManager;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        public async Task<AmbrisentanREMSModel> GetByRecordIDOther(int RecordID)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT [Record ID],[Activity_GUID] FROM Ambrisentan_REMS_Data WHERE ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<AmbrisentanREMSModel>(sQuery, new { RecordID = RecordID });
                return result.FirstOrDefault();
            }
        }

        public Task<AmbrisentanREMSModel> GetByRecordID(int RecordID)
        {
            string query = SQLUtility.AMBRISENTAN_REMS_TABLE_SELECT + SQLUtility.WHERE + " [Record ID] = " + RecordID;
            var entity = Task.FromResult(_dapperManager.Get<AmbrisentanREMSModel>(query, null,
                    commandType: CommandType.Text));
            return entity;
        }

        public Task<List<AmbrisentanREMSModel>> GetAll(string SortColumnName, string SortDirection, string SearchTerm)
        {
            SortColumnName = SQLUtility.AmbrisentanREMSGetSortColumnName(SortColumnName);

            DateTime ShipDateTime;
            string ShipDate;
            if (!string.IsNullOrEmpty(SearchTerm) && DateTime.TryParse(SearchTerm, out ShipDateTime))
            {
                ShipDate = ShipDateTime.ToShortDateString();
            }
            else
            {
                ShipDate = "05/28/2020";
            }
                
            var entities = Task.FromResult(_dapperManager.GetAll<AmbrisentanREMSModel>
                (SQLUtility.AmbrisentanREMSGetAllQuery(SortColumnName, SortDirection, ShipDate), null, commandType: CommandType.Text));
            return entities;
        }

        public Task<List<AmbrisentanREMSModel>> GetAll(string SortDirection = "DESC", string SearchTerm = "05/28/2020")
        {
            var entities = Task.FromResult(_dapperManager.GetAll<AmbrisentanREMSModel>
                (SQLUtility.AmbrisentanREMSListQuery(SortDirection,SearchTerm), null, commandType: CommandType.Text));
            return entities;
        }

        public Task<List<AmbrisentanREMSModel>> ListAll(int skip, int take, string orderBy, string direction = "DESC", string search = "")
        {
            var entities = Task.FromResult(_dapperManager.GetAll<AmbrisentanREMSModel>
                ($"SELECT * FROM Ambrisentan_REMS_Data WHERE Title like '%{search}%' ORDER BY {orderBy} {direction} OFFSET {skip} ROWS FETCH NEXT {take} ROWS ONLY; ", null, commandType: CommandType.Text));
            return entities;
        }

        public Task<int> Update(AmbrisentanREMSModel entity)
        {
            var Params = new DynamicParameters();
            Params.Add("RecordID", entity.RecordID);
            Params.Add("FinalStatus", entity.FinalStatus, DbType.String);
            Params.Add("UpdatedBy", entity.UpdatedBy, DbType.String);
            Params.Add("SubmitToUBC", entity.SubmitToUBC, DbType.String);

            var updateEntity = Task.FromResult(_dapperManager.Update<int>("[dbo].[AmbrisentanREMSUpdate]", Params, commandType: CommandType.StoredProcedure));
            return updateEntity;
        }
    }
}
