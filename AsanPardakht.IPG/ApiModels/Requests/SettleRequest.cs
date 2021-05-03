using AsanPardakht.IPG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.ApiModels.Requests
{
    public class SettleRequest : TransactionIdentity
    {
        public SettleRequest(int merchantConfigurationId, ulong payGateTranId)
            : base(merchantConfigurationId, payGateTranId)
        {
        }
    }
}
