namespace redis_stat.net.common.Models
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Stats
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public Stats()
        {
        }

        /// <summary>Gets or sets the date time requested at.</summary>
        [JsonProperty("at")]
        public long DateTimeRequestedAt { get; set; }

        [JsonProperty("static")]
        public Dictionary<string, string[]> StaticProperties { get; set; }
        
        [JsonProperty("dynamic")]
        public Dictionary<string, Dictionary<string, object[]>> DynamicProperties { get; set; }

        [JsonProperty("error")]
        public object[] Error { get; set; }
    }
}