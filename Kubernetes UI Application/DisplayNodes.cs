using k8s;
using k8s.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kubernetes_UI_Application
{
    public partial class DisplayNodes : Form
    {
        public DataTable dt;
        public Kubernetes Client;
        public DisplayNodes(Kubernetes client)
        {
            InitializeComponent();
            Client = client;
            this.buttonDelete.Enabled = false;
            this.buttonEdit.Enabled = false;
            this.buttonNew.Enabled = false;
        }
        
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SecondaryColor;
                }
            }

        }

        private async Task<k8s.Models.V1NodeList> GetNodesAsync()
        {
            var Ns = await Client.CoreV1.ListNodeAsync();
            return Ns;
        }

        private async void DisplayNodes_Load(object sender, EventArgs e)
        {
            LoadTheme();
            var List = await GetNodesAsync();


            dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Control Plane:");
            dt.Columns.Add("Capactiy(CPU)");
            dt.Columns.Add("Capactiy(RAM)");
            dt.Columns.Add("Pods");
            dt.Columns.Add("IP Address");
            foreach (var node in List.Items)
            {
                string RAMFormated = node.Status.Capacity["memory"].Value;
                dt.Rows.Add(new string[]
                {
                    node.Metadata.Name,
                    node.Metadata.Labels["node.kubernetes.io/microk8s-controlplane"],
                    node.Status.Capacity["cpu"].Value,
                    RAMFormated,
                    node.Status.Capacity["pods"].Value,
                    node.Metadata.Annotations["projectcalico.org/IPv4Address"],

                });
            }
            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }
    }
}
