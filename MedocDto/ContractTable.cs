using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedocDto
{
    public class ContractTable
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Name { get; set; }
        public decimal? Sum { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string ClientName { get; set; }
        public string ClientInn { get; set; }
        public string ClientKpp { get; set; }
        public string Performer { get; set; }
    }
}
