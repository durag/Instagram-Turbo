using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace InstaTurbo
{
    public partial class single : UserControl
    {
        public single()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if ((bunifuMetroTextbox1.Text == string.Empty) || (bunifuMetroTextbox2.Text == string.Empty))
            {
                MessageBox.Show("Please enter your username and password!", "Insta Tool Info", MessageBoxButtons.OK);
            }
            else
            {
                new Thread(new ThreadStart(doLogin)) { IsBackground = true }.Start();
            }
        }

        private void doLogin()
        {
            string username = bunifuMetroTextbox1.Text;
            string password = bunifuMetroTextbox2.Text;

            try
            {
                HttpWebRequest requestID = (HttpWebRequest)WebRequest.Create("https://instagram.com/" + username + "/");
                requestID.AllowAutoRedirect = true;
                HttpWebResponse responseID = (HttpWebResponse)requestID.GetResponse();
                string strID = new StreamReader(responseID.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                user.id = getToken(strID, "id\": \"", "\",", 0);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://instagram.com/");
                request.AllowAutoRedirect = true;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string str = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                user.csrf = getToken(str, "csrf_token\": \"", "\",", 0);

                string post = "username=" + username + "&password=" + password;

                HttpWebRequest requestAuth = (HttpWebRequest)WebRequest.Create("https://www.instagram.com/accounts/login/ajax/");
                requestAuth.Method = "POST";
                requestAuth.Host = "www.instagram.com";
                requestAuth.KeepAlive = true;
                requestAuth.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.103 Safari/537.36";
                requestAuth.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                requestAuth.Accept = "*/*";
                requestAuth.Referer = "https://www.instagram.com/accounts/login/";
                requestAuth.Headers.Add("Origin", "https://www.instagram.com");
                requestAuth.Headers.Add("X-Instagram-AJAX", "1");
                requestAuth.Headers.Add("X-Requested-With", "XMLHttpRequest");
                requestAuth.Headers.Add("X-CSRFToken", user.csrf);
                requestAuth.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                requestAuth.Headers.Add("Cookie", $"mid=VlW1MgAEAAEgkDVr8Pa-nokWXqCF; csrftoken={user.csrf}; ig_pr=1; ig_vw=1160");

                byte[] postBytes = Encoding.ASCII.GetBytes(post);
                requestAuth.ContentLength = postBytes.Length;
                Stream requestStream = requestAuth.GetRequestStream();

                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();

                HttpWebResponse responseAuth = (HttpWebResponse)requestAuth.GetResponse();
                string html = new StreamReader(responseAuth.GetResponseStream()).ReadToEnd();

                if (html.Contains("\"authenticated\": true"))
                {
                    var cookieTitle = "sessionid";
                    var cookie = responseAuth.Headers.GetValues("Set-Cookie").First(x => x.StartsWith(cookieTitle));

                    user.IGSessionIDLogin = cookie;
                    string[] splitter = user.IGSessionIDLogin.Split(new string[] { "sessionid=" }, StringSplitOptions.None);
                    user.IGSessionIDLogin = splitter[1];

                    HttpWebRequest requestToken = (HttpWebRequest)WebRequest.Create("https://www.instagram.com/accounts/edit/");
                    requestToken.AllowAutoRedirect = true;
                    HttpWebResponse responseToken = (HttpWebResponse)requestToken.GetResponse();
                    string strToken = new StreamReader(responseToken.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                    user.csrf = getToken(strToken, "csrf_token\": \"", "\",", 0);

                    HttpWebRequest requestData = (HttpWebRequest)WebRequest.Create("https://www.instagram.com/accounts/edit/");
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
                    requestData.Referer = "https://www.instagram.com/accounts/edit/?wo=1";
                    requestData.Headers.Add("Accept-Language", "en-US,en;q=0.8");
                    requestData.Headers.Add("Cookie", $"mid=V2x4dAAEAAHd2oZIb2KmOAfz8JkS; sessionid={user.IGSessionIDLogin}%3AVg99013lSewwdHbgutSa9193NbUuE9pV%3A%7B%22_token_ver%22%3A2%2C%22_auth_user_id%22%3A3455245393%2C%22_token%22%3A%223455245393%3Afw4QzyCRWEMCINbv0bMZTngjyPyxXKDk%3A8a1297388f0919512f629d76e15c15000479f8bc787a8a4389f1d89e2557781a%22%2C%22asns%22%3A%7B%2268.135.173.248%22%3A5650%2C%22time%22%3A1466832736%7D%2C%22_auth_user_backend%22%3A%22accounts.backends.CaseInsensitiveModelBackend%22%2C%22last_refreshed%22%3A1466833145.817771%2C%22_platform%22%3A4%7D; s_network=; ig_pr=0.8999999761581421; ig_vw=1517; csrftoken={user.csrf}; ds_user_id=3455245393");
                    requestData.AllowAutoRedirect = true;

                    HttpWebResponse responseData = (HttpWebResponse)requestData.GetResponse();
                    string strData = new StreamReader(responseData.GetResponseStream(), Encoding.UTF8).ReadToEnd();

                    user.username = getToken(strData, "username\": \"", "\"}},", 0);
                    user.full_name = getToken(strData, "full_name\": \"", "\",", 0);
                    user.website = getToken(strData, "external_url\": \"", "\",", 0);
                    user.biography = getToken(strData, "biography\": \"", "\",", 0);
                    user.email = getToken(strData, "email\": \"", "\",", 0);
                    user.phone_number = getToken(strData, "phone_number\": \"", "\",", 0);
                    user.gender = getToken(strData, "gender\": ", ",", 0);
                    user.chaining_enabled = getToken(strData, "chaining_enabled\": ", "}}]},", 0);

                    bool status = true;
                    bool cancel = (bool)this.Invoke((Func<bool, bool>)DoCheapGuiAccess, status);
                }
                else
                {
                    bool status = false;
                    bool cancel = (bool)this.Invoke((Func<bool, bool>)DoCheapGuiAccess, status);
                }
            }
            catch (Exception ex)
            {
                bool status = false;
                bool cancel = (bool)this.Invoke((Func<bool, bool>)DoCheapGuiAccess, status);
                Console.WriteLine(ex.Message);
            }
        }

        bool DoCheapGuiAccess(bool status)
        {
            if (!status)
            {
                bunifuCustomLabel6.Text = "Could not authenticate as: " + bunifuMetroTextbox1.Text;
                bunifuCustomLabel8.Text = "Could not authenticate as: " + bunifuMetroTextbox1.Text;
                user.isAuthenticated = false;
                return true;
            }
            else
            {
                bunifuCustomLabel6.Text = "Authenticated as: " + bunifuMetroTextbox1.Text;
                bunifuCustomLabel8.Text = "Authenticated as: " + bunifuMetroTextbox1.Text;
                user.isAuthenticated = true;
                return true;
            }
        }

        private string getToken(string string_0, string string_1, string string_2, int int_0)
        {
            string input = Regex.Split(string_0, string_1)[int_0 + 1];
            return Regex.Split(input, string_2)[0];
        }

        private void single_Load(object sender, EventArgs e)
        {
            if (user.isAuthenticated == false)
            {
                bunifuCustomLabel8.Text = "Not authenticated";
                bunifuCustomLabel6.Text = "Not authenticated";
            }
            else
            {
                bunifuCustomLabel8.Text = "Authenticated as: " + user.username;
                bunifuCustomLabel6.Text = "Already authenticated as: " + user.username;
            }
        }
    }
}