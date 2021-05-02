using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Exceptions
{
    public class InvalidTransactionStateException : AsanPardakhtIPGException
    {
        public InvalidTransactionStateException(int statusCode)
            : base(statusCode, "تراکنش در وضعیت درستی نیست.", null)
        {
        }
    }
}
