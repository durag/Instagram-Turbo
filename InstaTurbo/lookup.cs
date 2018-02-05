using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace InstaTurbo
{
    public partial class lookup : UserControl
    {
        public lookup()
        {
            InitializeComponent();
        }

        private void lookup_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.Rows[0].Cells[0].Value = "Undefined";
            dataGridView1.Rows[0].Cells[1].Value = "Undefined";
            dataGridView1.Rows[0].Cells[2].Value = "Undefined";
            dataGridView1.Rows[0].Cells[3].Value = "Undefined";
            dataGridView1.Rows[0].Cells[4].Value = "Undefined";
            dataGridView1.Rows[0].Cells[5].Value = "Undefined";
            dataGridView1.Rows[0].Cells[6].Value = "Undefined";
            dataGridView1.Rows[0].Cells[7].Value = "Undefined";
            dataGridView1.Rows[0].Cells[8].Value = "Undefined";

            if (user.isAuthenticated == false)
            {
                bunifuCustomLabel8.Text = "Not authenticated";
            }
            else
            {
                bunifuCustomLabel8.Text = "Authenticated as: " + user.username;
            }
        }

        private async void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if ((bunifuMetroTextbox3.Text == string.Empty))
            {
                MessageBox.Show("Please enter a user to lookup!", "Insta Tool Info", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    await getUserLookup();
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task getUserLookup()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string content = await client.GetStringAsync("http://pr0b.com/ig.php?user=" + bunifuMetroTextbox3.Text);

                    if (content.Contains("No users found"))
                    {
                        MessageBox.Show("No user data found!", "Insta Tool Info", MessageBoxButtons.OK);
                    }
                    else
                    {
                        JObject json = JObject.Parse(content);

                        if (dataGridView1.Rows[0].Cells[0].Value.ToString() == "Undefined")
                        {
                            dataGridView1.Rows.RemoveAt(0);
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[user.dataRow].Cells[0].Value = json["user"]["username"];

                            if (json["user"]["full_name"].ToString() == "")
                            {
                                dataGridView1.Rows[user.dataRow].Cells[1].Value = "undefined";
                            }
                            else
                            {
                                dataGridView1.Rows[user.dataRow].Cells[1].Value = json["user"]["full_name"];
                            }

                            dataGridView1.Rows[user.dataRow].Cells[2].Value = json["user"]["is_private"];
                            dataGridView1.Rows[user.dataRow].Cells[3].Value = json["user"]["is_verified"];
                            dataGridView1.Rows[user.dataRow].Cells[4].Value = json["user_id"];
                            dataGridView1.Rows[user.dataRow].Cells[5].Value = json["email_sent"];
                            dataGridView1.Rows[user.dataRow].Cells[6].Value = json["has_valid_phone"];
                            dataGridView1.Rows[user.dataRow].Cells[7].Value = json["can_email_reset"];
                            dataGridView1.Rows[user.dataRow].Cells[8].Value = json["can_sms_reset"];
                            user.dataRow = user.dataRow + 1;
                        }
                        else
                        {
                            dataGridView1.Rows.Add();
                            dataGridView1.Rows[user.dataRow].Cells[0].Value = json["user"]["username"];

                            if (json["user"]["full_name"].ToString() == "")
                            {
                                dataGridView1.Rows[user.dataRow].Cells[1].Value = "undefined";
                            }
                            else
                            {
                                dataGridView1.Rows[user.dataRow].Cells[1].Value = json["user"]["full_name"];
                            }

                            dataGridView1.Rows[user.dataRow].Cells[2].Value = json["user"]["is_private"];
                            dataGridView1.Rows[user.dataRow].Cells[3].Value = json["user"]["is_verified"];
                            dataGridView1.Rows[user.dataRow].Cells[4].Value = json["user_id"];
                            dataGridView1.Rows[user.dataRow].Cells[5].Value = json["email_sent"];
                            dataGridView1.Rows[user.dataRow].Cells[6].Value = json["has_valid_phone"];
                            dataGridView1.Rows[user.dataRow].Cells[7].Value = json["can_email_reset"];
                            dataGridView1.Rows[user.dataRow].Cells[8].Value = json["can_sms_reset"];
                            user.dataRow = user.dataRow + 1;
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[0].Cells[0].Value.ToString() == "Undefined")
            {
                MessageBox.Show("Please lookup a user before exporting data!", "Insta Tool Info", MessageBoxButtons.OK);
            }
            else
            {
                try
                {
                    await ExportUserData();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private async Task ExportUserData()
        {
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
                await writer.WriteAsync(line);
                line = "";
                await writer.WriteLineAsync("");
            }

            writer.Close();
            MessageBox.Show("Data exported to the following text file: " + file_name + "!", "Insta Tool Info", MessageBoxButtons.OK);
        }
    }
}
