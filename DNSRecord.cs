using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoDaddyRecordUpdater
{
    public class DNSRecord
    {
        [JsonIgnore]
        [Key]
        public string ID { get; set; } //A Unique ID for each DNS Record.

        [JsonProperty("data")]
        public string? Data { get; set; }  //The Address the record points to

        [JsonProperty("name")]
        public string Name { get; set; } 

        [JsonProperty("port")]
        public int Port { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonProperty("service")]
        public string Service { get; set; }

        [JsonProperty("ttl")]
        public int TTL { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("weight")]
        public int Weight { get; set; }
        [JsonIgnore]
        public string Domain { get; set; }
    }
}
