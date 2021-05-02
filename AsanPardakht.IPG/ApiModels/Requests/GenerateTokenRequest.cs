using AsanPardakht.IPG.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.ApiModels.Requests
{
    public class GenerateTokenRequest
    {
        public int MerchantConfigurationId { get; set; }
        public int ServiceTypeId { get; set; }
        public ulong LocalInvoiceId { get; set; }
        public ulong AmountInRials { get; set; }
        public string LocalDate
        {
            get
            {
                var d = DateTime.Now;
                return $"{d.Year.ToString("D4")}{d.Month.ToString("D2")}{d.Day.ToString("D2")} {d.Hour.ToString("D2")}{d.Minute.ToString("D2")}{d.Second.ToString("D2")}";
            }
        }
        public string AdditionalData { get; set; }
        public string CallbackURL { get; set; }
        [JsonIgnore]
        public string MobileNumber { get; set; }
        public string PaymentId { get; set; } = "0";
        public List<SettlementPortion> SettlementPortions { get; set; } = new List<SettlementPortion>();
    }
}