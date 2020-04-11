using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeworkApp
{
    public partial class Form1 : Form
    {

        public struct Homework
        {
            public int time;
            public int index;
            public string type;
            public string h;
        }

        readonly HttpClient client = new HttpClient();
        string responseBody;
        Thread curT;
        string request;
        bool exitApp = false;
        bool goToInternet = false;
        List<Homework> current = new List<Homework>();

        async Task GetAnswer(string url, CancellationToken ct)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:8080/"+url);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                if(ct.IsCancellationRequested){
                    throw new TaskCanceledException();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        public void SetText(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action<string>(SetText), new object[] { text });
                return;
            }
            richTextBox1.Text = text;
        }
        public Form1()
        {
            InitializeComponent();
            curT = new Thread(InternetShit);
            curT.Start();
        }

        public void InternetShit()
        {
            while (true)
            {
                while (!goToInternet && !exitApp) { }
                if (exitApp)
                {
                    return;
                }
                goToInternet = false;
                SetText("");
                CancellationTokenSource ct = new CancellationTokenSource();
                Task m = GetAnswer(request, ct.Token);
                m.Wait();
                if (responseBody == "no such group")
                {
                    SetText("This group is not found!");
                    continue;
                }
                else if (responseBody == null)
                {
                    SetText("Error while connecting to server");
                    continue;
                }
                else if (responseBody == "wrong format" || responseBody == "index not found")
                {
                    SetText("Error! Cannot complete action!");
                    continue;
                }
                else if (responseBody == "incorrect password")
                {
                    SetText("Error! Incorrect password!");
                    continue;
                }
                else if (responseBody == "")
                {
                    SetText("No homework for this group!");
                }
                else if (responseBody != "success" && responseBody != "group exists")
                {
                    string[] homeworks = responseBody.Split('\n');
                    current.Clear();
                    foreach (string home in homeworks)
                    {
                        if (home == "")
                        {
                            continue;
                        }
                        Homework t = new Homework();
                        string[] homes = home.Split(' ');
                        t.time = Convert.ToInt32(homes[0]);
                        t.index = Convert.ToInt32(homes[1]);
                        t.type = homes[2];
                        t.h = "";
                        for (int i = 3; i < homes.Length; i++)
                        {
                            t.h += homes[i] + " ";
                        }
                        current.Add(t);
                    }
                    foreach (Homework t in current)
                    {
                        if (listBox2.FindString(t.type) == -1)
                        {
                            listBox2.Invoke((MethodInvoker)(() => listBox2.Items.Add(t.type)));
                        }
                    }
                }
            }
        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            current.Clear();
            SetText("");
            if(listBox1.SelectedIndex == -1)
            {
                return;
            }
            request = listBox1.SelectedItem.ToString();
            goToInternet = true;
        }

        private void ListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex == -1)
            {
                return;
            }
            string output = "";
            foreach (Homework t in current)
            {
                if (t.type == listBox2.SelectedItem.ToString())
                {
                    DateTime date = new DateTime(1970,1,1);
                    date = date.AddSeconds(t.time);
                    output += date + ": " + t.h + "\n";
                }
            }
            SetText(output);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            AddGroup a = new AddGroup();
            a.ShowDialog();
            if (a.NeedInternet())
            {
                request = "addgroup/"+a.GetInternet();
                goToInternet = true;
            }
            listBox1.Items.Add(a.GetName());
            a.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DeleteGroup d = new DeleteGroup(listBox1.Items);
            d.ShowDialog();
            if (d.NeedInternet())
            {
                request = "deletegroup/" + d.GetInternet();
                goToInternet = true;
            }
            int index = -1;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                if (listBox1.Items[i].ToString() == d.GetName())
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                listBox1.Items.RemoveAt(index);
            }
            d.Close();
            SetText("");
            listBox2.Items.Clear();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            exitApp = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            AddHomework a = new AddHomework(listBox1.Items);
            a.ShowDialog();
            if(a.GetName() == "")
            {
                a.Close();
                return;
            }
            request = "add/" + a.GetName();
            goToInternet = true;
            listBox1.SelectedIndex = -1;
            a.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            DeleteHomework d = new DeleteHomework(current);
            d.ShowDialog();
            if(d.GetName() == "")
            {
                d.Close();
                return;
            }
            request = "delete/" + listBox1.SelectedItem.ToString() + ":" + d.GetName();
            goToInternet = true;
            listBox1.SelectedIndex = -1;
            d.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            ChangeHomework c = new ChangeHomework(current);
            c.ShowDialog();
            if (c.GetName() == "")
            {
                c.Close();
                return;
            }
            request = "change/" + listBox1.SelectedItem.ToString() + ":" + c.GetName();
            goToInternet = true;
            listBox1.SelectedIndex = -1;
            c.Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ChangePass c = new ChangePass(listBox1.Items);
            c.ShowDialog();
            if(c.GetName() == "")
            {
                c.Close();
                return;
            }
            request = "changepassword/" + c.GetName();
            goToInternet = true;
            listBox1.SelectedIndex = -1;
            c.Close();
        }
    }
}
