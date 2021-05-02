using AsanPardakht.IPG.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsanPardakht.IPG.ApiModels.Requests
{
    public class GenerateTokenRequest
    {
        public GenerateTokenRequest(int merchantConfigurationId, ulong localInvoiceId, ulong amountInRials, string callbackURL)
        {
            MerchantConfigurationId = merchantConfigurationId;
            LocalInvoiceId = localInvoiceId;
            AmountInRials = amountInRials;
            CallbackURL = string.Format(callbackURL, localInvoiceId);
            ServiceTypeId = (int)ServiceType.Buy;
            PaymentId = "0";

            SetLocalDate(DateTime.Now);

            SettlementPortions = new List<SettlementPortion>();
            AdditionalDataDic = new Dictionary<string, object>();
        }
        public int MerchantConfigurationId { get; private set; }
        public int ServiceTypeId { get; private set; }
        public ulong LocalInvoiceId { get; private set; }
        public ulong AmountInRials { get; private set; }
        public string LocalDate { get; private set; }
        [JsonIgnore]
        public Dictionary<string, object> AdditionalDataDic { get; private set; }
        public string AdditionalData
        {
            get
            {
                if (AdditionalDataDic == null || !AdditionalDataDic.Any())
                    return null;
                return JsonConvert.SerializeObject(AdditionalDataDic);
            }
        }
        public string CallbackURL { get; private set; }
        [JsonIgnore]
        public string MobileNumber { get; private set; }
        public string PaymentId { get; private set; }
        public List<SettlementPortion> SettlementPortions { get; private set; } = new List<SettlementPortion>();

        public GenerateTokenRequest AddAdditionalData(string key, object value)
        {
            AdditionalDataDic.Add(key, value);
            return this;
        }
        public GenerateTokenRequest AddSettlementPortion(SettlementPortion settlementPortion)
        {
            SettlementPortions.Add(settlementPortion);
            return this;
        }
        public GenerateTokenRequest SetServiceType(int serviceTypeId)
        {
            ServiceTypeId = serviceTypeId;
            return this;
        }
        public GenerateTokenRequest SetServiceType(ServiceType serviceType)
        {
            return SetServiceType((int)serviceType);
        }
        public GenerateTokenRequest SetMobileNumber(string mobileNumber)
        {
            MobileNumber = mobileNumber;
            return this;
        }
        public GenerateTokenRequest SetPaymentId(string paymentId)
        {
            if (string.IsNullOrWhiteSpace(paymentId))
                paymentId = "0";
            PaymentId = paymentId;
            return this;
        }
        public GenerateTokenRequest SetLocalDate(DateTime d)
        {
            LocalDate = $"{d.Year.ToString("D4")}{d.Month.ToString("D2")}{d.Day.ToString("D2")} {d.Hour.ToString("D2")}{d.Minute.ToString("D2")}{d.Second.ToString("D2")}";
            return this;
        }
        public GenerateTokenRequest SetLocalDate(string localDate)
        {
            LocalDate = localDate;
            return this;
        }
        public GenerateTokenRequest SetTelecomeCharge(TelecomeChargeData chargeData)
        {
            AddAdditionalData(nameof(chargeData.DestinationMobile), chargeData.DestinationMobile);
            AddAdditionalData(nameof(chargeData.ProductId), chargeData.ProductId);
            return SetServiceType(chargeData.TelecomOperator.ChargeServiceType);
        }
    }
}