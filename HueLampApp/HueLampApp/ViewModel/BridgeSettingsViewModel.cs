using HueLampApp.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HueLampApp.ViewModel
{
    public class BridgeSettingsViewModel : Singleton<BridgeSettingsViewModel>
    {
        private BridgeConnector _connector;
        public BridgeConnector Connector
        {
            get { return _connector; }           
        }

        private ICommand _registerUsernameCommand;
        public ICommand RegisterUsernameCommand
        {
            get { return _registerUsernameCommand; }
        }

        public BridgeSettingsViewModel()
        {
            _connector = BridgeConnector.Instance;
            _registerUsernameCommand = new DelegateCommand(SendRequestUsername);
        }
        
        private async void SendRequestUsername()
        {
            var response = await
                Connector.PostMessage(BridgeConnector.Content_Username,
                new Uri($"http://{Connector.Ip}:{Connector.Port}/api"));

            if(response != null)
            {
                var username = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(username))
                {
                    try
                    {
                        Connector.Username = GetUsernameOutOfJson(username);
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e.Message);
                    }

                }
            }            
        }

        private string GetUsernameOutOfJson(string jsonString)
        {
            char[] splitArray = { '{', '}', ':', '[', ']', '"' };
            string[] split = jsonString.Split(splitArray);
            int usernameIndex = 0;
            for (int i = 0; i < split.Length; i++)
            {
                //System.Diagnostics.Debug.WriteLine("Split: "+split[i]);
                if (split[i] == "username")
                {
                    usernameIndex = i;
                    break;
                }
            }
            //System.Diagnostics.Debug.WriteLine($"Index: {usernameIndex} - Item: {split[usernameIndex]}");
            usernameIndex += 3;
            //System.Diagnostics.Debug.WriteLine($"Index: {usernameIndex} - Item: {split[usernameIndex]}");
            return split[usernameIndex];
        }
    }
}
