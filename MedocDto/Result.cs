using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocDto
{
    public class Result
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
        public string Id { get; set; }
    }
}
