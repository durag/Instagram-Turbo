using Mono.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InstaTurbo
{
    public class authClass
    {
        // Authenticate function
        public bool authenticate(string username, string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://pr0b.com/instagram/handler/auth.php");
            request.Method = "POST";

            string formContent = "username=" + username + "&password=" + password;

            byte[] byteArray = Encoding.UTF8.GetBytes(formContent);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = HttpUtility.UrlDecode(reader.ReadToEnd());

            reader.Close();
            dataStream.Close();
            response.Close();

            if (responseFromServer.Contains("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Register account function
        public bool register(string username, string password, string serial)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://pr0b.com/instagram/handler/register.php");
            request.Method = "POST";

            string formContent = "username=" + username + "&password=" + password + "&serial=" + serial;

            byte[] byteArray = Encoding.UTF8.GetBytes(formContent);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            WebResponse response = request.GetResponse();
            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = HttpUtility.UrlDecode(reader.ReadToEnd());

            reader.Close();
            dataStream.Close();
            response.Close();

            if (responseFromServer.Contains("true"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
