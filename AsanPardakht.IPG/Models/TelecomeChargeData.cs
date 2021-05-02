using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public class TelecomeChargeData
    {
        public TelecomeChargeData(string destinationMobile, TelecomOperator telecomOperator, int productId)
        {
            DestinationMobile = destinationMobile;
            TelecomOperator = telecomOperator;
            ProductId = productId;
        }
        public string DestinationMobile { get; private set; }
        public int ProductId { get; private set; }
        public TelecomOperator TelecomOperator { get; private set; }
    }
}