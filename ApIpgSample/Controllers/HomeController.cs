﻿using ApIpgSample.Models;
using AsanPardakht.IPG;
using AsanPardakht.IPG.Abstractions;
using AsanPardakht.IPG.Exceptions;
using AsanPardakht.IPG.Models;
using Microsoft.AspNetCore.Http;
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

        public HomeController(ILogger<HomeController> logger, IServices services)
        {
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

                if (tranResult.ServiceType == ServiceType.Buy)
                {
                    model.VerifyResult = await _services.Verify(tranResult.PayGateTranID);
                }

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
        public IActionResult Headers()
        {
            return View(Request.Headers);
        }


        [HttpPost]
        public async Task<IActionResult> Pay(PayViewModel data)
        {
            try
            {
                var tokenModel = await _services.GenerateBuyToken(data.Amount, GenerateCallbackUrl(Request), data.Mobile);
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
                return View("Pay", tokenModel);
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
                return View("Pay", tokenModel);
            }
            catch (Exception exc)
            {
                return View("Error", new ErrorViewModel
                {
                    Message = exc.Message
                });
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
