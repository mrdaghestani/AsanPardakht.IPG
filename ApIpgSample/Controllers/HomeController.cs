using ApIpgSample.Models;
using AsanPardakht.IPG;
using AsanPardakht.IPG.Abstractions;
using AsanPardakht.IPG.ApiModels.Requests;
using AsanPardakht.IPG.ApiModels.Responses;
using AsanPardakht.IPG.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApIpgSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptionsMonitor<AsanPardakht.IPG.Config> _config;
        private readonly ILogger<HomeController> _logger;
        private readonly IServices _services;

        public HomeController(ILogger<HomeController> logger, IServices services, IOptionsMonitor<AsanPardakht.IPG.Config> config)
        {
            _config = config;
            _logger = logger;
            _services = services;
        }

        public async Task<IActionResult> Index(ulong? localInvoiceId = null)
        {
            var model = new CallbackViewModel();
            if (localInvoiceId.HasValue)
            {
                model = await Callback(localInvoiceId.Value);
            }
            model.Time = await _services.GetTime();
            return View(model);
        }
        public IActionResult Headers()
        {
            return View(Request.Headers);
        }

        private string GenerateCallbackUrl(HttpRequest request)
        {
            var scheme = request.Headers["X-Forwarded-Proto"].ToString();
            if (string.IsNullOrWhiteSpace(scheme))
                scheme = request.Scheme;
            var callbackUrl = $"{scheme}://{request.Host}/?localInvoiceId={{0}}";
            return callbackUrl;
        }
        private async Task<CallbackViewModel> Callback(ulong localInvoiceId)
        {
            try
            {
                var tranResult = await _services.GetTranResult(localInvoiceId);

                var model = new CallbackViewModel
                {
                    TranResult = tranResult,
                };

                return model;
            }
            catch (Exception exc)
            {
                return new CallbackViewModel
                {
                    ErrorMessage = exc.Message
                };
            }
        }
        private RedirectToGatewayViewModel GenerateRedirectToGatewayViewModel(GenerateTokenResponse tokenResponse)
        {
            var viewModel = new RedirectToGatewayViewModel();
            viewModel.GatewayUrl = tokenResponse.GatewayUrl;
            viewModel.PostData.Add("RefId", tokenResponse.RefId);
            if (!string.IsNullOrWhiteSpace(tokenResponse.Mobileap))
            {
                viewModel.PostData.Add("Mobileap", tokenResponse.Mobileap);
            }
            return viewModel;
        }

        #region Payments
        [HttpPost]
        public async Task<IActionResult> Pay(PayViewModel data)
        {
            try
            {
                var tokenModel = await _services.GenerateBuyToken(data.Amount, GenerateCallbackUrl(Request), data.PaymentId, data.Mobile, data.UseDefaultSharing);
                var viewModel = GenerateRedirectToGatewayViewModel(tokenModel);
                return View(viewModel);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = exc.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PayCharge(PayChargeViewModel data)
        {
            try
            {
                var telOperator = TelecomOperator.GetList().SingleOrDefault(x => x.Id == data.TelecomOperatorId);
                if (telOperator == null)
                    throw new Exception("TelecomOperator not found");
                var chargeData = new TelecomeChargeData(data.DestinationMobile, telOperator, data.ProductId);
                var tokenModel = await _services.GenerateTelecomeChargeToken(data.Amount, GenerateCallbackUrl(Request), chargeData, data.Mobile);
                var viewModel = GenerateRedirectToGatewayViewModel(tokenModel);
                return View("Pay", viewModel);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = exc.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PayBolton(PayBoltonViewModel data)
        {
            try
            {
                var telOperator = TelecomOperator.GetList().SingleOrDefault(x => x.Id == data.TelecomOperatorId);
                if (telOperator == null)
                    throw new Exception("TelecomOperator not found");
                var simType = SimType.GetList().SingleOrDefault(x => x.Id == data.SimTypeId);
                if (simType == null)
                    throw new Exception("SimType not found");
                var boltonData = new TelecomeBoltonData(data.DestinationMobile, telOperator, simType, data.ProductId);
                var tokenModel = await _services.GenerateTelecomeBoltonToken(data.Amount, GenerateCallbackUrl(Request), boltonData, data.Mobile);
                var viewModel = GenerateRedirectToGatewayViewModel(tokenModel);
                return View("Pay", viewModel);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = exc.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PayBill(PayBillViewModel data)
        {
            try
            {
                var billData = new BillData(data.BillId, data.PayId, data.PhoneNumber);
                var tokenModel = await _services.GenerateBillToken(data.Amount, GenerateCallbackUrl(Request), billData, data.Mobile);
                var viewModel = GenerateRedirectToGatewayViewModel(tokenModel);
                return View("Pay", viewModel);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = exc.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PayCustom(PayCustomViewModel data)
        {
            try
            {
                var config = data.Config;
                config.GatewayUrl = _config.CurrentValue.GatewayUrl;
                config.RestEndpoint = _config.CurrentValue.RestEndpoint;

                var client = new AsanPardakht.IPG.Client(config);
                var service = new AsanPardakht.IPG.Services(client, config);
                var localInvoiceIdGenerator = new AsanPardakht.IPG.DefaultLocalInvoiceIdGenerator();
                var id = await localInvoiceIdGenerator.GetNext();

                var tokenRequest = new GenerateTokenRequest(config.MerchantConfigurationId, id, data.Amount, GenerateCallbackUrl(Request));

                tokenRequest.SetPaymentId(data.PaymentId);
                tokenRequest.SetServiceType(data.ServiceTypeId);
                tokenRequest.SetMobileNumber(data.Mobile);

                if (data.AdditionalData != null && data.AdditionalData.Any())
                {
                    foreach (var item in data.AdditionalData)
                    {
                        tokenRequest.AddAdditionalData(item.Key, item.Value);
                    }
                }

                if (data.SettlementPortions != null && data.SettlementPortions.Any())
                {
                    foreach (var item in data.SettlementPortions)
                    {
                        tokenRequest.AddSettlementPortion(new SettlementPortion(item.IBAN, item.AmountInRials, item.PaymentId));
                    }
                }

                var tokenModel = await service.GenerateToken(tokenRequest);
                var viewModel = GenerateRedirectToGatewayViewModel(tokenModel);
                return View("Pay", viewModel);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = exc.Message
                });
            }
        }
        [HttpPost]
        public async Task<IActionResult> PayJud(PayJudViewModel data)
        {
            try
            {
                var vModel = new RedirectToGatewayViewModel
                {
                    GatewayUrl = $"{_services.GetGatewayUrl()}Jud"
                };
                vModel.PostData.Add("Orderid", data.ReferenceId);
                vModel.PostData.Add("Backurl", GenerateCallbackUrl(Request));
                if (!string.IsNullOrWhiteSpace(data.Mobile))
                {
                    vModel.PostData.Add("Mobileap", data.Mobile);
                }
                return View("Pay", vModel);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = exc.Message
                });
            }
        }
        #endregion

        #region Operations
        [HttpPost]
        public async Task<IActionResult> Settle([FromBody] TransactionIdentityViewModel data)
        {
            try
            {
                var tokenModel = await _services.Settle(new SettleRequest(data.MerchantConfigurationId, data.PayGateTranId));
                return Json(new { isSuccess = true, message = "با موفقیت ستل شد" });
            }
            catch (Exception exc)
            {
                return Json(new { isSuccess = false, error = exc.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Reverse([FromBody] TransactionIdentityViewModel data)
        {
            try
            {
                var tokenModel = await _services.Reverse(new ReverseRequest(data.MerchantConfigurationId, data.PayGateTranId));
                return Json(new { isSuccess = true, message = "با موفقیت ریورس شد" });
            }
            catch (Exception exc)
            {
                return Json(new { isSuccess = false, error = exc.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> Verify([FromBody] TransactionIdentityViewModel data)
        {
            try
            {
                var tokenModel = await _services.Verify(new VerifyRequest(data.MerchantConfigurationId, data.PayGateTranId));
                return Json(new { isSuccess = true, message = "با موفقیت وریفای شد" });
            }
            catch (Exception exc)
            {
                return Json(new { isSuccess = false, error = exc.Message });
            }
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
