using _9CHeadlessMonitoringAndLauncher.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9CHeadlessMonitoringAndLauncher.APV
{
    internal class Helper
    {
        public static async Task<APVModel> GetAPV()
        {
            var aPVModel = new APVModel();
            try
            {
                var client = new RestClient("https://release.nine-chronicles.com/9c-launcher-config.json");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                aPVModel = JsonConvert.DeserializeObject<APVModel>(response.Content);
            }catch(Exception ex)
            {
                MessageBox.Show("Unable to Get APV Data");
            }

            return aPVModel;
        }
    }
}
