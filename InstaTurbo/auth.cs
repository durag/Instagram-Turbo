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
    public partial class auth : Form
    {
        public auth()
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
            this.Hide();
            var register = new register();
            register.Closed += (s, args) => this.Close();
            register.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            string username = bunifuMetroTextbox1.Text;
            string password = bunifuMetroTextbox2.Text;

            if ((bunifuMetroTextbox1.Text == string.Empty) || (bunifuMetroTextbox2.Text == string.Empty))
            {
                MessageBox.Show("Please enter a username and password.", "Insta Tool Info");
            }
            else
            {
                authClass authentication = new authClass();
                bool authStatus = authentication.authenticate(username, password);

                if (authStatus == false)
                {
                    bunifuCustomLabel6.Text = "Could not authenticate as: " + username;
                }
                else
                {
                    this.Hide();
                    var form1 = new Form1();
                    form1.Closed += (s, args) => this.Close();
                    form1.Show();
                }
            }
        }
    }
}
