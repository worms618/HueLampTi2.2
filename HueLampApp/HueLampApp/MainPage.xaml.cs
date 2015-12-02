using HueLampApp.HueLampObject;
using HueLampApp.MainpageObjects;
using HueLampApp.Pasers;
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
        private MainViewModel mvm;
        public MainPage()
        {
            this.InitializeComponent();
            mvm = new MainViewModel();
            DataContext = mvm;
            HueLampLijst.ItemsSource = mvm.hueLampen;            
        }

        private void AllLamps_Click(object sender, RoutedEventArgs e)
        {
            mvm.GetCurrentLightsData();
            //UploadLamps.IsEnabled = true;          
        }

        private void UploadLamps_Click(object sender, RoutedEventArgs e)
        {
            mvm.GetCurrentLightsData();
        }
        
        private void HueLampLijst_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView listv = (ListView)sender;
            HueLamp h = (HueLamp)listv.SelectedItem;
            //System.Diagnostics.Debug.WriteLine("TAPPED HueLampLijst: ");

            if (h != null)
            {
                SelectedLamp.Text = " " + h.ID;
                AanTextBlock.Text = h.On + "";
                BrightnessTextBlock.Text = h.Brightness + "";
                HueTextBlock.Text = h.Hue + "";
                SatTextBlock.Text = h.Sat + "";


            }
        }
    }
}
