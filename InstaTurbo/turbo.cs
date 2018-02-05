using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace InstaTurbo
{
    public partial class turbo : UserControl
    {
        public turbo()
        {
            InitializeComponent();
        }

        bool changedUser = false;

        private async void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if ((bunifuMetroTextbox3.Text == string.Empty))
            {
                MessageBox.Show("Please enter a handle!", "Insta Tool Info", MessageBoxButtons.OK);
            }
            else if (user.username == bunifuMetroTextbox3.Text)
            {
                MessageBox.Show("Please enter a different handle! You already own the following handle: " + bunifuMetroTextbox3.Text + ".", "Insta Tool Info", MessageBoxButtons.OK);
            }
            else
            {
                await CheckUntilOneOfTheUsersIsAvaliable();
            }
        }

        private async Task CheckUntilOneOfTheUsersIsAvaliable()
        {
            this.Invoke((MethodInvoker)delegate
            {
                UpdateTurboStatus(true, "username");
            });

            int amount = Int32.Parse(bunifuCustomLabel11.Text);

            await Task.WhenAny(Enumerable.Range(0, amount).Select(i => CheckUntilUserIsAvaliable()));

            try
            {
                if (!changedUser)
                {
                    user.handle = bunifuMetroTextbox3.Text;
                    string post = $"first_name={user.full_name}&email={user.email}&username={user.handle}&phone_number={user.phone_number}&gender={user.gender}&biography={user.biography}&external_url={user.website}&chaining_enabled={user.chaining_enabled}";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://www.instagram.com/accounts/edit/");
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
                    request.Referer = "https://www.instagram.com/accounts/edit/?wo=1";
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
                        changedUser = true;

                        this.Invoke((MethodInvoker)delegate
                        {
                            UpdateTurboStatus(false, user.handle);
                        });

                        MessageBox.Show("Instagram Turbo finished. The following handle was claimed: " + user.handle + ".", "Insta Tool info");
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task CheckUntilUserIsAvaliable()
        {
            while (!await IsUserAvaliable(bunifuMetroTextbox3.Text) && changedUser == false)
            {
                method_5();
                await Task.Delay(1000);
            }
        }

        private async Task<bool> IsUserAvaliable(string handle)
        {
            while (true)
            {
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string content = await client.GetStringAsync("https://pr0b.com/ig.php?user=" + handle);
                        return content.Contains("No users found");
                    }
                    catch (WebException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
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

        bool UpdateTurboStatus(bool status, string handle)
        {
            if (status == true)
            {
                bunifuCustomLabel8.Text = "Turboing Instagram handle.";
                return true;
            }
            else
            {
                bunifuCustomLabel8.Text = "Turbo finished. Username: " + handle + " was claimed.";
                return true;
            }
        }

        private void turbo_Load(object sender, EventArgs e)
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

        private void bunifuSlider1_ValueChanged_1(object sender, EventArgs e)
        {
            int sliderValue = bunifuSlider1.Value + 1;
            bunifuCustomLabel11.Text = sliderValue.ToString();
        }
    }
}
