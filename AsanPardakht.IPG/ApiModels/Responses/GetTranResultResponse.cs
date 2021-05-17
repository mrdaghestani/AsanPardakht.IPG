using AsanPardakht.IPG.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AsanPardakht.IPG.ApiModels.Responses
{
    public class GetTranResultResponse
    {
        public string CardNumber { get; set; }
        public string RRN { get; set; }
        public string RefID { get; set; }
        public string Amount { get; set; }
        [JsonProperty("payGateTranID")]
        public ulong PayGateTranID { get; set; }
        [JsonProperty("salesOrderID")]
        public ulong LocalInvoiceId { get; set; }
        public ServiceType ServiceType
        {
            get
            {
                return (ServiceType)ServiceTypeId;
            }
        }

        /// <summary>
        /// هش کارت
        /// </summary>
        public string Hash { get; set; }

        public bool IsSuccessServiceStatusCode
        {
            get
            {
                return new[] { "500", "600" }.Contains(ServiceStatusCode?.Trim());
            }
        }
        public int MerchantConfigurationId { get; set; }

        #region Sub Service Result Info
        /// <summary>
        /// کد سرویس پرداخت
        /// این کد هنگام ساخت تراکنش توسط کلاینت ارسال می گردد
        /// برای مثال عدد 61 برای خرید شارژ همراه اول است
        /// </summary>
        public int ServiceTypeId { get; set; }
        /// <summary>
        /// نتیجه انجام تراکنش در سرویس مربوطه
        /// عدد 500 یا 600 به معنی تراکنش موفق می باشد
        /// عدد 700 به معنی ناموفق است و پول به حساب مشتری باز می گردد
        /// غیر از 3 عدد بالا، هر چه بود، خطا می باشد و نتیجه تراکنش نامشخص می گردد
        /// </summary>
        public string ServiceStatusCode { get; set; }
        /// <summary>
        /// شماره موبایل شارژ شده
        /// </summary>
        public string DestinationMobile { get; set; }
        /// <summary>
        /// کد محصول خریداری شده
        /// </summary>
        public int? ProductId { get; set; }
        /// <summary>
        /// شرح محصول خریداری شده
        /// </summary>
        public string ProductNameFa { get; set; }
        /// <summary>
        /// مبلغ محصول بدون مالیات بر ارزش افزوده
        /// </summary>
        public int? ProductPrice { get; set; }
        /// <summary>
        /// کد اپراتور
        /// </summary>
        public byte? OperatorId { get; set; }
        /// <summary>
        /// نام اپراتور
        /// </summary>
        public string OperatorNameFa { get; set; }
        /// <summary>
        /// کد نوع سیمکارت
        /// </summary>
        public byte? SimTypeId { get; set; }
        /// <summary>
        /// عنوان نوع سیمکارت
        /// </summary>
        public string SimTypeTitleFa { get; set; }
        /// <summary>
        /// شناسه قبض
        /// </summary>
        public string BillId { get; set; }
        /// <summary>
        /// شناسه پرداخت
        /// </summary>
        public string PayId { get; set; }
        /// <summary>
        /// شرح سازمان قبض
        /// </summary>
        public string BillOrganizationNameFa { get; set; }

        #endregion
        /// <summary>
        /// زمان انجام تراکنش
        /// </summary>
        public DateTime? PayGateTranDate { get; set; }
        /// <summary>
        /// زمان انجام تراکنش
        /// </summary>
        public double PayGateTranDateEpoch { get; set; }
    }
}
