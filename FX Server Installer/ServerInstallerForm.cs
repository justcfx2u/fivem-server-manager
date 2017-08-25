#region RESOURCES
// SYSTEM LIBS
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Reflection;
using System.Resources;
// DOWNLOADED LIBS
using Ionic.Zip;
using HtmlAgilityPack;

#endregion

namespace FX_Server_Installer
{
    public partial class ServerInstallerForm : MetroFramework.Forms.MetroForm
    {
        // CONSTRUCTOR
        public ServerInstallerForm()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIsClosing);
        }

        #region VARS
        // LOCATION TO GET FIVEM FOR WINDOWS SERVERS 
        static bool isInstalling = false;
        static bool isDownloading = false;
        const string windowsServerUrl = "https://runtime.fivem.net/artifacts/fivem/build_server_windows/master/";
        //const string linuxServerUrl = "https://runtime.fivem.net/artifacts/fivem/build_proot_linux/master/";
        const string resourceUrl = "https://github.com/citizenfx/cfx-server-data/archive/master.zip";
        string folderLoc = null; // FOLDER LOCATION TO INSTALL THE SERVER TO
        List<int> WindowsVersions = new List<int>();
        //List<int> LinuxVersions = new List<int>();
        Dictionary<int, string> windowsServers = new Dictionary<int, string>();
        //Dictionary<int, string> linuxServers = new Dictionary<int, string>();
        #endregion

        // PROGRAM CLOSE MANAGER
        private void FormIsClosing(object sender, FormClosingEventArgs e)
        {
            if (isInstalling == true || isDownloading == true)
            {
                DialogResult result = MessageBox.Show("Currently installing server! Closing this program right now could result in an incorrectly installed server. Are you sure you wish to exit?", "Warning!",MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }                
            }             
        }
        

        // GET ALL RELEVANT HREF ATTRIBUTES
        public Dictionary<int,string> GetWindowsServerLinks()
        {
            if (CanConnect(windowsServerUrl))
            {
                // CREATE AN HTML DOCUMENT FROM THE SPECIFIED URL (FX-SERVERS-WINDOWS)
                var document = new HtmlWeb().Load(windowsServerUrl);

                // GET ALL HREF ATTRIBUTE VALUES THAT ARE RELEVANT, AND STORE THEM IN A LIST OF STRINGS
                List<string> urls = document.DocumentNode.Descendants("a")
                                                .Select(b => b.GetAttributeValue("href", null))
                                                .Where(x => x != null && x != "../").ToList();

                // MAKE SURE LIST EXSISTS
                if (urls != null)
                {
                    // CREATE DICT TO HOLD THE SERVER ID AND LINK
                    Dictionary<int, string> links = new Dictionary<int, string>();

                    // CACHE TEMP DATA HOLDERS FOR USE IN THE LOOP
                    string link;
                    int id;

                    // LOOP THROUGH LINKS AND FIND THEIR ID
                    for (int i = 0; i < urls.Count; i++)
                    {
                        // GET SERVER VERSION ID 
                        if (urls[i].Contains("-"))
                        {
                            link = urls[i].Substring(0, urls[i].IndexOf("-"));
                            if (int.TryParse(link, out id))
                            {
                                // ADD ID AND SERVER LINK TO THE DICT AND UPDATE VERSION LIST
                                links.Add(id, urls[i]);
                                WindowsVersions.Add(id);
                            }
                        }
                    }

                    return links;
                }
                else
                {
                    ThrowError("Could not get windows database download links. Most likely the server database was changed or moved!", false);
                }
            }
            else
            {
                ThrowError("Could not connect to FiveM windows server database. Please ensure you have an internet connection.", false);
            }
            return null;
        }
        /*
        public Dictionary<int,string> GetLinuxServerLinks()
        {
            if (CanConnect(linuxServerUrl))
            {
                // CREATE AN HTML DOCUMENT FROM THE SPECIFIED URL (FX-SERVERS-WINDOWS)
                var document = new HtmlWeb().Load(linuxServerUrl);

                // GET ALL HREF ATTRIBUTE VALUES THAT ARE RELEVANT, AND STORE THEM IN A LIST OF STRINGS
                List<string> urls = document.DocumentNode.Descendants("a")
                                                .Select(b => b.GetAttributeValue("href", null))
                                                .Where(x => x != null && x != "../").ToList();

                // MAKE SURE LIST EXSISTS
                if (urls != null)
                {
                    // CREATE DICT TO HOLD THE SERVER ID AND LINK
                    Dictionary<int, string> links = new Dictionary<int, string>();

                    // CACHE TEMP DATA HOLDERS FOR USE IN THE LOOP
                    string link;
                    int id;

                    // LOOP THROUGH LINKS AND FIND THEIR ID
                    for (int i = 0; i < urls.Count; i++)
                    {
                        // GET SERVER VERSION ID 
                        if (urls[i].Contains("-"))
                        {
                            link = urls[i].Substring(0, urls[i].IndexOf("-"));
                            if (int.TryParse(link, out id))
                            {
                                // ADD ID AND SERVER LINK TO THE DICT AND UPDATE VERSION LIST
                                links.Add(id, urls[i]);
                                LinuxVersions.Add(id);
                            }
                        }
                    }

                    return links;
                }
                else
                {
                    // MUST BE FALSE SINCE IN A CONSTRUCTOR
                    ThrowError("Could not get linux database download links. Most likely the server database was changed or moved!", false);
                }
            }
            else
            {
                // MUST BE FALSE SINCE IN A CONSTRUCTOR
                ThrowError("Could not connect to FiveM linux server database. Please ensure you have an internet connection.", false);
            }
            return null;
        }
        */  // GETLINUXSERVERLINKS HIDDEN FROM THIS VERSION      

        private void ServerInstallerForm_Load(object sender, EventArgs e)
        {
            windowsServers = GetWindowsServerLinks();
            //linuxServers = GetLinuxServerLinks();
            GenerateWindowsVersions();
        }
        

        // DOWNLOAD SOMETHING
        #region DOWNLOADER
        bool StartDownload(string downloadLink,string fileName)
        {
            // CHECK THE USER CAN CONNECT TO THE DOWNLOAD SERVER
            if (CanConnect(downloadLink))
            {
                WebClient client = new WebClient();
                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadComplete);
                client.DownloadFileAsync(new Uri(downloadLink), fileName);
                isDownloading = true;
                return true;
            }
            else
            {
                isDownloading = false;                
                return false;
            }
        }

        void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            // AMOUNT LEFT TO DOWNLOAD IN MEGABYTES
            double amountLeft = ((totalBytes - bytesIn) / 1024d) / 1024d;

            // UPDATE PROGRESS BAR AND THE DOWNLOADER STATUS ( e.g. MB LEFT TO DOWNLOAD )
            LabelStatus.Text = "Downloading: " + amountLeft.ToString("n2") + "MB";
            ProgressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void DownloadComplete(object sender, AsyncCompletedEventArgs e)
        {
            // UPDATE THE STATUS TO COMPLETED
            LabelStatus.Text = "Download Complete";
            ProgressBar.Value = 0;
            isDownloading = false;
        }

        #endregion

        // INSTALL A SERVER
        #region INSTALLER COMPONENTS
        // MAIN SERVER INSTALL LOOP!
        async Task InstallServer()
        {
            isInstalling = true;

            // DOWNLOAD FIVEM SERVER FILES
            #region DOWNLOAD MAIN SERVER FILES


            string versionString = VersionSelect.SelectedItem.ToString();
            int versionNumber;

            // ENSURE VERSION EXSISTS AND CONVERSION FROM VERSIONSELECTOR TO VERSION IS SAFE!
            #region VERSION ERROR CHECKING
            // GET VERSION NUMBER FROM THE VERSION SELECTION DROP DOWN BY
            if (int.TryParse(Regex.Match(versionString, @"\d+").Value, out versionNumber) == false)
            { ThrowError("Error Converting version number from the version selector. You Selected: " + versionString, true); }

            // ENSURE VERSION EXSISTS, OTHERWISE CANCEL INSTALL AND LEAVE METHOD
            if (WindowsVersions.Exists( x => x == versionNumber ) == false)
            {
                ThrowError("Server version not found!", false);
                isInstalling = false;
                return;
            }
                        
            #endregion

            
            if (StartDownload(windowsServerUrl + windowsServers[versionNumber] + "/server.zip", folderLoc + @"\server.zip"))
            {
                // WAIT FOR THE DOWNLOAD TO COMPLETE, BEFORE MOVING ONTO EXTRACTION
                while (isDownloading == true) await Task.Delay(100);
            }
            else
            {
                // COULDN'T CONNECT TO FIVE SERVER DATABASE!
                ThrowError("Could not connect to FiveM server download database!", false);
                isInstalling = false;
                return;
            }
            
            

            #endregion

            #region EXTRACT SERVER FILES

            // START EXTRACTION THREAD
            Task extractServerFile = Task.Run(() => ExtractServer());
            // UPDATE STATUS LABEL WITH LOADING ...
            while (extractServerFile.IsCompleted == false)
            {
                LabelStatus.Text = "Extracting Server";
                await Task.Delay(600);
                LabelStatus.Text = "Extracting Server .";
                await Task.Delay(600);
                LabelStatus.Text = "Extracting Server ..";
                await Task.Delay(600);
                LabelStatus.Text = "Extracting Server ...";
                await Task.Delay(600);
            }

            // SERVER IS DONE SO UPDATE STATUS LABEL
            LabelStatus.Text = "Server Extracted!";
            await Task.Delay(300);
            #endregion

            #region DOWNLOAD LATEST SERVER RESOURCES

            StartDownload(resourceUrl, folderLoc + @"\master.zip");
            // WAIT FOR THE DOWNLOAD TO COMPLETE, BEFORE MOVING ONTO EXTRACTION
            while (isDownloading == true) await Task.Delay(100);

            #endregion

            #region EXTRACT SERVER RESOURCES

            // START EXTRACTION THREAD
            Task extractResourceFile = Task.Run(() => ExtractResources());
            // UPDATE STATUS LABEL WITH LOADING ...
            while (extractResourceFile.IsCompleted == false)
            {
                LabelStatus.Text = "Extracting Resources";
                await Task.Delay(600);
                LabelStatus.Text = "Extracting Resources .";
                await Task.Delay(600);
                LabelStatus.Text = "Extracting Resources ..";
                await Task.Delay(600);
                LabelStatus.Text = "Extracting Resources ...";
                await Task.Delay(600);
            }

            // SERVER IS DONE SO UPDATE STATUS LABEL
            LabelStatus.Text = "Resources Extracted!";
            await Task.Delay(300);

            #endregion

            #region INSTALL SERVER RESOURCES

            // START EXTRACTION THREAD
            Task installResources = Task.Run(() => InstallResources());
            // UPDATE STATUS LABEL WITH LOADING ...
            while (installResources.IsCompleted == false)
            {
                LabelStatus.Text = "Installing Resources";
                await Task.Delay(600);
                LabelStatus.Text = "Installing Resources .";
                await Task.Delay(600);
                LabelStatus.Text = "Installing Resources ..";
                await Task.Delay(600);
                LabelStatus.Text = "Installing Resources ...";
                await Task.Delay(600);
            }

            // SERVER IS DONE SO UPDATE STATUS LABEL
            LabelStatus.Text = "Resources Installed!";
            await Task.Delay(300);

            #endregion

            #region CREATE SERVER CONFIG FILE

            // START EXTRACTION THREAD
            Task setupServer = Task.Run(() => ServerSetup());
            // UPDATE STATUS LABEL WITH LOADING ...
            while (extractResourceFile.IsCompleted == false)
            {
                LabelStatus.Text = "Setting Up Server";
                await Task.Delay(600);
                LabelStatus.Text = "Setting Up Server .";
                await Task.Delay(600);
                LabelStatus.Text = "Setting Up Server ..";
                await Task.Delay(600);
                LabelStatus.Text = "Setting Up Server ...";
                await Task.Delay(600);
            }

            // SERVER IS DONE SO UPDATE STATUS LABEL
            LabelStatus.Text = "Setup Complete";
            await Task.Delay(300);

            #endregion

            #region CLEAN UP SERVER FILES

            // START EXTRACTION THREAD
            Task cleanUpServer = Task.Run(() => CleanUpServer());
            // UPDATE STATUS LABEL WITH LOADING ...
            while (cleanUpServer.IsCompleted == false)
            {
                LabelStatus.Text = "Cleaning Up Server";
                await Task.Delay(600);
                LabelStatus.Text = "Cleaning Up Server .";
                await Task.Delay(600);
                LabelStatus.Text = "Cleaning Up Server ..";
                await Task.Delay(600);
                LabelStatus.Text = "Cleaning Up Server ...";
                await Task.Delay(600);
            }

            // SERVER IS DONE SO UPDATE STATUS LABEL
            LabelStatus.Text = "Server Cleaned!";
            await Task.Delay(300);

            #endregion

            // UPDATE INSTALL STATUS
            LabelStatus.Text = "Done!";
            isInstalling = false;

            MessageBox.Show("Your server " + "(" + versionNumber.ToString() + ")" + " has now been installed to: " + folderLoc.ToString(), "Server Installed!");
        }

        #region INSTALL TASKS (ASYNC)

        async Task ServerSetup()
        {

            #region CREATE CONFIG FROM EMBEDED RESOURCE

            // MOVE EMBEDED CONFIG FILE TO THE SERVER ROOT
            using (Stream s = new FileStream(folderLoc + @"\server.cfg", FileMode.Create))
            {
                Assembly.GetExecutingAssembly().GetManifestResourceStream("FX_Server_Installer.server.cfg").CopyTo(s);
            }

            #endregion
           
            Task.Delay(100);

            #region OPEN SERVER CONFIG, AND EDIT IT
            // OPEN CONFIG FILE
            var content = string.Empty;
            using (StreamReader reader = new StreamReader( folderLoc + @"\server.cfg" ))
            {
                content = reader.ReadToEnd();
                reader.Close();
            }

            #region EDITING SETTINGS
            // ENABLE OR DISABLE SCRIPTHOOK
            content = Regex.Replace(content, @"sv_scriptHookAllowed \d", "sv_scriptHookAllowed " + Convert.ToInt32(CheckAllowScripthook.Checked));

            // ENABLE OR DISABLE GLOBAL SERVER LISTING
            if (CheckShowServer.Checked == true)
            {
                content = Regex.Replace(content, "DISPLAYSERVER", "#sv_master1 \"\"", RegexOptions.Singleline);
            }
            else
            {
                content = Regex.Replace(content, "DISPLAYSERVER", "sv_master1 \"\"", RegexOptions.Singleline);
            }
            
            // APPLY ENDPOINT PRIVACY SETTINGS
            content = Regex.Replace(content, @"sv_endpointprivacy \w+", "sv_endpointprivacy " + CheckAllowScripthook.Checked.ToString().ToLower(), RegexOptions.Singleline);

            // CHANGE SERVER NAME
            if (ServerName.Text != "")
            {
                content = Regex.Replace(content, @"SERVERNAME", "sv_hostname \"" + ServerName.Text + "\"", RegexOptions.Singleline);
            }
            else
            {
                content = Regex.Replace(content, @"SERVERNAME", "sv_hostname \"" + "MY NEW FX SERVER CREATED WITH FX INSTALLER" + "\"", RegexOptions.Singleline);
            }
            #endregion

            // SAVE CONFIG FILE
            using (StreamWriter writer = new StreamWriter(folderLoc + @"\server.cfg"))
            {
                writer.Write( content );
                writer.Close();
            }
            #endregion

            #region CREATE SIMPLE SERVER STARTER
            if (CheckAddSimpleStarter.Checked)
            {
                File.WriteAllText(folderLoc + @"\Simple Server Starter.bat", @"run.cmd +exec server.cfg");
            }
            #endregion

        }

        async Task CleanUpServer()
        {
            // DELETE DOWNLOADED ZIP FILES AND OLD DIRECTORIES
            if (Directory.Exists(folderLoc + @"\cfx-server-data-master"))
            {
                Directory.Delete(folderLoc + @"\cfx-server-data-master",true);
            }

            

            if (File.Exists(folderLoc + @"\server.zip"))
            {
                File.Delete(folderLoc + @"\server.zip");
            }

            if (File.Exists(folderLoc + @"\master.zip"))
            {
                File.Delete(folderLoc + @"\master.zip");
            }


        }
        
        async Task InstallResources()
        {
            // FIND RESOURCES FOLDER (FIRST ONE IF MULTIPLE EXSIST)
            string resourcesFolder = folderLoc + @"\cfx-server-data-master\resources";
            
            Directory.Move(resourcesFolder, folderLoc + @"\resources");            

        }

        async Task ExtractServer()
        {
            using (ZipFile zip = ZipFile.Read(folderLoc+ @"\server.zip" ))
            {
                zip.ExtractAll(folderLoc);
            }
        }

        async Task ExtractResources()
        {
            using (ZipFile zip = ZipFile.Read(folderLoc + @"\master.zip"))
            {
                zip.ExtractAll(folderLoc);
            }
        }

        #endregion

        // INSTALL BUTTON!
        private void metroButton1_Click(object sender, EventArgs e)
        {
            // MAKE SURE THE FOLDER WHERE THE SERVER SHOULD GO IS EMPTY
            if (folderLoc != null && folderLoc != "")
            {
                if (isInstalling == false)
                {
                    if (windowsServers != null)
                    {
                        InstallServer();
                    }
                    else
                    {
                        ThrowError("Server lists not found from FiveM database, restart and try again... database may have moved!", false);
                    }
                }
                else
                {
                    ThrowError("Currently installing a server! Please wait until the current server is installed to install another.", false);
                }
            }
            else
            {
                ThrowError("Please select where you want to install a server!", false);
            }
        }
       

        #endregion

        // GET FOLDER LOCATION
        #region FOLDER SELECTION
        private void metroButton2_Click(object sender, EventArgs e)
        {
            if (isInstalling == false && isDownloading == false)
            {
                FolderBrowserDialog folder = new FolderBrowserDialog();
                folder.ShowNewFolderButton = true;
                folder.Description = "Select the folder you wish to install the server too.";

                if (folder.ShowDialog() == DialogResult.OK)
                {
                    folderLoc = folder.SelectedPath;
                }
            }
            else
            {
                ThrowError("Currently installing a server, please wait until it is finished to install another!", false);
            }
        }
        #endregion

        #region VERSION AND PLATFORM SELECTION
        private void VersionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {            
        }


        void GenerateWindowsVersions()
        {
            if (WindowsVersions.Count > 0)
            {
                //FIND THE LATEST VERSION
                int latestVersion = WindowsVersions.Max();

                // LOOP THROUGH VERSIONS, ADD THEM TO THE LIST, AND ADD THE HIGHEST VERSION WITH SPECIAL TEXT
                for (int i = 0; i < WindowsVersions.Count; i++)
                {
                    if (WindowsVersions[i] != latestVersion)
                    {
                        VersionSelect.Items.Add(WindowsVersions[i]);
                    }
                    else
                    {
                        VersionSelect.Items.Add("Latest Version (" + latestVersion + ")");
                    }
                }

                // DEFAULT TO THE LATEST VERSION     
                VersionSelect.DropDownHeight = 256;
                VersionSelect.SelectedItem = "Latest Version (" + latestVersion + ")";
            }
        }

        /*
        void GenerateLinuxVersions()
        {
            if (LinuxVersions.Count > 0)
            {
                //FIND THE LATEST VERSION
                int latestVersion = LinuxVersions.Max();

                // LOOP THROUGH VERSIONS, ADD THEM TO THE LIST, AND ADD THE HIGHEST VERSION WITH SPECIAL TEXT
                for (int i = 0; i < LinuxVersions.Count; i++)
                {
                    if (LinuxVersions[i] != latestVersion)
                    {
                        VersionSelect.Items.Add(LinuxVersions[i]);
                    }
                    else
                    {
                        VersionSelect.Items.Add("Latest Version (" + latestVersion + ")");
                    }
                }

                // DEFAULT TO THE LATEST VERSION     
                VersionSelect.DropDownHeight = 256;
                VersionSelect.SelectedItem = "Latest Version (" + latestVersion + ")";
            }
        }
        */ // GENERATELINUXVERSION
        #endregion

        // GENERIC ERROR DISPLAY FUNCTION!
        private void ThrowError(string errorMessage, bool closeProgram)
        {
            if (closeProgram)
            {
                MessageBox.Show(errorMessage, "Error!", MessageBoxButtons.OK);
                Application.Exit();             
            }
            else
            {
                MessageBox.Show(errorMessage, "Error!", MessageBoxButtons.OK);
            }
        }

        // GENERIC WEB CHECKER TO ENSURE WEB ADDRESS IS REACHABLE
        private bool CanConnect(string address)
        {
            // CHECK THE USER CAN CONNECT TO THE SERVER
            HttpWebRequest rq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            rq.Method = "HEAD";
            HttpWebResponse responce;

            try
            {
                responce = (HttpWebResponse)rq.GetResponse();
            }
            catch
            {                
                return false;                
            }

            if (responce.StatusCode == HttpStatusCode.OK)
            {
                responce.Dispose();
                return true;
            }
            else
            {
                responce.Dispose();
                return false;
            }
        }

        private void metroLabel4_Click(object sender, EventArgs e)
        {

        }
    }
}
