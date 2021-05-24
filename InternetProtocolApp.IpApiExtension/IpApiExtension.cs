using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InternetProtocolApp.IpApiExtension
{
    public class IpApiExtension : IInternetProtocolExtension
    {
        private string _uri { get; set; }
        public IpApiExtension()
        {
            _uri = "http://ip-api.com/json/{0}";
        }
        public async Task<LocationResponse> GetLocation(string ipAddress)
        {
            var URL = string.Format(_uri, ipAddress);
            var connector = new Common.Connector();
            var response = await connector.ExecuteAsync<object, IpApiResponse>(null, HttpMethod.Get, URL, null);
            return new LocationResponse()
            {
                City = response.city,
                Country = response.country
            };
        }
    }
}
