﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLampApp.HueLampObject
{
    public class HueLamp
    {
        public int ID { get; set; }
        public bool On { get; set; }
        private int _brightness;

        public int Brightness
        {
            get { return _brightness; }
            set { _brightness = SetValue(0,255,value); }
        }

        private long _hue;
        public long Hue
        {
            get { return _hue; }
            set {_hue = SetValue(0,65535,value); }
        }

        private int _sat;
        public int Sat
        {
            get { return _sat; }
            set { _sat = SetValue(0,255,value); }
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
    }
}
