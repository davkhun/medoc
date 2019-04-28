using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocDto
{
    public class ClientModel
    {
        public int? UserId { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
        public bool? Active { get; set; }
    }
}
