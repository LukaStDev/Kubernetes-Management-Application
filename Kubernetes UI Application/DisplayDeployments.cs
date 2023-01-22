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
    public partial class DisplayDeployments : Form
    {
        public DataTable dt;
        public Kubernetes Client;
        public DisplayDeployments(Kubernetes client)
        {
            InitializeComponent();
            Client = client;
            comboBoxNS.SelectedIndexChanged -= new System.EventHandler(GetNamespacedDeployments);
            comboBoxNS.SelectedIndex = 0;
            comboBoxNS.SelectedIndexChanged += new System.EventHandler(GetNamespacedDeployments);
        }
        private async Task<k8s.Models.V1NamespaceList> GetNamespacesAsync()
        {
            var Ns = await Client.CoreV1.ListNamespaceAsync();
            return Ns;
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Form Create = new CreateDeployment(Client);
            Create.Show();
        }

        private async void GetNamespacedDeployments(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            var ServiceList = await Client.AppsV1.ListNamespacedDeploymentAsync(comboBoxNS.Text);
            foreach (var item in ServiceList.Items)
            {
                try
                {
                    DateTime creationTime = (DateTime)item.Metadata.CreationTimestamp;

                    var Date = creationTime.ToShortDateString();
                    var Age = DateTime.Now.Subtract(creationTime);


                    string app = "";
                    if (item.Spec.Template.Metadata.Labels != null)
                    {
                        if (item.Spec.Template.Metadata.Labels.ContainsKey("k8s-app"))
                        {
                            app = item.Spec.Template.Metadata.Labels["k8s-app"];
                        }
                        else if (item.Spec.Template.Metadata.Labels.ContainsKey("app"))
                        {
                            app = item.Spec.Template.Metadata.Labels["app"];
                        }

                    }

                    dt.Rows.Add(new string[]
                    {
                        item.Name(),
                        creationTime.ToLongDateString() + " - " + creationTime.ToLongTimeString(),
                        app,
                        item.Spec.Replicas.ToString(),
                        comboBoxNS.Text
                    });
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                dataGridView1.Refresh();

            }
        }

        private async void DisplayDeoployments_Load(object sender, EventArgs e)
        {
            LoadTheme();
            V1NamespaceList NsList = await GetNamespacesAsync();

            dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Created:");
            dt.Columns.Add("App");
            dt.Columns.Add("Replicas");
            dt.Columns.Add("Namespace");
            foreach (var Ns in NsList.Items)
            {
                comboBoxNS.Items.Add(Ns.Name());

                var AppList = await Client.AppsV1.ListNamespacedDeploymentAsync(Ns.Name());
                foreach (var item in AppList.Items)
                {
                    try
                    {
                        DateTime creationTime = (DateTime)item.Metadata.CreationTimestamp;

                        var Date = creationTime.ToShortDateString();
                        var Age = DateTime.Now.Subtract(creationTime);
                        

                        string app = "";
                        if (item.Spec.Template.Metadata.Labels != null)
                        {
                            if (item.Spec.Template.Metadata.Labels.ContainsKey("k8s-app"))
                            {
                                app = item.Spec.Template.Metadata.Labels["k8s-app"];
                            }
                            else if (item.Spec.Template.Metadata.Labels.ContainsKey("app"))
                            {
                                app = item.Spec.Template.Metadata.Labels["app"];
                            }

                        }

                        dt.Rows.Add(new string[]
                        {
                        item.Name(),
                        creationTime.ToLongDateString() + " - " + creationTime.ToLongTimeString(),
                        app,
                        item.Spec.Replicas.ToString(),
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
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Refresh();
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

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await Client.AppsV1.DeleteNamespacedDeploymentAsync(dataGridView1.SelectedCells[0].Value.ToString(),
                    dataGridView1.SelectedCells[4].Value.ToString());
                MessageBox.Show("Succsess!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonEdit_Click(object sender, EventArgs e)
        {
            V1Deployment Edit = await Client.ReadNamespacedDeploymentAsync(dataGridView1.SelectedCells[0].Value.ToString(),
                    dataGridView1.SelectedCells[4].Value.ToString());

            Form Create = new CreateDeployment(Client, Edit, dataGridView1.SelectedCells[4].Value.ToString());
            Create.Show();
        }
    }
}
