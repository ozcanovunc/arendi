using System;
using Windows.Phone.UI.Input;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Arendi
{
    public sealed partial class SignupPage : Page
    {
        public SignupPage()
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

        private async void SignupPage_SignupButton_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            SignupPage_ProcessRing.IsEnabled = true;

            try {
                var error_flag = false;
                var red_color = new SolidColorBrush(Colors.Red);

                // User properties 
                var name = SignupPage_NameText.Text;
                var surname = SignupPage_SurnameText.Text;
                var password = SignupPage_PasswordText.Password;
                var mail = SignupPage_MailText.Text;
                var company = SignupPage_CompanyText.Text;

                if (name.Equals(string.Empty))
                {
                    SignupPage_NameBorder.Background = red_color;
                    error_flag = true;
                }
                if (surname.Equals(string.Empty))
                {
                    SignupPage_SurnameBorder.Background = red_color;
                    error_flag = true;
                }
                if (password.Equals(string.Empty))
                {
                    SignupPage_PasswordBorder.Background = red_color;
                    error_flag = true;
                }
                if (mail.Equals(string.Empty))
                {
                    SignupPage_MailBorder.Background = red_color;
                    error_flag = true;
                }
                if (company.Equals(string.Empty))
                {
                    SignupPage_CompanyBorder.Background = red_color;
                    error_flag = true;
                }

                if (!error_flag)
                {
                    if (password.Length < 5)
                    {
                        MessageDialog msgbox =
                            new MessageDialog("Şifreniz 5 karakterden daha kısa olmamalıdır!", "Hata");
                        await msgbox.ShowAsync();
                    }
                    // New user added successfully
                    else if (await Controllers.UserController.AddUser(name, password, mail, "w" + company))
                    {
                        App.RoamingSettings.Values["Username"] = name;
                        App.RoamingSettings.Values["Surname"] = surname;
                        App.RoamingSettings.Values["Type"] = "w";               
                        App.RoamingSettings.Values["Company"] = company;
                        App.RoamingSettings.Values["Email"] = mail;

                        // Store login information
                        App.RoamingSettings.Values["Loggedin"] = true;

                        Frame.Navigate(typeof(HubPage));
                    }
                    else
                    {
                        MessageDialog msgbox =
                            new MessageDialog("Verilen mail adresine sahip bir kullanıcı sistemde bulunmaktadır!", "Hata");
                        await msgbox.ShowAsync();
                    }
                }
            }
            catch
            {
                MessageDialog msgbox = 
                    new MessageDialog("İnternet bağlantınızı kontrol edin!", "Hata");
                await msgbox.ShowAsync();
            }
            finally
            {
                SignupPage_ProcessRing.IsEnabled = false;
                this.IsEnabled = true;
            }
        }
    }
}
