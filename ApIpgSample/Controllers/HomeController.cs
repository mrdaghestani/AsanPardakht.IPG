using ApIpgSample.Models;
using AsanPardakht.IPG;
using AsanPardakht.IPG.Abstractions;
using AsanPardakht.IPG.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ApIpgSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServices _services;

        public HomeController(ILogger<HomeController> logger, IServices services, IOptionsMonitor<Config> config)
        {
            _logger = logger;
            _services = services;

            _logger.LogInformation("{@config}", config.CurrentValue.MerchantPassword);
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Headers()
        {
            return View(Request.Headers);
        }

        [HttpPost]
        public async Task<IActionResult> Pay(PayViewModel data)
        {
            try
            {
                var callbackUrl = $"{Request.Scheme}://{Request.Host}/Home/Callback/{{0}}";
                var tokenModel = await _services.GenerateBuyToken(data.Amount, callbackUrl, data.Mobile);
                return View(tokenModel);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = exc.Message
                });
            }
        }
        [HttpPost("{controller}/{action}/{localInvoiceId}")]
        public async Task<IActionResult> Callback(ulong localInvoiceId)
        {
            try
            {
                var tranResult = await _services.GetTranResult(localInvoiceId);

                var verifyResult = await _services.Verify(tranResult.PayGateTranID);

                var time = await _services.GetTime();

                var model = new CallbackViewModel
                {
                    TranResult = tranResult,
                    VerifyResult = verifyResult,
                    Time = time,
                };

                return View(model);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel { Message = exc.Message });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
