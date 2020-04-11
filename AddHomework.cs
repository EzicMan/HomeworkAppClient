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
    public partial class AddHomework : Form
    {
        string answer = "";
        public string GetName()
        {
            return answer;
        }
        public AddHomework(ListBox.ObjectCollection t)
        {
            InitializeComponent();
            if(t.Count == 0)
            {
                return;
            }
            foreach(var temp in t)
            {
                comboBox1.Items.Add(temp.ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            answer = comboBox1.SelectedItem.ToString() + ":" + richTextBox1.Text + ":" + textBox1.Text + ":" + (int)dateTimePicker1.Value.Subtract(new DateTime(1970,1,1)).TotalSeconds + ":" + textBox2.Text;
            this.Hide();
        }
        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && richTextBox1.Text != "")
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
        }
    }
}
