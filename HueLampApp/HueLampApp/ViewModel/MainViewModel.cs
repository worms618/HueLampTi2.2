using HueLampApp.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HueLampApp.ViewModel
{
    public class MainViewModel : Singleton<MainViewModel>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyname)
        {
            var eventhandler = PropertyChanged;

            if (eventhandler != null)
            {
                //System.Diagnostics.Debug.WriteLine($"Property is verandert: {propertyname}");
                eventhandler(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        private ObservableCollection<HueLamp> _huelampen;
        public ObservableCollection<HueLamp> HueLampen
        {
            get { return _huelampen; }
            set { _huelampen = value; OnPropertyChanged(nameof(HueLampen)); }
        }

        //icommands
        public ICommand GetLatestedLightsDataCommand { get; }
        public ICommand SortListOnCommand { get; }
        public ICommand SortListIdCommand { get; }
        public ICommand SortListNameCommand { get; }

        public MainViewModel()
        {
            _huelampen = new ObservableCollection<HueLamp>();
            GetLatestedLightsDataCommand = new DelegateCommand(SendRequestForAllLightsData);
            SortListOnCommand = new DelegateCommand(SortListOn);
            SortListIdCommand = new DelegateCommand(SortListId);
            SortListNameCommand = new DelegateCommand(SortListName);
        }

        public async void SendRequestForAllLightsData()
        {
            BridgeConnector c = BridgeConnector.Instance;
            var response = await c.GetMessage(new Uri($"http://{c.Ip}:{c.Port}/api/{c.Username}/lights/"));
            if(response != null)
            {
                var allLightsData = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(allLightsData))
                {
                    JObject jobject = JObject.Parse(allLightsData);
                    UpdateList(jobject);
                }
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

        private void MakeNewList(IEnumerable<HueLamp> sortedList)
        {
            HueLampen = new ObservableCollection<HueLamp>(sortedList);
        }
        

        private void SortListOn()
        {
            var sortLamps = 
            from l in _huelampen
            orderby !l.On, l.ID
            select l;
            MakeNewList(sortLamps);            
        }
        
        
        private void SortListId()
        {
            MakeNewList
                (
                from l in _huelampen
                orderby l.ID
                select l
                );
        }


        private void SortListName()
        {
            MakeNewList
                (
                from l in _huelampen
                orderby l.Name
                select l
                );
        }
    }
}
