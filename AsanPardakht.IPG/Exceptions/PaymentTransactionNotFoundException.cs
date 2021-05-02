using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Exceptions
{
    public class PaymentTransactionNotFoundException : AsanPardakhtIPGException
    {
        public PaymentTransactionNotFoundException(int statusCode)
            : base(statusCode, "تراکنش مالی یافت نشد و یا برگشت خورده است.", null)
        {
        }
    }
}
