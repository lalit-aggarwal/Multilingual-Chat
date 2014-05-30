using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SS.GoogleTranslation;
using SS.GoogleTranslation.API;
using SS.GoogleTranslation.WebProviders;

namespace MultiChatClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(GoogleTranslator.Languages.ToArray());
            comboBox2.Items.AddRange(GoogleTranslator.Languages.ToArray());
            textBox2.Text = leetSocket1.ServerIpAsDNS.ToString();
            comboBox1.Text = "English";
            comboBox2.Text = "French";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "Client : " + textBox1.Text + Environment.NewLine;
            leetSocket1.sendObject(textBox1.Text.ToString());
            textBox1.Text = null;
        }

        private void leetSocket1_OnReceiveCompletedDataEVENT(object value, byte[] bArray)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(delegate() { leetSocket1_OnReceiveCompletedDataEVENT(value, bArray); }));
                return;
            }
            string received_value = (string)value;
            StringGoogleTranslator translate = new StringGoogleTranslator();
            received_value = translate.Translate(received_value, comboBox2.Text.ToString(), comboBox1.Text.ToString());
            richTextBox1.Text += "Server : " + received_value + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = null;
        }
    }
}
