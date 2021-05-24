using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InternetProtocolApp.Common
{
    public class Connector : IConnector
    {
        public async Task<RS> ExecuteAsync<RQ, RS>(RQ request, HttpMethod httpMethod, string URL, IDictionary<string, string> headers)
        {
            HttpResponseMessage response;
            var rqContent = JsonConvert.SerializeObject(request);
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            httpRequest.RequestUri = new Uri(URL);
            httpRequest.Content = new StringContent(rqContent, Encoding.UTF8, "application/json");
            httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            httpRequest.Method = httpMethod;
            if(headers!=null)
            {
                foreach (var header in headers)
                {
                    httpRequest.Headers.Add(header.Key, header.Value);
                }
            }
            using (var client = new HttpClient())
            {
                response = await client.SendAsync(httpRequest);
                if (IsSuccessStatusCode(response.StatusCode))
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RS>(content);
                }
                else
                {
                    throw new Exception("Api Failure");
                }
            };
        }
        private bool IsSuccessStatusCode(HttpStatusCode statusCode)
        {
             return ((int)statusCode >= 200) && ((int)statusCode <= 299);
        }
    }
}
