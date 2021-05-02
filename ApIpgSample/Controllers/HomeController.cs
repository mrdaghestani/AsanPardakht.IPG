using ApIpgSample.Models;
using AsanPardakht.IPG.Abstractions;
using AsanPardakht.IPG.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        public HomeController(ILogger<HomeController> logger, AsanPardakht.IPG.Abstractions.IServices services)
        {
            _logger = logger;
            _services = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Pay(PayViewModel data)
        {
            var callbackUrl = $"{Request.Scheme}://{Request.Host}/Home/Callback/{{0}}";
            var tokenModel = await _services.GenerateBuyToken(data.Amount, callbackUrl, data.Mobile);
            return View(tokenModel);
        }
        [HttpPost("{controller}/{action}/{localInvoiceId}")]
        public async Task<IActionResult> Callback(ulong localInvoiceId)
        {
            try
            {
                var tranResult = await _services.GetTranResult(localInvoiceId);

                var verifyResult = await _services.Verify(tranResult.PayGateTranID);

                var model = new CallbackViewModel
                {
                    TranResult = tranResult,
                    VerifyResult = verifyResult,
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
