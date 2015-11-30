using HueLampApp.HueLampObject;
using HueLampApp.Pasers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.Web.Http;

namespace HueLampApp.MainpageObjects
{
    public class UriRouter
    {
        public string IP { get; set; }
        public string Username { get; set; }
        public int Port { get; set; }

        private HttpClient client;

        private UriRouter() { }

        public UriRouter(string username = "",int port = 80,string Ip = "Localhost")
        {
            IP = Ip;
            Username = username;
            Port = port;
            client = new HttpClient();
            var response = PostUsername();           
        }

        public async Task PostUsername()
        {
            var response = await postUsername();
            if (string.IsNullOrEmpty(response))
            {
                throw new ArgumentNullException("Username is niet aangekomen");
            }
            else
            {
                Username = HueLampParser.GetUsernameFromJson(response);
                JObject jo = JObject.Parse(response);
            }            
        }

        private async Task<string> postUsername()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpStringContent content = new HttpStringContent
               (
                   $"{{\"devicetype\":\"MijnApp#Remco\"}}",
                   Windows.Storage.Streams.UnicodeEncoding.Utf8,
                   "application/json"
               );
                Uri askUsername = new Uri($"http://{IP}:{Port}/api");

                var response = await client.PostAsync(askUsername, content).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
           

            return string.Empty;
        }
        
        public async Task<string> GetAllLamps()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            try
            {
                Uri uriAllLightInfo = new Uri($"http://{IP}:{Port}/api/{Username}/lights/");

                var response = await client.GetAsync(uriAllLightInfo).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return string.Empty;
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return string.Empty;
            }
        }

        public async Task<bool> PutLampProps(HueLamp lamp)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            try
            {
                HttpStringContent content = new HttpStringContent
                    (
                        $"{{\"on\":{lamp.On.ToString().ToLower()}}}",
                        Windows.Storage.Streams.UnicodeEncoding.Utf8,
                        "application/json"
                    );
                Uri uriLampState = new Uri($"http://{IP}:{Port}/api/{Username}/light/{lamp.ID}/state");
                var response = await client.PutAsync(uriLampState, content).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
            }
            catch(Exception e)
            {                
                System.Diagnostics.Debug.WriteLine(e.Message);
                return false;
            }
            return true;
        }       
    }
}
