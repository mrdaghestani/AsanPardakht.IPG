using AsanPardakht.IPG.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsanPardakht.IPG.Abstractions
{
    public interface IClient
    {
        Task<T> Execute<T>(HttpMethod method, string url, object data = null);
        Task<(T result, AsanPardakhtIPGException exception, int responseStatusCode)> TryExecute<T>(HttpMethod method, string url, object data = null);
    }
}
