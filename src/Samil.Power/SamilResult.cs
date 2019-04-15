using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Samil.Power
{
    public class SamilResult
    {
        [JsonProperty("resportDatas")] 
        public Dictionary<int, double> DataRecords { get; set; }

        [JsonProperty("resportDay")]
        public int Day { get; set; }
        
        [JsonProperty("resportMonth")]
        public int Month { get; set; }
        
        [JsonProperty("resportYear")]
        public int Year { get; set; }        
        
        public double? TotalKilowatt => DataRecords?.Sum(x => x.Value);
    }
}