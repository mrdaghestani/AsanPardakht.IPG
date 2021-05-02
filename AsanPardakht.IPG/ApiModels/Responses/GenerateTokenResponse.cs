using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.ApiModels.Responses
{
    public class GenerateTokenResponse
    {
        public string RefId { get; internal set; }
        public string GatewayUrl { get; internal set; }
        public string Mobileap { get; internal set; }
        public ulong LocalInvoiceId { get; internal set; }
    }
}
