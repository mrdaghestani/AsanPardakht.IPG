using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Exceptions
{
    public class DuplicateVerificationException : AsanPardakhtIPGException
    {
        public DuplicateVerificationException(int statusCode)
            : base(statusCode, "این تراکنش قبلاً وریفای شده است.", null)
        {
        }
    }
}
