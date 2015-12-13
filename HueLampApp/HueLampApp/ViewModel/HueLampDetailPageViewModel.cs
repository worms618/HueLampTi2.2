using HueLampApp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HueLampApp.ViewModel
{
    public class HueLampDetailPageViewModel : Singleton<HueLampDetailPageViewModel>
    {        
        private HueLamp _lamp = null;

        public HueLamp SelectedHueLamp { get { return _lamp; } set { _lamp = value; } }        
    }
}
