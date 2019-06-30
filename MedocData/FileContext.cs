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
    public class FileContext
    {
        private Connect sql = new Connect();

        public Result Upload(UploadFileModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var result = new Result();
            try
            {
                result.Id = sql.ExecuteScalar<int>("CALL `file_upload`(@model)", new Dictionary<string, string>
            {
                {"@model", json }
            }).ToString();
                result.IsError = false;
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Message = ex.Message;
            };
            return result;
        }

        public List<FileModel> Get(int contractId)
        {
            var result = new List<FileModel>();
            var raw = sql.ExecuteAdapter("CALL `file_get`(@contractId)", new Dictionary<string, string>
            {
                {"@contractId", contractId.ToString() }
            });
            foreach (DataRow r in raw.Rows)
            {
                result.Add(new FileModel
                {
                    FileId = Convert.ToInt32(r["id"]),
                    FileName = r["file_name"].ToString(),
                    CreatedOn = Convert.ToDateTime(r["created_on"]),
                    CreatedBy = r["login"].ToString(),
                    FileType = r["type_name"].ToString()
                });
            }
            return result;
        }

        public byte[] Download(int fileId)
        {
            var result = sql.ExecuteScalar<string>("CALL `file_download`(@fileId)", new Dictionary<string, string>
            {
                {"@fileId", fileId.ToString() }
            });
            return Convert.FromBase64String(result);
        }
    }
}
