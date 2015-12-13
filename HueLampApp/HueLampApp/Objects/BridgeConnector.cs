using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Web.Http;

namespace HueLampApp
{
    public class BridgeConnector : PropertyChange
    {
        private static BridgeConnector connector = null;

        public static BridgeConnector Instance
        {
            get
            {
                if (connector == null)
                {
                    connector = new BridgeConnector();
                }
                return connector;
            }            
        }

        public static readonly HttpStringContent Content_Username = new HttpStringContent
               (
                   $"{{\"devicetype\":\"MijnApp#Remco\"}}",
                   Windows.Storage.Streams.UnicodeEncoding.Utf8,
                   "application/json"
               );
                
        private string _ip;
        public string Ip
        {
            get { return _ip; }
            set { _ip = value; OnPropertyChanged(nameof(Ip)); }
        }

        private int _port;
        public int Port
        {
            get { return _port; }
            set { _port = value; OnPropertyChanged(nameof(Port)); }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(nameof(Username)); }
        }     
        
        public BridgeConnector(string ip = "localhost", int port = 80, string username = "")
        {
            Ip = ip;
            Port = port;
            Username = username;           
        }
        
        //username
        public async Task<HttpResponseMessage> PostMessage(HttpStringContent content,Uri uri)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpClient client = new HttpClient();

                var response = await client.PostAsync(uri, content).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return response;
            }catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }            
        }

        //get lights
        public async Task<HttpResponseMessage> GetMessage(Uri uri)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpClient client = new HttpClient();

                var response = await client.GetAsync(uri).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return response;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }

        //send lights info
        public async Task<HttpResponseMessage> PutMessage(HttpStringContent content, Uri uri)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpClient client = new HttpClient();

                var response = await client.PutAsync(uri, content).AsTask(cts.Token);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                return response;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
