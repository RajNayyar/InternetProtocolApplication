using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace InternetProtocolApp.Core
{
    public class ExtensionFactory : IExtensionFactory
    {
        public List<FactoryInstance> GetInstance()
        {
            string extensionName = "";
            List<FactoryInstance> instances = new List<FactoryInstance>();
            Record.ExtensionRecords = JsonConvert.DeserializeObject<List<ExtensionRecord>>(File.ReadAllText(@"../InternetProtocolApp.Core/ExtensionRecords.json"));
            // Searching if any record has insufficient data, in that random instance will be returned
            if (Record.ExtensionRecords.Exists(record => record.Statuses == null || (record.Statuses != null && record.Statuses.Count < 10)))
            {
                Random rnd = new Random();
                int r = rnd.Next(0, Record.ExtensionRecords.Count);
                extensionName = Record.ExtensionRecords[r].Name;
                instances.Add(GetFactoryInstance(extensionName));
            }         
            else // sorting records based on average of TimeTakenInMs
            {
                var records = Record.ExtensionRecords;
                foreach(var record in records)
                {
                    record.TimeTakenRecord.Sort();
                    record.AverageTimeTaken = record.TimeTakenRecord.Sum()/record.TimeTakenRecord.Count;
                    int successStatusCount = record.Statuses.FindAll(status => status.Equals(Status.Success)).Count;
                    int totalStatus = (record.Statuses.Count);
                    record.SuccessPercentage = 100 * ((double)successStatusCount / (double)totalStatus);
                }
                // Check the CompareTo method in ExtensionRecords for more on sorting the record objects
                records.Sort();
                foreach (var record in records)
                {
                    instances.Add(GetFactoryInstance(record.Name));
                }         
            }
            return instances;
        }
        public FactoryInstance GetFactoryInstance(string extensionName)
        {
            switch (extensionName)
            {
                case Common.Keystore.IpWhoIsApi:
                    return new IpWhoIsExtension.IpWhoIsExtension().ToFactoryInstance(Common.Keystore.IpWhoIsApi);
                case Common.Keystore.IpApi:
                    return new IpApiExtension.IpApiExtension().ToFactoryInstance(Common.Keystore.IpApi);
                case Common.Keystore.GeoLocationApi:
                    return new GeolocationApiExtension.GeolocationApiExtension().ToFactoryInstance(Common.Keystore.GeoLocationApi);
                default:
                    return null;
            }
        }
    }
    public class FactoryInstance
    {
        public string Name { get; set; }
        public IInternetProtocolExtension Instance { get; set; }
    }
    public static class FactoryInstanceTanslator
    {
        public static FactoryInstance ToFactoryInstance(this IInternetProtocolExtension internetProtocolExtension, string name)
        {
            if (internetProtocolExtension == null)
                return null;
            return new FactoryInstance()
            {
                Instance = internetProtocolExtension,
                Name = name
            };
        }
    }

}
