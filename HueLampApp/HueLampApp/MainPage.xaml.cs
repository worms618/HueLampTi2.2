using HueLampApp.MainpageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HueLampApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private UriRouter webInfo = new UriRouter("3939a4a8fe6b19404939e53f9b29426");
        public MainPage()
        {
            this.InitializeComponent();
            var task = LightOn();
        }

        private void AllLamps()
        {
            
        }

        private async Task LightOn()
        {
            var response = await LightOnTask();
            if (string.IsNullOrEmpty(response))
            {
                //consoleView.Text = "niks";
                await new MessageDialog("Error while setting light properties. ….").ShowAsync();
            }
            else
                consoleView.Text = response;
        }

        private async Task<string> LightOnTask()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(5000);

            try
            {
                HttpClient client = new HttpClient();
                

                HttpStringContent content
                    = new HttpStringContent
                          ($"{{ \"on\": {false.ToString().ToLower()} }}",
                            Windows.Storage.Streams.UnicodeEncoding.Utf8,
                            "application/json");
                
                //string ip, username;
                //int port;
                //MainPage.RetrieveSettings(out ip, out port, out username);

                Uri uriLampState = new Uri($"http://{webInfo.IP}:{webInfo.Port}/api/{webInfo.Username}/lights/{1}/state");
               

                HttpResponseMessage response = await client.PutAsync(uriLampState, content).AsTask(cts.Token);
                
                System.Diagnostics.Debug.WriteLine("er is response");

                if (!response.IsSuccessStatusCode)
                {
                    consoleView.Text += "1";
                    return string.Empty;
                }

                string jsonResponse = await response.Content.ReadAsStringAsync();      

                System.Diagnostics.Debug.WriteLine("JSonresponse: "+jsonResponse);

                consoleView.Text += "2";
                return jsonResponse;
            }
            catch (Exception ex)
            {
                consoleView.Text += " fout!!";
                System.Diagnostics.Debug.WriteLine(ex.Message);

            }
            consoleView.Text += "3";
            return string.Empty;
        }        
    }
}
