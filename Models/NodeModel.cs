using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9CHeadlessMonitoringAndLauncher.Models
{
    public class NodeModel
    {
        [JsonProperty("nodePath")]
        public string nodePath { get; set; }

        [JsonProperty("nodeIp")]
        public string nodeIp { get; set; }

    }
}
