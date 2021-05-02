using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Exceptions
{
    public class DuplicateReverseException : AsanPardakhtIPGException
    {
        public DuplicateReverseException(int statusCode)
            : base(statusCode, "این تراکنش قبلاً برگشت خورده است.", null)
        {
        }
    }
}
