using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace _9CHeadlessMonitoringAndLauncher
{
    public partial class MainForm : Form
    {
        //Fields
        private int borderSize = 2;
        private Size formSize; //Keep form size when it is minimized and restored.Since the form is resized because it takes into account the size of the title bar and borders.

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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            formSize = this.ClientSize;
        }

        //Drag Form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

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

        //Private methods
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
                btnMenu.Dock = DockStyle.Top;
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
                btnMenu.Dock = DockStyle.None;
                foreach (Button menuButton in panelMenu.Controls.OfType<Button>())
                {
                    menuButton.Text = "   " + menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        //Event methods
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void snapshotPathBTN_Click(object sender, EventArgs e)
        {
            //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    //snapshotConfigCurrentPathLBL.Text = folderBrowserDialog1.SelectedPath.ToString();
            //}
        }

        private void snapshotMenuStartSnapshot(object sender, EventArgs e)
        {
            progressBarReference = progressBar1;
            SnapshotStartWorker.RunWorkerAsync();
        }

        private async void SnapshotStartWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //////proof of concept code below

            //bool basicsetupdone = false;

            //List<string> folderlist = new List<string>();

            //var setupcheck = Directory.Exists(snapshotConfigCurrentPathLBL.Text + "\\9c-main-partition");
            //var donexistst = Directory.Exists(snapshotConfigCurrentPathLBL.Text + "\\done");

            //if (setupcheck && donexistst)
            //{
            //    basicsetupdone = true;
            //    folderlist = Directory.GetFiles(snapshotConfigCurrentPathLBL.Text + "\\done", "*.*", SearchOption.AllDirectories).ToList();
            //    folderlist1 = folderlist;
            //}
            //else
            //{
            //    Directory.CreateDirectory(snapshotConfigCurrentPathLBL.Text + "\\9c-main-partition");
            //    Directory.CreateDirectory(snapshotConfigCurrentPathLBL.Text + "\\done");
            //    folderlist = Directory.GetFiles(snapshotConfigCurrentPathLBL.Text + "\\done", "*.*", SearchOption.AllDirectories).ToList();
            //    folderlist1 = folderlist;
            //}

            //////manage Snapshot
            //var snapshotlist = await Snapshot.Helper.SnapshotCallEndpoint();

            //snapshotlist1 = snapshotlist;

            //chainpath1 = snapshotConfigCurrentPathLBL.Text;

            //Directory.CreateDirectory(snapshotConfigCurrentPathLBL.Text + "\\temp");

            //SnapshotDownloadWrkr.RunWorkerAsync();
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
                        Snapshot.Helper.UpdateStatusLabel(snapshotMenuFileLBL, "Epoch: " + snapshoturl);
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

                            Snapshot.Helper.UpdateStatusLabel(snapshotMenuFileLBL, "Epoch: " + epoch);
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
            }
            catch (Exception ex) { Console.WriteLine(); MessageBox.Show(ex.Message); };
        }

        private void LoadSnapshotMenu(object sender, EventArgs e)
        {
            snapshotPanel.Visible = true;
            snapshotPanel.Location = new Point(190, 50);
        }

        private void LoadSnapshotConfigMenu(object sender, EventArgs e)
        {
            snapshotPanel.Visible = false;
            snapshotConfigPanel.Visible = true;
        }
    }
}