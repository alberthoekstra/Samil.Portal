using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Samil.Power
{
    public class SamilDayResult
    {
        [JsonProperty("resportDatas")] 
        public Dictionary<TimeSpan, double> DataRecords { get; set; }

        [JsonProperty("resportDay")]
        public int Day { get; set; }
        
        [JsonProperty("resportMonth")]
        public int Month { get; set; }
        
        [JsonProperty("resportYear")]
        public int Year { get; set; }
    }
}