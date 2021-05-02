using AsanPardakht.IPG.Abstractions;
using AsanPardakht.IPG.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AsanPardakht.IPG
{
    public class Client : IClient
    {
        private const string DefaultRestEndpoint = "https://ipgrest.asanpardakht.ir/";
        private Config _config;
        private HttpClient _client;
        private JsonSerializerSettings _serializerSettings;

        public Client(Config config)
        {
            _config = config;

            _client = new HttpClient();

            var endpoint = config.RestEndpoint;
            if (string.IsNullOrWhiteSpace(endpoint))
                endpoint = DefaultRestEndpoint;

            _client.BaseAddress = new Uri(endpoint);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("usr", config.MerchantUser);
            _client.DefaultRequestHeaders.Add("pwd", config.MerchantPassword);

            _serializerSettings = new JsonSerializerSettings();
            _serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public async Task<T> Execute<T>(HttpMethod method, string url, object data = null)
        {
            var result = await TryExecute<T>(method, url, data);

            if (result.responseStatusCode == 200)
            {
                return result.result;
            }
            throw result.exception;
        }

        public async Task<(T result, AsanPardakhtIPGException exception, int responseStatusCode)> TryExecute<T>(HttpMethod method, string url, object data = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (data != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings), Encoding.UTF8, "application/json");
            }

            var response = await _client.SendAsync(request);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return (default, new AsanPardakhtIPGException((int)response.StatusCode, responseContent), (int)response.StatusCode);
            }

            var result = JsonConvert.DeserializeObject<T>(responseContent);
            return (result, default, (int)response.StatusCode);
        }
    }
}
