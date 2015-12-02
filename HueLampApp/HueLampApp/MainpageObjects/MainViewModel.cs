using HueLampApp.HueLampObject;
using HueLampApp.Pasers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace HueLampApp.MainpageObjects
{
    public class MainViewModel
    {
        public UriRouter LampsConnecter { get; }
        public ObservableCollection<HueLamp> hueLampen { get; }

        private JObject jsonObject = null;                

        public MainViewModel()
        {
            LampsConnecter = new UriRouter();
            hueLampen = new ObservableCollection<HueLamp>();               
        }

        public async void GetCurrentLightsData()
        {
            var response = await LampsConnecter.GetAllLamps();
            if (string.IsNullOrEmpty(response))
            {
                throw new ArgumentNullException("Lampen data is niet aangekomen");
            }
            else
            {
                try {
                    jsonObject = JObject.Parse(response);
                }catch(Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(response);
                    System.Diagnostics.Debug.WriteLine(e.Message);
                }
                //System.Diagnostics.Debug.WriteLine("Amount of huelamps by json parser: " + jsonObject.Count);
                UpdateList();
            }
        }

        public void UpdateList()
        {
            //System.Diagnostics.Debug.WriteLine($"List count: {hueLampen.Count} - Json object count: {jsonObject.Count}");
            
            //System.Diagnostics.Debug.WriteLine("de lijst heeft minder lampen dan het jsonobject");
            for (int i = hueLampen.Count; i < jsonObject.Count; i++)
            {
                //System.Diagnostics.Debug.WriteLine($"Huelampen count: {hueLampen.Count} - ID: {hueLampen.Count + 1}");
                hueLampen.Add(new HueLamp(i + 1));
            }
            
            UpdateLights();
            System.Diagnostics.Debug.WriteLine("Amount of huelamps: " + hueLampen.Count);
            foreach (HueLamp hl in hueLampen)
            {
                System.Diagnostics.Debug.WriteLine(hl);
            }
        }

        public void UpdateLights()
        {            
            foreach(HueLamp hl in hueLampen)
            {
                hl.UpdateHueLamp(jsonObject);                
            }
        }

        public async void UploadLights()
        {
            foreach (HueLamp hl in hueLampen)
            {
                hl.On = !hl.On;
                await LampsConnecter.PutLampProps(hl);
            }
        }
    }
}
