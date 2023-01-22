using k8s;
using k8s.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kubernetes_UI_Application
{
    public partial class CreateService : Form
    {
        Kubernetes Client;
        Dictionary<string, int> ContainterPorts = new Dictionary<string, int>();

        V1Service Edit;
        string NsEditName;
        public CreateService(Kubernetes client, V1Service edit = null, string nsEditName = "")
        {
            InitializeComponent();
            Client = client;
            Edit = edit;
            NsEditName = nsEditName;

            if (Edit != null && NsEditName != "")
            {
                PopulateFormEdit();
            }
        }

        private void PopulateFormEdit()
        {
            labelHeader.Text = "Edit Service";
            buttonCreate.Text = "Update Service";

            comboBoxNs.Text = NsEditName;
            textBoxName.Text = this.Edit.Name();

            if (Edit.Spec.Selector != null)
            {
                if (Edit.Spec.Selector.ContainsKey("k8s-app"))
                {
                    comboBoxApp.Text = Edit.Spec.Selector["k8s-app"];
                }
                else if (Edit.Spec.Selector.ContainsKey("app"))
                {
                    comboBoxApp.Text = Edit.Spec.Selector["app"];
                }

            }

            comboBoxType.Enabled = false;
            
            numericUpDown1.Value = Edit.Spec.Ports[0].Port;


        }

        private async Task<k8s.Models.V1NamespaceList> GetNamespacesAsync()
        {
            var Ns = await Client.CoreV1.ListNamespaceAsync();
            return Ns;
        }

        private async Task<List<string>> GetAppList()
        {
            List<string> AppList = new List<string>();
            var DeployList = await Client.AppsV1.ListDeploymentForAllNamespacesAsync();

            foreach (var Deployment in DeployList.Items)
            {

                string app = "";
                if (Deployment.Spec.Template.Metadata.Labels != null)
                {
                    if (Deployment.Spec.Template.Metadata.Labels.ContainsKey("k8s-app"))
                    {
                        app = Deployment.Spec.Template.Metadata.Labels["k8s-app"];
                    }
                    else if (Deployment.Spec.Template.Metadata.Labels.ContainsKey("app"))
                    {
                        app = Deployment.Spec.Template.Metadata.Labels["app"];
                    }

                }

                if (!AppList.Contains(app))
                {
                    AppList.Add(app);

                    if(Deployment.Spec.Template.Spec.Containers[0].Ports != null)
                        ContainterPorts.Add(app, Deployment.Spec.Template.Spec.Containers[0].Ports[0].ContainerPort);
                }
                //foreach (var App in Deployment.Metadata.Labels[])
                //{
                //    int LastIndex = Image.Names[0].LastIndexOf('/');
                //    int Count = Image.Names[0].Length;
                //    string Capture = Image.Names[0].Substring(LastIndex + 1, Count - LastIndex - 1);
                //    ImageList.Add(Capture);
                //}
            }

            return AppList;
        }

        private async void CreateService_Load(object sender, EventArgs e)
        {
            var appList = await GetAppList();
            comboBoxApp.Items.AddRange(appList.ToArray());

            var NsList = await GetNamespacesAsync();
            foreach (var Namespace in NsList.Items)
            {
                comboBoxNs.Items.Add(Namespace.Name());
            }
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            if (this.Edit != null && NsEditName != "")
            {
                PatchService();
                return;
            }

            V1Service New = new V1Service
            {
                Metadata = new V1ObjectMeta
                {
                    Name = textBoxName.Text
                },
                Spec = new V1ServiceSpec
                {
                    //Ports = new List<V1ServicePort>()
                    //{
                    //    new V1ServicePort
                    //    {
                    //        Port = ((int)numericUpDown1.Value),
                    //        TargetPort = ContainterPorts[comboBoxApp.Text]
                    //    }
                    //},
                    Selector = new Dictionary<string, string>()
                    {
                        {"app", comboBoxApp.Text }
                    },
                    Type = comboBoxType.Text
                }
            };

            try
            {
                var result = await Client.CoreV1.CreateNamespacedServiceAsync(New, comboBoxNs.Text);
                MessageBox.Show("Success");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private async void PatchService()
        {
            string Spec = "";
            string Spec2 = "";

            string App = "";
            if (Edit.Spec.Selector != null)
            {
                if (Edit.Spec.Selector.ContainsKey("k8s-app"))
                {
                    App = @"""selector"": { ""app"": " + comboBoxApp.Text + @"""}";
                    Spec = @", ""spec"": { ";
                    Spec2 = "}";
                }
                else if (Edit.Spec.Selector.ContainsKey("app"))
                {
                    App = @"""selector"": { ""app"": """ + comboBoxApp.Text + @"""}";
                    Spec = @", ""spec"": { ";
                    Spec2 = "}";
                }
                
            }

            string Ports = "";
            if (ContainterPorts.ContainsKey(comboBoxApp.Text))
            {
                Ports = @"""ports"": [

            { ""port"": " + ((int)numericUpDown1.Value) + @",""targetPort"": " + ContainterPorts[comboBoxApp.Text] + @"}], ";

                Spec = @", ""spec"": { ";
                Spec2 = "}";
            }

            string JSONPatch = @"{ ""metadata"": { ""name"": """ + textBoxName.Text + @""", ""namespace"": """ + comboBoxNs.Text + @"""}" + Spec + Ports +
            App + Spec2 + "}";

            try
            {
                File.WriteAllText("JSON.txt", JSONPatch);
                var result = await Client.CoreV1.PatchNamespacedServiceAsync(new V1Patch(JSONPatch, V1Patch.PatchType.MergePatch),
                textBoxName.Text, comboBoxNs.Text);
                MessageBox.Show("Success!!!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
