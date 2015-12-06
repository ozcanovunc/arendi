using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Arendi.DataModel;
using System.Diagnostics;

namespace Arendi.Pages
{
    public sealed partial class AddIdeaPage : Page
    {
        public AddIdeaPage()
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
                Frame.GoBack();
            }
        }

        private async void AppBarButton_SubmitIdea_Click(object sender, RoutedEventArgs e)
        {
            string idea_content = AddIdeaPage_ContentText.Text.ToString();
            string idea_header = AddIdeaPage_HeaderText.Text.ToString();

            this.IsEnabled = false;

            if (idea_header.Equals(string.Empty) || idea_content.Equals(string.Empty))
            {
                MessageDialog msgbox =
                    new MessageDialog("Fikir başlığı veya içeriği boş bırakılamaz!", "Hata");
                await msgbox.ShowAsync();
            }
            else
            {
                await Controllers.IdeaController.AddIdea
                    ((int)App.RoamingSettings.Values["UserID"], // user_id 
                    idea_header + "///" + idea_content,         // content
                    DateTime.Now.ToString("dd/MM HH:mm"));      // date  
                Frame.GoBack();
            }            

            this.IsEnabled = true;
        }
    }
}
