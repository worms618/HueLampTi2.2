using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLampApp.Objects
{
    public class PropertyChange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyname)
        {
            var eventhandler = PropertyChanged;

            if (eventhandler != null)
            {
                //System.Diagnostics.Debug.WriteLine($"Property is verandert: {propertyname}");
                eventhandler(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
