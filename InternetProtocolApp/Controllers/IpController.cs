using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetProtocolApp.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetProtocolApp.Web.Controllers
{
    [Route("api/ip")]
    [ApiController]
    public class IpController : ControllerBase
    {
        // GET: api/Ip
        [HttpGet]
        [Route("location/country/{ipAddress}")]
        public async Task<string> GetCountry(string ipAddress)
        {
            var internetProtocolService = new InternetProtocolService();
            return await internetProtocolService.GetIpCountry(ipAddress);
        }
    }
}
