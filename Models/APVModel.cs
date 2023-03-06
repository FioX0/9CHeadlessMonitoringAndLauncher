using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9CHeadlessMonitoringAndLauncher.Models
{
    public class APVModel
    {
        [JsonProperty("AppProtocolVersion")]
        public string AppProtocolVersion { get; set; }

        [JsonProperty("IceServerStrings")]
        public List<string> IceServerStrings { get; set; }

        [JsonProperty("PeerStrings")]
        public List<string> PeerStrings { get; set; }

        [JsonProperty("TrustedAppProtocolVersionSigners")]
        public List<string> TrustedAppProtocolVersionSigners { get; set; }

    }
}
