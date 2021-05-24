using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InternetProtocolApp.Core
{
    public class InternetProtocolService : IInternetProtocolService
    {
        public async Task<string> GetIpCountry(string ipAddress)
        {
            var extensionFactory = new ExtensionFactory();
            var factoryInstances = extensionFactory.GetInstance();
            bool IsSuccess = false;
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            // Currently iteration based on priority set on timeTakenInMs, Can also be done on the basis of SuccessPercentage Rate. Need Discussion for Success Rate
            foreach(var factory in factoryInstances)
            {
                try
                {
                    watch.Start();
                    var response = await factory.Instance.GetLocation(ipAddress);
                    IsSuccess = true;
                    return response.Country;
                }
                catch (Exception ex)
                {
                    IsSuccess = false;
                }
                finally
                {
                    watch.Stop();
                    var timeTakenInMs = watch.ElapsedMilliseconds;
                    ExtensionRecord record = null;
                    switch (factory.Name)
                    {
                        case (Common.Keystore.GeoLocationApi):
                            record = Record.ExtensionRecords.Find(rec => rec.Name.Equals(Common.Keystore.GeoLocationApi));
                            record.Statuses.Add(IsSuccess ? Status.Success : Status.Failure);
                            if (IsSuccess)
                                record.TimeTakenRecord.Add(timeTakenInMs);
                            break;
                        case (Common.Keystore.IpApi):
                            record = Record.ExtensionRecords.Find(rec => rec.Name.Equals(Common.Keystore.IpApi));
                            record.Statuses.Add(IsSuccess ? Status.Success : Status.Failure);
                            if (IsSuccess)
                                record.TimeTakenRecord.Add(timeTakenInMs);
                            break;
                        case (Common.Keystore.IpWhoIsApi):
                            record = Record.ExtensionRecords.Find(rec => rec.Name.Equals(Common.Keystore.IpWhoIsApi));
                            record.Statuses.Add(IsSuccess ? Status.Success : Status.Failure);
                            if (IsSuccess)
                                record.TimeTakenRecord.Add(timeTakenInMs);
                            break;
                        default:
                            break;
                    }
                    string json = JsonConvert.SerializeObject(Record.ExtensionRecords);
                    File.WriteAllText(@"../InternetProtocolApp.Core/ExtensionRecords.json", json);
                }
            }
            return "No Extension Found OR No Extension Responded";
        }
    }
}
