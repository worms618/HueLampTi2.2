﻿using HueLampApp.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.Web.Http;

namespace HueLampApp
{
    public class HueLamp : PropertyChange
    {
        public int ID { get; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value;
                OnPropertyChanged(nameof(Name));
                SendAllPropertys();
                }
        }

        private bool _on;
        public bool On
        {
            get { return _on; }
            set { _on = value;
                OnPropertyChanged(nameof(On));
                SendAllPropertys();
                }
        }

        private int _brightness;
        public int Brightness
        {
            get { return _brightness; }
            set { _brightness = SetValue(0,255,value);
                SetHSV();
                OnPropertyChanged(nameof(Brightness));
                SendAllPropertys();
                }
        }

        private long _hue;
        public long Hue
        {
            get { return _hue; }
            set {_hue = SetValue(0,65535,value);
                SetHSV();
                OnPropertyChanged(nameof(Hue));
                SendAllPropertys();
                }
        }

        private int _sat;
        public int Sat
        {
            get { return _sat; }
            set { _sat = SetValue(0,255,value);
                SetHSV();
                OnPropertyChanged(nameof(Sat));
                SendAllPropertys();
                }
        }

        private Tuple<int, long, int> _hsv;
        public Tuple<int, long, int> HSV
        {
            get { return _hsv; }
            private set { _hsv = value; OnPropertyChanged(nameof(HSV)); }
        }        

        private void SetHSV()
        {
            HSV = new Tuple<int, long, int>(Brightness, Hue, Sat);
        }

        private long SetValue(long min, long max, long value)
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
            Brightness = 254;
            Hue = 44444;
            Sat = 254;
        }

        public HueLamp(int id, JObject jsonObject)
        {
            ID = id;
            UpdateHueLamp(jsonObject);
        }        

        public void UpdateHueLamp(JObject jsonObject)
        {
            Name = (string)jsonObject   ["" + ID]["name"];
            On = (bool)jsonObject       ["" + ID]["state"]["on"];
            Brightness = (int)jsonObject["" + ID]["state"]["bri"];
            Hue = (long)jsonObject      ["" + ID]["state"]["hue"];
            Sat = (int)jsonObject       ["" + ID]["state"]["sat"];
        }
        
        public void SendAllPropertys()
        {
            BridgeConnector bc = BridgeConnector.Instance;
            HttpStringContent content = new HttpStringContent
                    (
                    $"{{\"on\":{On.ToString().ToLower()},\"bri\":{Brightness.ToString().ToLower()},\"hue\":{Hue.ToString().ToLower()},\"sat\":{Sat.ToString().ToLower()}}}",
                                Windows.Storage.Streams.UnicodeEncoding.Utf8,
                                "application/json"
                    );
            Uri uriLampState = new Uri($"http://{bc.Ip}:{bc.Port}/api/{bc.Username}/light/{ID}/state");
            bc.PutMessage(content,uriLampState);            
        }

        public override string ToString()
        {
            return $"ID: {ID} - ON: {On} - Brightness: {Brightness} - Hue: {Hue} - Sat: {Sat}";
        }
        
    }
}
