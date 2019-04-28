using MedocDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocData
{
    public class ClientContext
    {
        private Connect sql = new Connect();
        public enum UpdateClientResult
        {
            Created= 1,
            Updated =2,
            AlreadyExists = 0,
        };

        public UpdateClientResult Update(ClientModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var result = sql.ExecuteScalar<int>("CALL `clients_update`(@model)", new Dictionary<string, string>
            {
                {"@model", json }
            });
            return (UpdateClientResult)result;
        }

        public List<ClientModel> GetTable(ClientSearchModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var raw = sql.ExecuteAdapter("CALL `clients_table`(@model)", new Dictionary<string, string>
            {
                {"@model", json }
            });
            var result = new List<ClientModel>();
            foreach (DataRow r in raw.Rows)
            {
                result.Add(new ClientModel
                {
                    ClientId = Convert.ToInt32(r["id"].ToString()),
                    ClientName = r["org_name"].ToString(),
                    Inn = r["inn"].ToString(),
                    Kpp = r["kpp"].ToString(),
                    Active = Convert.ToBoolean(Convert.ToInt32(r["active"].ToString()))
                });
            }
            return result;
        }

        public ClientModel Get(int cliendId)
        {
            var raw = sql.ExecuteAdapter("CALL `clients_get`(@clientId)", new Dictionary<string, string>
            {
                {"@clientId", cliendId.ToString() }
            });
            var result = new ClientModel();
            if (raw.Rows.Count> 0)
            {
                result.ClientId = Convert.ToInt32(raw.Rows[0]["id"]);
                result.ClientName = raw.Rows[0]["org_name"].ToString();
                result.Inn = raw.Rows[0]["inn"].ToString();
                result.Kpp = raw.Rows[0]["kpp"].ToString();
            }
            return result;
        }

        public void Delete(int clientId, int userId)
        {
            sql.ExecuteNonQuery("CALL `clients_delete`(@clientId, @userId)", new Dictionary<string, string>
            {
                {"@clientId", clientId.ToString() },
                {"@userId", userId.ToString()}
            });
        }

        public List<ClientTypeaheadModel> SearchTypeahead(string name)
        {
            var result = new List<ClientTypeaheadModel>();
            var raw = sql.ExecuteAdapter("CALL `clients_search`(@name)", new Dictionary<string, string>
            {
                {"@name",name }
            });
            if (raw.Rows.Count > 0)
            {
                foreach (DataRow r in raw.Rows)
                {
                    result.Add(new ClientTypeaheadModel
                    {
                        Id = Convert.ToInt32(r["id"].ToString()),
                        Name = r["org_name"].ToString()
                    });
                }
            }
            return result;
        }
    }
}
