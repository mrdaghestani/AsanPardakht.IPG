using AsanPardakht.IPG.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApIpgSample
{
    /// <summary>
    /// this LocalInvoiceIdGenerator is not useful in production invironment.
    /// you need to implement one with your own database and get a new id from the database
    /// </summary>
    public class LocalInvoiceIdGenerator : ILocalInvoiceIdGenerator
    {
        private static object _lock = new object();
        public async Task<ulong> GetNext()
        {
            var d = DateTime.Now;
            lock (_lock)
            {
                d = DateTime.Now;
                Thread.Sleep(1);
            }
            return ulong.Parse($"{d.Year.ToString("D4").Substring(2)}{d.Month.ToString("D2")}{d.Day.ToString("D2")}{d.Hour.ToString("D2")}{d.Minute.ToString("D2")}{d.Second.ToString("D2")}{d.Millisecond.ToString("D3")}");
        }
    }
}
