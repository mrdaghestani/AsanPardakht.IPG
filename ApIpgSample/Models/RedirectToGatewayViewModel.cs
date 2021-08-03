using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApIpgSample.Models
{
    public class RedirectToGatewayViewModel
    {
        public string GatewayUrl { get; set; }
        public Dictionary<string, string> PostData { get; private set; } = new Dictionary<string, string>();
    }
}
