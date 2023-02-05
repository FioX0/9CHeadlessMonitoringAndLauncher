using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _9CHeadlessMonitoringAndLauncher.Snapshot
{
    public partial class SnapshotForm : Form
    {
        static List<string> snapshotlist1;
        static List<string> folderlist1;

        static string chainpath1;

        static int preload = 0;

        static string pathnode = "";
        static string ipnode = "";

        private delegate void SafeCallDelegate(int perc);
        private delegate void SafeCallDelegate2(string text);

        static WebClient lWebClient = new WebClient();

        static int download = 1;

        public SnapshotForm()
        {
            InitializeComponent();
            GetPath();
        }

        public async void GetPath()
        {
            var path = ShowDialog("Please provide snapshot path. (Root Path, before \\9c-main-partition)", "Snapshot Path");
            var ip = ShowDialog("Please provide IP for -H parameter.", "NodeIP");
            pathnode = path;
            ipnode = ip;
            Start(path, ip);
        }

        public async void Start(string chainpath, string ip)
        {
            //proof of concept code below

            //string chainpath = "E:\\9c\\Miner2";

            bool basicsetupdone = false;

            List<string> folderlist = new List<string>();

            var setupcheck = Directory.Exists(chainpath + "\\9c-main-partition");
            var donexistst = Directory.Exists(chainpath + "\\done");

            if (setupcheck && donexistst)
            {
                basicsetupdone = true;
                folderlist = Directory.GetFiles(chainpath + "\\done", "*.*", SearchOption.AllDirectories).ToList();
                folderlist1 = folderlist;
            }
            else
            {
                Directory.CreateDirectory(chainpath + "\\9c-main-partition");
                Directory.CreateDirectory(chainpath + "\\done");
                folderlist = Directory.GetFiles(chainpath + "\\done", "*.*", SearchOption.AllDirectories).ToList();
                folderlist1 = folderlist;
            }

            //manage Snapshot
            var result = await SnapshotManager(basicsetupdone, chainpath);

            while (preload == 0)
            {
                await Task.Delay(1000);
            }
            //Launch Node after setup is done.
            await LaunchNode(chainpath + "\\9c-main-partition", ip);
            backgroundWorker2.RunWorkerAsync();
            this.Hide();
        }

        public async Task<bool> LaunchNode(string path, string ip)
        {
            new Thread(() =>
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                var process = new Process { StartInfo = startInfo };

                process.Start();
                process.StandardInput.WriteLine(@"""C:\Users\User\AppData\Local\Programs\Nine Chronicles\resources\app\publish\NineChronicles.Headless.Executable""  -V=100360/AB2da648b9154F2cCcAFBD85e0Bc3d51f97330Fc/MEQCIEaMO9Ubu73pV9lYK0TldM.o0hhbA9wuFaZNOnocvmGLAiA3N5jT00saE3sRyQlchyLMas8FewctlesG7+0ATz2cfw==/ZHU4OmxhdW5jaGVydTQyOjIvNGQxMWI4YzU1Zjg1ZTQ1YTgzYWYwMmE5NTA4YTRhYjIxMGMwZTU1N3U2OnBsYXllcnU0MjoxLzhmZjdkNWEwYmI4YmM1MjNjZDM4NzVjNGY5NTlkNGRhMTNlYzkwY2F1OTp0aW1lc3RhbXB1MTA6MjAyMy0wMi0wMmU=  -G=https://release.nine-chronicles.com/genesis-block-9c-main  --store-type=rocksdb  --store-path=" + path + " -H=" + ip + " --peer=027bd36895d68681290e570692ad3736750ceaab37be402442ffb203967f98f7b6,9c-main-tcp-seed-1.planetarium.dev,31234  --peer=02f164e3139e53eef2c17e52d99d343b8cbdb09eeed88af46c352b1c8be6329d71,9c-main-tcp-seed-2.planetarium.dev,31234  --peer=0247e289aa332260b99dfd50e578f779df9e6702d67e50848bb68f3e0737d9b9a5,9c-main-tcp-seed-3.planetarium.dev,31234  -T=030ffa9bd579ee1503ce008394f687c182279da913bfaec12baca34e79698a7cd1  --no-miner --graphql-server  --graphql-host=localhost  --graphql-port=23061  --confirmations=0  --minimum-broadcast-target=20  --bucket-size=20  --chain-tip-stale-behavior-type=reboot  --tip-timeout=180  --tx-life-time=60 --skip-preload=false");

            }).Start();

            await Task.Delay(10000);
            return true;
        }

        public static async Task<bool> KillNode()
        {
            new Thread(() =>
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = false
                };

                var process = new Process { StartInfo = startInfo };

                process.Start();
                process.StandardInput.WriteLine(@"taskkill /IM ""NineChronicles.Headless.Executable.exe"" /F");
            }).Start();
            await Task.Delay(10000);
            return true;
        }

        #region LaunchNode
        #endregion

        #region Snapshot
        private async Task<bool> SnapshotManager(bool setupcheckstatus, string chainpath)
        {

            var snapshotlist = await Snapshot.Helper.SnapshotCallEndpoint();

            snapshotlist1 = snapshotlist;

            chainpath1 = chainpath;

            Directory.CreateDirectory(chainpath + "\\temp");

            backgroundWorker1.RunWorkerAsync();

            return true;
        }

        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            System.Windows.Forms.Label textLabel = new System.Windows.Forms.Label() { Left = 50, Top = 20, Width = 400, Text = text };
            System.Windows.Forms.TextBox textBox = new System.Windows.Forms.TextBox() { Left = 50, Top = 50, Width = 400 };
            System.Windows.Forms.Button confirmation = new System.Windows.Forms.Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }


        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            int extractRequired = 0;
            foreach (var snapshoturl in snapshotlist1)
            {
                if (!folderlist1.Contains(chainpath1 + "\\done\\" + snapshoturl))
                {
                    UpdateStatusLabel("Downloading Epoch: " + snapshoturl);
                    download = 1;

                    if (!snapshoturl.Contains("latest"))
                    {
                        new Thread(async () =>
                        {
                            try
                            {
                                System.Net.ServicePointManager.Expect100Continue = false;
                                Thread.CurrentThread.IsBackground = true;
                                lWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDone);
                                lWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Wc_DownloadProgressChanged);
                                await lWebClient.DownloadFileTaskAsync("https://snapshots.nine-chronicles.com/main/partition/snapshot-" + snapshoturl + "-" + snapshoturl + ".zip", chainpath1 + "\\temp\\snapshot-" + snapshoturl + "-" + snapshoturl + ".zip");
                            }
                            catch (Exception ex) { Console.WriteLine("Unstable Connection, download failed"); Console.ReadLine(); }
                        }).Start();
                    }
                    else
                    {
                        new Thread(async () =>
                        {
                            try
                            {
                                System.Net.ServicePointManager.Expect100Continue = false;
                                Thread.CurrentThread.IsBackground = true;
                                lWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(FileDone);
                                lWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Wc_DownloadProgressChanged);
                                await lWebClient.DownloadFileTaskAsync("https://snapshots.nine-chronicles.com/main/partition/" + snapshoturl + ".zip", chainpath1 + "\\temp\\" + snapshoturl + ".zip");
                            }
                            catch (Exception ex) { Console.WriteLine("Unstable Connection, download failed"); Console.ReadLine(); }
                        }).Start();
                    }

                    while (download == 1)
                    {
                        Thread.Sleep(2000);
                        extractRequired++;
                    }
                }
            }

            if (extractRequired > 0)
            {
                var amount = snapshotlist1.Count;

                for (int y = amount; y > 0; y--)
                {
                    try
                    {
                        var epoch = snapshotlist1[y - 2];
                        UpdateStatusLabel("Extracting Epoch: " + epoch);
                        ZipFile.ExtractToDirectory(chainpath1 + "\\temp\\snapshot-" + epoch + "-" + epoch + ".zip", chainpath1 + "\\9c-main-partition\\", Encoding.UTF8, true);
                        File.Delete(chainpath1 + "\\temp\\snapshot-" + epoch + "-" + epoch + ".zip");
                        File.WriteAllText(chainpath1 + "\\done\\" + epoch, "done");
                    }
                    catch (Exception ex) { Console.WriteLine(ex.ToString()); };
                }
                UpdateStatusLabel("Extracting Epoch: latest_state");
                ZipFile.ExtractToDirectory(chainpath1 + "\\temp\\state_latest.zip", chainpath1 + "\\9c-main-partition\\", Encoding.UTF8, true);
                File.Delete(chainpath1 + "\\temp\\state_latest.zip");
                UpdateStatusLabel("All Tasks Completed");
            }

            preload = 1;
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.WriteLine(e.ProgressPercentage);
            UpdateProgressBar(e.ProgressPercentage);
        }

        private void UpdateProgressBar(int percentage)
        {
            if (progressBar1.InvokeRequired)
            {
                var d = new SafeCallDelegate(UpdateProgressBar);
                progressBar1.Invoke(d, new object[] { percentage });

            }
            else
            {
                progressBar1.Value = percentage;
            }
        }

        private void UpdateStatusLabel(string text)
        {
            if (label1.InvokeRequired)
            {
                var d = new SafeCallDelegate2(UpdateStatusLabel);
                label1.Invoke(d, new object[] { text });

            }
            else
            {
                label1.Text = text;
            }
        }

        private static void FileDone(object sender, AsyncCompletedEventArgs e)
        {
            download = 0;
        }

        #endregion

        private async void backgroundWorker2_DoWorkAsync(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            while (true)
            {
                i = 0;
                var preload = await checkIfPreloading();

                if (preload)
                {
                    while(i == 0)
                    {
                        var nodetip = CheckLocalNodeTopBlock();
                        var ninetip = LatestBlockAPI();
                        var sub = ninetip.Result - nodetip.Result;
                        if (ninetip.Result - nodetip.Result > 40)
                        {
                            await KillNode();
                            await LaunchNode(pathnode,ipnode);
                            i = 1;
                            await Task.Delay(10000);
                        }
                        await Task.Delay(10000);
                    }
                }
                await Task.Delay(10000);
            }
        }

        public async Task<bool> checkIfPreloading()
        {
            var client = new RestClient("http://localhost:23061/graphql");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"query\":\"query{\\r\\n  nodeStatus{\\r\\n    preloadEnded\\r\\n  }\\r\\n}\",\"variables\":{}}",
                       ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            JObject joResponse = JObject.Parse(response.Content);
            JObject ojObject = (JObject)joResponse["data"];
            JValue goldvalue = (JValue)ojObject["nodeStatus"]["preloadEnded"];
            bool utx = (bool)goldvalue;
            return utx;
        }

        public async Task<int> CheckLocalNodeTopBlock()
        {
            var client = new RestClient("http://localhost:23061/graphql");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"query\":\"query{\\r\\nnodeStatus{topmostBlocks(limit: 1){index}}}\",\"variables\":{}}",
                       ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            JObject joResponse = JObject.Parse(response.Content);
            JObject ojObject = (JObject)joResponse["data"];
            JArray goldvalue = (JArray)ojObject["nodeStatus"]["topmostBlocks"];
            int index = 0;
            foreach(var entry in goldvalue)
            {
                index = (int)entry["index"];
            }
            return index;
        }

        public static async Task<int> LatestBlockAPI()
        {
                try
                {
                    string address = string.Empty;
                    int blockindex = 0;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    var client = new RestClient("https://api.9cscan.com/status");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request); while (response.StatusCode != System.Net.HttpStatusCode.OK) { Thread.Sleep(1000); response = client.Execute(request); }
                    JObject joResponse = JObject.Parse(response.Content);
                    int latestblock = (int)joResponse["latestIndex"];

                    return latestblock;
                }
                catch (Exception ex)
                {
                    return 0;
                }
        }
    }
}
