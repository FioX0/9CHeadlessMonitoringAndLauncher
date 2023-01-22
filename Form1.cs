using System.Net;

namespace _9CHeadlessMonitoringAndLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Start();
        }

        public static async void Start()
        {
            //proof of concept code below

            string chainpath = "E:\\9c\\Miner2";

            bool basicsetupdone = false;

            var setupcheck = Directory.Exists(chainpath + "\\9c-main-partition");

            if (setupcheck)
            {
                basicsetupdone = true;
            }
            else
            {
                Directory.CreateDirectory(chainpath + "\\9c-main-particion");
            }

            await SnapshotManager(basicsetupdone, chainpath);
        }

        private static async Task<bool> SnapshotManager(bool setupcheckstatus, string chainpath)
        {
            var folderlist = Directory.GetDirectories(chainpath + "\\9c-main-partition\\block", "*.*", SearchOption.AllDirectories).ToList();

            var snapshotlist = await Snapshot.Helper.SnapshotCallEndpoint();

            foreach (var snapshoturl in snapshotlist)
            {
                Console.WriteLine("Downloading Epoch: " + snapshoturl);


                using (WebClient wc = new WebClient())
                {
                    //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                    wc.DownloadFileAsync(new System.Uri("https://snapshots.nine-chronicles.com/main/partition/snapshot-" + snapshoturl + "-" + snapshoturl + ".zip"), chainpath + "\\temp");
                }
            }

            Console.ReadLine();

            return true;
        }

        //void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        //{
        //    progressBar.Value = e.ProgressPercentage;
        //}
    }
}