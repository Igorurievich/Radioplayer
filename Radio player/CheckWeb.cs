using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Check
{
    class CheckWeb
    {
        WebRequest myWebRequest;
        WebResponse myWebResponse;
        Uri ourUri;

        public bool CheckURL(string url)
        {
            ourUri = new Uri(url);
            myWebRequest = WebRequest.Create(url);
            try
            {
                myWebResponse = myWebRequest.GetResponse();
            }
            catch
            {
                myWebResponse.Close(); 
                return false;
                 
            }
            myWebResponse.Close();  
            return true;
                           
        }
    }
}
