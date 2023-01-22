namespace Kubernetes_UI_Application
{
    partial class CreateDeployment
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
            this.numericUpDownRepl = new System.Windows.Forms.NumericUpDown();
            this.comboBoxImage = new System.Windows.Forms.ComboBox();
            this.comboBoxNs = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelHeader = new System.Windows.Forms.Label();
            this.textBoxAppName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRepl)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownRepl
            // 
            this.numericUpDownRepl.Location = new System.Drawing.Point(98, 274);
            this.numericUpDownRepl.Maximum = new decimal(new int[] {
            66000,
            0,
            0,
            0});
            this.numericUpDownRepl.Name = "numericUpDownRepl";
            this.numericUpDownRepl.Size = new System.Drawing.Size(70, 23);
            this.numericUpDownRepl.TabIndex = 38;
            // 
            // comboBoxImage
            // 
            this.comboBoxImage.FormattingEnabled = true;
            this.comboBoxImage.Items.AddRange(new object[] {
            "ClusterIP",
            "NodePort"});
            this.comboBoxImage.Location = new System.Drawing.Point(97, 228);
            this.comboBoxImage.Name = "comboBoxImage";
            this.comboBoxImage.Size = new System.Drawing.Size(192, 23);
            this.comboBoxImage.TabIndex = 37;
            // 
            // comboBoxNs
            // 
            this.comboBoxNs.FormattingEnabled = true;
            this.comboBoxNs.Location = new System.Drawing.Point(98, 72);
            this.comboBoxNs.Name = "comboBoxNs";
            this.comboBoxNs.Size = new System.Drawing.Size(220, 23);
            this.comboBoxNs.TabIndex = 36;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 15);
            this.label6.TabIndex = 35;
            this.label6.Text = "Namespace: ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 274);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 15);
            this.label5.TabIndex = 34;
            this.label5.Text = "Replicas";
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(118, 338);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(121, 44);
            this.buttonCreate.TabIndex = 33;
            this.buttonCreate.Text = "Create new Service";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 231);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 32;
            this.label4.Text = "Image";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 30;
            this.label3.Text = "App Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(68, 132);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(250, 23);
            this.textBoxName.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 28;
            this.label2.Text = "Name: ";
            // 
            // labelHeader
            // 
            this.labelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHeader.Location = new System.Drawing.Point(0, 0);
            this.labelHeader.Name = "labelHeader";
            this.labelHeader.Size = new System.Drawing.Size(415, 40);
            this.labelHeader.TabIndex = 27;
            this.labelHeader.Text = "Create new Deployment";
            this.labelHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxAppName
            // 
            this.textBoxAppName.Location = new System.Drawing.Point(97, 186);
            this.textBoxAppName.Name = "textBoxAppName";
            this.textBoxAppName.Size = new System.Drawing.Size(221, 23);
            this.textBoxAppName.TabIndex = 39;
            // 
            // CreateDeployment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(415, 394);
            this.Controls.Add(this.textBoxAppName);
            this.Controls.Add(this.numericUpDownRepl);
            this.Controls.Add(this.comboBoxImage);
            this.Controls.Add(this.comboBoxNs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelHeader);
            this.Name = "CreateDeployment";
            this.Text = "CreateDeployment";
            this.Load += new System.EventHandler(this.CreateDeployment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRepl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownRepl;
        private System.Windows.Forms.ComboBox comboBoxImage;
        private System.Windows.Forms.ComboBox comboBoxNs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelHeader;
        private System.Windows.Forms.TextBox textBoxAppName;
    }
}