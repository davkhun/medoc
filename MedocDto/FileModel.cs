using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocDto
{
    public class FileModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string FileType { get; set; }
    }
}
