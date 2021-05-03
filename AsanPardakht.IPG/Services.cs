using AsanPardakht.IPG.Abstractions;
using AsanPardakht.IPG.ApiModels.Requests;
using AsanPardakht.IPG.ApiModels.Responses;
using AsanPardakht.IPG.Exceptions;
using AsanPardakht.IPG.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsanPardakht.IPG
{
    public class Services : IServices
    {
        private const string DefaultGatewayUrl = "https://asan.shaparak.ir/";
        private Config _config;
        private IClient _client;
        private ILocalInvoiceIdGenerator _localInvoiceIdGenerator;

        public Services(IClient client, Config config, ILocalInvoiceIdGenerator localInvoiceIdGenerator = null)
        {
            _config = config;
            _client = client;
            _localInvoiceIdGenerator = localInvoiceIdGenerator ?? new DefaultLocalInvoiceIdGenerator();
        }

        private Task<ulong> GetNextLocalInvoiceId()
        {
            if (_localInvoiceIdGenerator == null)
            {
                throw new AsanPardakhtIPGException("You need to register an 'IlocalInvoiceIdGenerator' implementation.");
            }
            return _localInvoiceIdGenerator.GetNext();
        }
        private string GetGatewayUrl()
        {
            if (string.IsNullOrWhiteSpace(_config.GatewayUrl))
                _config.GatewayUrl = null;
            return _config.GatewayUrl ?? DefaultGatewayUrl;
        }
        public async Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest data)
        {
            var response = await _client.TryExecute<string>(HttpMethod.Post, "/v1/Token", data);

            switch (response.responseStatusCode)
            {
                case 200:
                    return new GenerateTokenResponse
                    {
                        RefId = response.result,
                        Mobileap = data.MobileNumber,
                        LocalInvoiceId = data.LocalInvoiceId,
                        GatewayUrl = GetGatewayUrl(),
                    };
                case 473:
                    throw new InvalidIdentityException(response.responseStatusCode);
                default:
                    throw response.exception;
            }

        }

        public async Task<VerifyResponse> Verify(VerifyRequest data)
        {
            var response = await _client.TryExecute<BlankResponse>(HttpMethod.Post, "/v1/Verify", data);

            switch (response.responseStatusCode)
            {
                case 200:
                    return new VerifyResponse();
                case 477:
                    throw new InvalidIdentityException(response.responseStatusCode);
                case 472:
                case 473:
                case 475:
                    throw new DuplicateVerificationException(response.responseStatusCode);
                case 471:
                case 474:
                case 476:
                    throw new PaymentTransactionNotFoundException(response.responseStatusCode);
                default:
                    throw response.exception;
            }
        }
        public Task<VerifyResponse> Verify(ulong payGateTranId)
        {
            var request = new VerifyRequest
            {
                MerchantConfigurationId = _config.MerchantConfigurationId,
                PayGateTranId = payGateTranId
            };
            return Verify(request);
        }

        public async Task<GetTranResultResponse> GetTranResult(GetTranResultRequest data)
        {
            var url = $"/v1/TranResult?merchantConfigurationId={data.MerchantConfigurationId}&localInvoiceId={data.LocalInvoiceId}";
            var response = await _client.TryExecute<GetTranResultResponse>(HttpMethod.Get, url);
            switch (response.responseStatusCode)
            {
                case 200:
                    return response.result;
                case 472:
                    throw new PaymentTransactionNotFoundException(response.responseStatusCode);
                case 471:
                    throw new InvalidIdentityException(response.responseStatusCode);
                default:
                    throw response.exception;
            }
        }

        public Task<GetTranResultResponse> GetTranResult(ulong localInvoiceId)
        {
            var request = new GetTranResultRequest
            {
                MerchantConfigurationId = _config.MerchantConfigurationId,
                LocalInvoiceId = localInvoiceId
            };
            return GetTranResult(request);
        }

        public async Task<SettleResponse> Settle(SettleRequest data)
        {
            var response = await _client.TryExecute<BlankResponse>(HttpMethod.Post, "/v1/Settlement", data);

            switch (response.responseStatusCode)
            {
                case 200:
                    return new SettleResponse();
                case 477:
                    throw new InvalidIdentityException(response.responseStatusCode);
                case 471:
                case 473:
                case 475:
                    throw new PaymentTransactionNotFoundException(response.responseStatusCode);
                case 474:
                case 476:
                    throw new DuplicateSettlementException(response.responseStatusCode);
                default:
                    throw response.exception;
            }
        }

        public Task<SettleResponse> Settle(ulong payGateTranId)
        {
            var request = new SettleRequest
            {
                MerchantConfigurationId = _config.MerchantConfigurationId,
                PayGateTranId = payGateTranId
            };
            return Settle(request);
        }

        public async Task<ReverseResponse> Reverse(ReverseRequest data)
        {
            var response = await _client.TryExecute<BlankResponse>(HttpMethod.Post, "/v1/Reverse", data);

            switch (response.responseStatusCode)
            {
                case 200:
                    return new ReverseResponse();
                case 477:
                    throw new InvalidIdentityException(response.responseStatusCode);
                case 572:
                case 471:
                    throw new PaymentTransactionNotFoundException(response.responseStatusCode);
                case 472:
                    await _client.TryExecute<BlankResponse>(HttpMethod.Post, "/v1/Cancel", data);
                    return await Reverse(data);
                case 474:
                case 476:
                    throw new InvalidTransactionStateException(response.responseStatusCode);
                case 473:
                case 475:
                    throw new DuplicateReverseException(response.responseStatusCode);
                default:
                    throw response.exception;
            }
        }

        public Task<ReverseResponse> Reverse(ulong payGateTranId)
        {
            var request = new ReverseRequest
            {
                MerchantConfigurationId = _config.MerchantConfigurationId,
                PayGateTranId = payGateTranId
            };
            return Reverse(request);
        }

        public Task<string> GetTime()
        {
            return _client.Execute<string>(HttpMethod.Get, "/v1/Time");
        }

        public async Task<GenerateTokenResponse> GenerateBuyToken(ulong amountInRials, string callbackURL, string mobileNumber = null)
        {
            var request = new GenerateTokenRequest(_config.MerchantConfigurationId, await _localInvoiceIdGenerator.GetNext(), amountInRials, callbackURL);
            if (!string.IsNullOrWhiteSpace(mobileNumber))
                request.SetMobileNumber(mobileNumber);
            return await GenerateToken(request);
        }

        public async Task<GenerateTokenResponse> GenerateTelecomeChargeToken(ulong amountInRials, string callbackURL, TelecomeChargeData chargeData, string mobileNumber = null)
        {
            if (chargeData == null)
                throw new ArgumentNullException(nameof(chargeData));

            var request = new GenerateTokenRequest(_config.MerchantConfigurationId, await _localInvoiceIdGenerator.GetNext(), amountInRials, callbackURL)
                .SetTelecomeCharge(chargeData);
            if (!string.IsNullOrWhiteSpace(mobileNumber))
                request.SetMobileNumber(mobileNumber);
            return await GenerateToken(request);
        }

        public async Task<GenerateTokenResponse> GenerateTelecomeBoltonToken(ulong amountInRials, string callbackURL, TelecomeBoltonData boltonData, string mobileNumber = null)
        {
            if (boltonData == null)
                throw new ArgumentNullException(nameof(boltonData));

            var request = new GenerateTokenRequest(_config.MerchantConfigurationId, await _localInvoiceIdGenerator.GetNext(), amountInRials, callbackURL)
                .SetTelecomeCharge(boltonData);
            if (!string.IsNullOrWhiteSpace(mobileNumber))
                request.SetMobileNumber(mobileNumber);
            return await GenerateToken(request);
        }
    }
}
