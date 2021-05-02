using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Exceptions
{
    public class TransactionNotVerifiedException : AsanPardakhtIPGException
    {
        public TransactionNotVerifiedException(int statusCode)
            : base(statusCode, "ابتدا باید تراکنش را وریفای کنید.", null)
        {
        }
    }
}
