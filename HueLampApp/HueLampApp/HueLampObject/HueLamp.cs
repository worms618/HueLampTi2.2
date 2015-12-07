using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace HueLampApp.HueLampObject
{
    public class HueLamp : INotifyPropertyChanged
    {
        public int ID { get; }

        private bool _on;
        public bool On
        {
            get { return _on; }
            set { _on = value; OnPropertyChanged(nameof(On)); }
        }

        private int _brightness;
        public int Brightness
        {
            get { return _brightness; }
            set { _brightness = SetValue(0,255,value); OnPropertyChanged(nameof(Brightness)); }
        }

        private long _hue;
        public long Hue
        {
            get { return _hue; }
            set {_hue = SetValue(0,65535,value); OnPropertyChanged(nameof(Hue)); }
        }

        private int _sat;
        public int Sat
        {
            get { return _sat; }
            set { _sat = SetValue(0,255,value); OnPropertyChanged(nameof(Sat)); }
        }             

        public Tuple<int, long, int> HSV
        {
            get { return new Tuple<int, long, int>(Brightness,Hue,Sat); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            var eventhandler = PropertyChanged;

            if (eventhandler != null)
            {
                //System.Diagnostics.Debug.WriteLine($"event is afgegaan: {propertyname}");
                eventhandler(this, new PropertyChangedEventArgs(propertyname));
            }

        }

        private long SetValue(long min, long max, long value)
        {
            if(value < min)
            {
                value = min;
            }else if(value > max)
            {
                value = max;
            }
            return value;
        }

        private int SetValue(int min, int max, int value)
        {
            if (value < min)
            {
                value = min;
            }
            else if (value > max)
            {
                value = max;
            }
            return value;
        }

        private HueLamp() { }

        public HueLamp(int id)
        {
            ID = id;
            On = true;
            Brightness = 255;
            Hue = 44444;
            Sat = 254;
        }

        public void UpdateHueLamp(JObject jsonObject)
        {
            On = (bool)jsonObject       ["" + ID]["state"]["on"];
            Brightness = (int)jsonObject["" + ID]["state"]["bri"];
            Hue = (long)jsonObject      ["" + ID]["state"]["hue"];
            Sat = (int)jsonObject       ["" + ID]["state"]["sat"];
        }

        public override string ToString()
        {
            return $"ID: {ID} - ON: {On} - Brightness: {Brightness} - Hue: {Hue} - Sat: {Sat}";
        }
        
    }
}
