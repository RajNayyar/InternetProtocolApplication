using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace InternetProtocolApp.Common
{
    public interface IConnector
    {
        Task<RS> ExecuteAsync<RQ, RS>(RQ request, HttpMethod httpMethod, string URL, IDictionary<string, string> headers);
    }
}
