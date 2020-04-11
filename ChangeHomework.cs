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
    public partial class ChangeHomework : Form
    {
        string answer = "";
        List<Form1.Homework> t = new List<Form1.Homework>();
        public string GetName()
        {
            return answer;
        }
        public ChangeHomework(List<Form1.Homework> a)
        {
            InitializeComponent();
            t = a;
            if(t.Count == 0)
            {
                return;
            }
            foreach(Form1.Homework tem in t)
            {
                DateTime date = new DateTime(1970, 1, 1);
                date = date.AddSeconds(tem.time);
                string temp = date + " " + tem.type + ":" + tem.h;
                comboBox1.Items.Add(temp);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            answer = t[comboBox1.SelectedIndex].index + ":" + richTextBox1.Text + ":" + textBox1.Text + ":" + (int)dateTimePicker1.Value.Subtract(new DateTime(1970,1,1)).TotalSeconds + ":" + textBox2.Text;
            this.Hide();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                textBox1.Text = t[comboBox1.SelectedIndex].type;
                DateTime date = new DateTime(1970, 1, 1);
                date = date.AddSeconds(t[comboBox1.SelectedIndex].time);
                dateTimePicker1.Value = date;
                richTextBox1.Text = t[comboBox1.SelectedIndex].h;
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }
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
