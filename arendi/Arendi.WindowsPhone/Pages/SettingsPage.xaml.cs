using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Arendi.Pages
{
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            HardwareButtons.BackPressed += this.HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                e.Handled = true;
                App.viewModel.Comments.Clear();
                Frame.GoBack();
            }
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            SettingsPage_NameText.Text = App.RoamingSettings.Values["Username"].ToString();
            SettingsPage_SurnameText.Text = App.RoamingSettings.Values["Surname"].ToString();
            SettingsPage_MailText.Text = App.RoamingSettings.Values["Email"].ToString();
        }

        private async void AppBarButton_SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            var error_flag = false;
            var red_color = new SolidColorBrush(Colors.Red);
            var white_color = new SolidColorBrush(Colors.White);

            string name = SettingsPage_NameText.Text;
            string surname = SettingsPage_SurnameText.Text;
            string password = SettingsPage_PasswordText.Password;
            string mail = SettingsPage_MailText.Text;
            string company = App.RoamingSettings.Values["Company"].ToString();

            this.IsEnabled = false;
            SettingsPage_ProcessRing.IsEnabled = true;

            try
            {
                SettingsPage_NameText.Background = 
                    SettingsPage_SurnameText.Background = 
                    SettingsPage_PasswordText.Background = 
                    SettingsPage_MailText.Background = white_color;

                ////////// ERROR INDICATORS //////////
                if (name.Equals(string.Empty))
                {
                    SettingsPage_NameText.Background = red_color;
                    error_flag = true;
                }
                if (surname.Equals(string.Empty))
                {
                    SettingsPage_SurnameText.Background = red_color;
                    error_flag = true;
                }
                if (password.Equals(string.Empty))
                {
                    SettingsPage_PasswordText.Background = red_color;
                    error_flag = true;
                }
                if (mail.Equals(string.Empty))
                {
                    SettingsPage_MailText.Background = red_color;
                    error_flag = true;
                }
                ////////////////////////////////////////

                if (!error_flag)
                {
                    if (password.Length < 5)
                    {
                        MessageDialog msgbox =
                            new MessageDialog("Şifreniz 5 karakterden daha kısa olmamalıdır!", "Hata");
                        await msgbox.ShowAsync();
                    }
                    // Update user
                    else if(await Controllers.UserController.UpdateUserByEmail(
                        App.RoamingSettings.Values["Email"].ToString(), 
                        name + " " + surname, 
                        password, 
                        mail, 
                        "w" + company))
                    {
                        App.RoamingSettings.Values["Username"] = name;
                        App.RoamingSettings.Values["Surname"] = surname;
                        App.RoamingSettings.Values["Email"] = mail;
                        Frame.GoBack();
                    }
                    else
                    {
                        MessageDialog msgbox =
                            new MessageDialog("Bir hata oluştu!", "Hata");
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
                SettingsPage_ProcessRing.IsEnabled = false;
                this.IsEnabled = true;
                SettingsPage_PasswordText.Password = string.Empty;
            }
        }

        private void SettingsPage_ChangeProfileImage_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker filePicker = new FileOpenPicker();
            filePicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            filePicker.ViewMode = PickerViewMode.Thumbnail;

            filePicker.FileTypeFilter.Clear();
            filePicker.FileTypeFilter.Add(".bmp");
            filePicker.FileTypeFilter.Add(".png");
            filePicker.FileTypeFilter.Add(".jpeg");
            filePicker.FileTypeFilter.Add(".jpg");

            filePicker.PickSingleFileAndContinue();
        }

        public void ContinueFileOpenPicker(FileOpenPickerContinuationEventArgs args)
        {
            Image profile_image = RecursiveVisualChildFinder<Image>(SettingsPage_ProfileImage_Button) as Image;
            profile_image.Source = new BitmapImage(new Uri("file://" + args.Files[0].Path));
        }

        private static DependencyObject RecursiveVisualChildFinder<T>(DependencyObject rootObject)
        {
            var child = VisualTreeHelper.GetChild(rootObject, 0);
            if (child == null) return null;

            return child.GetType() == typeof(T) ? child : RecursiveVisualChildFinder<T>(child);
        }
    }
}
