using HueLampApp.HueLampObject;
using HueLampApp.MainpageObjects;
using HueLampApp.Pasers;
using HueRemote.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
            mvm = MainViewModel.GetInstanceOf();
            DataContext = mvm;
            HueLampLijst.ItemsSource = mvm.hueLampen;

            ReceiveUsername();
            System.Diagnostics.Debug.WriteLine($"Image size: {image.Width} : {image.Height}");
            System.Diagnostics.Debug.WriteLine($"Color rect size: {colorRect.Width} : {colorRect.Height}");
        }

        private void HueLampLijst_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ListView listv = (ListView)sender;
            HueLamp h = (HueLamp)listv.SelectedItem;
            System.Diagnostics.Debug.WriteLine("TAPPED HueLampLijst: ");
            ChangeColorRect(h);
            
            //UpdateLampInfoBox(h);            
        }

        private void ChangeColorRect(HueLamp hue)
        {
            if (hue.On == true)
                colorRect.Fill = new SolidColorBrush(ColorUtil.getColor(hue));
            else
                colorRect.Fill = new SolidColorBrush(Colors.Transparent);

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
        

        private async void refresh_Click(object sender, RoutedEventArgs e)
        {
            var response = await mvm.GetCurrentLightsData();
            refresh.IsEnabled = response;
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            //ReceiveUsername();    
            this.Frame.Navigate(typeof(InfoPage));            
        }   
        
        private async void ReceiveUsername()
        {
            if (string.IsNullOrEmpty(mvm.LampsConnecter.Username))
            {
                var response = await mvm.LampsConnecter.PostUsername();
                refresh.IsEnabled = response;
            }
            else
            {
                refresh.IsEnabled = true;
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

        private void Slider_DragLeave(object sender, DragEventArgs e)
        {
            ChangeColorRect((HueLamp)HueLampLijst.SelectedItem);
        }
    }
}
