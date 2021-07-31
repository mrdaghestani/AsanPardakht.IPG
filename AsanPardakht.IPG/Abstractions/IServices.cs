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
        Task<GenerateTokenResponse> GenerateBuyToken(ulong amountInRials, string callbackURL, string paymentId = null, string mobileNumber = null);
      /// <summary>
      /// this methode share amount of payment to accounts
      /// first you should register IBN in Asan Pardakht and then 
      /// Asan pardakht do it automatically
      /// for more info you should talk with AP 
      /// </summary>
      /// <param name="amountInRials"></param>
      /// <param name="callbackURL"></param>
      /// <param name="paymentId"></param>
      /// <param name="mobileNumber"></param>
      /// <returns></returns>
        Task<GenerateTokenResponse> GenerateBuyDefaultSharingToken(ulong amountInRials, string callbackURL, string paymentId = null, string mobileNumber = null);
        Task<GenerateTokenResponse> GenerateTelecomeChargeToken(ulong amountInRials, string callbackURL, TelecomeChargeData chargeData, string mobileNumber = null);
        Task<GenerateTokenResponse> GenerateTelecomeBoltonToken(ulong amountInRials, string callbackURL, TelecomeBoltonData boltonData, string mobileNumber = null);
        Task<GenerateTokenResponse> GenerateBillToken(ulong amountInRials, string callbackURL, BillData billData, string mobileNumber = null);
    }
}
