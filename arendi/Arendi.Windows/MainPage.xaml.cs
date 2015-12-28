using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Arendi
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void MainPage_SignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (await IsNetworkAvailable())
                Frame.Navigate(typeof(SignupPage));
            else
                Application.Current.Exit();
        }

        private async void MainPage_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (await IsNetworkAvailable())
                Frame.Navigate(typeof(LoginPage));
            else
                Application.Current.Exit();
        }

        private async Task<bool> IsNetworkAvailable()
        {
            // Check internet connectivity
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageDialog msgbox =
                    new MessageDialog("İnternet bağlantınızı kontrol edin!", "Hata");
                await msgbox.ShowAsync();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
