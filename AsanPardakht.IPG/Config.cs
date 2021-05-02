using AsanPardakht.IPG.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG
{
    public class Config
    {
        public string GatewayUrl { get; set; }
        public string RestEndpoint { get; set; }
        public string MerchantUser { get; set; }
        public string MerchantPassword { get; set; }
        public int MerchantConfigurationId { get; set; }
    }
}
