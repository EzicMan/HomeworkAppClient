using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeworkApp
{
    public partial class DeleteGroup : Form
    {
        string answer = "";
        string internet = "";
        bool needInternet = false;
        public string GetName()
        {
            return answer;
        }
        public bool NeedInternet()
        {
            return needInternet;
        }
        public string GetInternet()
        {
            return internet;
        }
        public DeleteGroup(ListBox.ObjectCollection t)
        {
            InitializeComponent();
            if(t.Count == 0)
            {
                return;
            }
            foreach (var b in t)
            {
                comboBox1.Items.Add(b.ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                answer = comboBox1.SelectedItem.ToString();
                internet = comboBox1.SelectedItem.ToString() + ":" + textBox2.Text;
                needInternet = true;
                this.Hide();
                return;
            }
            answer = comboBox1.SelectedItem.ToString();
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                button1.Enabled = true;
                textBox2.Enabled = true;
            }
        }
    }
}
