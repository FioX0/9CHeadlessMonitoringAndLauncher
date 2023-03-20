using _9CHeadlessMonitoringAndLauncher.Models;
using _9CHeadlessMonitoringAndLauncher.Snapshot;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;
using System.Text.Json.Nodes;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System;

namespace _9CHeadlessMonitoringAndLauncher
{
    public partial class MainForm : Form
    {
        //Fields
        private int borderSize = 2;
        public static int preload = 1;
        private Size formSize; //Keep form size when it is minimized and restored.Since the form is resized because it takes into account the size of the title bar and borders.
        private APVModel aPVModel;

        #region  SnapshotVariables
        static List<string> folderlist1;
        private List<string> snapshotlist1;
        private string chainpath1;
        static WebClient lWebClient = new WebClient();
        public static int download = 1;
        public delegate void SafeCallDelegateProgressBar(ProgressBar progressBar, int perc);
        public delegate void SafeCallDelegateLabel(Label label, string text);
        public static ProgressBar progressBarReference;
        #endregion

        public MainForm()
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);//Border size
            this.BackColor = Color.FromArgb(98, 102, 244);//Border color
            aPVModel = APV.Helper.GetAPV().Result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
        }

        #region SetupFormAndUIOverrides
        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        //Overridden methods
        protected override void WndProc(ref Message m)
        {
            const int WM_NCCALCSIZE = 0x0083;//Standar Title Bar - Snap Window
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MINIMIZE = 0xF020; //Minimize form (Before)
            const int SC_RESTORE = 0xF120; //Restore form (Before)
            const int WM_NCHITTEST = 0x0084;//Win32, Mouse Input Notification: Determine what part of the window corresponds to a point, allows to resize the form.
            const int resizeAreaSize = 2;

            #region Form Resize
            // Resize/WM_NCHITTEST values
            const int HTCLIENT = 1; //Represents the client area of the window
            const int HTLEFT = 10;  //Left border of a window, allows resize horizontally to the left
            const int HTRIGHT = 11; //Right border of a window, allows resize horizontally to the right
            const int HTTOP = 12;   //Upper-horizontal border of a window, allows resize vertically up
            const int HTTOPLEFT = 13;//Upper-left corner of a window border, allows resize diagonally to the left
            const int HTTOPRIGHT = 14;//Upper-right corner of a window border, allows resize diagonally to the right
            const int HTBOTTOM = 15; //Lower-horizontal border of a window, allows resize vertically down
            const int HTBOTTOMLEFT = 16;//Lower-left corner of a window border, allows resize diagonally to the left
            const int HTBOTTOMRIGHT = 17;//Lower-right corner of a window border, allows resize diagonally to the right

            ///<Doc> More Information: https://docs.microsoft.com/en-us/windows/win32/inputdev/wm-nchittest </Doc>

            if (m.Msg == WM_NCHITTEST)
            { //If the windows m is WM_NCHITTEST
                base.WndProc(ref m);
                if (this.WindowState == FormWindowState.Normal)//Resize the form if it is in normal state
                {
                    if ((int)m.Result == HTCLIENT)//If the result of the m (mouse pointer) is in the client area of the window
                    {
                        Point screenPoint = new Point(m.LParam.ToInt32()); //Gets screen point coordinates(X and Y coordinate of the pointer)                           
                        Point clientPoint = this.PointToClient(screenPoint); //Computes the location of the screen point into client coordinates                          

                        if (clientPoint.Y <= resizeAreaSize)//If the pointer is at the top of the form (within the resize area- X coordinate)
                        {
                            if (clientPoint.X <= resizeAreaSize) //If the pointer is at the coordinate X=0 or less than the resizing area(X=10) in 
                                m.Result = (IntPtr)HTTOPLEFT; //Resize diagonally to the left
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize))//If the pointer is at the coordinate X=11 or less than the width of the form(X=Form.Width-resizeArea)
                                m.Result = (IntPtr)HTTOP; //Resize vertically up
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTTOPRIGHT;
                        }
                        else if (clientPoint.Y <= (this.Size.Height - resizeAreaSize)) //If the pointer is inside the form at the Y coordinate(discounting the resize area size)
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize horizontally to the left
                                m.Result = (IntPtr)HTLEFT;
                            else if (clientPoint.X > (this.Width - resizeAreaSize))//Resize horizontally to the right
                                m.Result = (IntPtr)HTRIGHT;
                        }
                        else
                        {
                            if (clientPoint.X <= resizeAreaSize)//Resize diagonally to the left
                                m.Result = (IntPtr)HTBOTTOMLEFT;
                            else if (clientPoint.X < (this.Size.Width - resizeAreaSize)) //Resize vertically down
                                m.Result = (IntPtr)HTBOTTOM;
                            else //Resize diagonally to the right
                                m.Result = (IntPtr)HTBOTTOMRIGHT;
                        }
                    }
                }
                return;
            }
            #endregion

            //Remove border and keep snap window
            if (m.Msg == WM_NCCALCSIZE && m.WParam.ToInt32() == 1)
            {
                return;
            }

            //Keep form size when it is minimized and restored. Since the form is resized because it takes into account the size of the title bar and borders.
            if (m.Msg == WM_SYSCOMMAND)
            {
                /// <see cref="https://docs.microsoft.com/en-us/windows/win32/menurc/wm-syscommand"/>
                /// Quote:
                /// In WM_SYSCOMMAND messages, the four low - order bits of the wParam parameter 
                /// are used internally by the system.To obtain the correct result when testing 
                /// the value of wParam, an application must combine the value 0xFFF0 with the 
                /// wParam value by using the bitwise AND operator.
                int wParam = (m.WParam.ToInt32() & 0xFFF0);

                if (wParam == SC_MINIMIZE)  //Before
                    formSize = this.ClientSize;
                if (wParam == SC_RESTORE)// Restored form(Before)
                    this.Size = formSize;
            }
            base.WndProc(ref m);
        }

        private void AdjustForm()
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized: //Maximized form (After)
                    this.Padding = new Padding(8, 8, 8, 0);
                    break;
                case FormWindowState.Normal: //Restored form (After)
                    if (this.Padding.Top != borderSize)
                        this.Padding = new Padding(borderSize);
                    break;
            }
        }

        private void CollapseMenu()
        {
            if (this.panelMenu.Width > 200) //Collapse menu
            {
                panelMenu.Width = 100;
                pictureBox1.Visible = false;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else
            { //Expand menu
                panelMenu.Width = 180;
                pictureBox1.Visible = true;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            AdjustForm();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            if (this.panelMenu.Width == 100)
                this.panelMenu.Width = 180;
            else
                this.panelMenu.Width = 201;
            CollapseMenu();
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                formSize = this.ClientSize;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
                this.Size = formSize;
            }
        }

        private void MinimizeClick(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Hide();
            notifyIcon.Visible = true;
        }

        private void notifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            if (File.Exists(Application.StartupPath + "/config/snapshotconfig.json"))
            {
                var json = JsonConvert.DeserializeObject<SnapshotModel>(File.ReadAllText(Application.StartupPath + "/config/snapshotconfig.json"));
                snapshotConfigCurrentPathLBL.Text = Snapshot.Helper.Truncate(json.snapshotPath, 30);
            }

            if (File.Exists(Application.StartupPath + "/config/nodeconfig.json"))
            {
                var oldModel = JsonConvert.DeserializeObject<NodeModel>(File.ReadAllText(Application.StartupPath + "/config/nodeconfig.json"));
                if (oldModel.nodeIp != null)
                    nodeConfigIPTBOX.Text = oldModel.nodeIp;
                if(oldModel.nodePath != null)
                    nodeConfigCurrentPathLBL.Text = Snapshot.Helper.Truncate(oldModel.nodePath, 30);
            }
            LoadMainMenu(sender, e);
            APVWorker.RunWorkerAsync();
            NodeMonitoring.RunWorkerAsync();
        }

        private void LoadMainMenu(object sender, EventArgs e)
        {
            snapshotConfigPanel2.Visible = false;
            snapshotPanel.Visible = false;
            nodeConfigPanel.Visible = false;
            mainMenuPanel.Visible = true;
            mainMenuPanel.Dock = DockStyle.Fill;
        }

        #endregion

        #region AllowWindowToMove

        private void panelTitleBar_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void label1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void IconPanel_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void pictureBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        #endregion

        #region SnapshotStuff
        private void snapshotPathBTN_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                snapshotConfigCurrentPathLBL.Text = folderBrowserDialog2.SelectedPath;
            }

            SnapshotModel snapshotModel = new SnapshotModel();
            snapshotModel.snapshotPath = snapshotConfigCurrentPathLBL.Text;
            Directory.CreateDirectory(Application.StartupPath + "/config/");
            using (StreamWriter file = File.CreateText(Application.StartupPath + "/config/snapshotconfig.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, snapshotModel);
            }
        }

        private void snapshotMenuStartSnapshot(object sender, EventArgs e)
        {
            progressBarReference = progressBar1;
            Helper.UpdateStatusLabel(SnapshotMenuCurrentStatusLBL, "Running");
            snapshotMenuStartBTN.Enabled = false;
            SnapshotStartWorker.RunWorkerAsync();
        }

        private async void SnapshotStartWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            ////proof of concept code below

            bool basicsetupdone = false;

            string folderpath = snapshotConfigCurrentPathLBL.Text;

            List<string> folderlist = new List<string>();

            var setupcheck = Directory.Exists(snapshotConfigCurrentPathLBL.Text + "\\9c-main-partition");
            var donexistst = Directory.Exists(snapshotConfigCurrentPathLBL.Text + "\\done");

            if (setupcheck && donexistst)
            {
                basicsetupdone = true;
                folderlist = Directory.GetFiles(snapshotConfigCurrentPathLBL.Text + "\\done", "*.*", SearchOption.AllDirectories).ToList();
                folderlist1 = folderlist;
            }
            else
            {
                Directory.CreateDirectory(snapshotConfigCurrentPathLBL.Text + "\\9c-main-partition");
                Directory.CreateDirectory(snapshotConfigCurrentPathLBL.Text + "\\done");
                folderlist = Directory.GetFiles(snapshotConfigCurrentPathLBL.Text + "\\done", "*.*", SearchOption.AllDirectories).ToList();
                folderlist1 = folderlist;
            }

            ////manage Snapshot
            var snapshotlist = await Snapshot.Helper.SnapshotCallEndpoint();

            snapshotlist1 = snapshotlist;

            chainpath1 = snapshotConfigCurrentPathLBL.Text;

            Directory.CreateDirectory(snapshotConfigCurrentPathLBL.Text + "\\temp");

            SnapshotDownloadWrkr.RunWorkerAsync();
        }

        private void SnapshotDownloadWorker(object sender, DoWorkEventArgs e)
        {
            try
            {
                int extractRequired = 0;
                Snapshot.Helper.UpdateStatusLabel(snapshotMenuActionLBL, "Downloading");
                foreach (var snapshoturl in snapshotlist1)
                {
                    if (!folderlist1.Contains(chainpath1 + "\\done\\" + snapshoturl))
                    {
                        Snapshot.Helper.UpdateStatusLabel(snapshotMenuFileLBL, snapshoturl);
                        download = 1;

                        if (!snapshoturl.Contains("latest"))
                        {
                            new Thread(async () =>
                            {
                                try
                                {
                                    System.Net.ServicePointManager.Expect100Continue = false;
                                    Thread.CurrentThread.IsBackground = true;
                                    lWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Snapshot.Helper.FileDone);
                                    lWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Snapshot.Helper.Wc_DownloadProgressChanged);
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
                                    lWebClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Snapshot.Helper.FileDone);
                                    lWebClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Snapshot.Helper.Wc_DownloadProgressChanged);
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
                    Snapshot.Helper.UpdateStatusLabel(snapshotMenuActionLBL, "Extracting");
                    var amount = snapshotlist1.Count;

                    for (int y = amount; y > 0; y--)
                    {
                        try
                        {
                            var epoch = snapshotlist1[y - 2];

                            Snapshot.Helper.UpdateStatusLabel(snapshotMenuFileLBL, epoch);
                            ZipFile.ExtractToDirectory(chainpath1 + "\\temp\\snapshot-" + epoch + "-" + epoch + ".zip", chainpath1 + "\\9c-main-partition\\", Encoding.UTF8, true);
                            File.Delete(chainpath1 + "\\temp\\snapshot-" + epoch + "-" + epoch + ".zip");
                            File.WriteAllText(chainpath1 + "\\done\\" + epoch, "done");
                        }
                        catch (Exception ex) { Console.WriteLine(ex.ToString()); };
                    }
                    Snapshot.Helper.UpdateStatusLabel(snapshotMenuFileLBL, "latest_state");
                    ZipFile.ExtractToDirectory(chainpath1 + "\\temp\\state_latest.zip", chainpath1 + "\\9c-main-partition\\", Encoding.UTF8, true);
                    File.Delete(chainpath1 + "\\temp\\state_latest.zip");
                }
                Snapshot.Helper.UpdateStatusLabel(snapshotMenuActionLBL, "Done");
                Helper.UpdateStatusLabel(SnapshotMenuCurrentStatusLBL, "Stopped");
            }
            catch (Exception ex) { Console.WriteLine(); MessageBox.Show(ex.Message); };
        }

        private async void LoadSnapshotMenu(object sender, EventArgs e)
        {
            snapshotConfigPanel2.Visible = false;
            mainMenuPanel.Visible = false;
            nodeConfigPanel.Visible = false;
            snapshotPanel.Visible = true;
            snapshotPanel.Dock = DockStyle.Fill;
        }

        private async void LoadSnapshotConfigMenu(object sender, EventArgs e)
        {
            snapshotPanel.Visible = false;
            mainMenuPanel.Visible = false;
            nodeConfigPanel.Visible = false;
            snapshotConfigPanel2.Visible = true;
            snapshotConfigPanel2.Dock = DockStyle.Fill;
        }
        #endregion

        #region NodeSetup
        private void LoadNodeSetup(object sender, EventArgs e)
        {
            snapshotConfigPanel2.Visible = false;
            snapshotPanel.Visible = false;
            mainMenuPanel.Visible = false;
            nodeConfigPanel.Visible = true;
            nodeConfigPanel.Dock = DockStyle.Fill;
        }

        private void NodePathClick(object sender, EventArgs e)
        {
            NodeModel oldModel = new NodeModel();
            NodeModel nodeModel = new NodeModel();

            DialogResult result = folderBrowserDialog2.ShowDialog();
            if (result == DialogResult.OK)
            {
                nodeModel.nodePath = folderBrowserDialog2.SelectedPath;
                nodeConfigCurrentPathLBL.Text = Snapshot.Helper.Truncate(folderBrowserDialog2.SelectedPath,30);
            }

            if (File.Exists(Application.StartupPath + "/config/nodeconfig.json"))
            {
                oldModel = JsonConvert.DeserializeObject<NodeModel>(File.ReadAllText(Application.StartupPath + "/config/nodeconfig.json"));
                if(oldModel.nodeIp != null)
                    nodeModel.nodeIp = oldModel.nodeIp;
            }

            
            Directory.CreateDirectory(Application.StartupPath + "/config/");
            using (StreamWriter file = File.CreateText(Application.StartupPath + "/config/nodeconfig.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, nodeModel);
            }
        }

        private void NodeIPValidated(object sender, EventArgs e)
        {
            NodeModel oldModel = new NodeModel();
            NodeModel nodeModel = new NodeModel();

            if (File.Exists(Application.StartupPath + "/config/nodeconfig.json"))
            {
                oldModel = JsonConvert.DeserializeObject<NodeModel>(File.ReadAllText(Application.StartupPath + "/config/nodeconfig.json"));
                if (oldModel.nodePath != null)
                    nodeModel.nodePath = oldModel.nodePath;
            }

            nodeModel.nodeIp = nodeConfigIPTBOX.Text;
            Directory.CreateDirectory(Application.StartupPath + "/config/");
            using (StreamWriter file = File.CreateText(Application.StartupPath + "/config/nodeconfig.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, nodeModel);
            }
        }

        #endregion

        #region Workers
        private void APVWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                var aPVModelNEW = APV.Helper.GetAPV().Result;

                if (aPVModelNEW.AppProtocolVersion != aPVModel.AppProtocolVersion)
                {
                    //Check CurrentRunning APV is the same as currently pulled.
                }

                Task.Delay(60000 * 15).Wait();
            }
        }
        private void NodeMonitoring_DoWork(object sender, DoWorkEventArgs e)
        {
            new Thread(async () => { await Node.Monitoring.ChainNodeMonitor(mainMenuChainBlockLBL); }).Start();
            new Thread(async () => { await DifferenceCalc(); }).Start();
            new Thread(async () => { await NodeMonitorTaskWorker(); }).Start();
            new Thread(async () => { await Node.Monitoring.PreloadDone(this); }).Start();

        }

        public async Task<bool> NodeMonitorTaskWorker()
        {

            int update = 0;
            int complete = 0;
            while (complete == 0)
            {
                //check if Node is running
                if (mainMenuRunningLBL.Text == "No")
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
                        if (!response.IsSuccessful)
                            await Task.Delay(1000);
                        else
                            Snapshot.Helper.UpdateStatusLabel(mainMenuRunningLBL, "Yes");
                        preload = 1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Node Check went very wrong");
                    }
                }
                else
                {
                    if (mainMenuPreloadLBL.Text == "Unknown")
                    {
                        Snapshot.Helper.UpdateStatusLabel(mainMenuPreloadLBL, $"Yes - P:1/5 Checking Blocks");

                        //Launch Subscriber.
                        var graphQLClient = new GraphQLHttpClient("http://localhost:23061/graphql", new NewtonsoftJsonSerializer());

                        var userJoinedRequest = new GraphQLRequest
                        {
                            Query = @"
                            subscription {
                              preloadProgress {
                                currentPhase
                                totalPhase
                                extra {
                                  type
                                  currentCount
                                  __typename
                                  totalCount
                                }
                              }
                            }"
                        };

                        IObservable<GraphQLResponse<UserJoinedSubscriptionResult>> subscriptionStream
                            = graphQLClient.CreateSubscriptionStream<UserJoinedSubscriptionResult>(userJoinedRequest);

                        var subscription = subscriptionStream.Subscribe(response =>
                        {
                            //Updating the label is asynchronous despite being on it's own thread so we need to limit the updates we do.
                            update++;

                            if (int.Parse((string)response.Data.preloadProgress.extra["totalCount"]) - int.Parse((string)response.Data.preloadProgress.extra["currentCount"]) < 50 && response.Data.preloadProgress.currentPhase == 5)
                            {
                                Snapshot.Helper.UpdateStatusLabel(mainMenuPreloadLBL, $"Yes - P:{response.Data.preloadProgress.currentPhase}/5 - C:{response.Data.preloadProgress.extra["currentCount"]}/{response.Data.preloadProgress.extra["totalCount"]}");
                            }
                            else if (update > 100 & response.Data.preloadProgress.currentPhase > 1 && response.Data.preloadProgress.currentPhase < 5)
                            {
                                Snapshot.Helper.UpdateStatusLabel(mainMenuPreloadLBL, $"Yes - P:{response.Data.preloadProgress.currentPhase}/5 - C:{response.Data.preloadProgress.extra["currentCount"]}/{response.Data.preloadProgress.extra["totalCount"]}");
                                update = 0;
                            }
                            //This phase is very slow, we can reduce the limiter a bit.
                            else if (update > 10 & response.Data.preloadProgress.currentPhase == 5)
                            {
                                Snapshot.Helper.UpdateStatusLabel(mainMenuPreloadLBL, $"Yes - P:{response.Data.preloadProgress.currentPhase}/5 - C:{response.Data.preloadProgress.extra["currentCount"]}/{response.Data.preloadProgress.extra["totalCount"]}");
                                update = 0;
                            }
                        });
                    }
                    else if (preload == 1)
                    {
                        await Task.Delay(1000);
                    }
                    else
                    {
                        Snapshot.Helper.UpdateStatusLabel(mainMenuPreloadLBL, $"No");
                        new Thread(async () => { await Node.Monitoring.LocalNodeMonitor(mainMenuNodeBlockLBL, mainMenuRunningLBL); }).Start();
                        await Task.Delay(60000);
                        complete = 1;
                        return false;
                    }
                }
                await Task.Delay(2000);
            }
            return true;
        }

        public async Task<bool> DifferenceCalc()
        {
            while(true)
            {
                int currentNode = int.Parse(mainMenuNodeBlockLBL.Text);
                int currentChain = int.Parse(mainMenuChainBlockLBL.Text);

                if(currentChain != 0 && currentNode != 0 && mainMenuPreloadLBL.Text == "No")
                {
                    string change = (currentChain - currentNode).ToString();
                    Snapshot.Helper.UpdateStatusLabel(mainMenuDifferenceLBL, change);

                    if(int.Parse(change) > 50)
                    {
                        Process.Start("cmd", string.Format("/c \"taskkill /IM NineChronicles.Headless.Executable.exe /F"));
                        Task.Delay(5000).Wait();
                        Snapshot.Helper.UpdateStatusLabel(mainMenuRunningLBL, "No");
                        Task.Delay(1000).Wait();
                        Snapshot.Helper.UpdateStatusLabel(mainMenuPreloadLBL, "Unknown");
                        Task.Delay(1000).Wait();
                        preload = 1;
                        Snapshot.Helper.UpdateStatusLabel(mainMenuNodeBlockLBL, "0");
                        new Thread(async () => { await NodeMonitorTaskWorker(); }).Start();
                        new Thread(async () => { await Node.Monitoring.PreloadDone(this); }).Start();
                        Task.Delay(1000).Wait();
                        System.Diagnostics.Process.Start(Application.StartupPath + "/runnode.bat");
                        Task.Delay(60000).Wait();
                        new Thread(async () => { await Node.Monitoring.LocalNodeMonitor(mainMenuNodeBlockLBL, mainMenuRunningLBL); }).Start();
                    }
                }

                await Task.Delay(11000);
            }
        }


        public class UserJoinedSubscriptionResult
        {
            public PreloadProgress? preloadProgress { get; set; }

            public class PreloadProgress
            {
                public int? currentPhase { get; set; }
                public int? totalPhase { get; set; }
                public JObject? extra { get; set; }
                public int? currentCount { get; set; }
                public int? totalCount { get; set;}
            }
        }
        #endregion

        private void iconButton3_Click(object sender, EventArgs e)
        {

            SnapshotModel snapshotModel = new SnapshotModel();
            NodeModel nodeModel = new NodeModel();

            if (File.Exists(Application.StartupPath + "/config/snapshotconfig.json"))
            {
                snapshotModel = JsonConvert.DeserializeObject<SnapshotModel>(File.ReadAllText(Application.StartupPath + "/config/snapshotconfig.json"));
            }

            if (File.Exists(Application.StartupPath + "/config/nodeconfig.json"))
            {
                nodeModel = JsonConvert.DeserializeObject<NodeModel>(File.ReadAllText(Application.StartupPath + "/config/nodeconfig.json"));
            }

            string snapshotPath = snapshotModel.snapshotPath;
            string nodePath = nodeModel.nodePath;
            string nodeIp = nodeModel.nodeIp;

            //Initial plan was to call cmd directly and pass the params, however launching the cmd line nativaly like this will lock the node to the app.
            //One we try to verify if the node is running by doing a GQL call, both node and app will indefinitely freeze as app sleeps waiting for a response form the Node.
            //However as the node is connected to the app it will also freeze the node, thereby never getting a response.
            //Workaround is to dump the params into a bat file.
            using (StreamWriter file = File.CreateText(Application.StartupPath + "/runnode.bat"))
            {
                file.WriteLine("\"" + nodePath + "\\NineChronicles.Headless.Executable\" -V=" + aPVModel.AppProtocolVersion + " -G=https://release.nine-chronicles.com/genesis-block-9c-main  --store-type=rocksdb  --store-path=" + snapshotPath + "\\9c-main-partition -H=" + nodeIp + " --peer=027bd36895d68681290e570692ad3736750ceaab37be402442ffb203967f98f7b6,9c-main-tcp-seed-1.planetarium.dev,31234  --peer=02f164e3139e53eef2c17e52d99d343b8cbdb09eeed88af46c352b1c8be6329d71,9c-main-tcp-seed-2.planetarium.dev,31234  --peer=0247e289aa332260b99dfd50e578f779df9e6702d67e50848bb68f3e0737d9b9a5,9c-main-tcp-seed-3.planetarium.dev,31234  -T=" + aPVModel.TrustedAppProtocolVersionSigners[0] + "  --no-miner --graphql-server  --graphql-host=localhost  --graphql-port=23061  --confirmations=0  --minimum-broadcast-target=20  --bucket-size=20  --chain-tip-stale-behavior-type=reboot  --tip-timeout=180  --tx-life-time=60 --skip-preload=false");
            }

            Thread.Sleep(1000);

            System.Diagnostics.Process.Start(Application.StartupPath + "/runnode.bat");

            MessageBox.Show("Node has been started.");
        }
    }
}