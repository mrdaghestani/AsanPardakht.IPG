using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApIpgSample.Models
{
    public class PayBillViewModel : PayChargeViewModel
    {
        public string BillId { get; set; }
        public string PayId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
