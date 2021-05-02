using AsanPardakht.IPG.ApiModels.Requests;
using AsanPardakht.IPG.ApiModels.Responses;
using AsanPardakht.IPG.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsanPardakht.IPG.Abstractions
{
    public interface IServices
    {
        Task<string> GetTime();
        Task<GenerateTokenResponse> GenerateToken(GenerateTokenRequest data);
        Task<VerifyResponse> Verify(VerifyRequest data);
        Task<VerifyResponse> Verify(ulong payGateTranId);
        Task<GetTranResultResponse> GetTranResult(GetTranResultRequest data);
        Task<GetTranResultResponse> GetTranResult(ulong localInvoiceId);
        Task<SettleResponse> Settle(SettleRequest data);
        Task<SettleResponse> Settle(ulong payGateTranId);
        Task<ReverseResponse> Reverse(ReverseRequest data);
        Task<ReverseResponse> Reverse(ulong payGateTranId);
        Task<GenerateTokenResponse> GenerateBuyToken(ulong amountInRials, string callbackURL, string mobileNumber = null);
        Task<GenerateTokenResponse> GenerateTelecomeChargeToken(ulong amountInRials, string callbackURL, TelecomeChargeData chargeData, string mobileNumber = null);
    }
}
