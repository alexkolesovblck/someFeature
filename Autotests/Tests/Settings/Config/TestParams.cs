using System.Configuration;
using System.Collections.Specialized;

namespace Tests.Settings
{
    public class TestParams
    {

        public static string protocol
        {
            get
            {
                var creds = (NameValueCollection)ConfigurationManager.GetSection("userCredentials");
                return creds["protocol"];
            }
        }

        public static string yandexURL
        {
            get
            {
                var creds = (NameValueCollection)ConfigurationManager.GetSection("userCredentials");
                return creds["yandexURL"];
            }
        }

        public static string baseUrl
        {
            get
            {
                string BaseURL = protocol + yandexURL;
                return BaseURL;
            }
        }
    }
}
