using System;
using System.Collections.Generic;
using System.Text;

namespace ApIpgSample.Models
{
    public class SettlementPortionViewModel
    {
        public string IBAN { get; set; }
        public ulong AmountInRials { get; set; }
        public string PaymentId { get; set; }
    }
}
