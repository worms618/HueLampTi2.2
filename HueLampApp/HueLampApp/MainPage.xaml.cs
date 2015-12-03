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
            ReceiveUsername();
             
        }

        private void HueLampLijst_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView listv = (ListView)sender;
            HueLamp h = (HueLamp)listv.SelectedItem;
            //System.Diagnostics.Debug.WriteLine("TAPPED HueLampLijst: ");
            //UpdateLampInfoBox(h);            
        }

        private void UpdateLampInfoBox(HueLamp h)
        {
            if (h != null)
            {
                SelectedLamp.Text = " " + h.ID;
                AanTextBlock.Text = h.On + "";
                BrightnessTextBlock.Text = h.Brightness + "";
                HueTextBlock.Text = h.Hue + "";
                SatTextBlock.Text = h.Sat + "";
            }
        }

        private void UpdateLampMutatorBox(HueLamp h)
        {
            if(h != null)
            {
                //toggleSwitch.IsOn = h.On;

            }
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            mvm.GetCurrentLightsData();
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            ReceiveUsername();          
        }   
        
        private async void ReceiveUsername()
        {
            if (string.IsNullOrEmpty(mvm.LampsConnecter.Username))
            {
                var response = await mvm.LampsConnecter.PostUsername();
                refresh.IsEnabled = response;
            }
        }     

        private async void update_Click(object sender, RoutedEventArgs e)
        {
            if (HueLampLijst.SelectedItem != null)
            {
                System.Diagnostics.Debug.WriteLine($"Huelamp: {HueLampLijst.SelectedItem.ToString()}");
                var response = await mvm.LampsConnecter.PutLampProps((HueLamp)HueLampLijst.SelectedItem);
            }
        }
    }
}
