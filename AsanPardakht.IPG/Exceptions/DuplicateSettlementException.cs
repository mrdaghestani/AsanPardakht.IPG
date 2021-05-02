using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Exceptions
{
    public class DuplicateSettlementException : AsanPardakhtIPGException
    {
        public DuplicateSettlementException(int statusCode)
            : base(statusCode, "این تراکنش قبلاً ستل شده است.", null)
        {
        }
    }
}
