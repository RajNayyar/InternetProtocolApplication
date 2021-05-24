using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProtocolApp.Core
{
    public interface IInternetProtocolService
    {
        Task<string> GetIpCountry(string ipAddress);
    }
}
