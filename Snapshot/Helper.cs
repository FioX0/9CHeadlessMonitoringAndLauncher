using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace _9CHeadlessMonitoringAndLauncher.Snapshot
{
    public static class Helper
    {
        public static async Task<List<string>> SnapshotCallEndpoint()
        {
            List<string> endpoints = new List<string>();

            var client = new RestClient("https://snapshots.nine-chronicles.com/main/partition/latest.json");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var stuff = JObject.Parse(response.Content);
            string currentepoch = (string)stuff["BlockEpoch"];
            int previousepoch = (int)stuff["PreviousBlockEpoch"];

            endpoints.Add(currentepoch);

            while (previousepoch != 0)
            {
                var client2 = new RestClient("https://snapshots.nine-chronicles.com/main/partition/snapshot-" + previousepoch + "-" + previousepoch + ".json");
                client2.Timeout = -1;
                var request2 = new RestRequest(Method.GET);
                IRestResponse response2 = client2.Execute(request2);
                var stuff2 = JObject.Parse(response2.Content);
                currentepoch = (string)stuff2["BlockEpoch"];
                endpoints.Add(currentepoch);
                previousepoch = (int)stuff2["PreviousBlockEpoch"];
            }
            Console.WriteLine("");

            endpoints.Add("state_latest");
            return endpoints;
        }

        public static void UpdateProgressBar(ProgressBar progressBar, int percentage)
        {
            if (progressBar.InvokeRequired)
            {
                var d = new MainForm.SafeCallDelegateProgressBar(UpdateProgressBar);
                progressBar.Invoke(d, new object[] { progressBar, percentage });

            }
            else
            {
                progressBar.Value = percentage;
            }
        }

        public static void UpdateStatusLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                var d = new MainForm.SafeCallDelegateLabel(UpdateStatusLabel);
                label.Invoke(d, new object[] { label, text });

            }
            else
            {
                label.Text = text;
            }
        }

        public static void FileDone(object sender, AsyncCompletedEventArgs e)
        {
            MainForm.download = 0;
        }

        public static void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage);
            UpdateProgressBar(MainForm.progressBarReference, e.ProgressPercentage);
        }

        public static string Truncate(this string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars) + "...";
        }

    }
}
