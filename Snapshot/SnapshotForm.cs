using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
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

        private delegate void SafeCallDelegate(int perc);
        private delegate void SafeCallDelegate2(string text);

        static WebClient lWebClient = new WebClient();

        static int download = 1;

        public SnapshotForm()
        {
            InitializeComponent();
            Start();
        }

        public async void Start()
        {
            //proof of concept code below

            string chainpath = "E:\\9c\\Miner2";

            bool basicsetupdone = false;

            List<string> folderlist = new List<string>();

            var setupcheck = Directory.Exists(chainpath + "\\9c-main-partition");
            Directory.Exists(chainpath + "\\done");

            if (setupcheck)
            {
                basicsetupdone = true;
                folderlist = Directory.GetFiles(chainpath + "\\done", "*.*", SearchOption.AllDirectories).ToList();
                folderlist1 = folderlist;
            }
            else
            {
                Directory.CreateDirectory(chainpath + "\\9c-main-partition");
            }

            SnapshotManager(basicsetupdone, chainpath);
        }

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
                                //await lWebClient.DownloadFileTaskAsync("https://snapshots.nine-chronicles.com/main/partition/full/9c-main-snapshot.zip", "snapshot.zip");
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
                                //await lWebClient.DownloadFileTaskAsync("https://snapshots.nine-chronicles.com/main/partition/full/9c-main-snapshot.zip", "snapshot.zip");
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
    }
}
