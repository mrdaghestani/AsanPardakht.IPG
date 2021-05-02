using System;
using System.Collections.Generic;
using System.Text;

namespace AsanPardakht.IPG.Exceptions
{
    public class AsanPardakhtIPGException : Exception
    {
        public AsanPardakhtIPGException(int statusCode, string message, Exception innerException = null)
            : base($"({statusCode}) - {message}", innerException)
        {

        }
        public AsanPardakhtIPGException(string message, Exception innerException = null)
            : base(message, innerException)
        {

        }
    }
}
