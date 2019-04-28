using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocDto
{
    public class ContractModel
    {
        public int? UserId { get; set; }
        public int? ContractId { get; set; }
        public string ContractName { get; set; }
        public decimal ContractSum { get; set; }
        public DateTime? ContractFrom { get; set; }
        public DateTime? ContractTo { get; set; }
        public int? CounterpartyId { get; set; }
        public string Comment { get; set; }
    }
}
