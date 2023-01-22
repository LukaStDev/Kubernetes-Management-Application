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
    public partial class DisplayPods : Form
    {
        public DataTable dt;
        public Kubernetes Client;
        public DisplayPods(Kubernetes client)
        {
            InitializeComponent();
            Client = client;
            comboBoxNS.SelectedIndexChanged -= new System.EventHandler(GetNamespacedPod);
            comboBoxNS.SelectedIndex = 0;
            comboBoxNS.SelectedIndexChanged += new System.EventHandler(GetNamespacedPod);
        }
        private async Task<k8s.Models.V1NamespaceList> GetNamespacesAsync()
        {
            var Ns = await Client.CoreV1.ListNamespaceAsync();
            return Ns;
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

        private async void DisplayPods_Load(object sender, EventArgs e)
        {
            LoadTheme();
            V1NamespaceList NsList = await GetNamespacesAsync();
            


            dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Created:");
            dt.Columns.Add("App");
            dt.Columns.Add("Image");
            dt.Columns.Add("Status");
            dt.Columns.Add("Ports");
            dt.Columns.Add("Namespace");
            foreach (var Ns in NsList.Items)
            {
                comboBoxNS.Items.Add(Ns.Name());

                var PodList = await Client.CoreV1.ListNamespacedPodAsync(Ns.Metadata.Name);
                foreach (var item in PodList.Items)
                {
                    try
                    {
                        DateTime creationTime = (DateTime)item.Metadata.CreationTimestamp;

                        var Date = creationTime.ToShortDateString();
                        var Age = DateTime.Now.Subtract(creationTime);
                        string Ports = "";
                        if (item.Spec.Containers[0].Ports != null)
                            foreach (var port in item.Spec.Containers[0].Ports)
                                Ports += port.ContainerPort + " ";

                        string app = "";
                        if (item.Metadata.Labels != null)
                        {
                            if (item.Metadata.Labels.ContainsKey("k8s-app"))
                            {
                                app = item.Metadata.Labels["k8s-app"];
                            }
                            else if (item.Metadata.Labels.ContainsKey("app"))
                            {
                                app = item.Metadata.Labels["app"];
                            }
                            
                        }

                        dt.Rows.Add(new string[]
                        {
                        item.Name(),
                        creationTime.ToLongDateString() + " - " + creationTime.ToLongTimeString(),
                        app,
                        item.Spec.Containers[0].Image,
                        item.Status.Phase,
                        Ports,
                        Ns.Name()
                        });
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                    }
                    
                }

            }

            dataGridView1.DataSource = dt;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Refresh();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await Client.CoreV1.DeleteNamespacedPodAsync(dataGridView1.SelectedCells[0].Value.ToString(),
                    dataGridView1.SelectedCells[6].Value.ToString());
                MessageBox.Show("Succsess!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void GetNamespacedPod(object sender, EventArgs e)
        {
            string NsName;
            dt.Rows.Clear();
            if(comboBoxNS.Text == "All")
            {
                DisplayPods_Load(sender, e);
                return;
            } else
            {
                NsName = comboBoxNS.Text;
            }
            var PodList = await Client.CoreV1.ListNamespacedPodAsync(NsName);
            foreach (var item in PodList.Items)
            {
                DateTime creationTime = (DateTime)item.Metadata.CreationTimestamp;

                var Date = creationTime.ToShortDateString();
                var Age = DateTime.Now.Subtract(creationTime);
                string Ports = "";
                if (item.Spec.Containers[0].Ports != null)
                    foreach (var port in item.Spec.Containers[0].Ports)
                        Ports += port.ContainerPort + " ";

                string app = "";
                if (item.Metadata.Labels != null)
                {
                    if (item.Metadata.Labels.ContainsKey("k8s-app"))
                    {
                        app = item.Metadata.Labels["k8s-app"];
                    }
                    else
                    {
                        app = item.Metadata.Labels["app"];
                    }
                }

                dt.Rows.Add(new string[]
                {
                    item.Name(),
                    creationTime.ToLongDateString() + " - " + creationTime.ToLongTimeString(),
                    app,
                    item.Spec.Containers[0].Image,
                    item.Status.Phase,
                    Ports,
                    NsName
                });
            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Form CreatePod = new CreatePod(Client);
            CreatePod.Show();
        }
    }
}
