using System.Net.NetworkInformation;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System;
using System.Threading.Tasks;

namespace Arendi
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private async void MainPage_SignupButton_Click(object sender, RoutedEventArgs e)
        {
            if (await IsNetworkAvailable())
                Frame.Navigate(typeof(SignupPage));
            else
                Application.Current.Exit();
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
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
