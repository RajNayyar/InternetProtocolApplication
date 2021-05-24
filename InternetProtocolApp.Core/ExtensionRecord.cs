using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetProtocolApp.Core
{
    public class ExtensionRecord : IComparable<ExtensionRecord>
    {
        public string Name { get; set; }
        public List<long> TimeTakenRecord { get; set; }
        public List<Status> Statuses { get; set; }
        public long? AverageTimeTaken { get; set; }
        public double? SuccessPercentage { get; set; }

        public int CompareTo(ExtensionRecord record)
        {
            if (AverageTimeTaken < record.AverageTimeTaken)
                return -1;
            else if (AverageTimeTaken > record.AverageTimeTaken)
                return 1;
            else
                return 0;
        }
    }
    public static class Record
    {
        public static List<ExtensionRecord> ExtensionRecords { get; set; } = new List<ExtensionRecord>();
        public static List<FactoryInstance> StatusesInstances { get; set; } = null;
        public static List<FactoryInstance> TimeTakenInstances { get; set; } = null;
    }
    public enum Status
    {
        Success,
        Failure
    }
}
