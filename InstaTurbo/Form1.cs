using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstaTurbo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bunifuFlatButton1.selected = true;
            bunifuFlatButton2.selected = false;
            bunifuFlatButton3.selected = false;
            bunifuFlatButton4.selected = false;
            single singleUi = new single();
            content.Controls.Add(singleUi);
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            content.Controls.Clear();
            bunifuFlatButton1.selected = true;
            bunifuFlatButton2.selected = false;
            bunifuFlatButton3.selected = false;
            bunifuFlatButton4.selected = false;
            single singleUi = new single();
            content.Controls.Add(singleUi);
        }

        private void bunifuCustomLabel1_Click(object sender, EventArgs e)
        {
            string currentVersion = "1.2";
            string htmlCode;

            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString("http://pr0b.com/instagram/handler/currentVersion.php");
            }

            if (currentVersion != htmlCode)
            {
                System.Diagnostics.Process.Start("http://pr0b.com/products/instagram.php");
            }
            else
            {
                MessageBox.Show("Your running the newest version!", "Insta Tool Info", MessageBoxButtons.OK);
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (user.isAuthenticated == true)
            {
                content.Controls.Clear();
                bunifuFlatButton1.selected = false;
                bunifuFlatButton2.selected = true;
                bunifuFlatButton3.selected = false;
                bunifuFlatButton4.selected = false;
                turbo turbo = new turbo();
                content.Controls.Add(turbo);
            }
            else
            {  
                content.Controls.Clear();
                bunifuFlatButton1.selected = true;
                bunifuFlatButton2.selected = false;
                bunifuFlatButton3.selected = false;
                bunifuFlatButton4.selected = false;
                single single = new single();
                content.Controls.Add(single);

                MessageBox.Show("Please authenticate your instagram account!", "Insta Tool Info", MessageBoxButtons.OK);
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (user.isAuthenticated == true)
            {
                content.Controls.Clear();
                bunifuFlatButton1.selected = false;
                bunifuFlatButton2.selected = false;
                bunifuFlatButton3.selected = true;
                bunifuFlatButton4.selected = false;
                lookup lookup = new lookup();
                content.Controls.Add(lookup);
            }
            else
            {
                content.Controls.Clear();
                bunifuFlatButton1.selected = true;
                bunifuFlatButton2.selected = false;
                bunifuFlatButton3.selected = false;
                bunifuFlatButton4.selected = false;
                single single = new single();
                content.Controls.Add(single);

                MessageBox.Show("Please authenticate your instagram account!", "Insta Tool Info", MessageBoxButtons.OK);
            }
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (user.isAuthenticated == true)
            {
                content.Controls.Clear();
                bunifuFlatButton1.selected = false;
                bunifuFlatButton2.selected = false;
                bunifuFlatButton3.selected = false;
                bunifuFlatButton4.selected = true;
                followers followers = new followers();
                content.Controls.Add(followers);
            }
            else
            {
                content.Controls.Clear();
                bunifuFlatButton1.selected = true;
                bunifuFlatButton2.selected = false;
                bunifuFlatButton3.selected = false;
                bunifuFlatButton4.selected = false;
                single single = new single();
                content.Controls.Add(single);

                MessageBox.Show("Please authenticate your instagram account!", "Insta Tool Info", MessageBoxButtons.OK);
            }
        }
    }
}
