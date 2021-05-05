using AsanPardakht.IPG.ApiModels.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApIpgSample.Models
{
    public class CallbackViewModel
    {
        public GetTranResultResponse TranResult { get; internal set; }
        public string Time { get; internal set; }
        public string ErrorMessage { get; internal set; }
    }
}
