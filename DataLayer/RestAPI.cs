using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RestAPI
    {
        public static string apiUrl = "https://api.zalando.com"; //this will be set when the app starts in the case we need to change country.
        public static async Task<string> callAsync(string api, Filter filter)
        {
            HttpClient client = new HttpClient();
            Task<string> getStringTask = client.GetStringAsync(getApiUrl(api, filter));
            
            string urlContents = await getStringTask;
            return urlContents;
        }
        private static string getApiUrl(string api, Filter filter)
        {
            return apiUrl + api + filter.UrlFilter;
        }
    }
}
