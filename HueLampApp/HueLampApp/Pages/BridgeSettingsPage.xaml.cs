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
using HueLampApp.ViewModel;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace HueLampApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BridgeSettingsPage : Page
    {
        private bool editStatus = false;
        private TextBlock[] dynamicBoxes;
        private TextBox[] editBoxes;
        private Button[] buttons;

        public BridgeSettingsPage()
        {
            this.InitializeComponent();
            DataContext = BridgeSettingsViewModel.Instance;
            dynamicBoxes = new TextBlock[] { dynamicIpBox, dynamicPortBox, dynamicUsernameBox };
            editBoxes = new TextBox[] { editIpBox, editPortBox, editUsernameBox };
            buttons = new Button[] { homeButton,editButton, registerButton };            
        }

        private void BackToMainPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void RefreshEditStatus(object sender, RoutedEventArgs e)
        {
            if (editStatus)
            {                
                for(int i = 0; i < dynamicBoxes.Length; i++)
                {
                    dynamicBoxes[i].Text = editBoxes[i].Text;
                }
            }
            editStatus = !editStatus;

            foreach(TextBlock tb in dynamicBoxes)
            {
                tb.Visibility = SetVisibility(!editStatus);
            }

            foreach(TextBox tb in editBoxes)
            {
                tb.Visibility = SetVisibility(editStatus);
                tb.IsEnabled = editStatus;
            }
            
           foreach(Button b in buttons)
            {
                if(b.Name != "editButton")
                {
                    b.Visibility = SetVisibility(!editStatus);
                    b.IsEnabled = !editStatus;
                }
                else
                {
                    AppBarButton abb = (AppBarButton)b;
                    abb.Label = editStatus ? "Disable edit mode" : "Set edit mode";
                }
            }
        }

        private Visibility SetVisibility(bool b)
        {
            return b ? Visibility.Visible : Visibility.Collapsed;
        }         
    }
}
