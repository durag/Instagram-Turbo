using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace InstaTurbo
{
    public partial class followers : UserControl
    {
        public followers()
        {
            InitializeComponent();
        }

        private void followers_Load(object sender, EventArgs e)
        {
            if (user.isAuthenticated == false)
            {
                bunifuCustomLabel8.Text = "Not authenticated";
            }
            else
            {
                bunifuCustomLabel8.Text = "Authenticated as: " + user.username;
            }
        }

        private void bunifuSlider1_ValueChanged(object sender, EventArgs e)
        {
            int sliderValue = bunifuSlider1.Value + 1;
            bunifuCustomLabel11.Text = sliderValue.ToString();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if ((bunifuMetroTextbox1.Text == string.Empty))
            {
                MessageBox.Show("Please enter a desired tag!", "Insta Tool Info", MessageBoxButtons.OK);
            }
            else
            {
                new Thread(new ThreadStart(followInstaPeople)) { IsBackground = true }.Start();
            }
        }

        private void followInstaPeople()
        {
            string tag = bunifuMetroTextbox1.Text;

            HttpWebRequest requestData = (HttpWebRequest)WebRequest.Create("https://www.instagram.com/web/search/topsearch/?context=blended&query=" + tag);
            requestData.Method = "GET";
            requestData.Accept = "*/*";
            requestData.Host = "www.instagram.com";
            requestData.Headers.Add("Origin", "https://www.instagram.com");
            requestData.Headers.Add("X-Instagram-AJAX", "1");
            requestData.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
            requestData.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            requestData.Headers.Add("X-Requested-With", "XMLHttpRequest");
            requestData.Headers.Add("X-CSRFToken", user.csrf);
            requestData.KeepAlive = true;
            requestData.Headers.Add("Cookie", $"mid=V2x4dAAEAAHd2oZIb2KmOAfz8JkS; s_network=; ig_pr=0.8999999761581421; ig_vw=1517; csrftoken={user.csrf}");
            requestData.ProtocolVersion = HttpVersion.Version10;
            requestData.Referer = "https://www.instagram.com";
            requestData.Headers.Add("Accept-Language", "en-US,en;q=0.8");
            requestData.Headers.Add("Cookie", $"mid=V2x4dAAEAAHd2oZIb2KmOAfz8JkS; sessionid={user.IGSessionIDLogin}%3AVg99013lSewwdHbgutSa9193NbUuE9pV%3A%7B%22_token_ver%22%3A2%2C%22_auth_user_id%22%3A3455245393%2C%22_token%22%3A%223455245393%3Afw4QzyCRWEMCINbv0bMZTngjyPyxXKDk%3A8a1297388f0919512f629d76e15c15000479f8bc787a8a4389f1d89e2557781a%22%2C%22asns%22%3A%7B%2268.135.173.248%22%3A5650%2C%22time%22%3A1466832736%7D%2C%22_auth_user_backend%22%3A%22accounts.backends.CaseInsensitiveModelBackend%22%2C%22last_refreshed%22%3A1466833145.817771%2C%22_platform%22%3A4%7D; s_network=; ig_pr=0.8999999761581421; ig_vw=1517; csrftoken={user.csrf}; ds_user_id=3455245393");
            requestData.AllowAutoRedirect = true;

            HttpWebResponse responseData = (HttpWebResponse)requestData.GetResponse();
            string strData = new StreamReader(responseData.GetResponseStream(), Encoding.UTF8).ReadToEnd();

            JObject json = JObject.Parse(strData);

            int amount = Int32.Parse(bunifuCustomLabel11.Text);

            for (int i = 0; i < amount; i++)
            {
                //MessageBox.Show(json["users"][i]["user"]["pk"].ToString());
                followPerson(json["users"][i]["user"]["pk"].ToString());
            }

            MessageBox.Show("Followed: " + amount + " users!", "Insta Tool Info", MessageBoxButtons.OK);
        }

        private void followPerson(string id)
        {
            try
            {
                string post = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.instagram.com/web/friendships/" + id + "/follow/");
                request.Method = "POST";
                request.Accept = "*/*";
                request.Host = "www.instagram.com";
                request.Headers.Add("Origin", "https://www.instagram.com");
                request.Headers.Add("X-Instagram-AJAX", "1");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                request.Headers.Add("X-CSRFToken", user.csrf);
                request.KeepAlive = true;
                request.Headers.Add("Cookie", $"mid=V2x4dAAEAAHd2oZIb2KmOAfz8JkS; s_network=; ig_pr=0.8999999761581421; ig_vw=1517; csrftoken={user.csrf}");
                request.ProtocolVersion = HttpVersion.Version10;
                request.Referer = "https://www.instagram.com";
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                request.Headers.Add("Cookie", $"mid=V2x4dAAEAAHd2oZIb2KmOAfz8JkS; sessionid={user.IGSessionIDLogin}%3AVg99013lSewwdHbgutSa9193NbUuE9pV%3A%7B%22_token_ver%22%3A2%2C%22_auth_user_id%22%3A3455245393%2C%22_token%22%3A%223455245393%3Afw4QzyCRWEMCINbv0bMZTngjyPyxXKDk%3A8a1297388f0919512f629d76e15c15000479f8bc787a8a4389f1d89e2557781a%22%2C%22asns%22%3A%7B%2268.135.173.248%22%3A5650%2C%22time%22%3A1466832736%7D%2C%22_auth_user_backend%22%3A%22accounts.backends.CaseInsensitiveModelBackend%22%2C%22last_refreshed%22%3A1466833145.817771%2C%22_platform%22%3A4%7D; s_network=; ig_pr=0.8999999761581421; ig_vw=1517; csrftoken={user.csrf}; ds_user_id=3455245393");

                byte[] postBytes = Encoding.ASCII.GetBytes(post);
                request.ContentLength = postBytes.Length;
                Stream requestStream = request.GetRequestStream();

                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (html.Contains("ok"))
                {
                    base.Invoke(new Action(method_5));
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Please wait a little before you follow more users!", "Insta Tool Info", MessageBoxButtons.OK);
            }
        }

        private void method_5()
        {
            int num = Convert.ToInt32(bunifuCustomLabel10.Text);
            if (num > 0x77359400)
            {
                num = 0;
            }

            num++;
            bunifuCustomLabel10.Text = num.ToString();
        }

    }
}
