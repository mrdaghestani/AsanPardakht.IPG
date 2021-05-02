using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsanPardakht.IPG.Abstractions
{
    public interface ILocalInvoiceIdGenerator
    {
        Task<ulong> GetNext();
    }
}
