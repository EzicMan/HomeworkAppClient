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
    public partial class ChangePass : Form
    {
        string answer = "";
        public string GetName()
        {
            return answer;
        }
        public ChangePass(ListBox.ObjectCollection t)
        {
            InitializeComponent();
            if (t.Count == 0)
            {
                return;
            }
            foreach (var b in t)
            {
                comboBox1.Items.Add(b.ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                button1.Enabled = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            answer = comboBox1.SelectedItem.ToString() + ":" + textBox1.Text + ":" + textBox2.Text;
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
