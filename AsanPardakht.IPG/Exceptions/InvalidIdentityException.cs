using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Exceptions
{
    public class InvalidIdentityException : AsanPardakhtIPGException
    {
        public InvalidIdentityException(int statusCode)
            : base(statusCode, "اطلاعات ارسالی پذیرنده معتبر نیست و یا آدرس آی پی شما تعریف نشده است.", null)
        {
        }
    }
}
