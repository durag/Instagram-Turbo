using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstaTurbo
{
    public partial class register : Form
    {
        public register()
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

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://pr0b.com/products/instagram.php");
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            string username = bunifuMetroTextbox1.Text;
            string password = bunifuMetroTextbox2.Text;
            string keygen = bunifuMetroTextbox3.Text;

            if ((bunifuMetroTextbox1.Text == string.Empty) || (bunifuMetroTextbox2.Text == string.Empty) || (bunifuMetroTextbox3.Text == string.Empty))
            {
                MessageBox.Show("Please enter a username, a password and a serial key.", "Insta Tool Info");
            }
            else
            {
                authClass authentication = new authClass();
                bool registerStatus = authentication.register(username, password, keygen);

                if (registerStatus == false)
                {
                    bunifuCustomLabel6.Text = "Could not register: " + username + ". Please try again.";
                }
                else
                {
                    MessageBox.Show("The account: " + username + " was successful registered. You can login now.", "Insta Tool Info");
                    this.Hide();
                    var auth = new auth();
                    auth.Closed += (s, args) => this.Close();
                    auth.Show();
                }
            }

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            var auth = new auth();
            auth.Closed += (s, args) => this.Close();
            auth.Show();
        }
    }
}
