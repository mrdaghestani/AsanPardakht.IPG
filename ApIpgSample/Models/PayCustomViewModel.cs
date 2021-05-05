using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApIpgSample.Models
{
    public class PayCustomViewModel : PayViewModel
    {
        public AsanPardakht.IPG.Config Config { get; set; }
        public int ServiceTypeId { get; set; }
        public Dictionary<string, string> AdditionalData { get; set; }
        public List<SettlementPortionViewModel> SettlementPortions { get; set; }
    }
}
