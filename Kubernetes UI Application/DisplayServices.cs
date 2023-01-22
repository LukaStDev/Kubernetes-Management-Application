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
    public partial class DisplayServices : Form
    {
        public DataTable dt;
        public Kubernetes Client;
        
        public DisplayServices(Kubernetes client)
        {
            InitializeComponent();
            Client = client;
            comboBoxNS.SelectedIndexChanged -= new System.EventHandler(GetNamespacedService);
            comboBoxNS.SelectedIndex = 0;
            comboBoxNS.SelectedIndexChanged += new System.EventHandler(GetNamespacedService);
        }

        private async Task<k8s.Models.V1NamespaceList> GetNamespacesAsync()
        {
            var Ns = await Client.CoreV1.ListNamespaceAsync();
            return Ns;
        }

        private async void GetNamespacedService(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            var ServiceList = await Client.CoreV1.ListNamespacedServiceAsync(comboBoxNS.Text);
            foreach (var item in ServiceList.Items)
            {
                try
                {
                    DateTime creationTime = (DateTime)item.Metadata.CreationTimestamp;

                    var Date = creationTime.ToShortDateString();
                    var Age = DateTime.Now.Subtract(creationTime);
                    string Ports = "";
                    if (item.Spec.Ports != null)
                        foreach (var port in item.Spec.Ports)
                            Ports = port.Name + ", " + port.Port + ", " + port.TargetPort;

                    string app = "";
                    if (item.Spec.Selector != null)
                    {
                        if (item.Spec.Selector.ContainsKey("k8s-app"))
                        {
                            app = item.Spec.Selector["k8s-app"];
                        }
                        else if (item.Spec.Selector.ContainsKey("app"))
                        {
                            app = item.Spec.Selector["app"];
                        }

                    }

                    dt.Rows.Add(new string[]
                    {
                        item.Name(),
                        creationTime.ToLongDateString() + " - " + creationTime.ToLongTimeString(),
                        app,
                        Ports,
                        item.Spec.ClusterIP,
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

        private async void DisplayServices_Load(object sender, EventArgs e)
        {
            LoadTheme();
            V1NamespaceList NsList = await GetNamespacesAsync();

            dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Created:");
            dt.Columns.Add("App");
            dt.Columns.Add("Ports");
            dt.Columns.Add("Custer IP");
            dt.Columns.Add("Namespace");
            foreach (var Ns in NsList.Items)
            {
                comboBoxNS.Items.Add(Ns.Name());

                var ServiceList = await Client.CoreV1.ListNamespacedServiceAsync(Ns.Name());
                foreach (var item in ServiceList.Items)
                {
                    try
                    {
                        DateTime creationTime = (DateTime)item.Metadata.CreationTimestamp;

                        var Date = creationTime.ToShortDateString();
                        var Age = DateTime.Now.Subtract(creationTime);
                        string Ports = "";
                        if (item.Spec.Ports != null)
                            foreach (var port in item.Spec.Ports)
                                Ports = port.Name + ", " + port.Port + ", " + port.TargetPort;

                        string app = "";
                        if (item.Spec.Selector != null)
                        {
                            if (item.Spec.Selector.ContainsKey("k8s-app"))
                            {
                                app = item.Spec.Selector["k8s-app"];
                            }
                            else if (item.Spec.Selector.ContainsKey("app"))
                            {
                                app = item.Spec.Selector["app"];
                            }
                            
                        }

                        dt.Rows.Add(new string[]
                        {
                        item.Name(),
                        creationTime.ToLongDateString() + " - " + creationTime.ToLongTimeString(),
                        app,
                        Ports,
                        item.Spec.ClusterIP,
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
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Refresh();
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Form CreateService = new CreateService(Client);
            CreateService.Show();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await Client.CoreV1.DeleteNamespacedServiceAsync(dataGridView1.SelectedCells[0].Value.ToString(),
                    dataGridView1.SelectedCells[5].Value.ToString());
                MessageBox.Show("Succsess!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void buttonEdit_Click(object sender, EventArgs e)
        {
            V1Service Edit = await Client.CoreV1.ReadNamespacedServiceAsync(dataGridView1.SelectedCells[0].Value.ToString(),
                    dataGridView1.SelectedCells[5].Value.ToString());

            Form Create = new CreateService(Client, Edit, dataGridView1.SelectedCells[5].Value.ToString());
            Create.Show();
        }
    }
}
