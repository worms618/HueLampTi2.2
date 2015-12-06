using HueLampApp.MainpageObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HueLampApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfoPage : Page
    {
        public InfoPage()
        {
            this.InitializeComponent();
            DataContext = UriRouter.GetInstanceOf();            
        }
        
        private void IP_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditTextBlock(IP_Adres_Input, IP_Edit_Box);
        }

        private void Port_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditTextBlock(Port_Input, Port_Edit_Box);
        }        

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void Username_Edit_Click(object sender, RoutedEventArgs e)
        {
            EditTextBlock(Username_Input, Username_Edit_Box);
        }

        private void EditTextBlock(TextBlock text,TextBox editer)
        {
            text.Text = editer.Text;
            editer.Text = string.Empty;
        }
    }
}
