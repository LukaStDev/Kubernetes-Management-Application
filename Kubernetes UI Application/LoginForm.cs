using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kubernetes_UI_Application
{
    public partial class LoginForm : Form
    {
        string IpPort = "";
        public LoginForm()
        {
            InitializeComponent();
            
        }

        


        private void button1_Click(object sender, EventArgs e)
        {
            IpPort = textBox1.Text;
            this.Visible = false;
            Form Main = new MainScreen(IpPort);
            Main.Show();
        }
    }
}
