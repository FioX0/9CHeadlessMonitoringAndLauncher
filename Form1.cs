using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace _9CHeadlessMonitoringAndLauncher
{
    public partial class Form1 : Form
    {
        static string chainpath1;

        static List<string> snapshotlist1;
        static List<string> folderlist1;

        private delegate void SafeCallDelegate(int perc);
        private delegate void SafeCallDelegate2(string text);

        static WebClient lWebClient = new WebClient();

        static int download = 1;

        public Form1()
        {
            InitializeComponent();
            OpenSnapshot();
        }

        public void OpenSnapshot()
        {
            var Snapform = new Snapshot.SnapshotForm();
            Snapform.Show();
        }
    }
}