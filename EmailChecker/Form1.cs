using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EmailChecker
{
    public partial class Form1 : Form
    {
        bool proxyStatus = false;
        List<string> proxyList = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }

        // pictureBox2_Click
        // Function which handles the close application event
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // pictureBox4_Click
        // Function which handles the minimizes the application event
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        // Form1_Load
        // Function which adds the first row to the dataGridView1
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "No email added";
            dataGridView1.Rows[0].Cells[1].Value = "Unchecked";
            dataGridView1.Rows[0].Cells[2].Value = "Unchecked";
            dataGridView1.Rows[0].Cells[3].Value = "Unchecked";
            dataGridView1.Rows[0].Cells[4].Value = "No error";
        }

        // bunifuFlatButton2_Click
        // Function which handles the button click to start lookups
        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 1)
            {
                MessageBox.Show("Please upload a email list containing atleast 1 email address.", "Email checker Error");
            }
            else
            {
                new Thread(new ThreadStart(check)) { IsBackground = true }.Start();
            }
        }

        // bunifuFlatButton1_Click
        // Function to add handles to the dataGridView1
        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.Title = "Browse Text Files";

            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;

            openFileDialog1.DefaultExt = "txt";
            openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            openFileDialog1.ReadOnlyChecked = true;
            openFileDialog1.ShowReadOnly = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;

                dataGridView1.Rows.RemoveAt(0);

                int i = 0;

                foreach (var line in File.ReadAllLines(filePath))
                {
                    string[] words = line.Split('@');

                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[i].Cells[0].Value = words[0];
                    dataGridView1.Rows[i].Cells[1].Value = "Unchecked";
                    dataGridView1.Rows[i].Cells[2].Value = "Unchecked";
                    dataGridView1.Rows[i].Cells[3].Value = "Unchecked";
                    dataGridView1.Rows[i].Cells[4].Value = "No error";
                    i++;
                }

                bunifuCustomLabel5.Text = dataGridView1.Rows.Count.ToString();
            }
        }

        // bunifuFlatButton3_Click
        // Function to add the proxys to the proxy list
        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            openFileDialog2.InitialDirectory = @"C:\";
            openFileDialog2.Title = "Browse Text Files";

            openFileDialog2.CheckFileExists = true;
            openFileDialog2.CheckPathExists = true;

            openFileDialog2.DefaultExt = "txt";
            openFileDialog2.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog2.FilterIndex = 2;
            openFileDialog2.RestoreDirectory = true;

            openFileDialog2.ReadOnlyChecked = true;
            openFileDialog2.ShowReadOnly = true;

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog2.FileName;
                bunifuCustomLabel6.Text = File.ReadAllLines(filePath).Length.ToString();

                int i = 0;

                foreach (var line in File.ReadAllLines(filePath))
                {
                    proxyList.Add(line);
                    i++;
                }
            }
        }

        // check
        // Function to handle the email lookups
        private void check()
        {
            bool status = false;
            bool cancel = (bool)this.Invoke((Func<bool, bool>)DoCheapGuiAccess, status);

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                string address = dataGridView1.Rows[i].Cells[0].Value.ToString();

                checkMails mailCheck = new checkMails();

                try
                {
                    if (bunifuCheckbox1.Checked)
                    {
                        Random rand = new Random();
                        string randomProxy = proxyList[rand.Next(proxyList.Count)];

                        string[] results = new Func<string, string, string>[] { mailCheck.checkGmail, mailCheck.checkHotmail, mailCheck.checkYahoo }
                        .AsParallel()
                        .AsOrdered()
                        .Select(f => f(address, randomProxy))
                        .ToArray();

                        string checkGmail = results[0];
                        string checkHotmail = results[1];
                        string checkYahoo = results[2];

                        if (checkGmail == "true")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = "Doesnt exist";
                        }
                        else if (checkGmail == "false")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = "Exist";
                        }
                        else if (checkGmail == "charackters")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = "Error";
                            dataGridView1.Rows[i].Cells[4].Value = "Less characters";
                        }
                        else if (checkGmail == "notAllowed")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = "Error";
                            dataGridView1.Rows[i].Cells[4].Value = "Not allowed";
                        }

                        if (checkHotmail == "true")
                        {
                            dataGridView1.Rows[i].Cells[2].Value = "Doesnt exist";
                        }
                        else if (checkHotmail == "false")
                        {
                            dataGridView1.Rows[i].Cells[2].Value = "Exist";
                        }

                        if (checkYahoo == "true")
                        {
                            dataGridView1.Rows[i].Cells[3].Value = "Doesnt exist";
                        }
                        else if (checkYahoo == "false")
                        {
                            dataGridView1.Rows[i].Cells[3].Value = "Exist";
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[3].Value = "Error";
                            dataGridView1.Rows[i].Cells[4].Value = "Less characters";
                        }
                    }
                    else
                    {
                        string[] results = new Func<string, string, string>[] { mailCheck.checkGmail, mailCheck.checkHotmail, mailCheck.checkYahoo }
                        .AsParallel()
                        .AsOrdered()
                        .Select(f => f(address, "localhost"))
                        .ToArray();

                        string checkGmail = results[0];
                        string checkHotmail = results[1];
                        string checkYahoo = results[2];

                        if(checkGmail == "true")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = "Doesnt exist";
                        }
                        else if(checkGmail == "false")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = "Exist";
                        }
                        else if(checkGmail == "charackters")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = "Error";
                            dataGridView1.Rows[i].Cells[4].Value = "Less characters";
                        }
                        else if(checkGmail == "notAllowed")
                        {
                            dataGridView1.Rows[i].Cells[1].Value = "Error";
                            dataGridView1.Rows[i].Cells[4].Value = "Not allowed";
                        }

                        if(checkHotmail == "true")
                        {
                            dataGridView1.Rows[i].Cells[2].Value = "Doesnt exist";
                        }
                        else if (checkHotmail == "false")
                        {
                            dataGridView1.Rows[i].Cells[2].Value = "Exist";
                        }

                        if (checkYahoo == "true")
                        {
                            dataGridView1.Rows[i].Cells[3].Value = "Doesnt exist";
                        }
                        else if (checkYahoo == "false")
                        {
                            dataGridView1.Rows[i].Cells[3].Value = "Exist";
                        }
                        else
                        {
                            dataGridView1.Rows[i].Cells[3].Value = "Error";
                            dataGridView1.Rows[i].Cells[4].Value = "Less characters";
                        }
                    }
                }
                catch
                {
                    dataGridView1.Rows[i].Cells[1].Value = "Error";
                    dataGridView1.Rows[i].Cells[2].Value = "Error";
                    dataGridView1.Rows[i].Cells[3].Value = "Error";
                    dataGridView1.Rows[i].Cells[4].Value = "Proxy dead";
                }
            }

            string file_name = AppDomain.CurrentDomain.BaseDirectory + "/export.txt";
            TextWriter writer = new StreamWriter(file_name);
            string line = "";

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    line += dataGridView1.Columns[j].HeaderText + ": " + dataGridView1.Rows[i].Cells[j].Value.ToString() + " | ";
                }

                line = line.Remove(line.Length - 3);
                writer.Write(line);
                line = "";
                writer.WriteLine("");
            }

            writer.Close();
            MessageBox.Show("Data exported to the following text file: " + file_name + "!", "Email checker info");
        }

        // DoCheapGuiAccess
        // Function to update the UI labels
        bool DoCheapGuiAccess(bool status)
        {
            if (status == false)
            {
                bunifuCustomLabel8.Text = "Running.";
                return true;
            }
            else
            {
                bunifuCustomLabel8.Text = "Finished.";
                return true;
            }
        }

        // bunifuCheckbox1_OnChange
        // Function handling the checkbox on change
        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if(proxyStatus == true)
            {
                bunifuFlatButton3.Enabled = false;
                proxyStatus = false;
            }
            else
            {
                bunifuFlatButton3.Enabled = true;
                proxyStatus = true;
            }
        }
    }
}
