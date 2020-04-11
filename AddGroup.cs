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
    public partial class AddGroup : Form
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

        public AddGroup()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                button1.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
                textBox2.Enabled = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text != "")
            {
                answer = textBox1.Text;
                internet = textBox1.Text + ":" + textBox2.Text;
                needInternet = true;
                this.Hide();
                return;
            }
            answer = textBox1.Text;
            this.Hide();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
