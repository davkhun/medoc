using MedocDto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
    }
}
