using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static _9CHeadlessMonitoringAndLauncher.MainForm;
using static System.Net.Mime.MediaTypeNames;

namespace _9CHeadlessMonitoringAndLauncher.Node
{
    public class Monitoring
    {
        public static async Task<bool> ChainNodeMonitor(Label labeltoUpdate)
        {
            while (true)
            {
                try
                {
                    var client = new RestClient("https://9c-main-full-state.planetarium.dev/graphql");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", "{\"query\":\"query{\\r\\nnodeStatus{topmostBlocks(limit: 1){index}}}\",\"variables\":{}}",
                               ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    JObject joResponse = JObject.Parse(response.Content);
                    string latestblock = (string)joResponse["data"]["nodeStatus"]["topmostBlocks"][0]["index"];

                    Snapshot.Helper.UpdateStatusLabel(labeltoUpdate, latestblock);

                    Thread.Sleep(11000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public static async Task<bool> LocalNodeMonitor(Label mainMenuNodeBlockLBL, Label mainMenuRunningLBL)
        {
            while (mainMenuRunningLBL.Text == "Yes")
            {
                try
                {
                    var client = new RestClient("http://localhost:23061/graphql");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", "{\"query\":\"query{\\r\\nnodeStatus{topmostBlocks(limit: 1){index}}}\",\"variables\":{}}",
                               ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    JObject joResponse = JObject.Parse(response.Content);
                    string latestblock = (string)joResponse["data"]["nodeStatus"]["topmostBlocks"][0]["index"];

                    Snapshot.Helper.UpdateStatusLabel(mainMenuNodeBlockLBL, latestblock);

                    Thread.Sleep(11000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return true;
        }

        public static async Task<bool> PreloadDone(Form form)
        {
            Task.Delay(20000).Wait();
            while (true)
            {
                try
                {
                    var client = new RestClient("http://localhost:23061/graphql");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    request.AddParameter("application/json", "{\"query\":\"query{\\r\\n  nodeStatus{\\r\\n    preloadEnded\\r\\n  }\\r\\n}\",\"variables\":{}}",
                               ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    JObject joResponse = JObject.Parse(response.Content);
                    bool latestblock = (bool)joResponse["data"]["nodeStatus"]["preloadEnded"];

                    if(latestblock)
                    {
                        _9CHeadlessMonitoringAndLauncher.MainForm.preload = 0;
                        return latestblock;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                await Task.Delay(10000);
            }
        }
    }
}
