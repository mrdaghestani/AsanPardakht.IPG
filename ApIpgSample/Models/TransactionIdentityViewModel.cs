using System;
using System.Collections.Generic;
using System.Text;

namespace ApIpgSample.Models
{
    public class TransactionIdentityViewModel
    {
        public int MerchantConfigurationId { get; set; }
        public ulong PayGateTranId { get; set; }
    }
}
