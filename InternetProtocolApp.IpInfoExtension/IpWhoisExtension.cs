using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InternetProtocolApp.IpWhoIsExtension
{
    public class IpWhoIsExtension : IInternetProtocolExtension
    {
        private string _url { get; set; }
        public IpWhoIsExtension()
        {
            _url = "http://ipwhois.app/json/{0}";
        }

        public async Task<LocationResponse> GetLocation(string ipAddress)
        {
            var connector = new Common.Connector();
            var URL = string.Format(_url, ipAddress);
            var response =  await connector.ExecuteAsync<object, IpWhoIsResponse>(null, HttpMethod.Get, URL, null);
            return new LocationResponse()
            {
                City = response.city,
                Country = response.country
            };
        }
    }
}
