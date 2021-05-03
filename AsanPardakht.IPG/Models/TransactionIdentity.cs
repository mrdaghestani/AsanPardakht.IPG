using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public abstract class TransactionIdentity
    {
        public TransactionIdentity(int merchantConfigurationId, ulong payGateTranId)
        {
            MerchantConfigurationId = merchantConfigurationId;
            PayGateTranId = payGateTranId;
        }
        public int MerchantConfigurationId { get; private set; }
        public ulong PayGateTranId { get; private set; }
    }
}
