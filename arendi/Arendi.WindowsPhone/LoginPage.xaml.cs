using System;
using System.Diagnostics;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Arendi
{
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
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

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Application.Current.Exit();
        }

        private async void MainPage_LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try {

                var list = await Controllers.UserController.UpdateUserByEmail("rt", "a", "a", "a", "a");
                    Debug.WriteLine(list);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            // TODO: Login mechanism
            //Frame.Navigate(typeof(HubPage));
        }
    }
}
