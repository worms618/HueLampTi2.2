using HueLampApp.Pages;
using HueLampApp.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HueLampApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {        
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = MainViewModel.Instance;            
            //System.Diagnostics.Debug.WriteLine(MainViewModel.GetInstanceOf().HueLampen.Count);            
        }
        
        private void HueLampenLijst_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(sender.GetType());
            ListView lv = (ListView)sender;
            HueLampDetailPageViewModel.Instance.SelectedHueLamp = (HueLamp)lv.SelectedItem;
            this.Frame.Navigate(typeof(HueLampDetailPage));
        }

        private void NavigateToBridgeSettingsPage(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BridgeSettingsPage));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(BridgeConnector.Instance.Username))
            //{
            //    MainViewModel.Instance.SendRequestForAllLightsData();
            //}
        }        
    }
}
