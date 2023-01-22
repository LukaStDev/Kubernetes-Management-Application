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
    public partial class DisplayNamespaces : Form
    {
        public DataTable dt;
        public Kubernetes Client;

        public DisplayNamespaces(Kubernetes client)
        {
            InitializeComponent();
            Client = client;
            buttonEdit.Enabled = false;

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

        
        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await Client.CoreV1.DeleteNamespaceAsync(dataGridView1.SelectedCells[0].Value.ToString());
                MessageBox.Show("Succsess!!!");
            }
            catch (Exception)
            {
                MessageBox.Show("Forbidden!!!\nYou tried doing something stupid didn't you... It won't work, stop wasting my time");
            }
            
            
        }

        private async void DisplayNamespaces_Load(object sender, EventArgs e)
        {
            LoadTheme();
            V1NamespaceList List = await GetNamespacesAsync();
            comboBoxNS.SelectedIndex = 0;
            

            dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Created:");
            dt.Columns.Add("Age (Hours)");
            foreach (var Ns in List.Items)
            {

                
                DateTime creationTime = (DateTime)Ns.Metadata.CreationTimestamp;

                var Date = creationTime.ToShortDateString();
                var Age = DateTime.Now.Subtract(creationTime);
                string[] data = new string[] { Ns.Name(), Date, ((long)Age.TotalHours).ToString() };

                dt.Rows.Add(data);

            }

            dataGridView1.DataSource = dt;
            dataGridView1.Refresh();
        }

        

        private void buttonNew_Click(object sender, EventArgs e)
        {
            Form Create = new CreateNamespace(Client);
            Create.Show();
        }
    }

    
}
