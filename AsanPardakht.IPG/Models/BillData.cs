using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public class BillData
    {
        public BillData(string billId, string payId, string phoneNumber = null)
        {
            BillId = billId;
            PayId = payId;
            PhoneNumber = phoneNumber;
        }
        public string BillId { get; private set; }
        public string PayId { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}