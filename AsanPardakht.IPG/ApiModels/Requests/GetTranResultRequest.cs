using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.ApiModels.Requests
{
    public class GetTranResultRequest
    {
        public int MerchantConfigurationId { get; set; }
        public ulong LocalInvoiceId { get; set; }
    }
}
