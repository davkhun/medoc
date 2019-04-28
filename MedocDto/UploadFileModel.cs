using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocDto
{
    public class UploadFileModel
    {
        public int? ContractId { get; set; }
        public int? UserId { get; set; }
        public string FileName { get; set; }
        public int? FileType { get; set; }
        public string FileContent { get; set; }
    }
}
