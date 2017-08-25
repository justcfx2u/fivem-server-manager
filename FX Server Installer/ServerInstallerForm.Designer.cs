namespace FX_Server_Installer
{
    partial class ServerInstallerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerInstallerForm));
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.ProgressBar = new MetroFramework.Controls.MetroProgressBar();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.LabelStatus = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.ServerName = new MetroFramework.Controls.MetroTextBox();
            this.CheckAllowScripthook = new MetroFramework.Controls.MetroCheckBox();
            this.CheckEndPointPrivacy = new MetroFramework.Controls.MetroCheckBox();
            this.CheckShowServer = new MetroFramework.Controls.MetroCheckBox();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.VersionSelect = new MetroFramework.Controls.MetroComboBox();
            this.CheckAddSimpleStarter = new MetroFramework.Controls.MetroCheckBox();
            this.SuspendLayout();
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(176, 191);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(175, 40);
            this.metroButton1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroButton1.TabIndex = 0;
            this.metroButton1.Text = "Install";
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(17, 246);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(334, 10);
            this.ProgressBar.Style = MetroFramework.MetroColorStyle.Green;
            this.ProgressBar.TabIndex = 1;
            this.ProgressBar.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(17, 270);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(52, 20);
            this.metroLabel1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "Status: ";
            this.metroLabel1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // LabelStatus
            // 
            this.LabelStatus.AutoSize = true;
            this.LabelStatus.Location = new System.Drawing.Point(60, 270);
            this.LabelStatus.Name = "LabelStatus";
            this.LabelStatus.Size = new System.Drawing.Size(47, 20);
            this.LabelStatus.Style = MetroFramework.MetroColorStyle.Green;
            this.LabelStatus.TabIndex = 3;
            this.LabelStatus.Text = "Done!";
            this.LabelStatus.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(18, 78);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(95, 20);
            this.metroLabel3.Style = MetroFramework.MetroColorStyle.Green;
            this.metroLabel3.TabIndex = 4;
            this.metroLabel3.Text = "Server Name:";
            this.metroLabel3.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ServerName
            // 
            // 
            // 
            // 
            this.ServerName.CustomButton.Image = null;
            this.ServerName.CustomButton.Location = new System.Drawing.Point(124, 1);
            this.ServerName.CustomButton.Name = "";
            this.ServerName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.ServerName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.ServerName.CustomButton.TabIndex = 1;
            this.ServerName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ServerName.CustomButton.UseSelectable = true;
            this.ServerName.CustomButton.Visible = false;
            this.ServerName.Lines = new string[0];
            this.ServerName.Location = new System.Drawing.Point(18, 101);
            this.ServerName.MaxLength = 32767;
            this.ServerName.Name = "ServerName";
            this.ServerName.PasswordChar = '\0';
            this.ServerName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.ServerName.SelectedText = "";
            this.ServerName.SelectionLength = 0;
            this.ServerName.SelectionStart = 0;
            this.ServerName.ShortcutsEnabled = true;
            this.ServerName.Size = new System.Drawing.Size(152, 29);
            this.ServerName.Style = MetroFramework.MetroColorStyle.Green;
            this.ServerName.TabIndex = 5;
            this.ServerName.Theme = MetroFramework.MetroThemeStyle.Light;
            this.ServerName.UseSelectable = true;
            this.ServerName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.ServerName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // CheckAllowScripthook
            // 
            this.CheckAllowScripthook.AutoSize = true;
            this.CheckAllowScripthook.Location = new System.Drawing.Point(18, 145);
            this.CheckAllowScripthook.Name = "CheckAllowScripthook";
            this.CheckAllowScripthook.Size = new System.Drawing.Size(121, 17);
            this.CheckAllowScripthook.Style = MetroFramework.MetroColorStyle.Green;
            this.CheckAllowScripthook.TabIndex = 8;
            this.CheckAllowScripthook.Text = "Allow Scripthook";
            this.CheckAllowScripthook.Theme = MetroFramework.MetroThemeStyle.Light;
            this.CheckAllowScripthook.UseSelectable = true;
            // 
            // CheckEndPointPrivacy
            // 
            this.CheckEndPointPrivacy.AutoSize = true;
            this.CheckEndPointPrivacy.Location = new System.Drawing.Point(18, 168);
            this.CheckEndPointPrivacy.Name = "CheckEndPointPrivacy";
            this.CheckEndPointPrivacy.Size = new System.Drawing.Size(123, 17);
            this.CheckEndPointPrivacy.Style = MetroFramework.MetroColorStyle.Green;
            this.CheckEndPointPrivacy.TabIndex = 9;
            this.CheckEndPointPrivacy.Text = "End Point Privacy";
            this.CheckEndPointPrivacy.Theme = MetroFramework.MetroThemeStyle.Light;
            this.CheckEndPointPrivacy.UseSelectable = true;
            // 
            // CheckShowServer
            // 
            this.CheckShowServer.AutoSize = true;
            this.CheckShowServer.Location = new System.Drawing.Point(18, 191);
            this.CheckShowServer.Name = "CheckShowServer";
            this.CheckShowServer.Size = new System.Drawing.Size(133, 17);
            this.CheckShowServer.Style = MetroFramework.MetroColorStyle.Green;
            this.CheckShowServer.TabIndex = 10;
            this.CheckShowServer.Text = "Show In Server List";
            this.CheckShowServer.Theme = MetroFramework.MetroThemeStyle.Light;
            this.CheckShowServer.UseSelectable = true;
            // 
            // metroButton2
            // 
            this.metroButton2.Location = new System.Drawing.Point(177, 157);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(174, 28);
            this.metroButton2.Style = MetroFramework.MetroColorStyle.Green;
            this.metroButton2.TabIndex = 11;
            this.metroButton2.Text = "Select Folder";
            this.metroButton2.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.metroButton2_Click);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.Location = new System.Drawing.Point(177, 78);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(55, 20);
            this.metroLabel5.Style = MetroFramework.MetroColorStyle.Green;
            this.metroLabel5.TabIndex = 15;
            this.metroLabel5.Text = "Version";
            this.metroLabel5.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // VersionSelect
            // 
            this.VersionSelect.FormattingEnabled = true;
            this.VersionSelect.ItemHeight = 24;
            this.VersionSelect.Location = new System.Drawing.Point(177, 101);
            this.VersionSelect.Name = "VersionSelect";
            this.VersionSelect.Size = new System.Drawing.Size(174, 30);
            this.VersionSelect.Style = MetroFramework.MetroColorStyle.Green;
            this.VersionSelect.TabIndex = 14;
            this.VersionSelect.Theme = MetroFramework.MetroThemeStyle.Light;
            this.VersionSelect.UseSelectable = true;
            this.VersionSelect.SelectedIndexChanged += new System.EventHandler(this.VersionSelect_SelectedIndexChanged);
            // 
            // CheckAddSimpleStarter
            // 
            this.CheckAddSimpleStarter.AutoSize = true;
            this.CheckAddSimpleStarter.Location = new System.Drawing.Point(18, 214);
            this.CheckAddSimpleStarter.Name = "CheckAddSimpleStarter";
            this.CheckAddSimpleStarter.Size = new System.Drawing.Size(134, 17);
            this.CheckAddSimpleStarter.Style = MetroFramework.MetroColorStyle.Green;
            this.CheckAddSimpleStarter.TabIndex = 16;
            this.CheckAddSimpleStarter.Text = "Add Simple Starter";
            this.CheckAddSimpleStarter.Theme = MetroFramework.MetroThemeStyle.Light;
            this.CheckAddSimpleStarter.UseSelectable = true;
            // 
            // ServerInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 303);
            this.Controls.Add(this.CheckAddSimpleStarter);
            this.Controls.Add(this.metroLabel5);
            this.Controls.Add(this.VersionSelect);
            this.Controls.Add(this.metroButton2);
            this.Controls.Add(this.CheckShowServer);
            this.Controls.Add(this.CheckEndPointPrivacy);
            this.Controls.Add(this.CheckAllowScripthook);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.LabelStatus);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.metroButton1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ServerInstallerForm";
            this.Padding = new System.Windows.Forms.Padding(18, 60, 18, 19);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "FiveM Server Installer";
            this.Load += new System.EventHandler(this.ServerInstallerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroProgressBar ProgressBar;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel LabelStatus;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox ServerName;
        private MetroFramework.Controls.MetroCheckBox CheckAllowScripthook;
        private MetroFramework.Controls.MetroCheckBox CheckEndPointPrivacy;
        private MetroFramework.Controls.MetroCheckBox CheckShowServer;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private MetroFramework.Controls.MetroComboBox VersionSelect;
        private MetroFramework.Controls.MetroCheckBox CheckAddSimpleStarter;
    }
}

