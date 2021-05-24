using System.Collections.Generic;

namespace InternetProtocolApp.Core
{
    public interface IExtensionFactory
    {
        List<FactoryInstance> GetInstance();
    }
}