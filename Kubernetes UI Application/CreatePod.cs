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
    public partial class CreatePod : Form
    {
        Kubernetes Client;
        
        public CreatePod(Kubernetes client)
        {
            InitializeComponent();
            Client = client;

            
        }
        private async Task<k8s.Models.V1NamespaceList> GetNamespacesAsync()
        {
            var Ns = await Client.CoreV1.ListNamespaceAsync();
            return Ns;
        }

        private async Task<List<string>> GetImageList()
        {
            List<string> ImageList = new List<string>();
            var NodeList = await Client.CoreV1.ListNodeAsync();

            foreach(var Node in NodeList.Items)
            {
                foreach(var Image in Node.Status.Images)
                {
                    int LastIndex = Image.Names[0].LastIndexOf('/');
                    int Count = Image.Names[0].Length;
                    string Capture = Image.Names[0].Substring(LastIndex+1,Count - LastIndex-1);
                    ImageList.Add(Capture);
                }
            }

            return ImageList;
        }

        

        private async void button1_Click(object sender, EventArgs e)
        {
            
            
            V1PodSecurityContext RunAsRoot;
            if (checkBox1.Checked)
            {
                RunAsRoot = new V1PodSecurityContext
                {
                    RunAsUser = 0
                };
            }
            else
            {
                RunAsRoot = null;
            }
            V1Pod v1Pod = new V1Pod
            {
                Metadata = new V1ObjectMeta
                {
                    Name = textBoxName.Text.ToLower()
                },
                Spec = new V1PodSpec
                {
                    Containers = new List<V1Container>()
                    {
                        new V1Container
                        {
                            Name = textBoxName.Text.ToLower(),
                            Image = comboBoxImage.Text,
                            Ports = new List<V1ContainerPort>()
                            {
                                new V1ContainerPort
                                {
                                    ContainerPort = ((int)numericUpDown1.Value)
                                }
                            }
                        }
                    },
                    SecurityContext = RunAsRoot
                }
            };

            try
            {
                await Client.CoreV1.CreateNamespacedPodAsync(v1Pod, comboBoxNs.Text);
                MessageBox.Show("Success!!!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void CreatePod_Load(object sender, EventArgs e)
        {
            var ImageList = await GetImageList();
            comboBoxImage.Items.AddRange(ImageList.ToArray());

            var NsList = await GetNamespacesAsync();
            foreach(var Namespace in NsList.Items)
            {
                comboBoxNs.Items.Add(Namespace.Name());
            }
        }

        private void comboBoxNs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxImage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxContName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
