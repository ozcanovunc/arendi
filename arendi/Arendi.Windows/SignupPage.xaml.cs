using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Arendi
{
    public sealed partial class SignupPage : Page
    {
        public SignupPage()
        {
            this.InitializeComponent();
        }

        private async void SignupPage_SignupButton_Click(object sender, RoutedEventArgs e)
        {
            var error_flag = false;
            var red_color = new SolidColorBrush(Colors.Red);
            var white_color = new SolidColorBrush(Colors.White);

            // User properties 
            string name = SignupPage_NameText.Text;
            string surname = SignupPage_SurnameText.Text;
            string password = SignupPage_PasswordText.Password;
            string mail = SignupPage_MailText.Text;
            string company = SignupPage_CompanyText.Text;

            this.IsEnabled = false;
            SignupPage_ProcessRing.IsActive = true;
            
            try
            {
                // Recolor all the fields
                SignupPage_NameBorder.Background =
                    SignupPage_SurnameBorder.Background =
                    SignupPage_PasswordBorder.Background =
                    SignupPage_MailBorder.Background =
                    SignupPage_CompanyBorder.Background = white_color;

                ////////// ERROR INDICATORS //////////
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
                ////////////////////////////////////////

                if (!error_flag)
                {
                    await Controllers.CompanyController.AddCompany(company);

                    if (password.Length < 5)
                    {
                        MessageDialog msgbox =
                            new MessageDialog("Şifreniz 5 karakterden daha kısa olmamalıdır!", "Hata");
                        await msgbox.ShowAsync();
                    }
                    // New user added successfully
                    else if (await Controllers.UserController.
                        AddUser(name + " " + surname, password, mail, "w" + company))
                    {
                        App.RoamingSettings.Values["Username"] = name;
                        App.RoamingSettings.Values["Surname"] = surname;
                        App.RoamingSettings.Values["Type"] = "w";
                        App.RoamingSettings.Values["Company"] = company;
                        App.RoamingSettings.Values["Email"] = mail;
                        App.RoamingSettings.Values["UserID"]
                            = (await Controllers.UserController.GetUserByEmail(mail)).ID;

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
                throw;
            }
            finally
            {
                SignupPage_ProcessRing.IsActive = false;
                this.IsEnabled = true;
            }
        }
    }
}
