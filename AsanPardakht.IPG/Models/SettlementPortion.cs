using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public class SettlementPortion
    {
        public SettlementPortion(string iban, ulong amountInRials, string paymentId = null)
        {
            IBAN = iban;
            AmountInRials = amountInRials;
            PaymentId = string.IsNullOrWhiteSpace(paymentId) ? "0" : paymentId.Trim();
        }
        public string IBAN { get; private set; }
        public ulong AmountInRials { get; private set; }
        public string PaymentId { get; private set; }
    }
}
