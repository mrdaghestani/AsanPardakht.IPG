using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Models
{
    public abstract class TransactionIdentity
    {
        public int MerchantConfigurationId { get; set; }
        public ulong PayGateTranId { get; set; }
    }
}
