using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InternetProtocolApp.GeolocationApiExtension
{
    public class GeolocationApiExtension : IInternetProtocolExtension
    {
        private string _uri { get; set; }
        public GeolocationApiExtension()
        {
            _uri = "https://api.ipgeolocationapi.com/geolocate/{0}";
        }

        public async Task<LocationResponse> GetLocation(string ipAddress)
        {
            var URL = string.Format(_uri, ipAddress);
            var connector = new Common.Connector();
            var response = await connector.ExecuteAsync<object, GeolocationApiResponse>(null, HttpMethod.Get, URL, null);
            return new LocationResponse()
            {
                Country = response.name
            };
        }
    }
}
