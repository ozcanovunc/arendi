using Arendi.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Arendi.Pages
{
    public sealed partial class EditIdeaPage : Page
    {
        private HubIdea selectedIdea = null;

        public EditIdeaPage()
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedIdea = e.Parameter as HubIdea;
            EditIdeaPage_ContentText.Text = selectedIdea.iContent;
            EditIdeaPage_HeaderText.Text = selectedIdea.iHeader;
        }

        private async void AppBarButton_SubmitIdea_Click(object sender, RoutedEventArgs e)
        {
            appBar.IsEnabled = false;
            this.IsEnabled = false;
            EditIdeaPage_HeaderText.IsEnabled = true;

            string header = EditIdeaPage_HeaderText.Text.ToString();
            string content = EditIdeaPage_ContentText.Text.ToString();
            int user_id = (int)App.RoamingSettings.Values["UserID"];

            if (header != string.Empty || content != string.Empty)
            {
                await Controllers.IdeaController.UpdateIdea(
                    selectedIdea.id,
                    header + "///" + content,
                    DateTime.Now.ToString("dd/MM HH:mm"));
                Frame.GoBack();
            }

            EditIdeaPage_ProcessRing.IsActive = false;
            this.IsEnabled = true;
            appBar.IsEnabled = true;
        }
    }
}
