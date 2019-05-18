using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedocDto;
using Newtonsoft.Json;
using System.Data;

namespace MedocData
{
    public class ContractContext
    {
        private Connect sql = new Connect();

        public Result Update(ContractModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var result = sql.ExecuteScalar<int>("CALL `contract_update`(@model)", new Dictionary<string, string>
            {
                {"@model", json }
            });
            var res = new Result {
                 Id = result.ToString(),
                 IsError=false
            };
            return res;
        }

        public ContractModel GetContract(int contractId)
        {
            var result = new ContractModel();
            var raw = sql.ExecuteAdapter("CALL `contract_get`(@contractId)", new Dictionary<string, string>
            {
                {"@contractId", contractId.ToString() }
            });
            foreach(DataRow r in raw.Rows)
            {
                result.ContractId = Convert.ToInt32(r["contract_id"]);
                result.CounterpartyId = Convert.ToInt32(r["client_id"]);
                result.CounterpartyName = r["org_name"].ToString();
                result.ContractName = r["contract_name"].ToString();
                result.ContractSum = Convert.ToDecimal(r["contract_sum"]);
                result.ContractFrom = r["contract_from"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["contract_from"]);
                result.ContractTo = r["contract_to"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["contract_to"]);
                result.Comment = r["comment"].ToString();
            }
            return result;
        }

        public void DeleteContract(int contractId)
        {
            sql.ExecuteNonQuery("CALL `contract_delete`(@contractId)", new Dictionary<string, string>
            {
                {"@contractId", contractId.ToString() }
            });
        }

        public List<ContractTable> GetTable(int userId, bool showAll)
        {
            var result = new List<ContractTable>();
            var raw = sql.ExecuteAdapter("CALL `contract_table`(@userId, @showAll)", new Dictionary<string, string>
            {
                {"@userId", userId.ToString() },
                {"@showAll", showAll? "1": "0" }
            });
            foreach (DataRow r in raw.Rows)
            {
                result.Add(new ContractTable
                {
                    Active = r["active"].ToString() == "1",
                    ClientInn = r["inn"].ToString(),
                    ClientKpp = r["kpp"].ToString(),
                    ClientName = r["org_name"].ToString(),
                    CreateDate = Convert.ToDateTime(r["create_date"].ToString()),
                    From = r["contract_from"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["contract_from"]),
                    Id = Convert.ToInt32(r["contract_id"]),
                    Name = r["contract_name"].ToString(),
                    Performer = r["performer"].ToString(),
                    Status = r["status_name"].ToString(),
                    Sum = Convert.ToDecimal(r["contract_sum"]),
                    To = r["contract_to"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["contract_to"]),
                    UpdateDate = r["update_date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(r["update_date"])
                });
            }
            return result;
        }
    }
}
