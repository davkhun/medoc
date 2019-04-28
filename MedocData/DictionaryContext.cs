using MedocDto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocData
{
    public class DictionaryContext
    {
        private Connect sql = new Connect();
        private Dictionary<DictionaryType, string> dictTypes = new Dictionary<DictionaryType, string>
        {
            {DictionaryType.FileTypes,"fileTypes" },
            {DictionaryType.ContractStatuses,"contractStatuses" }
        };

        public enum DictionaryType
        {
            FileTypes=0,
            ContractStatuses=1
        };

        public List<DictionaryModel> Get(DictionaryType dictionaryType)
        {
            var result = new List<DictionaryModel>();
            var raw = sql.ExecuteAdapter("CALL `dictionary_get`(@dictionaryType)", new Dictionary<string, string>
            {
                {"@dictionaryType", dictTypes[dictionaryType]}
            });
            foreach (DataRow r in raw.Rows)
            {
                result.Add(new DictionaryModel
                {
                    Id = Convert.ToInt32(r["id"]),
                    Name = r["name"].ToString(),
                    Active = Convert.ToBoolean(r["active"])
                });
            }
            return result;
        }

        public void Update(DictionaryType dictionaryType, DictionaryModel model)
        {
            sql.ExecuteNonQuery("CALL `dictionary_update`(@type,@id,@name,@active)", new Dictionary<string, string>
            {
                {"@type",  dictTypes[dictionaryType]},
                {"@id", model.Id.ToString() },
                {"@name", model.Name },
                {"@active", model.Active? "1":"0" }
            });
        }
    }
}
