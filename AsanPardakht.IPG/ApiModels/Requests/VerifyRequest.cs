using AsanPardakht.IPG.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.ApiModels.Requests
{
    public class VerifyRequest : TransactionIdentity
    {
        public VerifyRequest(int merchantConfigurationId, ulong payGateTranId)
            : base(merchantConfigurationId, payGateTranId)
        {
        }
    }
}
