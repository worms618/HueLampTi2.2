using HueLampApp.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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

        private bool _online;
        public bool Online
        {
            get { return _online; }
            set { _online = value; OnPropertyChanged(nameof(Online)); }
        }

        private ApplicationDataContainer _settings;

        private string _username;
        public string Username
        {
            get { return _username; }//return _username; }
            set { _username = value; SaveUsername(); OnPropertyChanged(nameof(Username)); } //_username = value;
        }

        public BridgeConnector(string ip = "localhost", int port = 80)
        {
            Ip = ip;
            Port = port;            
            _settings = ApplicationData.Current.RoamingSettings;
            ApplicationData.Current.DataChanged += Current_DataChanged;
            GetUsername();
        }
        
        private void SaveUsername()
        {            
            //Debug.WriteLine($"Save, Settings: {_settings}, Value: {username}");
            _settings.Values["username"] = _username;
        }


        private void GetUsername()
        {            
            Debug.WriteLine($"Get, Settings: {_settings}");
            Username = _settings.Values["username"] as string;
            //CheckBridge();
        }
       
        
        public void Current_DataChanged(ApplicationData sender, object args)
        {
            Debug.WriteLine("Roaming data is veranderd");
        }

        public async void CheckBridge()
        {
            //Debug.WriteLine("making uri");
            Uri checkUri = new Uri($"http://{Ip}:{Port}/api/{Username}/lights/");
            //Debug.WriteLine("uri created, making get message");
            var response = await GetMessage(checkUri);
            //Debug.WriteLine($"response had come {response}");
            var content = await response.Content.ReadAsStringAsync();
            //Debug.WriteLine("content had been read");
            Online = string.IsNullOrEmpty(content);
            //Debug.WriteLine($"online property has beed set: {Online}");
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
                Debug.WriteLine(e.Message);
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
        public async void PutMessage(HttpStringContent content, Uri uri)
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            try
            {
                HttpClient client = new HttpClient();
                await client.PutAsync(uri, content).AsTask(cts.Token);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);                
            }
        }
    }
}
