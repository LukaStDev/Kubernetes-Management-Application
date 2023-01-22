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
    public partial class CreateDeployment : Form
    {
        Kubernetes Client;
        V1Deployment Edit;
        string NsEditName;
        public CreateDeployment(Kubernetes client, V1Deployment edit = null, string nsEditName = "")
        {
            InitializeComponent();
            Client = client;
            Edit = edit;
            NsEditName = nsEditName;

            if(Edit != null && NsEditName != "")
            {
                PopulateFormEdit();
            }
        }
        private async Task<List<string>> GetImageList()
        {
            List<string> ImageList = new List<string>();
            var NodeList = await Client.CoreV1.ListNodeAsync();

            foreach (var Node in NodeList.Items)
            {
                foreach (var Image in Node.Status.Images)
                {
                    int LastIndex = Image.Names[0].LastIndexOf('/');
                    int Count = Image.Names[0].Length;
                    string Capture = Image.Names[0].Substring(LastIndex + 1, Count - LastIndex - 1);
                    ImageList.Add(Capture);
                }
            }

            return ImageList;
        }
        private async Task<k8s.Models.V1NamespaceList> GetNamespacesAsync()
        {
            var Ns = await Client.CoreV1.ListNamespaceAsync();
            return Ns;
        }

        private void PopulateFormEdit()
        {
            labelHeader.Text = "Edit Deplyoment";
            buttonCreate.Text = "Update Deployment";

            comboBoxNs.Text = NsEditName;
            textBoxName.Text = this.Edit.Name();

            if (Edit.Spec.Template.Metadata.Labels != null)
            {
                if (Edit.Spec.Template.Metadata.Labels.ContainsKey("k8s-app"))
                {
                    textBoxAppName.Text = Edit.Spec.Template.Metadata.Labels["k8s-app"];
                }
                else if (Edit.Spec.Template.Metadata.Labels.ContainsKey("app"))
                {
                    textBoxAppName.Text = Edit.Spec.Template.Metadata.Labels["app"];
                }

            }

            comboBoxImage.Text = Edit.Spec.Template.Spec.Containers[0].Image;

            numericUpDownRepl.Value = Edit.Spec.Replicas.Value;
            
            


        }

        private async void PatchDeployment()
        {
            V1ContainerPort port = null;
            string ContainerPort = "0";
            
            string PatchJSSON =
                @"{ ""metadata"": 
{ ""name"": """ + textBoxName.Text.ToLower() + @"""}," +
@"""spec"": { ""replicas"": " + ((int)numericUpDownRepl.Value).ToString() + @", 
                ""selector"": { ""matchLabels"": {""app"": """ + textBoxAppName.Text.ToLower() + @"""}},
""template"": { ""metadata"": { ""labels"": { ""app"": """ + textBoxAppName.Text.ToLower() + @"""}},
""spec"": { ""containers"": [ { ""name"": """ + textBoxAppName.Text.ToLower() + @""", ""image"": """ + comboBoxImage.Text +
@"""}]}}}}"; //


            try
            {
                var result = await Client.AppsV1.PatchNamespacedDeploymentAsync(
                    new V1Patch(PatchJSSON, V1Patch.PatchType.MergePatch),
                    Edit.Name(), comboBoxNs.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            

            File.WriteAllText("testDeplJson.txt", PatchJSSON);
            
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            if(this.Edit != null && NsEditName != "")
            {
                PatchDeployment();
                return;
            }
            
            V1Deployment New = new V1Deployment {
                Metadata = new V1ObjectMeta
                {
                    Name = textBoxName.Text.ToLower()
                },
                Spec = new V1DeploymentSpec
                {
                    Replicas = ((int)numericUpDownRepl.Value),
                    Selector = new V1LabelSelector
                    {
                        MatchLabels = new Dictionary<string, string>()
                        {
                            {"app",  textBoxAppName.Text.ToLower()}
                        }
                    },
                    Template = new V1PodTemplateSpec
                    {
                        Metadata = new V1ObjectMeta
                        {
                            Labels = new Dictionary<string, string>()
                            {
                                {"app",  textBoxAppName.Text.ToLower()}
                            }
                        },
                        Spec = new V1PodSpec
                        {
                            Containers = new List<V1Container>()
                            {
                                new V1Container
                                {
                                    Name = textBoxAppName.Text.ToLower(),
                                    Image = comboBoxImage.Text,
                                    
                                }
                            }
                        }
                    }
                }
            };

            try
            {
                var response = await Client.AppsV1.CreateNamespacedDeploymentAsync(New, comboBoxNs.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void CreateDeployment_Load(object sender, EventArgs e)
        {
            var NsList = await GetNamespacesAsync();
            foreach (var Namespace in NsList.Items)
            {
                comboBoxNs.Items.Add(Namespace.Name());
            }

            var ImageList = await GetImageList();
            comboBoxImage.Items.AddRange(ImageList.ToArray());
        }
    }
}
