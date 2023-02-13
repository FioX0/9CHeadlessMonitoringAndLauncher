using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9CHeadlessMonitoringAndLauncher.Models
{
    public class SnapshotModel
    {
        [JsonProperty("snapshotPath")]
        public string snapshotPath { get; set; }

    }
}
