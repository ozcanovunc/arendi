using Arendi.DataModel;
using System;
using System.Diagnostics;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
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

        private async void LoginPage_LoginButton_Click(object sender, RoutedEventArgs e)
        {

            this.IsEnabled = false;
            LoginPage_ProcessRing.IsActive = true;

            try {

                User user = await Controllers.UserController.GetUserByEmail(LoginPage_MailText.Text);

                // Email or password has not been typed
                if (LoginPage_PasswordText.Password.Equals(String.Empty) ||
                    LoginPage_MailText.Text.Equals(String.Empty))
                {
                    MessageDialog msgbox = new MessageDialog("Email ya da şifre boş bırakılamaz!", "Hata");
                    await msgbox.ShowAsync();
                }
                // Authentication successful
                else if (user != null && user.Password.Equals(LoginPage_PasswordText.Password))
                {
                    // Store login information
                    App.RoamingSettings.Values["Loggedin"] = true;

                    Frame.Navigate(typeof(HubPage));
                }
                // Password is not correct
                else if (user != null && !user.Password.Equals(LoginPage_PasswordText.Password))
                {
                    MessageDialog msgbox = new MessageDialog("Yanlış ya da eksik şifre girdiniz!", "Hata");
                    await msgbox.ShowAsync();
                }
                // There is no such user
                else if (user == null)
                {
                    MessageDialog msgbox = new MessageDialog("Belirtilen email ile kayıtlı kullanıcı bulunamadı!", "Hata");
                    await msgbox.ShowAsync();
                }
            }
            catch
            {
                MessageDialog msgbox = new MessageDialog("İnternet bağlantınızı kontrol edin!", "Hata");
                await msgbox.ShowAsync();
            }
            finally
            {
                LoginPage_ProcessRing.IsActive = false;
                this.IsEnabled = true;
            }
        }
    }
}
