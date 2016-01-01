using HueLampApp.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HueLampApp.ViewModel
{
    public class MainViewModel : Singleton<MainViewModel>
    {
        
        private ObservableCollection<HueLamp> _huelampen;
        public ObservableCollection<HueLamp> HueLampen { get { return _huelampen; } }

        private ICommand _getLatestedLightsDataCommand;
        public ICommand GetLatestedLightsDataCommand
        {
            get { return _getLatestedLightsDataCommand; }
        }
        
        public MainViewModel()
        {
            _huelampen = new ObservableCollection<HueLamp>();
            _getLatestedLightsDataCommand = new DelegateCommand(SendRequestForAllLightsData);
            //_huelampen.Add(new HueLamp(1));            
            //_huelampen.Add(new HueLamp(2) { On = false ,Hue = 0});
        }
        
        public async void SendRequestForAllLightsData()
        {
            BridgeConnector c = BridgeConnector.Instance;
            var response = await c.GetMessage(new Uri($"http://{c.Ip}:{c.Port}/api/{c.Username}/lights/"));
            var allLightsData = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(allLightsData))
            {
                JObject jobject = JObject.Parse(allLightsData);
                UpdateList(jobject);
            }
        }

        private void UpdateList(JObject jobject)
        {
            if(HueLampen.Count > jobject.Count)
            {
                HueLampen.Clear();
            }
            for (int i = HueLampen.Count; i < jobject.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine($"Huelampen count: {hueLampen.Count} - ID: {hueLampen.Count + 1}");
                HueLampen.Add(new HueLamp(i + 1));
            }
            UpdateLights(jobject);
        }

        private void UpdateLights(JObject jobject)
        {
            foreach(HueLamp h in HueLampen)
            {
                h.UpdateHueLamp(jobject);
            }
        }
    }
}
