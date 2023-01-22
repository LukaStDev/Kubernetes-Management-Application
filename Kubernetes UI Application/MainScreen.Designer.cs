namespace Kubernetes_UI_Application
{
    partial class MainScreen
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.panelMenu = new System.Windows.Forms.Panel();
            this.buttonPods = new System.Windows.Forms.Button();
            this.buttonDeployments = new System.Windows.Forms.Button();
            this.buttonServices = new System.Windows.Forms.Button();
            this.buttonNamespaces = new System.Windows.Forms.Button();
            this.buttonNodes = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.labelK8s = new System.Windows.Forms.Label();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.labelHome = new System.Windows.Forms.Label();
            this.panelDesktopPane = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenu.Controls.Add(this.buttonPods);
            this.panelMenu.Controls.Add(this.buttonDeployments);
            this.panelMenu.Controls.Add(this.buttonServices);
            this.panelMenu.Controls.Add(this.buttonNamespaces);
            this.panelMenu.Controls.Add(this.buttonNodes);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(150, 503);
            this.panelMenu.TabIndex = 0;
            // 
            // buttonPods
            // 
            this.buttonPods.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonPods.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonPods.FlatAppearance.BorderSize = 0;
            this.buttonPods.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPods.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonPods.Location = new System.Drawing.Point(0, 223);
            this.buttonPods.Name = "buttonPods";
            this.buttonPods.Size = new System.Drawing.Size(150, 40);
            this.buttonPods.TabIndex = 5;
            this.buttonPods.Text = "Pods:";
            this.buttonPods.UseVisualStyleBackColor = true;
            this.buttonPods.Click += new System.EventHandler(this.buttonPods_Click);
            // 
            // buttonDeployments
            // 
            this.buttonDeployments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonDeployments.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonDeployments.FlatAppearance.BorderSize = 0;
            this.buttonDeployments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeployments.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonDeployments.Location = new System.Drawing.Point(0, 183);
            this.buttonDeployments.Name = "buttonDeployments";
            this.buttonDeployments.Size = new System.Drawing.Size(150, 40);
            this.buttonDeployments.TabIndex = 4;
            this.buttonDeployments.Text = "Deployments:";
            this.buttonDeployments.UseVisualStyleBackColor = true;
            this.buttonDeployments.Click += new System.EventHandler(this.buttonDeployments_Click);
            // 
            // buttonServices
            // 
            this.buttonServices.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonServices.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonServices.FlatAppearance.BorderSize = 0;
            this.buttonServices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonServices.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonServices.Location = new System.Drawing.Point(0, 143);
            this.buttonServices.Name = "buttonServices";
            this.buttonServices.Size = new System.Drawing.Size(150, 40);
            this.buttonServices.TabIndex = 3;
            this.buttonServices.Text = "Services:";
            this.buttonServices.UseVisualStyleBackColor = true;
            this.buttonServices.Click += new System.EventHandler(this.buttonServices_Click);
            // 
            // buttonNamespaces
            // 
            this.buttonNamespaces.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonNamespaces.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonNamespaces.FlatAppearance.BorderSize = 0;
            this.buttonNamespaces.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNamespaces.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonNamespaces.Location = new System.Drawing.Point(0, 103);
            this.buttonNamespaces.Name = "buttonNamespaces";
            this.buttonNamespaces.Size = new System.Drawing.Size(150, 40);
            this.buttonNamespaces.TabIndex = 2;
            this.buttonNamespaces.Text = "Namespaces:";
            this.buttonNamespaces.UseVisualStyleBackColor = true;
            this.buttonNamespaces.Click += new System.EventHandler(this.buttonNamespaces_ClickAsync);
            // 
            // buttonNodes
            // 
            this.buttonNodes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonNodes.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonNodes.FlatAppearance.BorderSize = 0;
            this.buttonNodes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonNodes.ForeColor = System.Drawing.Color.Gainsboro;
            this.buttonNodes.Location = new System.Drawing.Point(0, 63);
            this.buttonNodes.Name = "buttonNodes";
            this.buttonNodes.Size = new System.Drawing.Size(150, 40);
            this.buttonNodes.TabIndex = 1;
            this.buttonNodes.Text = "Nodes:";
            this.buttonNodes.UseVisualStyleBackColor = true;
            this.buttonNodes.Click += new System.EventHandler(this.buttonNodes_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelLogo.Controls.Add(this.labelK8s);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(150, 63);
            this.panelLogo.TabIndex = 0;
            // 
            // labelK8s
            // 
            this.labelK8s.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelK8s.ForeColor = System.Drawing.Color.White;
            this.labelK8s.Location = new System.Drawing.Point(3, 0);
            this.labelK8s.Name = "labelK8s";
            this.labelK8s.Size = new System.Drawing.Size(144, 60);
            this.labelK8s.TabIndex = 2;
            this.labelK8s.Text = "Kubernetes";
            this.labelK8s.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelK8s.Click += new System.EventHandler(this.labelK8s_Click);
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(108)))), ((int)(((byte)(230)))));
            this.panelTitleBar.Controls.Add(this.labelHome);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(150, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(849, 63);
            this.panelTitleBar.TabIndex = 1;
            // 
            // labelHome
            // 
            this.labelHome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelHome.AutoSize = true;
            this.labelHome.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHome.ForeColor = System.Drawing.Color.White;
            this.labelHome.Location = new System.Drawing.Point(366, 9);
            this.labelHome.Name = "labelHome";
            this.labelHome.Size = new System.Drawing.Size(74, 30);
            this.labelHome.TabIndex = 0;
            this.labelHome.Text = "HOME";
            // 
            // panelDesktopPane
            // 
            this.panelDesktopPane.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelDesktopPane.BackgroundImage")));
            this.panelDesktopPane.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panelDesktopPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktopPane.Location = new System.Drawing.Point(150, 63);
            this.panelDesktopPane.Name = "panelDesktopPane";
            this.panelDesktopPane.Size = new System.Drawing.Size(849, 440);
            this.panelDesktopPane.TabIndex = 2;
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 503);
            this.Controls.Add(this.panelDesktopPane);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Name = "MainScreen";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Shutdown);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button buttonPods;
        private System.Windows.Forms.Button buttonDeployments;
        private System.Windows.Forms.Button buttonServices;
        private System.Windows.Forms.Button buttonNamespaces;
        private System.Windows.Forms.Button buttonNodes;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label labelHome;
        private System.Windows.Forms.Label labelK8s;
        private System.Windows.Forms.Panel panelDesktopPane;
    }
}
