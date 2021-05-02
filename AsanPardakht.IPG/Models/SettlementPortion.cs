using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public class SettlementPortion
    {
        public string IBAN { get; set; }
        public ulong AmountInRials { get; set; }
        public string PaymentId { get; set; } = "0";
    }
}
