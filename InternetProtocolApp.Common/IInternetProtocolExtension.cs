using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProtocolApp
{
    public interface IInternetProtocolExtension
    {
        Task<LocationResponse> GetLocation(string ipAddress);
    }
}
