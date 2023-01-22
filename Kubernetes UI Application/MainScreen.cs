using k8s;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kubernetes_UI_Application
{
    public partial class MainScreen : Form
    {
        Kubernetes Client;

        string IPPort = "";
        Button CurrentButton;
        Random RNG;
        int TempIndex;
        private Form activeForm;

        public MainScreen(string t)
        {
            IPPort = t;
            Thread test = new Thread(Test);
            test.Start();
            test.Join();
            InitializeComponent();
            

            RNG = new Random();
            //btnCloseChildForm.Visible = false;
            this.Text = string.Empty;
            //this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private void Test()
        {
            var config = new KubernetesClientConfiguration { Host = "http://" + IPPort };

            Client = new Kubernetes(config);
            string print = "";
            var namespaces = Client.CoreV1.ListNamespace();
            foreach (var ns in namespaces.Items)
            {
                print += ns.Metadata.Name + '\n';
                var list = Client.CoreV1.ListNamespacedPod(ns.Metadata.Name);
                foreach (var item in list.Items)
                {
                    print += item.Metadata.Name + '\n';
                }
            }
            MessageBox.Show("Welcome");
        }

        private Color SelectThemeColor()
        {
            int index = RNG.Next(ThemeColor.ColorList.Count);
            while (TempIndex == index)
            {
                index = RNG.Next(ThemeColor.ColorList.Count);
            }
            TempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (CurrentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    CurrentButton = (Button)btnSender;
                    CurrentButton.BackColor = color;
                    CurrentButton.ForeColor = Color.White;
                    CurrentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    //btnCloseChildForm.Visible = true;
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void labelK8s_Click(object sender, EventArgs e)
        {

        }

        

        private void buttonNamespaces_ClickAsync(object sender, EventArgs e)
        {
            
            labelHome.Text = "Namespaces";
            OpenChildForm(new DisplayNamespaces(Client), sender);
        }

        private void buttonNodes_Click(object sender, EventArgs e)
        {
            labelHome.Text = "Nodes";
            OpenChildForm(new DisplayNodes(Client), sender);
        }

        private void buttonPods_Click(object sender, EventArgs e)
        {
            labelHome.Text = "Pods";
            OpenChildForm(new DisplayPods(Client), sender);
        }

        private void buttonServices_Click(object sender, EventArgs e)
        {
            labelHome.Text = "Services";
            OpenChildForm(new DisplayServices(Client), sender);
        }

        private void buttonDeployments_Click(object sender, EventArgs e)
        {
            labelHome.Text = "Deployments";
            OpenChildForm(new DisplayDeployments(Client), sender);
        }

        

        private void Shutdown(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
