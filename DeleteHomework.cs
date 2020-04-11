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
    public partial class DeleteHomework : Form
    {
        string answer = "";
        List<int> indexes = new List<int>();
        public string GetName()
        {
            return answer;
        }
        public DeleteHomework(List<Form1.Homework> t)
        {
            InitializeComponent();
            if (t.Count == 0)
            {
                return;
            }
            foreach (Form1.Homework tem in t)
            {
                DateTime date = new DateTime(1970, 1, 1);
                date = date.AddSeconds(tem.time);
                string temp = date + " " + tem.type + ":" + tem.h;
                comboBox1.Items.Add(temp);
                indexes.Add(tem.index);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            answer = indexes[comboBox1.SelectedIndex] + ":" + textBox1.Text;
            this.Hide();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem != null)
            {
                button1.Enabled = true;
            }
        }
    }
}
