using k8s;
using k8s.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Kubernetes_UI_Application
{
    public partial class CreateNamespace : Form
    {
        private string Editing; 
        private Kubernetes Client;
        public CreateNamespace(Kubernetes client)
        {
            InitializeComponent();
            Client = client;
            
        }   

        
        private async void button1_Click_1(object sender, EventArgs e)
        {
            
            
            
            var ns = new V1Namespace
            {
                Metadata = new V1ObjectMeta
                {
                    Name = textBox1.Text.ToLower()
                }
            };

            var result = await Client.CoreV1.CreateNamespaceAsync(ns);
            MessageBox.Show(result.ToString());
        }

        
    

            
        
    }
}
