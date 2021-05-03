using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApIpgSample.Models
{
    public class PayChargeViewModel : PayViewModel
    {
        public int ProductId { get; set; }
        public string DestinationMobile { get; set; }
        public int TelecomOperatorId { get; set; }
    }
}
